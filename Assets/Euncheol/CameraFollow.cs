using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public float xOffset = 5f;
    public Transform target;


    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y-1.0f, -10f);
        transform.position = Vector3.Slerp(transform.position,newPos,followSpeed*Time.deltaTime);
        
    }

}
