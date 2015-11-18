﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Text;

public class Purchase : IEquatable<Purchase>
{
    public int Сount { get;set; }
    public int Price { get; private set; }
    public string Name { get; private set; }

    public Purchase(string name, int count, int price)
    {
        this.Name = name;
        this.Сount = count;
        this.Price = price;
    }

    public Purchase(string name, int price)
    {
        this.Name = name;
        this.Сount = 1;
        this.Price = price;
    }
    public bool Equals(Purchase other)
    {

        if (Price == other.Price && Name == other.Name)
            return true;
        else
            return false;
    }
}

public class PurchaseCollection : ICollection<Purchase>, INotifyCollectionChanged
{
    const int initialSizeOfArray = 9;

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    //Inner collection is using like a storage for objects
    public Purchase[] purchaseArray;

    public float TotalPrice {
        get
        {
            float totalPrice = 0;
            for (int i = 0; i < Count; i++)
            {
                totalPrice += this[i].Price * this[i].Сount;
            }
            return totalPrice;
        }
    }

    //Constructor
    public PurchaseCollection()
    {
        purchaseArray = new Purchase[initialSizeOfArray];
    }

    //The address on an index
    public Purchase this[int index]
    {
        get { return purchaseArray[index]; }
        set { purchaseArray[index] = value; }
    }

    //-----------------------------------Realization of ICollection<T>---------------------------------------//
    //Returns a count of items
    public int Count
    {
        get
        {
            int counter = 0;
            while (purchaseArray[counter] != null)
            {
                counter++;
            }
            return counter;
        }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    //Adds item to the collection
    public void Add(Purchase item)
    {
        if (!Contains(item))
        {
            
            if (Count + 1 > purchaseArray.Length)
            {
                Array.Resize<Purchase>(ref purchaseArray, purchaseArray.Length * 2);
            }
            purchaseArray[Count] = item;
        } else
        {
            this[IndexOf(item)].Сount++;    
        }
        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    //Removes all items from the collection 
    public void Clear()
    { 
        Array.Clear(purchaseArray, 0, purchaseArray.Length);
        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    //Returns true if collection contains item, and false if it is not
    public bool Contains(Purchase item)
    {
        if (Count != 0)
        {
            for (int counter = 0; counter < Count; counter++)
            {
                try
                {
                    if (purchaseArray[counter].Equals(item))
                        return true;
                }
                catch (System.NullReferenceException)
                {
                    throw new System.NullReferenceException();
                }
                finally
                {
                    Console.WriteLine("NullReferenceException");
                }
            }
        }

        return false;
    }


    public void CopyTo(Purchase[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
        {
            array[i + arrayIndex] = purchaseArray[i];
        }
    }

    //Removes item from the collection
    public bool Remove(Purchase item)
    {
        bool result = false;

        //Finds item which will be removed
        for (int i = 0; i < Count; i++)
        {
            Purchase currentItem = purchaseArray[i];

            if (currentItem.Equals(item))
            {
                if (currentItem.Сount == 1)
                {
                    purchaseArray[i] = null;
                    for (int counter = i; counter < Count + 1; counter++)
                    {
                        purchaseArray[counter] = purchaseArray[counter + 1];
                    }
                    if (CollectionChanged != null)
                        CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));


                    result = true;
                    break;
                } else
                {
                    purchaseArray[i].Сount--;
                }
            }
        }
        if (CollectionChanged != null)
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        return result;
    }


    //-----------------------------------End of Realization of ICollection<T>---------------------------------------//

    //-----------------------------------Realization of Enumerable------------------------------------------//
    // Спросить зачем 2 GetEnumerator. ( по другому ошибка компиляции)
    public IEnumerator<Purchase> GetEnumerator()
    {
        foreach (var i in purchaseArray)
        {
            yield return i;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    //----------------------------------- End of Realization of Enumerable------------------------------------------//


    //-------------------------------------------------My methods---------------------------------------------------//
   
    //calculates discont
    public float calculateDiscont()
    {
        int discontInPercents = 0;
        for (int counter = 0; counter < Count; counter++)
        {
            discontInPercents += this[counter].Сount; //1 percent for one item
            if (discontInPercents > 5) //maximum 5%
                discontInPercents = 5;
        }
        return (TotalPrice / 100) * discontInPercents;
    }

    //return index of item
    //or returns -1
    public int IndexOf(Purchase item)
    {
        for (int counter = 0; counter < Count; counter++)
        {
            try
            {
                if (purchaseArray[counter].Equals(item))
                    return counter;
            }
            catch (System.NullReferenceException)
            {
                throw new System.NullReferenceException();
            }
            finally
            {
                Console.WriteLine("NullReferenceException");
            }

        }
        return -1;
    }

    public void writeToBinaryFile(string relativePath)
    {
        BinaryWriter writer = new BinaryWriter(File.Open(relativePath, FileMode.Create));

        // writing all properties
        foreach (Purchase current in this)
        {
            if (current != null)
            {
                writer.Write(current.Name);
                writer.Write(current.Price);
                writer.Write(current.Сount);
            }
        }
        writer.Close();
    }

    public void wrirteToTxtFile(string relativePath)
    {
        //creating new file
        if (File.Exists(relativePath))
        {
            File.Delete(relativePath);
        }
        //writing in file
        foreach (Purchase current in this)
        {
            string currentString;
            if (current != null)
            {
                currentString = current.Name;
                File.AppendAllText(relativePath, currentString + '\n');
                currentString = Convert.ToString(current.Сount);
                File.AppendAllText(relativePath, currentString + '\n');
                currentString = Convert.ToString(current.Price);
                File.AppendAllText(relativePath, currentString + '\n');
            }
        }
    }
    public bool readFromFile(string relativePath, int format)
    {
        if (format == 0) //Binary file
        {
            relativePath += ".my";
            if (File.Exists(relativePath))
            {
                //open stream for reading.
                BinaryReader reader = new BinaryReader(File.Open(relativePath, FileMode.Open));

                //remove all items from excisting collection
                Clear();

                // reading from file
                // while not eof
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    int price = reader.ReadInt32();
                    int count = reader.ReadInt32();

                    Add(new Purchase(name, count, price));
                }
                reader.Close();
                if (CollectionChanged != null)
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            else
            {
                return false;
            }
            return true;
        }
        if (format == 1) //Text file
        {
            relativePath += ".txt";
            if (File.Exists(relativePath))
            {
                Clear();   
                using (StreamReader reader = new StreamReader(relativePath))
                {
                    while (reader.Peek() > -1)
                    {
                        string name = reader.ReadLine();
                        int count = Convert.ToInt32(reader.ReadLine());
                        int price = Convert.ToInt32(reader.ReadLine());
                        Add(new Purchase(name, count, price));
                    }
                    if (CollectionChanged != null)
                        CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
                return true;
            }
        }
        return false;
    }

    public bool compressFile(string pathToInitialFile, string pathToCompressedFile)
    {
        if (File.Exists(pathToInitialFile))
        {
            using (FileStream originalFileStream = File.Open(pathToInitialFile, FileMode.Open))
            {
                using (FileStream compressedFileStream = File.Create(pathToCompressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                       CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        } else
        {
            return false;
        }
        return true;
    }

    public bool decompressFile(string pathToCompressedFile, string pathToDecompressedFile)
    {
        if (File.Exists(pathToCompressedFile))
        {
            // Get the stream of the source file.
            using (FileStream inFile = File.Open(pathToCompressedFile, FileMode.Open))
            {
                //Create the decompressed file.
                using (FileStream outFile = File.Create(pathToDecompressedFile))
                {
                    using (GZipStream Decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);
                    }
                }
            }
        } else
        {
            return false;
        }
        return true;
    }


    //returns index of item with some name
    //or returns -1
    public int IndexOf(string name)
    {
        for (int counter = 0; counter < Count; counter++)
        {
            if (name.Equals(this[counter].Name))
                return counter;
        }
        return -1;
    }


    
}

