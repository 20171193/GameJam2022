using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    // Wall
    public GameObject[] spawnWall;      // 생성할 벽
    public GameObject[] destroyWall;    // 제거할 벽
    public Vector2 spawnWallPos;        // 스폰 할 벽 좌표
    public Vector2 curWallPos;          // 현재 벽 좌표

    public int wallinterval;    // 벽 사이 간격

    public Sprite[] wallSpr;    // 벽 이미지 (상, 우, 하, 집)

    private int rand;   // 벽이 가르키는 방향 랜덤 값

    // Player
    public GameObject Player;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnWallPos = new Vector2(-10, 0);
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
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x+5.0f, destroyWall[1].transform.position.y+1.0f);
                    Debug.Log("다음 스폰위치 - 상");
                    break;
                }
            case 1: // 다음 스폰위치 - 중
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + 5.0f, destroyWall[1].transform.position.y);
                    Debug.Log("다음 스폰위치 - 중");
                    break;
                }
            case 2: // 다음 스폰위치 - 하
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + 5.0f, destroyWall[1].transform.position.y + -1.0f);
                    Debug.Log("다음 스폰위치 - 하");
                    break;
                }
        }
    }

    public bool CheckInPlayer()
    {
        return false;
    }

    public void SpawnWall()
    {
        // 기존에 생성한 벽이 있다면 삭제
        if (destroyWall[0])
        {
            Destroy(destroyWall[0]);
            Destroy(destroyWall[1]);

            System.Array.Clear(destroyWall, 0, destroyWall.Length);

            Debug.Log("삭제");
        }
        rand = Random.Range(0, 3);

        destroyWall[0] = Instantiate(spawnWall[0], spawnWallPos, Quaternion.identity);
        destroyWall[1] = Instantiate(spawnWall[1], spawnWallPos, Quaternion.identity);

        destroyWall[1].GetComponent<SpriteRenderer>().sprite = wallSpr[rand];
        destroyWall[1].GetComponent<Wall>().myArrow = rand;

        UpdateInfo();
        Debug.Log("생성");
    }
}
