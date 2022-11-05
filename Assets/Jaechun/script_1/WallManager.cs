using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    // Wall
    [Header("spawnWall 변수에 프리팹 할당-[0] : back, [1] : front")]
    public GameObject[] spawnWall;      // 생성할 벽
    
    public GameObject[] destroyWall;    // 제거할 벽

    public GameObject[] startWall;      // 플레이어가 도약을 시작할 벽

    public Vector2 spawnWallPos;        // 스폰 할 벽 좌표
    public Vector2 curWallPos;          // 현재 벽 좌표


    public int wallinterval_x;    // 벽 사이 간격_x
    public int wallinterval_y;    // 벽 사이 간격_y

    [SerializeField]
    private int spawnCount_house;   // house 벽 생성 조건

    private int spawnCount;

    [Header("벽 스프라이트 할당")]
    public Sprite[] wallSpr;    // 벽 이미지 (상, 우, 하, 집)

    private int rand;   // 벽이 가르키는 방향 랜덤 값

    public GameObject houseWall;

    // Player
    public GameObject Player;

    private void Awake()
    {
        
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateInfo()
    {
        // 현재 구름이 가르키고 있는 방향
        int dir = destroyWall[1].GetComponent<Wall>().myArrow;

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
        // 플레이어가 입력에 성공한 경우
        SpawnWall();
    }

    public void SpawnWall()
    { 
        // 기존에 생성한 벽이 있다면 삭제
        if (destroyWall[0])
        {
            Destroy(destroyWall[0]);
            Destroy(destroyWall[1]);

            System.Array.Clear(destroyWall, 0, destroyWall.Length);

            //Debug.Log("삭제");
        }
        //if (spawnCount == spawnCount_house)
        //{
        //    spawnCount = 0;
        //    destroyWall[0] = Instantiate(spawnWall[0], spawnWallPos, Quaternion.identity);
        //    destroyWall[1] = Instantiate(spawnWall[1], spawnWallPos, Quaternion.identity);

        //    destroyWall[1].GetComponent<SpriteRenderer>().sprite = wallSpr[rand];
        //    destroyWall[1].GetComponent<Wall>().myArrow = rand;
        //}
        //else 
        //{
            rand = Random.Range(0, 3);

            destroyWall[0] = Instantiate(spawnWall[0], spawnWallPos, Quaternion.identity);
            destroyWall[1] = Instantiate(spawnWall[1], spawnWallPos, Quaternion.identity);

            destroyWall[1].GetComponent<SpriteRenderer>().sprite = wallSpr[rand];
            destroyWall[1].GetComponent<Wall>().myArrow = rand;

            spawnCount++;

            UpdateInfo();
        //}
    }

}
