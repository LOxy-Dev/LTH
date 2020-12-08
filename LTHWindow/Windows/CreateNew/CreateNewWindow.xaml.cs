using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using LTHWindow.Windows.Main;
using Action = LTHWindow.Windows.Main.Action;

namespace LTHWindow.Windows.CreateNew
{
    public partial class CreateNewWindow : Window
    {
        private GenerationPhases _phase;
        
        private readonly TournamentGenerator _trnGenerator;
        private readonly PlayersGenerator _plyrGenerator;
        private readonly RoundGenerator _roundGenerator;
        
        public CreateNewWindow()
        {
            InitializeComponent();

            _phase = GenerationPhases.Tournament;
            
            _trnGenerator = new TournamentGenerator();
            _plyrGenerator = new PlayersGenerator();
            _roundGenerator = new RoundGenerator();
            ContentHolder.Content = _trnGenerator;
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (_phase)
            {
                case GenerationPhases.Tournament:
                    // Create tournament
                    _trnGenerator.GenerateTournament();
                    
                    // Generate and show player generator interface
                    _plyrGenerator.Init();
                    ContentHolder.Content = _plyrGenerator;
                    _phase = GenerationPhases.Players;
                    
                    // Specific case : NextButton enabled by default
                    NextButton.IsEnabled = true;
                    break;
                case GenerationPhases.Players:
                    // Generate player
                    _plyrGenerator.GeneratePlayers();
                    
                    // Generate and show round generator interface
                    _roundGenerator.Init();
                    ContentHolder.Content = _roundGenerator;
                    _phase = GenerationPhases.Round;
                    
                    // Specific case : NextButton enabled by default
                    NextButton.IsEnabled = true;
                    break;
                case GenerationPhases.Round:
                    App.Tournament.Round.Init();

                    // Close window
                    Close();
                    
                    // Generate and load tournament
                    GenerateThenLoad();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            NextButton.IsEnabled = false;
        }

        private void UIElement_OnLayoutUpdated(object sender, EventArgs e)
        {
            NextButton.IsEnabled = _phase switch
            {
                GenerationPhases.Tournament => _trnGenerator.IsFill,
                GenerationPhases.Players => _plyrGenerator.IsFill(),
                GenerationPhases.Round => true,
                _ => false
            };
        }

        private void GenerateThenLoad()
        {
            // Create the loading screen
            var loadingDial = new LoadingDialog();
            var actions = new List<Action>
            {
                new Action("Loading...")
            };
            
            loadingDial.AddActions(actions);
            loadingDial.Init();
            
            loadingDial.Show();

            // Load the file in main window
            App.LoadMainWindow(App.Tournament);
            loadingDial.FinishAction();
            loadingDial.Close();
            
            // Close create new window
            Close();
        }
    }

    public enum GenerationPhases
    {
        Tournament, Players, Round
    }
}