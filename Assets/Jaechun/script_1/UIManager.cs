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

    public GameObject HouseBGI;

    public GameObject[] arrowimg;

    public Sprite[] arrowSpr;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RenderHouseArrow(int first, int second, int third, int forth)
    {
        HouseBGI.SetActive(true);

        arrowimg[0].GetComponent<Image>().sprite = arrowSpr[first];
        arrowimg[0].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255);
        arrowimg[1].GetComponent<Image>().sprite = arrowSpr[second];
        arrowimg[1].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255);
        arrowimg[2].GetComponent<Image>().sprite = arrowSpr[third];
        arrowimg[2].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255);
        arrowimg[3].GetComponent<Image>().sprite = arrowSpr[forth];
        arrowimg[3].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255);
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
