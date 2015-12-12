using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.ComponentModel;
using System.Media;

namespace lab7
{
    class Player : INotifyPropertyChanged
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

        public Composition CurrentComposition {
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

        public int CurrentCompositionIndex {
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

        DateTime date { get; set; }
        public DateTime StopWatch
        {
            get { return stopWatch; }
            set {
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

        #endregion

        #region Constructor

        public Player(List<Composition> compositions)
        {
            isPlaying = false;
            isPaused = false;
            isStopped = true;
            this.Compositions = compositions;

        }

        #endregion

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
            if (isStopped)
            {
                isStopped = false;
                isPlaying = true;

                CurrentCompositionIndex = index;

                initializeTimer();

                var playThread = new Thread(playComp);
                playThread.Start(Compositions);
            }
  
            CurrentComposition = Compositions[CurrentCompositionIndex];
        }
        
        public void stop()
        {
            isStopped = true;
            isPaused = false;
            isPlaying = false;

            //CurrentCompositionIndex = 0;

            if (Timer != null)
                Timer.Stop();
        }

        public void pause()
        {
            isStopped = false;
            isPlaying = false;
            isPaused = true;

            date = DateTime.Now;
            Timer.Stop();
        }

        public void unpause()
        {
            isStopped = false;
            isPlaying = true;
            isPaused = false;
            Timer.Start();
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
                while(isPlaying)
                {   
                    currentTime = StopWatch.Second;

                    if (currentTime > compositionList[CurrentCompositionIndex].Length.TotalSeconds)
                    {
                        date = DateTime.Now;
                        currentTime = 0;
                        CurrentComposition = compositionList[++CurrentCompositionIndex];

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
        }
    }
}
