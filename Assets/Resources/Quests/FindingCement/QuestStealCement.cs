using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStealCement : QuestStep
{
    private int itemsCollected = 0; // ������ ������ ����
    private int itemsToComplete = 5; // �Ϸ��ϱ� ���� ������ ����
    private int targetItemID = 1; // ������ �������� ID

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
            itemsCollected += amount; // ������ ������ ���� ����
            UpdateState();

            if (itemsCollected >= itemsToComplete)
            {
                FinishQuestStep(); // ����Ʈ �Ϸ�
            }
        }
    }

    private void UpdateState()
    {
        string state = itemsCollected.ToString();
        string status = "Collected " + itemsCollected + " / " + itemsToComplete + " items.";
        ChangeState(state, status); // ���� ������Ʈ
    }

    protected override void SetQuestStepState(string state)
    {
        UpdateState();
    }
}
