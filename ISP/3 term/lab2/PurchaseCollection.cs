using System;
using System.Collections.Generic;
using System.Collections;



public class Purchase : IEquatable<Purchase>
{
    public int Сount { get;set; }
    public float Price { get; private set; }
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

public class PurchaseCollection : ICollection<Purchase>
{
    const int initialSizeOfArray = 10;

    //names of items
    public string[] Names;

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
        Names = new string[initialSizeOfArray];
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
                Array.Resize<string>(ref Names, Names.Length * 2);
            }

            Names[Count] = item.Name;
            purchaseArray[Count] = item;
        } else
        {
            this[IndexOf(item)].Сount++;    
        } 
    }

    //Removes all items from the collection 
    public void Clear()
    { 
        Array.Clear(purchaseArray, 0, purchaseArray.Length);
        Array.Clear(Names, 0, Names.Length);
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
                    Names[i] = null;
                    for (int counter = i; counter < Count; counter++)
                    {
                        purchaseArray[counter] = purchaseArray[counter + 1];
                        Names[counter] = Names[counter + 1];
                    }

                    result = true;
                    break;
                } else
                {
                    purchaseArray[i].Сount--;
                }
            }
        }
        return result;
    }


    //-----------------------------------End of Realization of ICollection<T>---------------------------------------//

    //-----------------------------------Realization of Enumerable------------------------------------------//
    // Спросить зачем 2 GetEnumerator. ( по другому ошибка компиляции)
    public IEnumerator<Purchase> GetEnumerator()
    {
        return new PurchaseEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new PurchaseEnumerator(this);
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

//My Enumerator class
public class PurchaseEnumerator : IEnumerator<Purchase>
{
    private PurchaseCollection collection;
    private Purchase[] array;
    private int currentIndex;
    private Purchase currentItem;

    public PurchaseEnumerator(PurchaseCollection collection)
    {
        this.collection = collection;
        this.array = collection.purchaseArray;
        currentIndex = -1;
        currentItem = default(Purchase);
    }

    public bool MoveNext()
    {
        //Increments index and check that is lower that count items of 
        // a collection
        if (++currentIndex >= array.Length)
        {
            return false;
        }
        else
        {
            currentItem = array[currentIndex];
        }
        return true;
    }

    //Set current index before the first item of the collection
    public void Reset() { currentIndex = -1; }

    //I dont know how to dispose resources!!
    void IDisposable.Dispose() { }

    public Purchase Current
    {
        get { return currentItem; }
    }

    object IEnumerator.Current
    {
        get { return Current; }
    }

}

