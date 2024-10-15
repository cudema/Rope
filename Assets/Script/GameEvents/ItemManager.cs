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

    // ������ �߰�
    private void AddItem(int itemID, int amount)
    {
        Item item = GetItemByID(itemID);
        if (item != null)
        {
            item.Add(amount);
            Debug.Log($"ID {itemID} �������� {amount}�� �߰��Ǿ����ϴ�.");
        }
        UpdateAllItems();
    }

    // Ư�� ������ ã��
    private Item GetItemByID(int id)
    {
        return items.Find(item => item.itemID == id);
    }

    // ��� ������ ���� ������Ʈ
    private void UpdateAllItems()
    {
        foreach (var item in items)
        {
            Debug.Log($"ID {item.itemID}: {item.count}��");
        }

        GameEventsManager.instance.itemEvents.ItemChange(items);
    }
}
