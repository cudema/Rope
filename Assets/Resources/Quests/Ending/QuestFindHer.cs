using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestFindHer : QuestStep
{
    [SerializeField]
    private string NPCName;

    private float timeLimit = 900f; // 15��
    private float timeRemaining;


    SoundManager soundmanager;

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
        timeRemaining = timeLimit; 
        UpdateState(); 
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; 
            UpdateState(); 
        }
        else
        {
            HandleQuestFailed(); 
        }
    }

    private void HandleInteraction()
    {
        GameObject detectedObject = ObjectDetector.Instance.GetDetectedObject();

        if (detectedObject != null && detectedObject.CompareTag("NPC"))
        {
            string status = NPCName + "�� ��ȭ�ߴ�.";
            ChangeState("", status); 
            FinishQuestStep();
            soundmanager.SoundPlay("HoppyEnding");
            SceneManager.LoadScene("Happyending");
        }
    }

    private void UpdateState()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        string state = string.Format("{0:00}:{1:00}", minutes, seconds);
        string status = NPCName + "�� ��ȭ�ϱ� [F] - ���� �ð�: " + state;

        ChangeState(state, status); 
    }

    private void HandleQuestFailed()
    {
        string status = "�ð� �ʰ�! " + NPCName + "�� ��ȭ���� ���߽��ϴ�.";
        ChangeState("����", status);
        Destroy(this.gameObject);
    }

    protected override void SetQuestStepState(string state)
    {
        Debug.Log($"����Ʈ �ܰ� ���� �ε��: {state}");
    }

}
