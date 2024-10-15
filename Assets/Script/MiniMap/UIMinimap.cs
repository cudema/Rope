using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMinimap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    //[SerializeField]
    //private float zoomMin = 1.0f;
    //[SerializeField]
    //private float zoomMax = 30.0f;
    //[SerializeField]
    //private float zoomStepSize = 1.0f;
    [SerializeField]
    private TextMeshProUGUI textMapInfo;

    private void Awake()
    {
        textMapInfo.text = SceneManager.GetActiveScene().name;
    }

}
