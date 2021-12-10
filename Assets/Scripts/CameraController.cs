using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float offsetX = 0;
    public float offsetY = 0;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z );
        
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x + offsetX, target.position.y + offsetY, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, .05f);
        }
    }
}