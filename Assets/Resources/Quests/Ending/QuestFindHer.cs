using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestFindHer : QuestStep
{
    [SerializeField]
    private string NPCName;

    private float timeLimit = 900f; // 15분
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
            string status = NPCName + "과 대화했다.";
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
        string status = NPCName + "과 대화하기 [F] - 남은 시간: " + state;

        ChangeState(state, status); 
    }

    private void HandleQuestFailed()
    {
        string status = "시간 초과! " + NPCName + "과 대화하지 못했습니다.";
        ChangeState("실패", status);
        Destroy(this.gameObject);
    }

    protected override void SetQuestStepState(string state)
    {
        Debug.Log($"퀘스트 단계 상태 로드됨: {state}");
    }

}
