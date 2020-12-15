using System;
using System.Windows;
using LTHWindow.Tournament.Brackets;

namespace LTHWindow.Windows.Main
{
    public partial class MainWindow : Window
    {
        private readonly Tournament.Tournament _tournament;

        public MainWindow(Tournament.Tournament tournament)
        {
            _tournament = tournament;

            InitializeComponent();

            // Set the window's size
            Top = 0;
            Left = 0;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;

            UpdateMatchTexts();
            UpdateVisualizer();
        }

        private void UpdateMatchTexts()
        {
            var bracket = _tournament.Round.Bracket;
            var actualMatch = bracket.GetActualMatch();
            var p1 = actualMatch.Player1;
            var p2 = actualMatch.Player2;

            // Updating match information label
            MInfos.Text = "Match " + (bracket.Matches.IndexOf(actualMatch) + 1);
            MInfos.Text += "\n" + p1.Name + " VS " + p2.Name;
            MInfos.Text += "\n" + bracket.Type + " " + bracket.ScoreObjective;
            
            // Updating players names
            P1.Text = p1.Name;
            P2.Text = p2.Name;
            
            // Reset values
            P1S.Value = 0;
            P2S.Value = 0;
            P1S.Maximum = _tournament.Round.Bracket.ScoreObjective;
            P2S.Maximum = _tournament.Round.Bracket.ScoreObjective;
        }

        private void UpdateVisualizer()
        {
            ViewerList.ItemsSource = _tournament.Round.Bracket.Players;
        }

        private void OnScoreValueChanged(object sender, RoutedEventArgs e)
        {
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

        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            var actualMatch = _tournament.Round.Bracket.GetActualMatch();
            
            actualMatch.Scores[0] = (int) P1S.Value;
            actualMatch.Scores[1] = (int) P2S.Value;
            
            _tournament.Round.Bracket.CheckMatch();

            // Code executed if the bracket is finished
            if (_tournament.Round.Bracket.IsFinished)
            {
                MessageBox.Show("The tournament " + _tournament.Name + " is over.\n" +
                                _tournament.Round.Bracket.Players[0].Name + " has won.", "Tournament ended",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                MInputs.Visibility = Visibility.Collapsed;
            }
            else
            {
                UpdateMatchTexts();
                UpdateVisualizer();
            }
        }
    }
}