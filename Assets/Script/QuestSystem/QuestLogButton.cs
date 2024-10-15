using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, IPointerClickHandler
{
    public Button button { get; private set; }
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;

    public void Initialize(string displayName, UnityAction selectAction, TMP_FontAsset customFont)
    {
        this.button = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        this.buttonText.text = displayName;
        this.buttonText.font = customFont;
        this.onSelectAction = selectAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelectAction?.Invoke();  // 버튼 클릭 시 액션 호출
    }
}
