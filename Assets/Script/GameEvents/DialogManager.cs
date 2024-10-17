using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DialogManager : MonoBehaviour
{
    [Header("���� ���")]
    [SerializeField]
    private Image photoImage;

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

    private float fadeTime = 0.6f;

    public IEnumerator PlayStartDialog(int dialogIndex, Action onDialogComplete = null, bool startBlackout = false, int BlackoutTiming = -1)
    {
        if (dialogIndex < startDialogSystems.Length)
        {
            yield return new WaitUntil(() => startDialogSystems[dialogIndex].UpdateDialog());

            // ���� ��� ����
            if (startBlackout && BlackoutTiming == 0)
            {
                StartCoroutine(StartBlackout());
            }

            // ��ȭ�� ������ �Ʒ� �̺�Ʈ ����
            onDialogComplete?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Invalid end dialog index: {dialogIndex}");
        }
    }

    public IEnumerator PlayEndDialog(int dialogIndex, Action onDialogComplete = null, bool startBlackout = false, int BlackoutTiming = -1, bool isNextScene = false)
    {
        if (dialogIndex < endDialogSystems.Length)
        {
            yield return new WaitUntil(() => endDialogSystems[dialogIndex].UpdateDialog());


            // ���� ��� ����
            if (startBlackout && BlackoutTiming == 1)
            {
                StartCoroutine(StartBlackout());
            }

            // ��ȭ�� ������ �Ʒ� �̺�Ʈ ����
            onDialogComplete?.Invoke();

            // ���� ������ ��ȯ ���� Ȯ��
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
        blackPanel.SetActive(true);
        yield return StartCoroutine(Fade(0, 1));
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(Fade(1, 0));
        textMiddle.gameObject.SetActive(false);
        blackPanel.SetActive(false);
    }

    private IEnumerator LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            yield return new WaitForSeconds(4); 
            SceneManager.LoadScene(nextSceneIndex); // ���� ������ ��ȯ
        }
        else
        {
            Debug.LogWarning("���� ���� �������� �ʽ��ϴ�."); 
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
