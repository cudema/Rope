using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageOneScript : MonoBehaviour
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
        instance = this;
        photo.sprite = photosprite;
    }

    private IEnumerator Start()
    {
        photo.gameObject.SetActive(true);
        yield return new WaitUntil(() => dialogSystem.UpdateDialog());
        photo.gameObject.SetActive(false);
        
    }

}
