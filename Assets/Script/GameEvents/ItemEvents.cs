using System;
using System.Collections.Generic;

public class ItemEvents
{
    public event Action<int, int> onItemGained;  
    public void ItemGained(int itemID, int amount)
    {
        onItemGained?.Invoke(itemID, amount);
    }

    public event Action<List<Item>> onItemChange;
    public void ItemChange(List<Item> items)
    {
        onItemChange?.Invoke(items);
    }
}
