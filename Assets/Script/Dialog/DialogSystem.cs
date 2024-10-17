using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Speaker[] speakers;
    [SerializeField]
    private DialogData[] dialogs;


    private bool isFirst = true;
    private int currentDialogIndex = -1;
    private int currentSpeakerIndex = 0;
    private float typingSpeed = 0.05f;
    private bool isTyping = false;
    private bool isDialogActive = false;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObjects(speakers[i], false);
        }
    }

    public bool UpdateDialog()
    {
        if (isFirst == true)
        {
            Setup();
            isFirst = false;
            isDialogActive = true;
            SetNextDialog();  // 첫 번째 대화 시작
            return false;
        }
        isDialogActive = true;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping == true)
            {
                isTyping = false;
                StopCoroutine("OnTypingText");
                speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
                speakers[currentSpeakerIndex].objectArrow.SetActive(true);
                return false;
            }
            if (dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            else
            {
                for (int i = 0; i < speakers.Length; i++)
                {
                    SetActiveObjects(speakers[i], false);
                }
                isDialogActive = false;
                return true;
            }
        }
        return false;
    }

    private void SetNextDialog()
    {
        currentDialogIndex++;
        currentSpeakerIndex = dialogs[currentSpeakerIndex].speakerIndex;

        SetActiveObjects(speakers[currentSpeakerIndex], true);
        Time.timeScale = 0;

        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;
        StartCoroutine("OnTypingText");
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);

        speaker.objectArrow.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTyping = true;

        while (index <= dialogs[currentDialogIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index);
            index++;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }

    public bool IsDialogActive() => isDialogActive;

    [System.Serializable]
    public struct Speaker
    {
        public Image imageDialog;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDialogue;
        public GameObject objectArrow;
    }

    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;
        public string name;
        [TextArea(3, 5)]
        public string dialogue;
    }
}