using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    // Wall
    [Header("spawnWall ������ ������ �Ҵ�-[0] : back, [1] : front")]
    public GameObject[] spawnWall;      // ������ ��
    
    public GameObject[] destroyWall;    // ������ ��

    public GameObject upEffect;
    public GameObject downEffect;
    public GameObject rightEffect;

    public GameObject upCloud;
    public GameObject downCloud;    
    public GameObject rightCloud;

    public GameObject effectWall;       // ���� ����Ʈ ���� ��

    public GameObject[] startWall;      // �÷��̾ ������ ������ ��

    public Vector2 spawnWallPos;        // ���� �� �� ��ǥ
    public Vector2 curWallPos;          // ���� �� ��ǥ


    public int wallinterval_x;    // �� ���� ����_x
    public int wallinterval_y;    // �� ���� ����_y

    [SerializeField]
    private int spawnCount_house;   // house �� ���� ����

    public int spawnCount;

    [Header("�� ��������Ʈ �Ҵ�")]
    public Sprite[] wallSpr;    // �� �̹��� (��, ��, ��, ��)

    private int rand;   // ���� ����Ű�� ���� ���� ��

    // Player
    public GameObject Player;

    [Header("���ھ� �Ŵ��� �Ҵ�")]
    // ���ھ� ��°���
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

        // ���� - �׽�Ʈ��
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
        UIManager.GetComponent<UIManager>().RenderScoreText(stage_score);
        //Debug.Log(spawnCount);
        // �÷��̾ �Է¿� ������ ���
        SpawnWall();
    }

    public void SpawnWall()
    {
        rand = Random.Range(0, 3);

        Vector2 arrowPos = new Vector2(spawnWallPos.x + 0.2f, spawnWallPos.y + 1.3f);

        if (spawnCount >= spawnCount_house)
        {
            // �Ͽ콺 ����
            spawnCount = 0;
            if (destroyWall[0])
            {
                Instantiate(effectWall, destroyWall[1].transform.position, Quaternion.identity);
                //UI �̺�Ʈ ȣ��
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

            destroyWall[1].GetComponent<House>().myArrow = rand;   // �׽�Ʈ �� - �Ŀ� rand �� ���

            UpdateInfo(destroyWall[1].GetComponent<House>().myArrow);
        }
        else
        { // ������ ������ ���� �ִٸ� ����
            if (destroyWall[0])
            {
                Destroy(destroyWall[0]);

                // ����Ʈ ���� ������Ʈ ����
                Instantiate(effectWall, destroyWall[1].transform.position, Quaternion.identity);

                // UI �̺�Ʈ ȣ��
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
