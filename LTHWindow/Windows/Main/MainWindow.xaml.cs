using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using LTHWindow.Tournament;
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
            
            InitScoreBoard();

            UpdateMatchTexts();
            UpdateViewers();
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

        private void UpdateViewers()
        {
            // Updating the list view mode
            ViewerList.ItemsSource = _tournament.Round.Bracket.Players;
            
            // Updating the calendar
            MatchList.ItemsSource = new List<Match>(); // Don't know why but it doesn't without this line
            MatchList.ItemsSource = _tournament.Round.Bracket.Matches;
            
            MatchList.SelectedItem = _tournament.Round.Bracket.GetActualMatch();
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
            }
            
            UpdateViewers();
        }

        private void InitScoreBoard()
        {
            // Adding headers
            var headerColumn = new ColumnDefinition {MinWidth = 50};
            var headerRow = new RowDefinition {MinHeight = 50};

            ScoreBoard.ColumnDefinitions.Add(headerColumn);
            ScoreBoard.RowDefinitions.Add(headerRow);
            
            // Adding placeholder
            var placeholder = new Button {IsEnabled = false};

            ScoreBoard.Children.Add(placeholder);
            placeholder.SetValue(Grid.RowProperty, 0);
            placeholder.SetValue(Grid.ColumnProperty, 0);

            var i = 1;
            foreach (var player in _tournament.Round.Bracket.Players)
            {
                // Adding definitions
                headerColumn = new ColumnDefinition {MinWidth = 50};
                headerRow = new RowDefinition {MinHeight = 50};

                ScoreBoard.ColumnDefinitions.Add(headerColumn);
                ScoreBoard.RowDefinitions.Add(headerRow);
                
                // Adding labels
                var tRow = new Label
                {
                    Content = player.Name,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                var tColumn = new Label
                {
                    Content = player.Name,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                ScoreBoard.Children.Add(tRow);
                ScoreBoard.Children.Add(tColumn);
                tRow.SetValue(Grid.RowProperty, i);
                tColumn.SetValue(Grid.ColumnProperty, i);

               // Adding placeholders
               placeholder = new Button {IsEnabled = false};
               // vs same player
               ScoreBoard.Children.Add(placeholder);
               placeholder.SetValue(Grid.RowProperty, i);
               placeholder.SetValue(Grid.ColumnProperty, i);

               // No two feet matches
               for (var j = 1; j < i; j++)
               {
                   placeholder = new Button {IsEnabled = false};

                   ScoreBoard.Children.Add(placeholder);
                   placeholder.SetValue(Grid.RowProperty, i);
                   placeholder.SetValue(Grid.ColumnProperty, j);
               }

               i++;
            }
        }
    }
}