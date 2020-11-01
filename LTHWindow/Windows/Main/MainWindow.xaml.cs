using System.Windows;
using System.Windows.Data;

namespace LTHWindow.Windows.Main
{
    public partial class MainWindow : Window
    {
        private Tournament.Tournament _tournament;

        public MainWindow(Tournament.Tournament tournament)
        {
            _tournament = tournament;

            InitializeComponent();

            Top = 0;
            Left = 0;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;

            Update();
        }

        private void Update()
        {
            _tournament.Round.Init();
            
            var actualMatch = _tournament.Round.Bracket.GetActualMatch();
            var p1 = actualMatch.Player1;
            var p2 = actualMatch.Player2;

            // Init Player 1
            P1.Text = p1.Name;
            P1S.Minimum = 0;
            P1S.Maximum = _tournament.Round.Bracket.ScoreObjective;
            
            // Init Player 2
            P2.Text = p2.Name;
            P1S.Minimum = 0;
            P1S.Maximum = _tournament.Round.Bracket.ScoreObjective;
        }

        private void MainWindow_OnTargetUpdated(object? sender, DataTransferEventArgs e)
        {
            Update();
        }
    }
}