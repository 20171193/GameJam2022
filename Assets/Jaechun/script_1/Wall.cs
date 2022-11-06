using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public SpriteRenderer spr;

    public float myAlpha;

    private BoxCollider2D col;

    public int myArrow;     // 0-상, 1-우, 2-하
    public int score = 100;

    GameObject player;

    private float intervalPr;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (tag == "EffectWall")
        {
            anim = gameObject.GetComponent<Animator>();
        }
        spr = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();

        myAlpha = spr.color.a;

        player = GameObject.FindGameObjectWithTag("Player");

        if (tag != "StartWall" && tag != "EffectWall")
        {
            StartCoroutine(FadeWall());
        }
        else if(tag == "EffectWall")
        {
            DestroyEffect();
        }
    }
    IEnumerator FadeWall()
    {
        // 벽 알파값 조정 함수
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
        if (tag != "EffectWall")
        {
            intervalPr = 1 - Mathf.Abs(0.2f * (transform.position.x - Mathf.Abs(player.transform.position.x)));

            if (myAlpha >= 1.0f && tag != "StartWall")
            {
                col.enabled = true;
                myAlpha = 0.99f;   // 후에 애니메이션 출력으로 변경
            }
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

    public void DestroyEffect()
    {
        StartCoroutine(DestroyEffectWall());
        spr.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        anim.Play("lsCloud_p", -1, 0.0f);
    }
    IEnumerator DestroyEffectWall()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!player.GetComponent<CharacterMovement_1>().isDie)
            {
                player.GetComponent<Animator>().SetBool("isJump", false);
                player.GetComponent<CharacterMovement_1>().jumpable = true;
                player.GetComponent<CharacterMovement_1>().jumpArrow = myArrow;
                SetAlpha(true);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //if(collision.gameObject.tag == "Player")
    //    //{
    //    //    player = collision.gameObject;
    //    //    if (player.transform.position.x >= transform.position.x)
    //    //    {
    //    //        //Debug.Log("in trigger");
    //    //        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
    //    //        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, 0);
    //    //        player.GetComponent<CharacterMovement_1>().jumpArrow = myArrow;
    //    //    }
    //    //}
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("exit trigger");

            player = collision.gameObject;
            //player.GetComponent<Rigidbody2D>().velocity = Vector3.down*2.0f;

            if(transform.position.y+2.7 > player.transform.position.y)   // 벽에서 떨어지는 상황
            {       
                // 사망 이벤트 실행
                // 후에 애니메이션 출력으로 대체
                SetAlpha(false);
                player.GetComponent<CharacterMovement_1>().DieEvent();
                player.GetComponent<CharacterMovement_1>().jumpable = false;
            }
        }
    }


}
