using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement_EC : MonoBehaviour
{
    Vector3 pos; //현재위치

    public float delta = 2.0f; // 위아래로 이동가능한 (x)최대값

    public float speed = 3.0f; // 이동속도




    void Start()
    {

        pos = transform.position;

    }


    void Update()
    {

        Vector3 v = pos;

        v.y += delta * Mathf.Sin(Time.time * speed);

        transform.position = v;
    }
}

