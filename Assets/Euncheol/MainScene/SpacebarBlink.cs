using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpacebarBlink : MonoBehaviour
{

    float time;
    bool spaceOn = false;
    public float spacebarOnSize = 0.5f;
    public float spacebarResetSpeed = 0.2f;

    private Color color;

    public Image image;

    void Update()
    {
        time += Time.deltaTime;

        if (!spaceOn)
        {
            if (time < 0.5f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - time);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time);
                if (time > 1f)
                {
                    time = 0;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            time = 0;
            spaceOn = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            transform.localScale = Vector3.one * spacebarOnSize;
        }


        if (spaceOn)
        {
            color = image.color;
            if (time > spacebarResetSpeed)
            {
                time = 0;
                transform.localScale = Vector3.one;
            }

            if (color.a < 1)
            {
                color.a += Time.deltaTime;
            }
            else
                SceneManager.LoadScene("Scene_EC");

            image.color = color;
        }

    }
}

