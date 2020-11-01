using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LTHWindow.Tournament.Brackets;
using WrapPanel = System.Windows.Controls.WrapPanel;

namespace LTHWindow.Tournament.Rounds
{
    public class GroupRound : IRound
    {
        // Implemented fields
        public List<Player> Players { get; set; }

        IBracket IRound.Bracket { get; set; }
        
        // Fields
        readonly WrapPanel Generator = new WrapPanel
        {
            Name = "Generator",
            Orientation = Orientation.Vertical
        };

        public void Init()
        {
            var participants = new List<Player>();
            foreach (UIElement generatorChild in Generator.Children) // Each WrapPanel in Grid
            {
                if (generatorChild.GetType() != typeof(WrapPanel)) continue;
                
                var panel = (WrapPanel) generatorChild;
                foreach (var panelChild in panel.Children) // Each WrapPanel in the first panel (player selector etc..)
                {
                    if (panelChild.GetType() != typeof(WrapPanel)) continue;
                    var selectorPanel = (WrapPanel) panelChild;
                    foreach (var selectorItems in selectorPanel.Children) // Each player selector (checkBox + label)
                    {
                        if (selectorItems.GetType() != typeof(CheckBox)) continue;
                        var checkBox = (CheckBox) selectorItems;

                        if (checkBox.IsChecked != true) continue;
                        var idOfBox = int.Parse(checkBox.Name.Replace("P", ""));

                        var player = App.Tournament.Players[idOfBox];
                        participants.Add(player);
                    }
                }
            }

            Players = participants;
            ((IRound) this).Bracket = new Groups(Players = Players);
        }

        public WrapPanel GetGenerator()
        {
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

            Generator.Children.Add(playerSelector);
            return Generator;
        }
    }
}