using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameObject contentParent;
    [SerializeField]
    private QuestScroll questScroll;
    [SerializeField]
    private TextMeshProUGUI questDisplayNameText;
    [SerializeField]
    private TextMeshProUGUI questStatusText;
    [SerializeField]
    private TextMeshProUGUI questRequirmentsText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestLogTogglePressed()
    {
        if(contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        Time.timeScale = 0;
        contentParent.SetActive(true);
        Cursor.lockState = CursorLockMode.None;  // 마우스 포인터 활성화
        Cursor.visible = true;
        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    private void HideUI()
    {
        Time.timeScale = 1;
        contentParent.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 포인터 비활성화
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = questScroll.CreateButtonIfNotExists(quest, () =>
        {
            SetQuestLogInfo(quest);
        });

        if(firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }
    }

    private void SetQuestLogInfo(Quest quest)
    {
        questDisplayNameText.text = quest.info.displayName;
        questStatusText.text = quest.GetFullStatusText();
        questRequirmentsText.text = "없음";
        foreach(QuestInfo prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            questRequirmentsText.text += prerequisiteQuestInfo.displayName + "\n";
        }
    }
}
