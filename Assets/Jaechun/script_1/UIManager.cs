using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public int score_val = 0;

    public GameObject player;

    public Image scoreImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RenderScoreImage(Vector3 pos)
    {
        scoreImage.transform.position = pos;
        scoreImage.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        scoreImage.GetComponent<Animator>().Play("lsScore", -1, 0.0f);
    }

    public void RenderScoreText(int val)
    {
        score_val += val;
        scoreText.text = score_val.ToString();
        Debug.Log(score_val.ToString());
    }
}
