using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private UIItem[] items;


    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryPressed += InventoryPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryPressed -= InventoryPressed;
    }

    private void InventoryPressed()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        Time.timeScale = 0;
        inventoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;  // ���콺 ������ Ȱ��ȭ
        Cursor.visible = true;
        
    }

    private void HideUI()
    {
        Time.timeScale = 1;
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 ������ ��Ȱ��ȭ
        Cursor.visible = false;
    }

    public void GetItem(int index)
    {
        items[index].ItemCount++;
    }
}
