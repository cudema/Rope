using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogSystemUI : MonoBehaviour
{

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;  // ���콺 ������ Ȱ��ȭ
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 ������ ��Ȱ��ȭ
        Cursor.visible = false;
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(); // ���콺�� UI ���� �ִ��� Ȯ��
    }
}
