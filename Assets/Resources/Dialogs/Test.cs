using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    public static Test instance;
    [SerializeField]
    private DialogSystem dialogSystem;
    [SerializeField]
    private GameObject blackPanel;
    [SerializeField]
    private TextMeshProUGUI textMiddle;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite imageSprite;


    private float fadeTime = 0.6f;

    private void Awake()
    {
        instance = this;
        image.sprite = imageSprite;
    }

    private IEnumerator Start()
    {
        // 프롤로그 진행
        image.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialogSystem.UpdateDialog());
        image.gameObject.SetActive(false);

        textMiddle.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1));
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(Fade(1, 0));
        textMiddle.gameObject.SetActive(false);
        blackPanel.SetActive(false);


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