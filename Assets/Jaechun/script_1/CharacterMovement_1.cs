using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_1 : MonoBehaviour
{
    [SerializeField] GameObject main_Background; //ĳ���Ͱ� ��ġ�ϴ� ���
    [SerializeField] GameObject[] other_Background;  //���� ����� �������� �ѷ��δ� ���

    private int upCheck = 0;
    private int downCheck = 0;
    private int rightCheck = 0;


    public float rightTileToTile; //������ Ÿ�� ����
    public float updownTileToTile; //���Ʒ� Ÿ�� ����

    private float bgWidth;   //��� ��������Ʈ�� �ʺ�
    private float bgHeight;  //��� ��������Ʈ�� ���̰�

    private Transform tr;

    private Rigidbody2D rd;

    private SpriteRenderer spr;

    public Animator anim;

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

    public bool anyEvent = false;   // �̺�Ʈ�� ���� �� ����? (���� �Է� ������)

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rd = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();    

        wallManager_st = wallManger_ob.GetComponent<WallManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        main_Background.transform.position = new Vector3(0, 0, 0);

        bgWidth = main_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        bgHeight = main_Background.GetComponent<SpriteRenderer>().bounds.size.y;

        anim = GetComponent<Animator>();

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
          //Debug.Log("jumping");
          if (Input.GetKeyUp(KeyCode.UpArrow))     // �̵� - ��
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
            anim.SetBool("IsJump", true);
                Debug.Log("��� �̵�");
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
            if (Input.GetKeyUp(KeyCode.RightArrow))  // �̵� - ��
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
            Debug.Log("���� �̵�");
            anim.SetBool("IsJump", true);

            rd.gravityScale = 1.5f;
                rd.velocity = Vector3.zero;
                transform.position = new Vector3(cur_xpos, cur_ypos, 0); // ���� ���� ��ġ �ʱ�ȭ
                rd.AddForce(jumppower_r, ForceMode2D.Impulse);
                wallManager_st.CheckInPlayer();
                jumpable = false;
                cur_xpos += wallManager_st.wallinterval_x;

                MainBackgroundForward(); //���
                rightCheck++;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow)) // �̵� - ��
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
            Debug.Log("�ϴ� �̵�");
            anim.SetBool("IsJump", true);

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

    public void DieEvent()
    {
        anyEvent = true;
        rd.velocity = Vector3.zero;
        anim.SetBool("IsDead", true);
        StartCoroutine(DieUI());
        //tr.Translate(transform.position.x + 1.0f, transform.position.y + 1.0f, 0.0f);
    }
    IEnumerator DieUI()
    {
        yield return new WaitForSeconds(1.8f);
        Debug.Log("die");
        rd.gravityScale = 0.0f;
        rd.velocity = Vector3.zero;
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
