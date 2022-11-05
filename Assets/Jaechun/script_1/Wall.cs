using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public SpriteRenderer spr;

    public float myAlpha;

    private BoxCollider2D col;

    public int myArrow;     // 0-��, 1-��, 2-��
    public int score = 100;

    GameObject player;

    private float intervalPr; 

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();

        myAlpha = spr.color.a;

        player = GameObject.FindGameObjectWithTag("Player");

        if (tag != "StartWall")
        {
            StartCoroutine(FadeWall());
        }
    }
    IEnumerator FadeWall()
    {
        // �� ���İ� ���� �Լ�
        while (myAlpha <= 1.0f)
        {
            yield return new WaitForSeconds(0.01f);
            myAlpha = intervalPr;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, myAlpha);
        }
    }


    // Update is called once per frame
    void Update()
    {
        intervalPr = 1 - Mathf.Abs(0.2f * (transform.position.x - Mathf.Abs(player.transform.position.x)));

        if(myAlpha >= 1.0f && tag != "StartWall")
        {
            col.enabled = true;
            myAlpha = 0.99f;   // �Ŀ� �ִϸ��̼� ������� ����
        }
    }

    public void SetAlpha(bool isIn)
    {
        if(isIn)
        {
            spr.color = new Color(0.0f, 255.0f, 0.0f, myAlpha);
        }
        else
        {
            spr.color = new Color(255.0f,0.0f, 0.0f, myAlpha);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player.GetComponent<Animator>().SetBool("isJump", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            if (player.transform.position.x >= transform.position.x)
            {
                Debug.Log("in trigger");
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, 0);
                player.GetComponent<CharacterMovement_1>().jumpable = true;
                player.GetComponent<CharacterMovement_1>().jumpArrow = myArrow;
                SetAlpha(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("exit trigger");

            player = collision.gameObject;
            //player.GetComponent<Rigidbody2D>().velocity = Vector3.down*2.0f;
            player.GetComponent<CharacterMovement_1>().jumpable = false;

            if(player.transform.position.x == transform.position.x              // �÷��̾ �� ���� �ְ�
                && transform.position.y + 2.5 >= player.transform.position.y)   // ������ �������� ��Ȳ
            {
                // ��� �̺�Ʈ ����
                // �Ŀ� �ִϸ��̼� ������� ��ü
                SetAlpha(false);
                player.GetComponent<CharacterMovement_1>().DieEvent();
            }
        }
    }


}
