using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTimer : MonoBehaviour
{
    public float curTime;

    public bool executeTimer;

    public bool TimerEnd;
    // Start is called before the first frame update
    void Start()
    {
        executeTimer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(executeTimer)
        {
            

            curTime -= Time.deltaTime;
            //Debug.Log(curTime);
            if(curTime <= 0)
            {
                TimerEnd = true;
                executeTimer = false;
            }
        }
    }

    public void ExecuteTimer(float limitTime)
    {
        curTime = limitTime;
        TimerEnd = false;
        executeTimer = true;
    }
}
