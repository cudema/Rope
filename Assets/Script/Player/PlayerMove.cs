using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    Loop loop;

    [SerializeField]
    float moveP;
    [SerializeField]
    float jumpP;
    [SerializeField]
    float runMaxSpeed;
    [SerializeField]
    float graundMexSpeed;
    [SerializeField]
    float airMaxSpeed;
    [SerializeField]
    float ropeP;

    Rigidbody rb;
    SoundManager soundManager;

    bool isGraund = false;
    public bool isRopeing = false;
    public bool IsRopeing
    {
        set => isRopeing = value;
        get => isRopeing;
    }
    public float AirMaxSpeed => airMaxSpeed;
    float maxSpeed;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(WalkSound());
        maxSpeed = graundMexSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 dis = new Vector3(xMove, 0, zMove).normalized * moveP * Time.deltaTime;

        if (isRopeing)
        {
            dis *= ropeP;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGraund)
        {
            maxSpeed = runMaxSpeed;
        }
        else
        {
            maxSpeed = graundMexSpeed;
        }

        if (isGraund || isRopeing)
        {
            rb.AddRelativeForce(dis);
        }

        if (isGraund && !isRopeing)
        { 
            rb.AddForce(0, Input.GetAxis("Jump") * jumpP * Time.deltaTime, 0);
            if (rb.velocity.magnitude > graundMexSpeed / 3)
            {
                rb.AddForce(-rb.velocity.normalized * 1500 * Time.deltaTime);
            }
        }

        if (rb.velocity.magnitude > maxSpeed && isGraund)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (rb.velocity.magnitude > airMaxSpeed && !isGraund)
        {
            rb.velocity = rb.velocity.normalized * airMaxSpeed;
        }

        //if (isRopeing)
        //{
        //    rb.AddRelativeForce(rb.velocity.normalized * 1000f * Time.deltaTime);
        //}
    }

    IEnumerator WalkSound()
    {
        while (true)
        {
            yield return new WaitUntil(() => rb.velocity.magnitude > 7.0f && isGraund && !IsRopeing);

            int temp = Random.Range(0, 4);

            soundManager.SoundPlay("Walk" + temp);

            yield return new WaitForSeconds(0.25f);
        }

    }

    public void StartRope()
    {
        rb.velocity /= 2;
    }

    public void PlayerKnockBack(Vector3 criteria, float pawer)
    {
        int temp = Random.Range(0, 6);
        soundManager.SoundPlay("HP" + temp);

        Vector3 dis = new Vector3(transform.position.x - criteria.x, transform.position.y - criteria.y, transform.position.z - criteria.z);
        rb.AddForce(dis.normalized * pawer);
        loop.CutRope();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Graund")
        {
            isGraund = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Graund")
        {
            isGraund = false;
        }
    }
}