using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public SpriteRenderer spr;

    public float myAlha;   

    private BoxCollider2D col;

    public int myArrow;         // 벽이 가진 방향키 (들어오는 방향 + 나갈 방향)

    public int[] randomArrow;   // 플레이어가 입력할 랜덤 방향키

    public UIManager uimanager;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();

        player = GameObject.FindWithTag("Player");
        uimanager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();  

        randomArrow = new int[3];

        // 랜덤 방향키 세팅
        for(int i=0; i<3; i++)
        {
            int rand = Random.Range(0, 3);
            randomArrow[i] = rand;
            Debug.Log(i);
            Debug.Log("번쨰");
            Debug.Log(rand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!player.GetComponent<CharacterMovement_1>().isDie)
            {
                player.GetComponent<CharacterMovement_1>().myEvent = CharacterMovement_1.MyEventType.House;

                player.GetComponent<Animator>().SetBool("isJump", false);

                player.GetComponent<CharacterMovement_1>().jumpable = true;
                player.GetComponent<CharacterMovement_1>().jumpArrow = myArrow;
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

            if (transform.position.y + 2.7 > player.transform.position.y)   // 벽에서 떨어지는 상황
            {
                // 사망 이벤트 실행
                // 후에 애니메이션 출력으로 대체
                player.GetComponent<CharacterMovement_1>().DieEvent();
                player.GetComponent<CharacterMovement_1>().jumpable = false;
            }
        }
    }

}
