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

    //public bool playertouch;//Ŭ�� ������ on���� ��ȯ Ŭ���� ������ off�� ��ȯ

    void Start()
    {
        
    }

    void Update()
    {
        Onclickplayer();
    }

    private void Onclickplayer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Player player = gameObject.GetComponent<Player>();

            GameObject playerfind = GameObject.Find("Player1");
            //Debug.Log(playerfind);
            Player player = playerfind.GetComponent<Player>();
            player.tests = true;
            //Debug.Log("�۵�");
        }
    }
}
