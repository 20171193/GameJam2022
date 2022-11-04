using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public Vector2 nextWall;

    public Vector2 curWall;
   
    public GameObject Player;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        nextWall = new Vector2(Player.GetComponent<CharacterMovement_1>().curWall.x + 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInfo()
    {
        nextWall.x += 2;
    }
    //public bool Arrived()
    //{
    //    if (Player.transform.position.x == nextWall.x)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
