using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_1 : MonoBehaviour
{
    [SerializeField] GameObject main_Background; //ĳ���Ͱ� ��ġ�ϴ� ���
    [SerializeField] GameObject[] other_Background;  //���� ����� �������� �ѷ��δ� ���

    public GameObject fadeOut;  // ���̵�ƿ�
    public GameObject deadMenu; // ��� â
    public GameObject menuTool; // ��� â Ÿ��Ʋ ��ư

    private int upCheck = 0;
    private int downCheck = 0;
    private int rightCheck = 0;

    Animator anim;

    public AudioSource audioSource;      ////////���� �Ҹ�

    public float rightTileToTile; //������ Ÿ�� ����
    public float updownTileToTile; //���Ʒ� Ÿ�� ����

    private float bgWidth;   //��� ��������Ʈ�� �ʺ�
    private float bgHeight;  //��� ��������Ʈ�� ���̰�

    private Transform tr;

    private Rigidbody2D rd;

    private SpriteRenderer spr;

    public Sprite dieSp;

    public Vector3 jumppower_r;
    public Vector3 jumppower_u;
    public Vector3 jumppower_d;

    [Header("WallManager ������Ʈ �Ҵ�")]
    [SerializeField]
    private GameObject wallManger_ob;
    private WallManager wallManager_st;

    public bool jumpable;

                            // ���� �÷��̾� ����
    public bool jumping;    // Ÿ�ֿ̹� ���� ��� - true, 
                            // ������ ���� ��� - false.

    public int jumpArrow;   // 0 - ��, 1 - ��, 2 - ��

    public float cur_xpos;
    public float cur_ypos;

    public bool isDie;
    public enum MyEventType { Normal, House, Die };

    public MyEventType myEvent = MyEventType.Normal;   // �̺�Ʈ�� ���� �� ����? (���� �Է� ������)

    public GameObject CountManager;

    public GameObject curHouse;

    public float HouseTime;

    public bool houseKeyCheck;

    public int[] inputKey;

    public int inputCount;

    public GameObject uiManager;

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rd = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        wallManager_st = wallManger_ob.GetComponent<WallManager>();
        inputKey = new int[3];
    }

    // Start is called before the first frame update
    void Start()
    {
        isDie = false;

        houseKeyCheck = false;

        inputCount = 0;

        main_Background.transform.position = new Vector3(0, 0, 0);

        audioSource = GetComponent<AudioSource>(); ////////���� �Ҹ�
        bgWidth = main_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        bgHeight = main_Background.GetComponent<SpriteRenderer>().bounds.size.y;
        CountManager = GameObject.FindWithTag("CountManager");
        uiManager = GameObject.FindWithTag("UIManager");
        OtherBackgroundSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (myEvent != MyEventType.Die && !houseKeyCheck)
        {
            Jump();
        }
        else if (houseKeyCheck)
        {
            HouseKeyCheck();
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
        if (Input.GetKeyDown(KeyCode.UpArrow))     // �̵� - ��
        {
            if (jumpable != true)
            {
                Debug.Log("Ÿ�̹� Ʋ��");
                DieEvent();
                return;
            }
            if (jumpArrow != 0)
            {
                Debug.Log("ȭ��ǥ Ʋ��");
                DieEvent();
                return;
            }
            else if(jumpArrow == 0 && myEvent == MyEventType.House)
            {
                HouseEvent();
                Debug.Log("�Ͽ콺 �̺�Ʈ ����");
                return;
            }

            Debug.Log("��� �̵�");
            anim.SetBool("isJump",true);  ////�ִϸ��̼�
            audioSource.Play();   /////////�����Ҹ�

            jumping = true; 
            rd.gravityScale = 1.5f;
            rd.velocity = Vector3.zero;
            transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
            rd.AddForce(jumppower_u, ForceMode2D.Impulse);
            wallManager_st.CheckInPlayer();
            jumpable = false;
            cur_xpos += wallManager_st.wallinterval_x;
            cur_ypos += wallManager_st.wallinterval_y;
            upCheck++; //���
            downCheck--;  //���
            rightCheck++;
            MainBackgroundUp();  //���
            MainBackgroundForward();  //���
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))  // �̵� - ��
        {
            if (jumpable != true)
            {
                Debug.Log("Ÿ�̹� Ʋ��");
                DieEvent();
                return;
            }
            if (jumpArrow != 1)
            {
                Debug.Log("ȭ��ǥ Ʋ��");
                DieEvent();
                return;
            }
            else if (jumpArrow == 1 && myEvent == MyEventType.House)
            {
                HouseEvent();
                Debug.Log("�Ͽ콺 �̺�Ʈ ����");
                return;
            }

            Debug.Log("���� �̵�");
            anim.SetBool("isJump", true);  ////�ִϸ��̼�
            audioSource.Play();   /////////�����Ҹ�

            jumping = true;
            rd.gravityScale = 1.5f;
            rd.velocity = Vector3.zero;
            transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
            rd.AddForce(jumppower_r, ForceMode2D.Impulse);
            wallManager_st.CheckInPlayer();
            jumpable = false;
            cur_xpos += wallManager_st.wallinterval_x;
            rightCheck++;

            MainBackgroundForward(); //���
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // �̵� - ��
        {
            if (jumpable != true)
            {
                Debug.Log("Ÿ�̹� Ʋ��");
                DieEvent();
                return;
            }
            if (jumpArrow != 2)
            {
                Debug.Log("ȭ��ǥ Ʋ��");
                DieEvent();
                return;
            }
            else if (jumpArrow == 0 && myEvent == MyEventType.House)
            {
                HouseEvent();
                Debug.Log("�Ͽ콺 �̺�Ʈ ����");
                return;
            }

            Debug.Log("�ϴ� �̵�");
            anim.SetBool("isJump", true);  ////�ִϸ��̼�
            audioSource.Play();   /////////�����Ҹ�

            jumping = true;
            rd.gravityScale = 0.8f;
            rd.velocity = Vector3.zero;
            transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
            rd.AddForce(jumppower_d, ForceMode2D.Impulse);
            wallManager_st.CheckInPlayer();
            jumpable = false;
            cur_xpos += wallManager_st.wallinterval_x;
            cur_ypos -= wallManager_st.wallinterval_y;

            downCheck++; //���
            upCheck--; //���
            rightCheck++;
            MainBackgroundDown(); //���
            MainBackgroundForward(); //���
        }
    }

    public void HouseEvent()
    {
        rd.gravityScale = 0.0f;
        rd.velocity = Vector3.zero;
        transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ

        CountManager.GetComponent<CountTimer>().ExecuteTimer(HouseTime);

        myEvent = MyEventType.House;

        houseKeyCheck = true;

        curHouse = wallManager_st.destroyWall[1];

        uiManager.GetComponent<UIManager>().RenderHouseArrow(
            curHouse.GetComponent<House>().randomArrow[0], curHouse.GetComponent<House>().randomArrow[1],
            curHouse.GetComponent<House>().randomArrow[2], curHouse.GetComponent<House>().myArrow);
        
    }
    public void HouseKeyCheck()
    {
        if(CountManager.GetComponent<CountTimer>().TimerEnd && inputCount <3)
        {
            houseKeyCheck = false;
            Debug.Log("timer end");
            DieEvent();
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("dd");
            if (inputCount >= 3 && 0 == curHouse.GetComponent<House>().myArrow)
            {
                houseKeyCheck = false;
                myEvent = MyEventType.Normal;

                Debug.Log("house done");
                inputCount = 0;

                uiManager.GetComponent<UIManager>().HouseBGI.SetActive(false);

                Debug.Log("��� �̵�");
                anim.SetBool("isJump", true);  ////�ִϸ��̼�
                audioSource.Play();   /////////�����Ҹ�

                jumping = true;
                rd.gravityScale = 1.5f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
                rd.AddForce(jumppower_u, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;
                cur_ypos += wallManager_st.wallinterval_y;
                upCheck++; //���
                downCheck--;  //���
                rightCheck++;
                MainBackgroundUp();  //���
                MainBackgroundForward();  //���
                return;
            }
            else if(inputCount < 3 && 0 == curHouse.GetComponent<House>().randomArrow[inputCount])
            {
                Debug.Log("key done");
                inputCount++;
                return;
            }
            else
            {
                Debug.Log("die");
                DieEvent();
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (inputCount >= 3 && 1 == curHouse.GetComponent<House>().myArrow)
            {
                houseKeyCheck = false;
                myEvent = MyEventType.Normal;

                uiManager.GetComponent<UIManager>().HouseBGI.SetActive(false);

                Debug.Log("house done");
                inputCount = 0;

                Debug.Log("���� �̵�");
                anim.SetBool("isJump", true);  ////�ִϸ��̼�
                audioSource.Play();   /////////�����Ҹ�

                jumping = true;
                rd.gravityScale = 1.5f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
                rd.AddForce(jumppower_r, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;
                rightCheck++;

                MainBackgroundForward(); //���
                return;
            }
            else if (inputCount < 3 && 1 == curHouse.GetComponent<House>().randomArrow[inputCount])
            {
                Debug.Log("key done");
                inputCount++;
                return;
            }
            else
            {
                Debug.Log("die");
                DieEvent();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (inputCount >= 3 && 2 == curHouse.GetComponent<House>().myArrow)
            {
                houseKeyCheck = false;
                myEvent = MyEventType.Normal;

                uiManager.GetComponent<UIManager>().HouseBGI.SetActive(false);

                Debug.Log("house done");
                inputCount = 0;

                Debug.Log("�ϴ� �̵�");
                anim.SetBool("isJump", true);  ////�ִϸ��̼�
                audioSource.Play();   /////////�����Ҹ�

                jumping = true;
                rd.gravityScale = 0.8f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
                rd.AddForce(jumppower_d, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;
                cur_ypos -= wallManager_st.wallinterval_y;

                downCheck++; //���
                upCheck--; //���
                rightCheck++;
                MainBackgroundDown(); //���
                MainBackgroundForward(); //���
                return;
            }
            if (inputCount < 3 && 2 == curHouse.GetComponent<House>().randomArrow[inputCount])
            {
                Debug.Log("key done");
                inputCount++;
                return;
            }
            else
            {
                Debug.Log("die");
                DieEvent();
            }

        }
    }

    public void DieEvent()
    {
        isDie = true;
        myEvent = MyEventType.Die;
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
