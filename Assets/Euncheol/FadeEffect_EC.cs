using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect_EC : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color;

        if(color.a < 1)
        {
            color.a += Time.deltaTime;
        }

        image.color = color;
        
    }
}
