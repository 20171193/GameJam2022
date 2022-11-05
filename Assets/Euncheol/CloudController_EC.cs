using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController_EC : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("CloudDestroy", true);
        
    }
}
