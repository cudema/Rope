using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLine : MonoBehaviour
{
    LineRenderer lineRenderer;

    [SerializeField]
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        LineRender();
    }

    void LineRender()
    {
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
