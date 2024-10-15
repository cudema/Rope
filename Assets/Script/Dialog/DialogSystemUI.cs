using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogSystemUI : MonoBehaviour
{

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;  // 마우스 포인터 활성화
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 포인터 비활성화
        Cursor.visible = false;
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(); // 마우스가 UI 위에 있는지 확인
    }
}
