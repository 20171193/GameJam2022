using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_1 : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rd;

    public Vector3 jumppower_r;
    public Vector3 jumppower_u;
    public Vector3 jumppower_d;

    public GameObject wallManger_ob;
    public WallManager wallManager_st;

    private bool jumpable;

    private int jumpArrow;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rd = gameObject.GetComponent<Rigidbody2D>();

        wallManager_st = wallManger_ob.GetComponent<WallManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            wallManager_st.SpawnWall();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            jumpArrow = collision.gameObject.GetComponent<Wall>().myArrow;
            jumpable = true;
        }
    }

    void Jump()
    {
        if (jumpable)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow)
                && jumpArrow == 1)
            {
                rd.AddForce(jumppower_u, ForceMode2D.Impulse);
                jumpable = false;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)
                && jumpArrow == 2)
            {
                rd.AddForce(jumppower_r, ForceMode2D.Impulse);
                jumpable = false;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)
                && jumpArrow == 3)
            {
                rd.AddForce(jumppower_d, ForceMode2D.Impulse);
                jumpable = false;
            }
        }
    }
}
