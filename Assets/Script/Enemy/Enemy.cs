using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Collider[] player;
    [SerializeField]
    float range;
    [SerializeField]
    LayerMask layer;

    [SerializeField]
    float aimingTime;
    [SerializeField]
    float shootTime;
    [SerializeField]
    float reloadingTime;

    [SerializeField]
    Animator animator;

    public float Range => range;

    bool playerInRange = false;
    GameObject bullet;
    LineRenderer lineRenderer;
    SoundManager soundManager;

    private void Awake()
    {
        bullet = transform.GetChild(0).gameObject;
        bullet.SetActive(false);
        bullet.transform.position = transform.position + Vector3.up;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (!playerInRange)
        {
            player = Physics.OverlapSphere(transform.position, range, layer);
        }
        if (player.Length > 0 && !playerInRange)
        {
            LookOtPlayer();
        }
    }

    void LookOtPlayer()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        playerInRange = true;
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        float time = 0;

        while (time < aimingTime)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, player[0].transform.position);
            time += Time.deltaTime;
            transform.LookAt(player[0].transform.position);
            yield return null;
        }

        bullet.transform.LookAt(player[0].transform.position);

        yield return new WaitForSeconds(shootTime);

        //ÃÑ ½î±â
        lineRenderer.enabled = false;
        bullet.SetActive(true);
        soundManager.SoundPlay("ShootGun");
        animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(0.2f);

        bullet.SetActive(false);

        yield return new WaitForSeconds(reloadingTime);

        soundManager.SoundPlay("Reload");

        bullet.transform.position = transform.position;
        playerInRange = false;

        yield break;
    }
}
