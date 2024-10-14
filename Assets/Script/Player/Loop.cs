using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField]
    GameObject loop;

    GameObject _loop;

    GameObject player;
    bool haveLoop = false;
    // Start is called before the first frame update
    void Awake()
    {
        loop.SetActive(false);
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !haveLoop)
        {
            haveLoop = true;
            ShootLoop();
        }
        if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
        {
            loop.SetActive(false);
            DesLoop();
            CutRope();
            player.GetComponent<PlayerMove>().IsRopeing = false;
        }
    }

    void ShootLoop()
    {
        loop.transform.SetParent(player.transform);
        loop.SetActive(true);
        loop.transform.position = transform.position;
        loop.transform.rotation = transform.rotation;

        loop.transform.localPosition += Vector3.forward;

        loop.transform.SetParent(null);
    }

    public void CutRope()
    {
        SpringJoint joint = player.GetComponent<SpringJoint>();
        if (joint == null)
        {
            return;
        }
        loop.SetActive(false);
        Destroy(joint);
    }

    public void DesLoop()
    {
        haveLoop = false;
    }
}
