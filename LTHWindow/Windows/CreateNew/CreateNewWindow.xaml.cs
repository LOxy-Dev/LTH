using System;
using System.Windows;

namespace LTHWindow.Windows.CreateNew
{
    public partial class CreateNewTournament : Window
    {
        private GenerationPhases _phase;
        
        private readonly TournamentGenerator _trnGenerator;
        private readonly PlayersGenerator _plyrGenerator;
        
        public CreateNewTournament()
        {
            InitializeComponent();

            _phase = GenerationPhases.Tournament;
            
            _trnGenerator = new TournamentGenerator();
            _plyrGenerator = new PlayersGenerator();
            ContentHolder.Content = _trnGenerator;
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (_phase)
            {
                case GenerationPhases.Tournament:
                    ContentHolder.Content = _plyrGenerator;
                    _phase = GenerationPhases.Players;
                    break;
                case GenerationPhases.Players:
                    break;
                case GenerationPhases.Round:
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
                _ => false
            };
        }
    }

    public enum GenerationPhases
    {
        Tournament, Players, Round
    }
}