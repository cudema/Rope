using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void OnEnable()
    {
        GameEventsManager.instance.itemEvents.onItemGained += AddItem;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.itemEvents.onItemGained -= AddItem;
    }

    private void Start()
    {
        UpdateAllItems();
    }

    // 아이템 추가
    private void AddItem(int itemID, int amount)
    {
        Item item = GetItemByID(itemID);
        if (item != null)
        {
            item.Add(amount);
            Debug.Log($"ID {itemID} 아이템이 {amount}개 추가되었습니다.");
        }
        UpdateAllItems();
    }

    // 특정 아이템 찾기
    private Item GetItemByID(int id)
    {
        return items.Find(item => item.itemID == id);
    }

    // 모든 아이템 상태 업데이트
    private void UpdateAllItems()
    {
        foreach (var item in items)
        {
            Debug.Log($"ID {item.itemID}: {item.count}개");
        }

        GameEventsManager.instance.itemEvents.ItemChange(items);
    }
}
