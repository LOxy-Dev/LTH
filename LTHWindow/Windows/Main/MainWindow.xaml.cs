using System;
using System.Windows;
using System.Windows.Data;
using LTHWindow.Tournament.Brackets;

namespace LTHWindow.Windows.Main
{
    public partial class MainWindow : Window
    {
        private Tournament.Tournament _tournament;

        public MainWindow(Tournament.Tournament tournament)
        {
            _tournament = tournament;

            InitializeComponent();

            // Set the window's size
            Top = 0;
            Left = 0;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;

            P1.Text = tournament.Round.Bracket.GetActualMatch().Player1.Name;
            P1.Text = tournament.Round.Bracket.GetActualMatch().Player1.Name;
        }

        private void OnScoreValueChanged(object sender, RoutedEventArgs e)
        {
            // Update Player 1
            P1S.Maximum = _tournament.Round.Bracket.ScoreObjective;
            
            // Update Player 2
            P2S.Maximum = _tournament.Round.Bracket.ScoreObjective;
            
            // Display the message error if needed
            switch (_tournament.Round.Bracket.Type)
            {
                case Objectives.BestOf:
                    var scoreSum = (int) (P1S.Value + P2S.Value);

                    if (scoreSum == _tournament.Round.Bracket.ScoreObjective)
                    {
                        ErrorText.Visibility = Visibility.Hidden;
                        RegisterButton.IsEnabled = true;
                    }
                    else
                    {
                        ErrorText.Visibility = Visibility.Visible;
                        RegisterButton.IsEnabled = false;
                    }
                    
                    break;
                case Objectives.FirstTo:
                    if (P1S.Value == _tournament.Round.Bracket.ScoreObjective ^
                        P2S.Value == _tournament.Round.Bracket.ScoreObjective)
                    {
                        ErrorText.Visibility = Visibility.Hidden;
                        RegisterButton.IsEnabled = true;
                    }
                    else
                    {
                        ErrorText.Visibility = Visibility.Visible;
                        RegisterButton.IsEnabled = false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MainWindow_OnTargetUpdated(object sender, DataTransferEventArgs e)
        {
            
        }
    }
}