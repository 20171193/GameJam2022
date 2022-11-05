using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{

    public GameObject menuTool;
    public GameObject menuTool_Tile; //////���â Ÿ��Ʋ�� ��ư
    public GameObject menuTool_Replay; //////���â Ÿ��Ʋ�� ��ư
    public GameObject menuTool_Finish; //////���â Ÿ��Ʋ�� ��ư

    // Start is called before the first frame update
    void Start()
    {
         menuTool.SetActive(true);
        Invoke("TitleOn", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Invoke("ButtonOn", 1f);
    }

    public void ButtonOn()
    {
        menuTool_Finish.gameObject.SetActive(true);
        menuTool_Replay.gameObject.SetActive(true);
    }
}
