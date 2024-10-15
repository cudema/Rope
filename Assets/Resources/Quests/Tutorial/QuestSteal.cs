using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSteal : QuestStep
{
    private int itemsCollected = 0; // 수집한 아이템 개수
    private int itemsToComplete = 1; // 완료하기 위한 아이템 개수
    private int targetItemID = 4; // 수집할 아이템의 ID

    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.itemEvents.onItemGained += ItemCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.itemEvents.onItemGained -= ItemCollected;
    }

    private void ItemCollected(int itemID, int amount)
    {
        if (itemID == targetItemID)
        {
            itemsCollected += amount; // 수집한 아이템 개수 증가
            UpdateState();

            if (itemsCollected >= itemsToComplete)
            {
                FinishQuestStep(); // 퀘스트 완료
            }
        }
    }

    private void UpdateState()
    {
        string state = itemsCollected.ToString();
        string status = "Collected " + itemsCollected + " / " + itemsToComplete + " items.";
        ChangeState(state, status); // 상태 업데이트
    }

    protected override void SetQuestStepState(string state)
    {
        UpdateState();
    }
}
