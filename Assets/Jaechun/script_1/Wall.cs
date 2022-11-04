using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private SpriteRenderer spr;

    public float myAlpha;

    private BoxCollider2D col;

    public int myArrow;     // 0-상, 1-우, 2-하

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();
        StartCoroutine(FadeWall());
    }

    // Update is called once per frame
    void Update()
    {
        if(myAlpha >= 255.0f)
        {
            col.enabled = true;
            myAlpha = 200.0f;   // 후에 애니메이션 출력으로 변경
        }
    }

    IEnumerator FadeWall()
    {
        // 벽 알파값 조정 함수
        float myAlpha = spr.color.a;
        while(myAlpha <= 255.0f)
        {
            yield return new WaitForSeconds(0.01f);
            myAlpha += 0.01f;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, myAlpha);
        }
    }

}
