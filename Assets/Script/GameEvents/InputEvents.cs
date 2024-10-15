using UnityEngine;
using System;

public class InputEvents
{
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        onSubmitPressed?.Invoke();
    }

    public event Action onQuestLogTogglePressed;
    public void QuestLogTogglePressed()
    {
        onQuestLogTogglePressed?.Invoke();
    }

    public event Action onInventoryPressed;
    public void InventoryPressed()
    {
        onInventoryPressed?.Invoke();
    }
}