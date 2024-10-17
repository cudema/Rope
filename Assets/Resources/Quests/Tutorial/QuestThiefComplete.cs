using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestThiefComplete : QuestStep
{
    [SerializeField]
    private string NPCName;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed += HandleInteraction;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= HandleInteraction;
    }

    private void Start()
    {
        string status = NPCName + "과 대화하기[F]";
        ChangeState("", status);
    }

    private void HandleInteraction()
    {
        GameObject detectedObject = ObjectDetector.Instance.GetDetectedObject();

        if (detectedObject != null && detectedObject.CompareTag("NPC"))
        {
            string status = NPCName + "과 대화했다.";
            ChangeState("", status);
            FinishQuestStep(); 
        }
    }


    protected override void SetQuestStepState(string state)
    {
    }
}
