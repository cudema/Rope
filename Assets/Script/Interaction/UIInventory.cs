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
        inventoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;  // 마우스 포인터 활성화
        Cursor.visible = true;
        
    }

    private void HideUI()
    {
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 포인터 비활성화
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(); // 마우스가 UI 위에 있는지 확인
    }

    public void GetItem(int index)
    {
        items[index].ItemCount++;
    }
}
