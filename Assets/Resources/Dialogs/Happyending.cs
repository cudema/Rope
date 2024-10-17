using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Happyending : MonoBehaviour
{
    public static StageOneScript instance;
    [SerializeField]
    private DialogSystem dialogSystem;
    [SerializeField]
    private Image photo;
    [SerializeField]
    private Sprite photosprite;


    private void Awake()
    {
        photo.sprite = photosprite;
    }

    private IEnumerator Start()
    {
        photo.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialogSystem.UpdateDialog());
        photo.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Title");

    }

}
