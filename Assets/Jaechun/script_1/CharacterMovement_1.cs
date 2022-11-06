using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_1 : MonoBehaviour
{
    [SerializeField] GameObject main_Background; //캐릭터가 위치하는 배경
    [SerializeField] GameObject[] other_Background;  //메인 배경을 기준으로 둘러싸는 배경

    public GameObject fadeOut;  // 페이드아웃
    public GameObject deadMenu; // 결과 창
    public GameObject menuTool; // 결과 창 타이틀 버튼

    private int upCheck = 0;
    private int downCheck = 0;
    private int rightCheck = 0;

    Animator anim;

    public AudioSource audioSource;      ////////점프 소리

    public float rightTileToTile; //오른쪽 타일 간격
    public float updownTileToTile; //위아래 타일 간격

    private float bgWidth;   //배경 스프라이트의 너비값
    private float bgHeight;  //배경 스프라이트의 높이값

    private Transform tr;

    private Rigidbody2D rd;

    private SpriteRenderer spr;

    public Sprite dieSp;

    public Vector3 jumppower_r;
    public Vector3 jumppower_u;
    public Vector3 jumppower_d;

    [Header("WallManager 오브젝트 할당")]
    [SerializeField]
    private GameObject wallManger_ob;
    private WallManager wallManager_st;

    public bool jumpable;

                            // 현재 플레이어 상태
    public bool jumping;    // 타이밍에 맞춘 경우 - true, 
                            // 맞추지 못한 경우 - false.

    public int jumpArrow;   // 0 - 상, 1 - 중, 2 - 하

    public float cur_xpos;
    public float cur_ypos;

    public bool isDie;

    public bool anyEvent = false;   // 이벤트가 실행 중 인지? (점프 입력 방지용)

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rd = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        wallManager_st = wallManger_ob.GetComponent<WallManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDie = false;

        main_Background.transform.position = new Vector3(0, 0, 0);

        audioSource = GetComponent<AudioSource>(); ////////점프 소리
        bgWidth = main_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        bgHeight = main_Background.GetComponent<SpriteRenderer>().bounds.size.y;

        OtherBackgroundSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anyEvent)
        {
            Jump();
        }
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
        //Debug.Log(ScoreManager.getScore());
        if (Input.GetKeyDown(KeyCode.UpArrow))     // 이동 - 상
        {
            if (jumpable != true)
            {
                Debug.Log("타이밍 틀림");
                DieEvent();
                return;
            }
            if (jumpArrow != 0)
            {
                Debug.Log("화살표 틀림");
                DieEvent();
                return;
            }
            Debug.Log("상단 이동");
            anim.SetBool("isJump",true);  ////애니메이션
            audioSource.Play();   /////////점프소리

            jumping = true; 
            rd.gravityScale = 1.5f;
            rd.velocity = Vector3.zero;
            transform.position = new Vector3(cur_xpos, cur_ypos, 0); // 점프 시작 위치 초기화
            rd.AddForce(jumppower_u, ForceMode2D.Impulse);
            wallManager_st.CheckInPlayer();
            jumpable = false;
            cur_xpos += wallManager_st.wallinterval_x;
            cur_ypos += wallManager_st.wallinterval_y;
            upCheck++; //배경
            downCheck--;  //배경
            rightCheck++;
            MainBackgroundUp();  //배경
            MainBackgroundForward();  //배경
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))  // 이동 - 중
        {
            if (jumpable != true)
            {
                Debug.Log("타이밍 틀림");
                DieEvent();
                return;
            }
            if (jumpArrow != 1)
            {
                Debug.Log("화살표 틀림");
                DieEvent();
                return;
            }
            Debug.Log("우측 이동");
            anim.SetBool("isJump", true);  ////애니메이션
            audioSource.Play();   /////////점프소리

            jumping = true;
            rd.gravityScale = 1.5f;
            rd.velocity = Vector3.zero;
            transform.position = new Vector3(cur_xpos, cur_ypos, 0); // 점프 시작 위치 초기화
            rd.AddForce(jumppower_r, ForceMode2D.Impulse);
            wallManager_st.CheckInPlayer();
            jumpable = false;
            cur_xpos += wallManager_st.wallinterval_x;
            rightCheck++;

            MainBackgroundForward(); //배경
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) // 이동 - 하
        {
            if (jumpable != true)
            {
                Debug.Log("타이밍 틀림");
                DieEvent();
                return;
            }
            if (jumpArrow != 2)
            {
                Debug.Log("화살표 틀림");
                DieEvent();
                return;
            }
            Debug.Log("하단 이동");
            anim.SetBool("isJump", true);  ////애니메이션
            audioSource.Play();   /////////점프소리

            jumping = true;
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
            rightCheck++;
            MainBackgroundDown(); //배경
            MainBackgroundForward(); //배경
        }
    }

    public void DieEvent()
    {
        isDie = true;
        anyEvent = true;
        spr.sprite = dieSp;
        rd.velocity = Vector3.zero;
        anim.SetBool("isDead", true);
        StartCoroutine(DieUI());
        //tr.Translate(transform.position.x + 1.0f, transform.position.y + 1.0f, 0.0f);
    }

    IEnumerator DieUI()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        rd.gravityScale = 0.0f;
        rd.velocity = Vector3.zero;
        UI_Pause();
        Invoke("ToolOn", 0.8f);
    }

    public void UI_Pause()
    {
        gameObject.SetActive(false);
        deadMenu.gameObject.SetActive(true);
    }
    public void ToolOn()
    {
        menuTool.gameObject.SetActive(true);
    }


    public void MainBackgroundForward()
    {
        if (rightCheck >= (bgWidth / rightTileToTile) - 1)
        {
            if (transform.position.x % bgWidth < rightTileToTile)
            {
                main_Background.transform.position = new Vector3(main_Background.transform.position.x + bgWidth, main_Background.transform.position.y, 0);
                OtherBackgroundSetting();

                rightCheck = 0;
            }
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
                rightCheck = 0;
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
                rightCheck = 0;
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
