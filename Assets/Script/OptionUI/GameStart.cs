using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void StartGame()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay("UI");

        SceneManager.LoadScene(1);
    }
}
