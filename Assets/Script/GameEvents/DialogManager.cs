using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("암전 기능 요소")]
    [SerializeField]
    private GameObject blackPanel;
    [SerializeField]
    private TextMeshProUGUI textMiddle;

    [Header("대화 시스템 요소")]
    [SerializeField]
    private DialogSystem[] startDialogSystems;
    [SerializeField]
    private DialogSystem[] endDialogSystems;

    public IEnumerator PlayStartDialog(int dialogIndex)
    {
        if (dialogIndex < startDialogSystems.Length)
        {
            yield return new WaitUntil(() => startDialogSystems[dialogIndex].UpdateDialog());
        }
        else
        {
            Debug.LogWarning($"Invalid start dialog index: {dialogIndex}");
        }
    }

    public IEnumerator PlayEndDialog(int dialogIndex)
    {
        if (dialogIndex < endDialogSystems.Length)
        {
            yield return new WaitUntil(() => endDialogSystems[dialogIndex].UpdateDialog());
        }
        else
        {
            Debug.LogWarning($"Invalid end dialog index: {dialogIndex}");
        }
    }
}
