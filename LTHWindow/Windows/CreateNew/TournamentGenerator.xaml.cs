using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LTHWindow.Tournament;
using LTHWindow.Tournament.Rounds;
using LTHWindow.Windows.Home;

namespace LTHWindow.Windows.CreateNew
{
    public partial class TournamentGenerator : UserControl
    {
        public bool IsFill { get; private set; }

        public TournamentGenerator()
        {
            InitializeComponent();
            
            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            FileBox.Text = userPath + "\\Documents";
        }

        public void GenerateTournament()
        {
            var tName = NameBox.Text;
            if (NbPlayersBox.Value != null)
            {
                var tNbPlayers = NbPlayersBox.Value.Value;
                IRound tRound = new GroupRound();

                var t = (Array) Application.Current.FindResource("TournamentTypes");
                var ch = t.GetValue(0)?.ToString() ?? throw new ArgumentNullException("t.GetValue(0).ToString()");
                var de = t.GetValue(1)?.ToString() ?? throw new ArgumentNullException("t.GetValue(1)?.ToString()");
                var mr = t.GetValue(2)?.ToString() ?? throw new ArgumentNullException("t.GetValue(2)?.ToString()");

                // define round type
                if (TypeBox.SelectedItem.ToString() == ch)
                    tRound = new GroupRound();
                else if (TypeBox.SelectedItem.ToString() == de)
                    tRound = new DirectEliminationRound();
                else if (TypeBox.SelectedItem.ToString() == mr) tRound = new MultiRounds();

                App.Tournament = new Tournament.Tournament
                {
                    Name = tName,
                    FilePath = FileBox.Text + "\\" + NameBox.Text + ".json",
                    Players = new List<Player>(tNbPlayers),
                    Round = tRound
                };
            }
        }

        private void TypeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsFill = true;
        }
    }
}