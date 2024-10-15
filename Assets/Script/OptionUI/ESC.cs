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
        OptionPanel.SetActive(false);
    }
}
