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

        public List<PlayList> PlayLists { get; set; }
        public PlayList SelectedPlayList { get; set; }
      

        public int NumberOfCurrentTab { get; set; }
        public int CompositionsListBoxSelectedIndex { get; set; }

        
        #endregion

        #region fields

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
        
            PlayLists = new List<PlayList>();
            
            initializePlayLists();
            SelectedPlayList = PlayLists[0];

            PlayButtonPressedCommand = new Command(arg => onPlayButtonClick());
            NextButtonPressedCommand = new Command(arg => onNextButtonClick());
            PreviousButtonPressedCommand = new Command(arg => onPreviousButtonClick());

            CompositionsListBoxItemSelectionChangedCommand = new RelayCommand<int>(onCompositionsListBoxSelectionChanged);
            CompositionsListBoxItemDoubleClickedCommand = new Command(arg => onCompositionsListBoxItemDoubleClick());

        }

        public void initializePlayLists()
        {
            PlayLists.Add(new PlayList("First playlist"));
            PlayLists[0].addComposition(new Composition(1, 5, "Ария", "Беспечный ангел",
                new TimeSpan(0, 0, 5)));
            PlayLists[0].addComposition(new Composition(2, 10, "Song 2", "...",
                new TimeSpan(0, 0, 13)));
            PlayLists[0].addComposition(new Composition(3, 9, "Выпьем за любовь!", "Игорь Николаев",
                new TimeSpan(0, 2, 48)));

            PlayLists.Add(new PlayList("Songs"));
            PlayLists[1].addComposition(new Composition(1, 10, "Still loving you", "Scorpions",
                new TimeSpan(0, 0, 15)));
            PlayLists[1].addComposition(new Composition(2, 10, "Unforgiven", "Metallica",
                new TimeSpan(0, 0, 22)));
            PlayLists[1].addComposition(new Composition(3, 100500, "The road most travelled", "Jeremy Soule",
                new TimeSpan(0, 2, 33)));

        }

        #endregion

        #region userEvents

        private void onPlayButtonClick()
        {
            if (SelectedPlayList.PlayButtonState.Equals(SelectedPlayList.PlayState))
            {
                SelectedPlayList.play(CompositionsListBoxSelectedIndex);
            }
            else
                SelectedPlayList.pause();
        }

        private void onNextButtonClick()
        {
            SelectedPlayList.playNext();
        }

        private void onPreviousButtonClick()
        {
                SelectedPlayList.playPrevious();
        }

        private void onCompositionsListBoxSelectionChanged(int newSelectedIndex)
        {
            CompositionsListBoxSelectedIndex = newSelectedIndex;
            if (SelectedPlayList.isPlaying)
            {
                if (CompositionsListBoxSelectedIndex != SelectedPlayList.CurrentCompositionIndex)
                    SelectedPlayList.PlayButtonState = SelectedPlayList.PlayState;
                else
                    SelectedPlayList.PlayButtonState = SelectedPlayList.PauseState;
            }
        }

        private void onCompositionsListBoxItemDoubleClick()
        {
            //SelectedPlayList.stop();
            SelectedPlayList.play(CompositionsListBoxSelectedIndex);
            //PlayButtonState = pause;
        }

        #endregion

    }
}
