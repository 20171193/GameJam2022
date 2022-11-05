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

    public AudioSource audioSource;

    private Color color;

    public Image image;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            audioSource.Play();
        }


        if (spaceOn)
        {
            if (time > spacebarResetSpeed)
            {
                time = 0;
                transform.localScale = Vector3.one;
            }
            color = image.color;

            if (color.a < 2)
            {
                color.a += Time.deltaTime/2;
            }
            else
                SceneManager.LoadScene("MainTestScene");

            image.color = color;
        }

    }
}

