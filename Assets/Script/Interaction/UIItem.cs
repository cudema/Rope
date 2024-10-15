using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textItemCount;

    private int itemCount;
    public int ItemCount
    {
        set
        {
            itemCount = Mathf.Clamp(value, 0, 9999);
            textItemCount.text = itemCount.ToString();
        }
        get => itemCount;
    }

    private void Awake()
    {
        ItemCount = 0;
    }
}
