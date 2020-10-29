using System.Windows;
using System.Windows.Controls;
using WrapPanel = System.Windows.Controls.WrapPanel;

namespace LTHWindow.Tournament.Rounds
{
    public class GroupRound : Round
    {
        public override void Init()
        {
            foreach (UIElement generatorChild in Generator.Children)
            {
                if (generatorChild.GetType() != typeof(WrapPanel)) continue;
                var panel = (WrapPanel) generatorChild;
                foreach (var panelChild in panel.Children)
                {
                    if (panelChild.GetType() != typeof(CheckBox)) continue;
                    var checkBox = (CheckBox) panelChild;

                    if (checkBox.IsChecked != true) continue;
                    var idOfBox = int.Parse(checkBox.Name.Replace("P", ""));
                    
                    Players.Add(App.Tournament.Players[idOfBox]);
                }
            }
        }

        public override WrapPanel GetGenerator()
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