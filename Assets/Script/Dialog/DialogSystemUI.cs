using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogSystemUI : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;  // 마우스 포인터 활성화
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 포인터 비활성화
        Cursor.visible = false;
    }

}
