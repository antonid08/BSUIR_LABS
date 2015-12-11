using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;

namespace lab7
{
    class Player
    {
        List<Composition> compositions;

        public bool isPlaying { get; set; }
        public bool isPaused { get; set; }
        public bool isStopped { get; set; }

        public int currentCompositionIndex { get; set; }

        DateTime date;
        DateTime stopWatch;
        DispatcherTimer timer;

        TextBlock currentTimeTextBlock;
        ListBox compositionsListBox;
        Label nameOfCurrentSongLabel;
        Slider slider;


        public Player(List<Composition> compositions, ref TextBlock currentTimeTextBlock, ref ListBox compositionsListBox,
            ref Label nameOfCurrentSongLabel, ref Slider slider)
        {
            isPlaying = false;
            isPaused = false;
            isStopped = true;
            this.compositions = compositions;

            this.compositionsListBox = compositionsListBox;
            this.currentTimeTextBlock = currentTimeTextBlock;
            this.nameOfCurrentSongLabel = nameOfCurrentSongLabel;
            this.slider = slider;
        }

        void initializeTimer()
        {
            date = DateTime.Now;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(tickTimer);
        }

        private void tickTimer(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - date.Ticks;
            stopWatch = new DateTime();

            stopWatch = stopWatch.AddTicks(tick);

            currentTimeTextBlock.Text = String.Format("{0:mm:ss}", stopWatch);
        }

        public void playPressed(int index)
        {
            if (isStopped)
            {
                isStopped = false;
                isPlaying = true;

                currentCompositionIndex = index;

                initializeTimer();

                var playThread = new Thread(playComp);
                playThread.Start(compositions);
            }
            else if(isPaused)
            {
                timer.Start();
                isPlaying = true;
                isPaused = false;
            }

            nameOfCurrentSongLabel.Content = compositions[currentCompositionIndex].Name;
        }
        
        public void stopPressed()
        {
            isStopped = true;
            isPaused = false;
            isPlaying = false;

            currentCompositionIndex = 0;

            if (timer != null)
                timer.Stop();
        }

        public void pausePressed()
        {
            isStopped = false;
            isPlaying = false;
            isPaused = true;

            date = DateTime.Now;
            timer.Stop();
        }

        void playComp(object listOfCompositions)
        {
            List<Composition> compositionList = (List<Composition>)listOfCompositions;
            //int currentIndex = (int)index;

            int currentTime = 0;
            
            timer.Start();
            
            while (currentCompositionIndex != compositionList.Count)
            {
                while(isPlaying)
                {   
                    currentTime = stopWatch.Second;

                 /*   slider.Dispatcher.Invoke(new Action(() =>
                    {
                        slider.Value = slider.Maximum / compositionList[currentCompositionIndex].Length.TotalSeconds * currentTime;
      
                    }));*/
                   

                    if (currentTime > compositionList[currentCompositionIndex].Length.TotalSeconds)
                    {
                        date = DateTime.Now;
                        currentTime = 0;
                        currentCompositionIndex++;

                        compositionsListBox.Dispatcher.Invoke(DispatcherPriority.Background, new
                        Action(() =>
                        {
                            compositionsListBox.SelectedIndex++;
                        }));

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
