using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        // ��ȣ�ۿ� Ű(F) ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("fŰ ����");
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }

        // ��� Ű(Q) ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
        }

        // �κ��丮Ű(I) ����
        if(Input.GetKeyDown(KeyCode.I))
        {
            GameEventsManager.instance.inputEvents.InventoryPressed();
        }
    }
}