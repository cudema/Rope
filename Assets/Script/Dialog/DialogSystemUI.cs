using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogSystemUI : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;  // ���콺 ������ Ȱ��ȭ
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 ������ ��Ȱ��ȭ
        Cursor.visible = false;
    }

}
