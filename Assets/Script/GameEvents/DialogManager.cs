using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DialogManager : MonoBehaviour
{
    [Header("사진 요소")]
    [SerializeField]
    private Image photoImage;

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

    private float fadeTime = 0.6f;

    public IEnumerator PlayStartDialog(int dialogIndex, Action onDialogComplete = null, bool startBlackout = false, bool showPhoto = false, Sprite photoSprite = null, bool isNextScene = false)
    {
        if (dialogIndex < startDialogSystems.Length)
        {
            yield return new WaitUntil(() => startDialogSystems[dialogIndex].UpdateDialog());

            // 암전 기능 시작
            if (startBlackout)
            {
                StartCoroutine(StartBlackout());
            }

            // 사진 표시 여부 확인
            if (showPhoto && photoSprite != null)
            {
                ShowPhoto(photoSprite);
            }

            // 대화가 끝나면 아래 이벤트 실행
            onDialogComplete?.Invoke();

            // 다음 씬으로 전환 여부 확인
            if (isNextScene)
            {
                StartCoroutine(LoadNextScene());
            }
        }
        else
        {
            Debug.LogWarning($"Invalid end dialog index: {dialogIndex}");
        }
    }

    public IEnumerator PlayEndDialog(int dialogIndex, Action onDialogComplete = null, bool startBlackout = false, bool showPhoto = false, Sprite photoSprite = null, bool isNextScene = false)
    {
        if (dialogIndex < endDialogSystems.Length)
        {
            yield return new WaitUntil(() => endDialogSystems[dialogIndex].UpdateDialog());

            if (showPhoto && photoSprite != null)
            {
                ShowPhoto(photoSprite);
            }

            // 암전 기능 시작
            if (startBlackout)
            {
                StartCoroutine(StartBlackout());
            }

            // 대화가 끝나면 아래 이벤트 실행
            onDialogComplete?.Invoke();

            // 다음 씬으로 전환 여부 확인
            if (isNextScene)
            {
                StartCoroutine(LoadNextScene());
            }
        }
        else
        {
            Debug.LogWarning($"Invalid end dialog index: {dialogIndex}");
        }
    }

    private IEnumerator StartBlackout()
    {
        textMiddle.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1));
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(Fade(1, 0));
        textMiddle.gameObject.SetActive(false);
        blackPanel.SetActive(false);
        photoImage.gameObject.SetActive(false);
    }

    private void ShowPhoto(Sprite photoSprite)
    {
        blackPanel.SetActive(true);
        photoImage.gameObject.SetActive(true); // 패널 활성화
        photoImage.sprite = photoSprite; // 이미지 설정
    }

    private IEnumerator LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            yield return new WaitForSeconds(1); 
            yield return SceneManager.LoadSceneAsync(nextSceneIndex); // 다음 씬으로 전환
        }
        else
        {
            Debug.LogWarning("다음 씬이 존재하지 않습니다."); 
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textMiddle.color;
            color.a = Mathf.Lerp(start, end, percent);
            textMiddle.color = color;

            yield return null;
        }
    }
}
