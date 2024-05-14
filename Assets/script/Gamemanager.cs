using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public static Gamemanager Instance;


    //private Player player;

    //public Player Player
    //{
    //    get { return player; }
    //    set { player = value; }
    //}

    //public bool playertouch;//클릭 했을때 on으로 전환 클릭이 끝나면 off로 전환

    void Start()
    {
        
    }

    void Update()
    {
        Onclickplayer();

        testcode();
    }

    private void Onclickplayer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            #region
            //Player player = gameObject.GetComponent<Player>();

            GameObject playerfind = GameObject.Find("Player1");
            //Debug.Log(playerfind);
            Player player = playerfind.GetComponent<Player>();
            player.playerchoice = true;
            //Debug.Log("작동");
            #endregion

            //GameObject playerfind = GameObject.FindGameObjectWithTag("player");
            //Player player = playerfind.GetComponent<Player>();
            //player.playerchoice = true;
            //Debug.Log(playerfind);
        }
    }

    private void testcode()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject tags = GameObject.FindGameObjectWithTag("player");
            Player player = tags.GetComponent<Player>();
            player.tests = true;
            Debug.Log("작동");
        }
    }
}
