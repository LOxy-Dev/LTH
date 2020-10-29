using System;
using System.Windows;

namespace LTHWindow.Windows.CreateNew
{
    // TODO Make generation at the end of the process
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
                    break;
                case GenerationPhases.Round:
                    App.Tournament.Round.Init();
                
                    Close();                
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
    }

    public enum GenerationPhases
    {
        Tournament, Players, Round
    }
}