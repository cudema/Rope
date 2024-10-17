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
    SoundManager soundmanager;



    private void Awake()
    {
        photo.sprite = photosprite;
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private IEnumerator Start()
    {
        photo.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialogSystem.UpdateDialog());
        photo.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        soundmanager.SoundPlay("MainMenu");
        SceneManager.LoadScene("Title");

    }

}
