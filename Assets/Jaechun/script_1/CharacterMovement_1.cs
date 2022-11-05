using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_1 : MonoBehaviour
{
    [SerializeField] GameObject main_Background; //캐릭터가 위치하는 배경
    [SerializeField] GameObject[] other_Background;  //메인 배경을 기준으로 둘러싸는 배경

    private int upCheck = 0;
    private int downCheck = 0;

    public float rightTileToTile; //오른쪽 타일 간격
    public float updownTileToTile; //위아래 타일 간격

    private float bgWidth;   //배경 스프라이트의 너비값
    private float bgHeight;  //배경 스프라이트의 높이값

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
        main_Background.transform.position = new Vector3(0, 0, 0);

        bgWidth = main_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        bgHeight = main_Background.GetComponent<SpriteRenderer>().bounds.size.y;

        OtherBackgroundSetting();
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

                upCheck++; //배경
                downCheck--;  //배경
                MainBackgroundUp();  //배경
                MainBackgroundForward();  //배경
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

                MainBackgroundForward(); //배경
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

                downCheck++; //배경
                upCheck--; //배경
                MainBackgroundDown(); //배경
                MainBackgroundForward(); //배경
            }
        }
    }


    public void MainBackgroundForward()
    {
        if (transform.position.x % bgWidth < rightTileToTile)
        {
            main_Background.transform.position = new Vector3(main_Background.transform.position.x + bgWidth, main_Background.transform.position.y, 0);
            OtherBackgroundSetting();
        }
    }

    public void MainBackgroundUp()
    {
        if (upCheck >= (bgHeight / updownTileToTile) - 1)
        {
            if (Mathf.Abs(transform.position.y) % bgHeight < updownTileToTile)
            {
                main_Background.transform.position = new Vector3(main_Background.transform.position.x, main_Background.transform.position.y + bgHeight, 0);
                OtherBackgroundSetting();
                upCheck = 0;
                downCheck = 0;
            }
        }
    }

    public void MainBackgroundDown()
    {
        if (downCheck >= (bgHeight / updownTileToTile) - 1)
        {
            if (Mathf.Abs(transform.position.y) % bgHeight < updownTileToTile)
            {
                main_Background.transform.position = new Vector3(main_Background.transform.position.x, main_Background.transform.position.y - bgHeight, 0);
                OtherBackgroundSetting();
                downCheck = 0;
                upCheck = 0;
            }
        }
    }
    public void OtherBackgroundSetting()
    {
        other_Background[0].transform.position = new Vector3(main_Background.transform.position.x, main_Background.transform.position.y + bgHeight, 0);
        other_Background[1].transform.position = new Vector3(main_Background.transform.position.x + bgWidth, main_Background.transform.position.y + bgHeight, 0);
        other_Background[2].transform.position = new Vector3(main_Background.transform.position.x + bgWidth, main_Background.transform.position.y, 0);
        other_Background[3].transform.position = new Vector3(main_Background.transform.position.x, main_Background.transform.position.y - bgHeight, 0);
        other_Background[4].transform.position = new Vector3(main_Background.transform.position.x + bgWidth, main_Background.transform.position.y - bgHeight, 0);
    }
}
