using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    // Wall
    [Header("spawnWall ������ ������ �Ҵ�-[0] : back, [1] : front")]
    public GameObject[] spawnWall;      // ������ ��
    
    public GameObject[] destroyWall;    // ������ ��

    public GameObject[] startWall;      // �÷��̾ ������ ������ ��

    public Vector2 spawnWallPos;        // ���� �� �� ��ǥ
    public Vector2 curWallPos;          // ���� �� ��ǥ


    public int wallinterval_x;    // �� ���� ����_x
    public int wallinterval_y;    // �� ���� ����_y

    [SerializeField]
    private int spawnCount_house;   // house �� ���� ����

    private int spawnCount;

    [Header("�� ��������Ʈ �Ҵ�")]
    public Sprite[] wallSpr;    // �� �̹��� (��, ��, ��, ��)

    private int rand;   // ���� ����Ű�� ���� ���� ��

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

        // ���� - �׽�Ʈ��
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
        // ���� ������ ����Ű�� �ִ� ����
        int dir = destroyWall[1].GetComponent<Wall>().myArrow;

        switch (dir)
        {
            case 0: // ���� ������ġ - ��
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x+ wallinterval_x, destroyWall[1].transform.position.y+1.0f);
                    Debug.Log("���� ������ġ - ��");
                    break;
                }
            case 1: // ���� ������ġ - ��
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + wallinterval_x, destroyWall[1].transform.position.y);
                    Debug.Log("���� ������ġ - ��");
                    break;
                }
            case 2: // ���� ������ġ - ��
                {
                    spawnWallPos = new Vector2(destroyWall[1].transform.position.x + wallinterval_x, destroyWall[1].transform.position.y -1.0f);
                    //Debug.Log("���� ������ġ - ��");
                    break;
                }
        }
    }

    public void CheckInPlayer()
    {
        // �÷��̾ �Է¿� ������ ���
        SpawnWall();
    }

    public void SpawnWall()
    { 
        // ������ ������ ���� �ִٸ� ����
        if (destroyWall[0])
        {
            Destroy(destroyWall[0]);
            Destroy(destroyWall[1]);

            System.Array.Clear(destroyWall, 0, destroyWall.Length);

            //Debug.Log("����");
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
