using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESC : MonoBehaviour
{
    [SerializeField]
    private GameObject OptionPanel;

    public void ESCOption()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay("UI");

        OptionPanel.SetActive(false);
    }
}
