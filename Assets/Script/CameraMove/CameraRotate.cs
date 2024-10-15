using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    float xMoveSpeed;
    [SerializeField]
    float yMoveSpeed;

    float x, y;

    [SerializeField]
    Transform playerTransform;



    private void Awake()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = playerTransform.position + (Vector3.up * 1);

        x -= xMoveSpeed * Input.GetAxisRaw("Mouse Y") * Time.deltaTime;
        y += yMoveSpeed * Input.GetAxisRaw("Mouse X") * Time.deltaTime;

        x = ClampAngle(x);

        transform.rotation = Quaternion.Euler(new Vector3(x, transform.eulerAngles.y, 0));
        playerTransform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
    }

    float ClampAngle(float angle)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, -80.0f, 80.0f);
    }
}
