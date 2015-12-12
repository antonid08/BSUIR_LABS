using lab7.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace lab7.ViewModel
{
    class MainWindowViewModel :INotifyPropertyChanged
    {

        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region properties

        public PlayList StandartPlaylist { get; set; }
        public Player Player { get; set; }


        public int CompositionsListBoxSelectedIndex { get; set; }

        public string PlayButtonState {
            get
            { return playButtonState; }
            set
            {
                if (playButtonState != value)
                {
                    playButtonState = value;
                    OnPropertyChanged("PlayButtonState");
                }
            }
        }

        #endregion

        #region fields

        string playButtonState;

        const string play = "Play";
        const string pause = "Pause";

        #endregion

        #region Commands

        /// <summary>
        /// Get or set ClickCommand.
        /// </summary>
        public ICommand PlayButtonPressedCommand { get; set; }
        public ICommand NextButtonPressedCommand { get; set; }
        public ICommand PreviousButtonPressedCommand { get; set; }

        public ICommand CompositionsListBoxItemSelectionChangedCommand { get; set; }
        public ICommand CompositionsListBoxItemDoubleClickedCommand { get; set; }
        

        #endregion

        #region constructor

        public MainWindowViewModel()
        {
            PlayButtonState = play; 

            StandartPlaylist = new PlayList();
            PlayButtonPressedCommand = new Command(arg => onPlayButtonClick());
            NextButtonPressedCommand = new Command(arg => onNextButtonClick());
            PreviousButtonPressedCommand = new Command(arg => onPreviousButtonClick());

            CompositionsListBoxItemSelectionChangedCommand = new RelayCommand<int>(onCompositionsListBoxSelectionChanged);
            CompositionsListBoxItemDoubleClickedCommand = new Command(arg => onCompositionsListBoxItemDoubleClick());

            Player = new Player(StandartPlaylist.compositions);
        }

        #endregion

        #region userEvents

        private void onPlayButtonClick()
        {
            if (Player == null)
                Player = new Player(StandartPlaylist.compositions);


            if (PlayButtonState.Equals(play))
            {
                if (!Player.isPaused)
                {
                    Player.stop();
                    Player.play(CompositionsListBoxSelectedIndex);
                }
                else
                {
                    Player.unpause();
                }
                PlayButtonState = pause;
            }
            else
            {
                Player.pause();
                PlayButtonState = play;
            }
        }

        private void onNextButtonClick()
        {
            if (Player != null)
            {
                Player.playNext();
                PlayButtonState = pause;
            }

        }

        private void onPreviousButtonClick()
        {
            if (Player != null)
            {
                Player.playPrevious();
                PlayButtonState = pause;
            }
        }

        private void onCompositionsListBoxSelectionChanged(int par)
        {
            CompositionsListBoxSelectedIndex = par;
            if (Player != null)
            {
                if (Player.isPlaying)
                {
                    if (CompositionsListBoxSelectedIndex != Player.CurrentCompositionIndex)
                        PlayButtonState = play;
                    else
                        PlayButtonState = pause;
                }
            }
        }

        private void onCompositionsListBoxItemDoubleClick()
        {
            if (Player == null)
            {
                Player = new Player(StandartPlaylist.compositions);
            }

            Player.stop();
            Player.play(CompositionsListBoxSelectedIndex);
            PlayButtonState = pause;
        }

        #endregion

    }
}
