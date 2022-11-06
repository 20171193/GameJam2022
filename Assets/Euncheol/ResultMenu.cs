using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{

    public GameObject menuTool;
    public GameObject menuTool_Tile; //////결과창 타이틀과 버튼
    public GameObject menuTool_Replay; //////결과창 타이틀과 버튼
    public GameObject menuTool_Finish; //////결과창 타이틀과 버튼
    public GameObject UIManager;       //////최종점수가져오기

    public Text scoreText;  ///////점수 넣을곳

    private int finalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
         menuTool.SetActive(true);
        Invoke("TitleOn", 1f);
    }

    public void Restart()
    {
        Debug.Log("restart");
        SceneManager.LoadScene("MainTestScene");
    }

    public void Finish()
    {
        Debug.Log("finish");
        Application.Quit();
    }

    public void TitleOn()
    {
        menuTool_Tile.gameObject.SetActive(true);
        Invoke("RenderScoreText", 0.5f);
    }

    public void RenderScoreText()
    {
        finalScore = UIManager.GetComponent<UIManager>().score_val;
        scoreText.text = finalScore.ToString();
        scoreText.gameObject.SetActive(true);
        Invoke("ButtonOn", 0.5f);
    }

    public void ButtonOn()
    {
        menuTool_Finish.gameObject.SetActive(true);
        menuTool_Replay.gameObject.SetActive(true);
    }
}
