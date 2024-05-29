using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] List<GameObject> objtype;
    [SerializeField] GameObject objplayer1_1;
    [SerializeField] GameObject objplayer1_2;
    [SerializeField] GameObject objplayer2_1;
    [SerializeField] GameObject objplayer2_2;
    public static Gamemanager Instance;

    public int Gameplayertype = 0;//1은 1번 차례일때 2는 2번 차례일때



    private Player player;
    private Numberroom numberroom;


    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public Numberroom Numberroom
    {
        get { return numberroom; }
        set { numberroom = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public bool playertouch;//클릭 했을때 on으로 전환 클릭이 끝나면 off로 전환

    void Start()
    {
        
    }

    void Update()
    {
        Onclickplayer();

        testcode();

        playertypechoice();
    }

    private void Onclickplayer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Gameplayertype == 1)
            {
                //GameObject playerfind = GameObject.Find("Player1");
                //Player player = playerfind.GetComponent<Player>();
                //player.playerchoice = true;
                //Debug.Log(playerfind);
                //Debug.Log(player);

                Player player = objplayer1_1.GetComponent<Player>();
                Player player2 = objplayer1_2.GetComponent<Player>();
                player.playerchoice = true;
                //player.checkobj.SetActive(true);
                //player.playertype1 = true;
                player2.playerchoice = true;
                //player2.checkobj.SetActive(true);
                //player2.playertype2 = true;
            }
            else if(Gameplayertype == 2)
            {
                //GameObject playerfind = GameObject.Find("Player2");
                //Player player = playerfind.GetComponent<Player>();
                //player.playerchoice = true;

                Player player = objplayer2_1.GetComponent<Player>();
                Player player2 = objplayer2_2.GetComponent<Player>();
                player.playerchoice = true;
                player2.playerchoice = true;
            }

            #region
            //Player player = gameObject.GetComponent<Player>();

            //GameObject playerfind = GameObject.Find("Player1");
            //Debug.Log(playerfind);
            //Player player = playerfind.GetComponent<Player>();
            //player.playerchoice = true;
            //Debug.Log("작동");

            //GameObject playerfind = GameObject.FindGameObjectWithTag("player");
            //Player player = playerfind.GetComponent<Player>();
            //player.playerchoice = true;
            //Debug.Log(playerfind);
            #endregion
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

    private void playertypechoice()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo);

    }
}
