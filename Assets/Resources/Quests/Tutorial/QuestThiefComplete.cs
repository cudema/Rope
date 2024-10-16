using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestThiefComplete : MonoBehaviour
{
    [SerializeField]
    private string NPCName;


    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed += HandleSubmit;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= HandleSubmit;
    }
}
