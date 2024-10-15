using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textInteraction;
    [SerializeField]
    private UIInventory uIInventory;

    private GameObject detectedObject;
    private int detectedItemID = -1;

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
                textInteraction.enabled = true;
                string name = other.name.Split('_')[0];
                textInteraction.text = $"[F] {name}";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") || other.CompareTag("NPC"))
        {
            if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
                detectedObject = null;
                detectedItemID = -1;
            }
        }
    }

    private void HandleSubmit()
    {
        if (detectedItemID != -1 && detectedObject != null)
        {
            GameEventsManager.instance.itemEvents.ItemGained(detectedItemID, 1);
            int index = detectedObject.gameObject.GetComponent<Item>().itemID - 1;
            uIInventory.GetItem(index);
            Destroy(detectedObject);

            detectedObject = null;
            detectedItemID = -1;
            textInteraction.enabled = false;
        }
    }
}
