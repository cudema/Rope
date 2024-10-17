using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDetector : MonoBehaviour
{
    public static ObjectDetector Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI textInteraction;
    [SerializeField]
    private UIInventory uIInventory;

    private GameObject detectedObject;
    private int detectedItemID = -1;

    SoundManager soundmanager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  
        }
        else
        {
            Destroy(gameObject);
        }

        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed += HandleSubmit;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= HandleSubmit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            if(textInteraction.enabled == false)
            {
                textInteraction.enabled = true;
                detectedItemID = other.GetComponent<Item>().itemID;
                detectedObject = other.gameObject;
                string name = other.name.Split('_')[0];
                textInteraction.text = $"[F] {name}";
            }
        }

        if(other.CompareTag("NPC"))
        {
            if (textInteraction.enabled == false)
            {
                detectedObject = other.gameObject;
                textInteraction.enabled = true;
                string name = other.name.Split('_')[0];
                textInteraction.text = $"[F] {name}";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
                detectedObject = null;
                detectedItemID = -1;
            }
        }

        if(other.CompareTag("NPC"))
        {
            if (detectedObject == other.gameObject)
            {
                detectedObject = null;
                textInteraction.enabled = false;
            }
        }
    }

    public GameObject GetDetectedObject()
    {
        return detectedObject;
    }

    private void HandleSubmit()
    {
        if (detectedItemID != -1 && detectedObject != null)
        {
            GameEventsManager.instance.itemEvents.ItemGained(detectedItemID, 1);
            int index = detectedObject.gameObject.GetComponent<Item>().itemID - 1;
            uIInventory.GetItem(index);
            Destroy(detectedObject);

            soundmanager.SoundPlay("GetItem");

            detectedObject = null;
            detectedItemID = -1;
            textInteraction.enabled = false;
        }
    }
}
