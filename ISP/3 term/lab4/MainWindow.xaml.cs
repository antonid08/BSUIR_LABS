using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO.Compression;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PurchaseCollection availablePurchases;
        PurchaseCollection myBasket;

        void setUpListBox()
        {
            availableItemsLB.ItemsSource = availablePurchases;
            basketLB.ItemsSource = myBasket;
        }

        public void setUpCollections()
        {
            availablePurchases = new PurchaseCollection();
            myBasket = new PurchaseCollection();

            availablePurchases.Add(new Purchase("Bread", 1));
            availablePurchases.Add(new Purchase("Butter", 1));
            availablePurchases.Add(new Purchase("Fish", 2));
            availablePurchases.Add(new Purchase("Meat", 3));
            availablePurchases.Add(new Purchase("Cheese", 3));
            availablePurchases.Add(new Purchase("Cake", 1));
            availablePurchases.Add(new Purchase("Apple", 1));
            availablePurchases.Add(new Purchase("Eggs", 2));
            availablePurchases.Add(new Purchase("Ice-cream", 3));
        }
        
        void addToBasketBtnClick(object sender, RoutedEventArgs e)
        {
            if (availableItemsLB.SelectedIndex != -1)
            {
                textBlock1.Visibility = Visibility.Hidden;
                textBlock2.Visibility = Visibility.Hidden;
                textBlock3.Visibility = Visibility.Hidden;
                selectedItemNameTB.Visibility = Visibility.Hidden;
                selectedItemPriceTB.Visibility = Visibility.Hidden;
                selectedItemCountTB.Visibility = Visibility.Hidden;

                myBasket.Add(availablePurchases[availableItemsLB.SelectedIndex]);

                // basketLB.ItemsSource = null;
                //basketLB.ItemsSource = myBasket;

                totalPriceTB.Text = Convert.ToString(myBasket.TotalPrice);
                discontTB.Text = Convert.ToString(myBasket.calculateDiscont());
                priceWithDiscontTB.Text = Convert.ToString(myBasket.TotalPrice - myBasket.calculateDiscont());

                availableItemsLB.SelectedIndex = -1;
            } else
            {
                MessageBox.Show("Продукт не выбран");
            }
           
        }

        private void removeFromBasketBtnClick(object sender, RoutedEventArgs e)
        {
            if (basketLB.SelectedIndex != -1)
            {
                textBlock1.Visibility = Visibility.Hidden;
                textBlock2.Visibility = Visibility.Hidden;
                textBlock3.Visibility = Visibility.Hidden;
                selectedItemNameTB.Visibility = Visibility.Hidden;
                selectedItemPriceTB.Visibility = Visibility.Hidden;
                selectedItemCountTB.Visibility = Visibility.Hidden;

                myBasket.Remove(myBasket[basketLB.SelectedIndex]);

                basketLB.ItemsSource = null;
                basketLB.ItemsSource = myBasket;

                totalPriceTB.Text = Convert.ToString(myBasket.TotalPrice);
                discontTB.Text = Convert.ToString(myBasket.calculateDiscont());
                priceWithDiscontTB.Text = Convert.ToString(myBasket.TotalPrice - myBasket.calculateDiscont());
                basketLB.SelectedIndex = -1;
            }
            else
            {

            }
        }

        private void availableItemsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (availableItemsLB.SelectedIndex != -1)
            {
                textBlock1.Visibility = Visibility.Visible;
                textBlock2.Visibility = Visibility.Visible;
                textBlock3.Visibility = Visibility.Hidden;
                selectedItemNameTB.Visibility = Visibility.Visible;
                selectedItemPriceTB.Visibility = Visibility.Visible;
                selectedItemCountTB.Visibility = Visibility.Hidden;

                Purchase selectedItem = availablePurchases[availableItemsLB.SelectedIndex];

               // selectedItemNameTB.Text = selectedItem.Name;
                selectedItemPriceTB.Text = Convert.ToString(selectedItem.Price);
            }
  
        }

        private void basketLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (basketLB.SelectedIndex != -1)
            {
                textBlock1.Visibility = Visibility.Visible;
                textBlock2.Visibility = Visibility.Visible;
                textBlock3.Visibility = Visibility.Visible;
                selectedItemNameTB.Visibility = Visibility.Visible;
                selectedItemPriceTB.Visibility = Visibility.Visible;
                selectedItemCountTB.Visibility = Visibility.Visible;

                Purchase selectedItem = myBasket[basketLB.SelectedIndex];

                selectedItemNameTB.Text = selectedItem.Name;
                selectedItemPriceTB.Text = Convert.ToString(selectedItem.Price);
                selectedItemCountTB.Text = Convert.ToString(selectedItem.Сount);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            setUpCollections();
            setUpListBox();
        }

        //Writing to binary file
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            myBasket.writeToBinaryFile("myFile.my");
            myBasket.wrirteToTxtFile("myFile.txt");
            
            if (!myBasket.compressFile("myFile.my", "myFile.gz"))
            {
                MessageBox.Show("Исходного файла не существует.");
            }
            
        }

        //Reading from binary file
        private void button2_Click(object sender, RoutedEventArgs e)
        {   
            if(!myBasket.readFromFile("myFile", 0))
            {
                MessageBox.Show("Файла не существует.");
            }
        }

        //reading from compressed file
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (!myBasket.decompressFile("myFile.gz", "myFile.my"))
            {
                MessageBox.Show("Сжатого файла не существует.");
            }
        
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (!myBasket.readFromFile("myFile", 1))
            {
                MessageBox.Show("Файла не существует.");
            }
        }
    }
}
