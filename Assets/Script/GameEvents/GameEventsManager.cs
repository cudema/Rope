using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public ItemEvents itemEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        itemEvents = new ItemEvents();
    }
}