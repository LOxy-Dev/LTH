using System;
using System.Windows;
using System.Windows.Controls;
using LTHWindow.Tournament.Rounds;
using LTHWindow.Windows.Home;

namespace LTHWindow.Windows.CreateNew
{
    public partial class TournamentGenerator : UserControl
    {
        public bool IsFill { get; set; }

        public TournamentGenerator()
        {
            InitializeComponent();
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            Tournament.Tournament tournament;
            var tName = NameBox.Text;
            var tNbPlayers = NbPlayersBox.Value.Value;
            Round round = new Round();

            var t = (Array) Application.Current.FindResource("TournamentTypes");
            var ch = t.GetValue(0).ToString() ?? throw new ArgumentNullException("t.GetValue(0).ToString()");
            var de = t.GetValue(1)?.ToString() ?? throw new ArgumentNullException("t.GetValue(1)?.ToString()");
            var mr = t.GetValue(2)?.ToString() ?? throw new ArgumentNullException("t.GetValue(2)?.ToString()");

            // define round type
            if (TypeBox.SelectedItem.ToString() == ch)
                round = new GroupRound();
            else if (TypeBox.SelectedItem.ToString() == de)
                round = new DirectEliminationRound();
            else if (TypeBox.SelectedItem.ToString() == mr) round = new MultiRounds();

            tournament = new Tournament.Tournament(tName, tNbPlayers, round);
        }

        private void TypeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsFill = true;
        }
    }
}