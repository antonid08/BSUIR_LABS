using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace lab7
{
    class PlayList :INotifyPropertyChanged
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

        #region Properties
        public List<Composition> Compositions { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public TimeSpan TotalLength { get; set; }
        public int Rating { get; set; }

        public Composition CurrentComposition
        {
            get { return currentComposition; }
            set
            {
                if (currentComposition != value)
                {
                    currentComposition = value;
                    OnPropertyChanged("CurrentComposition");
                }
            }
        }

        public bool isPlaying { get; set; }
        public bool isPaused { get; set; }
        public bool isStopped { get; set; }

        public int CurrentCompositionIndex
        {
            get { return currentCompositionIndex; }
            set
            {
                if (currentCompositionIndex != value)
                {
                    currentCompositionIndex = value;
                    OnPropertyChanged("CurrentCompositionIndex");
                }
            }
        }

        public string PlayButtonState
        {
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

        public string PlayState { get; } = "Play";
        public string PauseState { get; } = "Pause";

        DateTime date { get; set; }

        public DateTime StopWatch
        {
            get { return stopWatch; }
            set
            {
                if (stopWatch != value)
                {
                    stopWatch = value;
                    OnPropertyChanged("StopWatch");
                }
            }
        }
        DispatcherTimer Timer { get; set; }

        #endregion

        #region fields

        int currentCompositionIndex;

        DateTime stopWatch;

        Composition currentComposition;

        string playButtonState;
   


        #endregion

        #region Constructor
        public PlayList(string name)
        {
            Name = name;
            Compositions = new List<Composition>();

            isPlaying = false;
            isPaused = false;
            isStopped = true;

            PlayButtonState = PlayState;
        }
        #endregion

        #region Functions which edit playlist
        public void addComposition(Composition composition)
        {
            Compositions.Add(composition);
        }

        
        public void writeToFile(string path)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create));

            // writing all properties
            foreach (Composition current in Compositions)
            {
                if (current != null)
                {
                    writer.Write(current.Name);
                    writer.Write(current.Id);
                    writer.Write(current.Performer);
                    writer.Write(current.Rating);
                    writer.Write(current.Length.ToString());
                }
            }
            writer.Close();
        }

        #endregion

        #region PlayingFunctions

        void initializeTimer()
        {
            date = DateTime.Now;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Tick += new EventHandler(tickTimer);
  
        }

        private void tickTimer(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - date.Ticks;
            StopWatch = new DateTime();
            StopWatch = StopWatch.AddTicks(tick);
        }

        public void play(int index)
        {   
            if (PlayButtonState.Equals(PlayState))
            {
                if (isStopped || isPlaying)
                {
                    stop();

                    isStopped = false;
                    isPlaying = true;

                    CurrentCompositionIndex = index;

                    initializeTimer();

                    var playThread = new Thread(playComp);
                    playThread.Start(Compositions);
                }
                else if (isPaused)
                {
                    unpause();
                }
                PlayButtonState = PauseState;
            }
            else
            {
                pause();
                PlayButtonState = PauseState;
            }
         
            CurrentComposition = Compositions[CurrentCompositionIndex];
        }

        public void stop()
        {
            isStopped = true;
            isPaused = false;
            isPlaying = false;

            PlayButtonState = PlayState;
            //CurrentCompositionIndex = 0;

            if (Timer != null)
                Timer.Stop();
        }

        public void pause()
        {
            date = DateTime.Now;
            Timer.Stop();
         
            isStopped = false;
            isPlaying = false;
            isPaused = true;

            PlayButtonState = PlayState;

        }

        public void unpause()
        {
            Timer.Start();

            isStopped = false;
            isPlaying = true;
            isPaused = false;

            PlayButtonState = PauseState;
        }

        public void playNext()
        {
            if (CurrentCompositionIndex < Compositions.Count)
            {
                stop();
                play(++CurrentCompositionIndex);           
            }
        }

        public void playPrevious()
        {
            if (CurrentCompositionIndex > 0)
            {
                stop();
                play(--CurrentCompositionIndex);
            }
        }

        void playComp(object listOfCompositions)
        {
            List<Composition> compositionList = (List<Composition>)listOfCompositions;

            int currentTime = 0;

            Timer.Start();

            while (CurrentCompositionIndex != compositionList.Count)
            {
                while (isPlaying)
                {
                    currentTime = StopWatch.Second;

                    if (currentTime > compositionList[CurrentCompositionIndex].Length.TotalSeconds)
                    {
                        date = DateTime.Now;
                        currentTime = 0;
                        CurrentComposition = compositionList[++CurrentCompositionIndex];
                        PlayButtonState = PauseState;

                        break;
                    }
                }

                while (isPaused)
                {

                }

                while (isStopped)
                {
                    return;
                }
            }

            isStopped = true;
            isPaused = false;
            isPlaying = false;
        }
        #endregion


    }
}
