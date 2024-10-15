using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemID; 
    public int count;   

    public Item(int id, int initialCount = 0)
    {
        itemID = id;
        count = initialCount;
    }


    public void Add(int amount)
    {
        count += amount;
    }


    public bool Subtract(int amount)
    {
        if (count >= amount)
        {
            count -= amount;
            return true;
        }
        return false;
    }
}

