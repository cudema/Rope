using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("���� ��� ���")]
    [SerializeField]
    private GameObject blackPanel;
    [SerializeField]
    private TextMeshProUGUI textMiddle;

    [Header("��ȭ �ý��� ���")]
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
