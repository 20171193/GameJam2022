using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    // Wall
    public GameObject[] spawnWall;      // ������ ��
    public GameObject[] destroyWall;    // ������ ��
    public Vector2 spawnWallPos;        // ���� �� �� ��ǥ
    public Vector2 curWallPos;          // ���� �� ��ǥ

    public int wallinterval;    // �� ���� ����

    public Sprite[] wallSpr;    // �� �̹��� (��, ��, ��, ��)

    private int rand;   // ���� ����Ű�� ���� ���� ��

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
        // ���� ������ ����Ű�� �ִ� ����
        int dir = destroyWall[1].GetComponent<Wall>().myArrow;

        switch (dir)
        {
            case 0: // ���� ������ġ - ��
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x+5.0f, destroyWall[1].transform.position.y+1.0f);
                    Debug.Log("���� ������ġ - ��");
                    break;
                }
            case 1: // ���� ������ġ - ��
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + 5.0f, destroyWall[1].transform.position.y);
                    Debug.Log("���� ������ġ - ��");
                    break;
                }
            case 2: // ���� ������ġ - ��
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + 5.0f, destroyWall[1].transform.position.y + -1.0f);
                    Debug.Log("���� ������ġ - ��");
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
        // ������ ������ ���� �ִٸ� ����
        if (destroyWall[0])
        {
            Destroy(destroyWall[0]);
            Destroy(destroyWall[1]);

            System.Array.Clear(destroyWall, 0, destroyWall.Length);

            Debug.Log("����");
        }
        rand = Random.Range(0, 3);

        destroyWall[0] = Instantiate(spawnWall[0], spawnWallPos, Quaternion.identity);
        destroyWall[1] = Instantiate(spawnWall[1], spawnWallPos, Quaternion.identity);

        destroyWall[1].GetComponent<SpriteRenderer>().sprite = wallSpr[rand];
        destroyWall[1].GetComponent<Wall>().myArrow = rand;

        UpdateInfo();
        Debug.Log("����");
    }
}
