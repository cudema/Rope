using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenOption : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel;

    public void Open()
    {
        optionPanel.SetActive(true);

        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay("UI");
    }
}
