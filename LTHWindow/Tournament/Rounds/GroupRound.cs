using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LTHWindow.Tournament.Brackets;
using Xceed.Wpf.Toolkit;
using WrapPanel = System.Windows.Controls.WrapPanel;

namespace LTHWindow.Tournament.Rounds
{
    public class GroupRound : IRound
    {
        // Implemented fields
        public List<Player> Players { get; set; }

        public IBracket Bracket { get; set; }
        
        // Fields
        private readonly WrapPanel _generator = new WrapPanel
        {
            Name = "Generator",
            Orientation = Orientation.Vertical
        };

        public void Init()
        {
            var participants = new List<Player>();
            var type = Objectives.BestOf;
            var scoreObjective = 3;

            foreach (UIElement generatorChild in _generator.Children) // Each WrapPanel in Grid
            {
                if (generatorChild.GetType() != typeof(WrapPanel))
                    continue;
                
                switch ((generatorChild as WrapPanel).Name)
                {
                    // If selector box
                    // If objective selector
                    case "SelectorBox":
                    {
                        foreach (var selectorItem in (generatorChild as WrapPanel).Children) // Each player selector (checkBox + label)
                        {
                            if (selectorItem.GetType() != typeof(WrapPanel)) continue;
                            var playerSelector = (WrapPanel) selectorItem;

                            foreach (var playerSelectorChild in playerSelector.Children)
                            {
                                if (playerSelectorChild.GetType() != typeof(CheckBox)) continue;
                                var checkBox = (CheckBox) playerSelectorChild;

                                if (checkBox.IsChecked != true) continue;
                                var idOfBox = int.Parse(checkBox.Name.Replace("P", ""));

                                var player = App.Tournament.Players[idOfBox];
                                participants.Add(player);
                            }
                        }

                        break;
                    }
                    case "ObjectiveSelector":
                    {
                        foreach (var child in (generatorChild as WrapPanel).Children)
                        {
                            if (child.GetType() == typeof(ComboBox))
                            {
                                var comboBox = (ComboBox) child;
                                    
                                // get all element in resource
                                var t = (Array) Application.Current.FindResource("ScoreObjective");
                                var bo = t?.GetValue(0)?.ToString();
                                var fo = t?.GetValue(1)?.ToString();

                                if (comboBox.SelectedItem.Equals(bo))
                                    type = Objectives.BestOf;
                                else if (comboBox.SelectedItem.Equals(fo))
                                    type = Objectives.FirstTo;
                            }

                            else if (child.GetType() == typeof(IntegerUpDown))
                            {
                                var integerBox = (IntegerUpDown) child;

                                if (integerBox.Value != null) scoreObjective = (int) integerBox.Value;
                            }
                        }

                        break;
                    }
                }
            }

            Players = participants;
            Bracket = new Groups(Players, type, scoreObjective);
        }

        public WrapPanel GetGenerator()
        {
            // Player selector
            var playerSelector = new WrapPanel
            {
                Name = "SelectorBox",
                Orientation = Orientation.Vertical
            };

            var i = 0;
            foreach (var me in App.Tournament.Players)
            {
                var p = new WrapPanel();
                p.Children.Add(new CheckBox
                {
                    Name = "P" + i,
                    IsChecked = true,
                    VerticalAlignment = VerticalAlignment.Center
                });
                p.Children.Add(new Label
                {
                    Content = me.Name,
                    VerticalAlignment = VerticalAlignment.Center
                });

                playerSelector.Children.Add(p);
                i++;
            }

            _generator.Children.Add(playerSelector);
            
            // Objective selector
            var objectiveSelector = new WrapPanel
            {
                Name = "ObjectiveSelector",
                Orientation = Orientation.Horizontal
            };
            
            var typeSelector = new ComboBox
            {
                Name = "TypeSelector",
                ItemsSource = (Array) Application.Current.FindResource("ScoreObjective")
            };
            objectiveSelector.Children.Add(typeSelector);
            
            var scoreSelector = new IntegerUpDown
            {
                Name = "ScoreSelector",
                Value = 3,
                Minimum = 1
            };
            objectiveSelector.Children.Add(scoreSelector);

            _generator.Children.Add(objectiveSelector);
            return _generator;
        }
    }
}