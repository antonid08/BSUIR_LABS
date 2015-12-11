using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab7
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayList standartPlaylist;
        Player player;

        string play = "Play";
        string pause = "Pause";

        public MainWindow()
        {
            InitializeComponent();
        }


        //create standart playlist
        private void button_Click(object sender, RoutedEventArgs e)
        {
            standartPlaylist = new PlayList();
            playListListBox.ItemsSource = standartPlaylist.compositions;
        }


        //play and pause
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (player == null)
                player = new Player(standartPlaylist.compositions, ref currentTimeTextBlock, ref playListListBox,
                    ref nameOfCurrenSongLabel, ref slider);


            if (button1.Content.Equals(play))
            {
                if (player.isPlaying)
                    player.stopPressed();
                player.playPressed(playListListBox.SelectedIndex);
                button1.Content = pause;
            }
            else
            {
                player.pausePressed();
                button1.Content = play;
            }
        }


        //next
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            player.stopPressed();
            player.playPressed(++playListListBox.SelectedIndex);
        }

        //previous
        private void button4_Click(object sender, RoutedEventArgs e)
        {

        }

        //double click listbox item
        private void playListBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (player == null)
            {
                player = new Player(standartPlaylist.compositions, ref currentTimeTextBlock, ref playListListBox,
                    ref nameOfCurrenSongLabel, ref slider);
            }

            player.stopPressed();
            player.playPressed(playListListBox.SelectedIndex);
            button1.Content = pause;
           

        }

        //click listbox item
        private void playListListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (player != null)
            {
                if (player.isPlaying)
                {
                    if (playListListBox.SelectedIndex != player.currentCompositionIndex)
                        button1.Content = play;
                    else
                        button1.Content = pause;
                }
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label.Content = slider.Value;
        }
    }
}
