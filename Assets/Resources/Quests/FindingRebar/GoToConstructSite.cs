using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToConstructSite : QuestStep
{
    [SerializeField]
    private string visitPlace;

    private void Start()
    {
        string status = visitPlace + "(��)�� ����!";
        ChangeState("", status);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string status = visitPlace + "�� �����ߴ�.";
            ChangeState("", status);
            FinishQuestStep();
        }
    }



    protected override void SetQuestStepState(string state)
    {

    }
}
