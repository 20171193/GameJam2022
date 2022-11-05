using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor_E : MonoBehaviour
{
    [SerializeField] GameObject main_Background; //ĳ���Ͱ� ��ġ�ϴ� ���
    [SerializeField] GameObject[] other_Background;  //���� ����� �������� �ѷ��δ� ���

    private int upCheck = 0;  
    private int downCheck = 0;

    public float rightTileToTile; //������ Ÿ�� ����
    public float updownTileToTile; //���Ʒ� Ÿ�� ����

    private float bgWidth;   //��� ��������Ʈ�� �ʺ�
    private float bgHeight;  //��� ��������Ʈ�� ���̰�

    void Start()
    {
        main_Background.transform.position = new Vector3(0, 0, 0);

        bgWidth = main_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        bgHeight = main_Background.GetComponent<SpriteRenderer>().bounds.size.y;

        OtherBackgroundSetting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector2(transform.position.x + rightTileToTile, transform.position.y);
            MainBackgroundForward();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upCheck++;
            downCheck--;

            transform.position = new Vector2(transform.position.x + rightTileToTile, transform.position.y + updownTileToTile);
            MainBackgroundUp();
            MainBackgroundForward();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downCheck++;
            upCheck--;

            transform.position = new Vector2(transform.position.x + rightTileToTile, transform.position.y - updownTileToTile);
            Debug.Log(Mathf.Abs(transform.position.y) % 21.3);
            MainBackgroundDown();
            MainBackgroundForward();
        }
    }

    public void MainBackgroundForward()
    {
        if (transform.position.x % bgWidth < rightTileToTile)
        {
            main_Background.transform.position = new Vector3(main_Background.transform.position.x + bgWidth, main_Background.transform.position.y, 0);
            OtherBackgroundSetting();
        }
    }

    public void MainBackgroundUp()
    {
        if (upCheck >= (bgHeight/updownTileToTile)-1)
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
        if (downCheck >= (bgHeight / updownTileToTile)-1)
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
