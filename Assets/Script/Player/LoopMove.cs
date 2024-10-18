using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoopMove : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float ropeMaxDistance;
    [SerializeField]
    float swingMinDistance;
    [SerializeField]
    float swingMaxDistance;
    [SerializeField]
    float ropeMagnification;
    [SerializeField]
    float springStrength = 10f;
    [SerializeField]
    float rushP;
    [SerializeField]
    int recoverySwingGauge;

    [SerializeField]
    GameObject player;
    [SerializeField]
    Loop rope;

    SpringJoint joint;
    RopeSwing playerSwing;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        playerSwing = player.GetComponent<RopeSwing>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float temp = Vector3.Distance(transform.position, player.transform.position);

        if (!player.GetComponent<PlayerMove>().IsRopeing)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<PlayerMove>().IsRopeing)
        {
            StartCoroutine(Rush());
        }

        //if (player.GetComponent<PlayerMove>().isRopeing && temp < joint.minDistance)
        //{
        //    joint.minDistance = temp;
        //}

        if (temp > ropeMaxDistance && !player.GetComponent<PlayerMove>().IsRopeing)
        {
            this.gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            player.GetComponent<PlayerMove>().IsRopeing = true;
            Hold();
        }
    }

    IEnumerator Rush()
    {
        soundManager.SoundPlay("Rush");
        joint.spring = rushP;
        joint.damper = 0.1f;
        joint.minDistance = 0.1f;

        yield return null;

        yield return new WaitUntil(() => player.GetComponent<Rigidbody>().velocity.magnitude >= player.GetComponent<PlayerMove>().AirMaxSpeed * 0.7f);

        rope.CutRope();

        yield break;
    }

    void Hold()
    {
        //player.GetComponent<PlayerMove>().StartRope();

        if (player.GetComponent<SpringJoint>() != null)
        {
            return;
        }

        soundManager.SoundPlay("Attaching");

        StartCoroutine(SwingGaugeRecovery());

        joint = player.AddComponent<SpringJoint>();

        float dis = Mathf.Clamp(Vector3.Distance(transform.position, player.transform.position), swingMinDistance, swingMaxDistance);

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = transform.position;

        joint.spring = springStrength;
        joint.massScale = 5f;

        joint.maxDistance = 10;

        

        joint.minDistance = dis * ropeMagnification;
        joint.damper = dis * ropeMagnification;


        joint.breakForce = 10000000;
        joint.breakTorque = 10000000;

        //joint.minDistance = Vector3.Distance(transform.position, player.transform.position);
    }

    IEnumerator SwingGaugeRecovery()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1);

            playerSwing.SwingGauge += recoverySwingGauge;
            playerSwing.GaugeBar();
        }

        yield break;
    }
}
