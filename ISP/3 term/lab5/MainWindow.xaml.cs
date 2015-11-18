﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
           /* availablePurchases.Add(new Purchase("Fish", 2));
            availablePurchases.Add(new Purchase("Meat", 3));
            availablePurchases.Add(new Purchase("Cheese", 3));
            availablePurchases.Add(new Purchase("Cake", 1));
            availablePurchases.Add(new Purchase("Apple", 1));
            availablePurchases.Add(new Purchase("Eggs", 2));
            availablePurchases.Add(new Purchase("Ice-cream", 3));*/
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
            if (availableItemsLB.SelectedIndex != -1 && availablePurchases[availableItemsLB.SelectedIndex] != null)
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

        //SortByPrice
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            availablePurchases.sortByPrice();
        }

        //ShowTheCheapest
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            availablePurchases.showTheCheapest();
        }

        class Foo
        {
            public int i = 6;
            public Bar bar;
        }

        class Bar
        {
            public Foo foo;
        }
        

        private void button8_Click(object sender, RoutedEventArgs e)
        {
           // Serializer s = new Serializer("serialized.txt", availableItemsLB);
              Bar bubu = new Bar();
            Foo fufu = new Foo() { bar = bubu };
            bubu.foo = fufu;
            Serializer s = new Serializer("serialized.txt", bubu);
         /*   int obj = 5;
            Serializer s = new Serializer();
            Foo fufu = (Foo)s.readObject();

            PurchaseCollection test = (PurchaseCollection)s.readObject();
            int i = Convert.ToInt32(s.readObject());
            s.writeObject();*/
        }
    }
}