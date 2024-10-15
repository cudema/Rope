using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeSwing : MonoBehaviour
{
    [SerializeField]
    int swingGaugeConsumption;

    [SerializeField]
    Loop loop;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Vector3 swingVector;

    GameObject player;
    SpringJoint joint;
    LineRenderer lineRenderer;

    int swingGauge;
    public int SwingGauge
    {
        set 
        { 
            swingGauge = Mathf.Clamp(value, 0, 100); 
        }

        get
        {
            return swingGauge;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        lineRenderer = player.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwingGauge >= swingGaugeConsumption)
        {
            if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) && !player.GetComponent<PlayerMove>().IsRopeing)
            {
                Swing();
                player.GetComponent<PlayerMove>().IsRopeing = true;
            }
        }
        if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
        {
            loop.CutRope();
            lineRenderer.enabled = false;
            player.GetComponent<PlayerMove>().IsRopeing = false;
        }

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    public void Swing()
    {
        //player.GetComponent<PlayerMove>().StartRope();

        SwingGauge -= swingGaugeConsumption;
        GaugeBar();

        joint = player.AddComponent<SpringJoint>();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.localPosition + transform.TransformDirection(swingVector));

        GameObject temp = new GameObject();
        temp.transform.rotation = transform.rotation;
        temp.transform.position = transform.position;
        temp.transform.localPosition += transform.TransformDirection(swingVector);

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = temp.transform.position;

        joint.spring = 10f;
        joint.damper = 5f;

        float dis = Vector3.Distance(temp.transform.position, transform.position);

        joint.minDistance = dis;
        joint.maxDistance = dis * 0.7f;
        joint.massScale = 5f;

        //player.GetComponent<Rigidbody>().AddRelativeForce((Vector3.forward).normalized * 500);

        joint.breakForce = 10000000;
        joint.breakTorque = 10000000;

        Destroy(temp.gameObject);
    }

    public void GaugeBar()
    {
        slider.value = SwingGauge;
    }
}
