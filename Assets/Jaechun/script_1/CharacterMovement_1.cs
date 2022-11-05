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

    [Header("WallManager 오브젝트 할당")]
    [SerializeField]
    private GameObject wallManger_ob;
    private WallManager wallManager_st;

    public bool jumpable;

    public int jumpArrow;   // 0 - 상, 1 - 중, 2 - 하

    public float cur_xpos;
    public float cur_ypos;

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rd = gameObject.GetComponent<Rigidbody2D>();

        wallManager_st = wallManger_ob.GetComponent<WallManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "StartWall")
        {
            jumpable = true;
            jumpArrow = collision.gameObject.GetComponent<Wall>().myArrow;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StartWall")
        {
            jumpable = false;
        }
    }

    void Jump()
    {
        if (jumpable)
        {
            //Debug.Log("jumping");
            if (Input.GetKeyUp(KeyCode.UpArrow)     // 이동 - 상
                && jumpArrow == 0)
            {
                rd.gravityScale = 1.5f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos,0); // 점프 시작 위치 초기화
                rd.AddForce(jumppower_u, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;
                cur_ypos += wallManager_st.wallinterval_y;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)  // 이동 - 중
                && jumpArrow == 1)
            {
                rd.gravityScale = 1.5f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos, 0); // 점프 시작 위치 초기화
                rd.AddForce(jumppower_r, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) // 이동 - 하
                && jumpArrow == 2)
            {
                rd.gravityScale = 0.8f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos, 0); // 점프 시작 위치 초기화
                rd.AddForce(jumppower_d, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;
                cur_ypos -= wallManager_st.wallinterval_y;
            }
        }
    }
}
