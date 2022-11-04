using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_1 : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rd;

    public Vector3 jumppower_r;
    public Vector3 jumppower_u;

    public Vector2 curWall;

    public int wallinterval;

    public GameObject wallManger_ob;
    public WallManager wallManager_st;

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rd = gameObject.GetComponent<Rigidbody2D>();

        wallManager_st = wallManger_ob.GetComponent<WallManager>();

        curWall = new Vector2(-10, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        Jump();

        if(transform.position.x >= wallManager_st.nextWall.x)
       {
            Debug.Log("dd");
            rd.velocity = Vector2.zero;
            transform.position = new Vector3(wallManager_st.nextWall.x, 0.02f,0.0f);
            wallManager_st.UpdateInfo();
        }
    }
    void Jump()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rd.AddForce(jumppower_r, ForceMode2D.Impulse);
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            rd.AddForce(jumppower_u, ForceMode2D.Impulse);
        }
    }

}
