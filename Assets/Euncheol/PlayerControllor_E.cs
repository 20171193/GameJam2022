using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor_E : MonoBehaviour
{
    [SerializeField] GameObject f_gameObject;
    /*[SerializeField] GameObject s_gameObject;
    [SerializeField] GameObject t_gameObject;
    [SerializeField] GameObject u_gameObject;
    [SerializeField] GameObject d_gameObject;*/
    [SerializeField] GameObject[] other_gameObject;

    //private int fowardCheck = 0;
    //private int updownCheck = 0;

    //private bool rightCheck = false;
    public bool upRight;
    public bool downRight;
    public bool right;
    public bool up;
    public bool down;

    // Start is called before the first frame update
    void Start()
    {
        //other_gameObject = new GameObject[];
        f_gameObject.transform.position = new Vector3(0, 0, 0);
        OtherSetting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            if (right)
            {
                transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                ForwardCheck();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up");

            if (up)
            {
                transform.position = new Vector2(transform.position.x + 2f, transform.position.y + 1f);
                UpCheck();
                ForwardCheck();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("down");
            if (down)
            {
                transform.position = new Vector2(transform.position.x + 2f, transform.position.y - 1f);
                DownCheck();
                ForwardCheck();
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Right"))
        {
            right = true;
        }
        else
        {
            right = false;
        }

        if (other.CompareTag("Up"))
        {
            up = true;
        }
        else
        {
            up = false;
        }

        if (other.CompareTag("Down"))
        {
            down = true;
        }
        else
        {
            down = false;
        }
    }

    public void ForwardCheck()
    {
        if (transform.position.x % 40 < 2)
        {
            f_gameObject.transform.position = new Vector3(f_gameObject.transform.position.x + 40f, f_gameObject.transform.position.y, 0);
            OtherSetting();
        }
    }
    public void UpCheck()
    {
        if (transform.position.y % 21.3 < 1)
        {
            f_gameObject.transform.position = new Vector3(f_gameObject.transform.position.x, f_gameObject.transform.position.y + 21.3f, 0);
            OtherSetting();
        }
    }
    public void DownCheck()
    {
        if (transform.position.y % 21.3 < 1)
        {
            f_gameObject.transform.position = new Vector3(f_gameObject.transform.position.x, f_gameObject.transform.position.y - 21.3f, 0);
            OtherSetting();
        } 
    }

    public void OtherSetting()
    {
        other_gameObject[0].transform.position = new Vector3(f_gameObject.transform.position.x - 40f, f_gameObject.transform.position.y + 21.3f, 0);
        other_gameObject[1].transform.position = new Vector3(f_gameObject.transform.position.x, f_gameObject.transform.position.y + 21.3f, 0);
        other_gameObject[2].transform.position = new Vector3(f_gameObject.transform.position.x + 40f, f_gameObject.transform.position.y + 21.3f, 0);
        other_gameObject[3].transform.position = new Vector3(f_gameObject.transform.position.x - 40f, f_gameObject.transform.position.y, 0);
        other_gameObject[4].transform.position = new Vector3(f_gameObject.transform.position.x + 40f, f_gameObject.transform.position.y, 0);
        other_gameObject[5].transform.position = new Vector3(f_gameObject.transform.position.x - 40f, f_gameObject.transform.position.y - 21.3f, 0);
        other_gameObject[6].transform.position = new Vector3(f_gameObject.transform.position.x, f_gameObject.transform.position.y - 21.3f, 0);
        other_gameObject[7].transform.position = new Vector3(f_gameObject.transform.position.x + 40f, f_gameObject.transform.position.y - 21.3f, 0);
    }
}
