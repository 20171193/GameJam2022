using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    // Wall
    [Header("spawnWall 변수에 프리팹 할당-[0] : back, [1] : front")]
    public GameObject[] spawnWall;      // 생성할 벽
    
    public GameObject[] destroyWall;    // 제거할 벽

    public GameObject upEffect;
    public GameObject downEffect;
    public GameObject rightEffect;

    public GameObject upCloud;
    public GameObject downCloud;    
    public GameObject rightCloud;

    public GameObject effectWall;       // 제거 이펙트 전용 벽

    public GameObject[] startWall;      // 플레이어가 도약을 시작할 벽

    public Vector2 spawnWallPos;        // 스폰 할 벽 좌표
    public Vector2 curWallPos;          // 현재 벽 좌표


    public int wallinterval_x;    // 벽 사이 간격_x
    public int wallinterval_y;    // 벽 사이 간격_y

    [SerializeField]
    private int spawnCount_house;   // house 벽 생성 조건

    public int spawnCount;

    [Header("벽 스프라이트 할당")]
    public Sprite[] wallSpr;    // 벽 이미지 (상, 우, 하, 집)

    private int rand;   // 벽이 가르키는 방향 랜덤 값

    // Player
    public GameObject Player;

    [Header("스코어 매니져 할당")]
    // 스코어 출력관련
    public GameObject UIManager;

    public int stage_score;

    private void Awake()
    {
        spawnCount = 0;
        stage_score = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);
        destroyWall[0] = startWall[0];
        destroyWall[1] = startWall[1];

        // 수정 - 테스트용
        spawnWallPos = new Vector2(startWall[1].transform.position.x+5.0f, startWall[1].transform.position.y);
        Player.GetComponent<CharacterMovement_1>().cur_xpos = startWall[1].transform.position.x;
        Player.GetComponent<CharacterMovement_1>().cur_ypos = startWall[1].transform.position.y+2.9f;
        //Time.timeScale = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateInfo(int dir)
    {
        switch (dir)
        { 
            case 0: // 다음 스폰위치 - 상
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x+ wallinterval_x, destroyWall[1].transform.position.y+1.0f);
                    Debug.Log("다음 스폰위치 - 상");
                    break;
                }
            case 1: // 다음 스폰위치 - 중
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + wallinterval_x, destroyWall[1].transform.position.y);
                    Debug.Log("다음 스폰위치 - 중");
                    break;
                }
            case 2: // 다음 스폰위치 - 하
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + wallinterval_x, destroyWall[1].transform.position.y -1.0f);
                    //Debug.Log("다음 스폰위치 - 하");
                    break;
                }
        }
    }

    public void CheckInPlayer()
    {
        UIManager.GetComponent<UIManager>().RenderScoreText(stage_score);
        //Debug.Log(spawnCount);
        // 플레이어가 입력에 성공한 경우
        SpawnWall();
    }

    public void SpawnWall()
    {
        rand = Random.Range(0, 3);

        Vector2 arrowPos = new Vector2(spawnWallPos.x + 0.2f, spawnWallPos.y + 1.3f);

        if (spawnCount >= spawnCount_house)
        {
            // 하우스 스폰
            spawnCount = 0;
            if (destroyWall[0])
            {
                Instantiate(effectWall, destroyWall[1].transform.position, Quaternion.identity);
                //UI 이벤트 호출
                UIManager.GetComponent<UIManager>().RenderScoreImage(new Vector3(destroyWall[1].transform.position.x, destroyWall[1].transform.position.y, 0.0f));

                Destroy(destroyWall[1]);

                System.Array.Clear(destroyWall, 0, destroyWall.Length);
            }

            destroyWall[0] = Instantiate(spawnWall[2], spawnWallPos, Quaternion.identity);
            destroyWall[1] = Instantiate(spawnWall[3], spawnWallPos, Quaternion.identity);
            
            if (rand == 0)
            {
                Instantiate(upEffect, arrowPos, Quaternion.identity);
            }
            if (rand == 1)
            {
                Instantiate(rightEffect, arrowPos, Quaternion.identity);
            }
            if (rand == 2)
            {
                Instantiate(downEffect, arrowPos, Quaternion.identity);
            }

            destroyWall[1].GetComponent<House>().myArrow = rand;   // 테스트 용 - 후에 rand 값 사용

            UpdateInfo(destroyWall[1].GetComponent<House>().myArrow);
        }
        else
        { // 기존에 생성한 벽이 있다면 삭제
            if (destroyWall[0])
            {
                Destroy(destroyWall[0]);

                // 이펙트 전용 오브젝트 생성
                Instantiate(effectWall, destroyWall[1].transform.position, Quaternion.identity);

                // UI 이벤트 호출
                UIManager.GetComponent<UIManager>().RenderScoreImage(new Vector3(destroyWall[1].transform.position.x, destroyWall[1].transform.position.y, 0.0f));

                Destroy(destroyWall[1]);

                System.Array.Clear(destroyWall, 0, destroyWall.Length);

            }
            if(rand == 0)
            {
                Instantiate(upEffect, arrowPos, Quaternion.identity);
                Instantiate(upCloud, spawnWallPos, Quaternion.identity);
            }
            if (rand == 1)
            {
                Instantiate(rightEffect, arrowPos, Quaternion.identity);
                Instantiate(rightCloud, spawnWallPos, Quaternion.identity);
            }
            if (rand == 2)
            {
                Instantiate(downEffect, arrowPos, Quaternion.identity);
                Instantiate(downCloud, spawnWallPos, Quaternion.identity);
            }

            destroyWall[0] = Instantiate(spawnWall[0], spawnWallPos, Quaternion.identity);
            destroyWall[1] = Instantiate(spawnWall[1], spawnWallPos, Quaternion.identity);

            destroyWall[1].GetComponent<SpriteRenderer>().sprite = wallSpr[rand];
            destroyWall[1].GetComponent<Wall>().myArrow = rand;

            spawnCount++;

            UpdateInfo(destroyWall[1].GetComponent<Wall>().myArrow);
        }
    }
}
