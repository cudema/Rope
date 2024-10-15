using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        // 상호작용 키(F) 감지
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("f키 감지");
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }

        // 토글 키(Q) 감지
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
        }

        // 인벤토리키(I) 감지
        if(Input.GetKeyDown(KeyCode.I))
        {
            GameEventsManager.instance.inputEvents.InventoryPressed();
        }
    }
}