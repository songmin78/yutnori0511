using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    Animation anim;
    Animator animator;
    [SerializeField] Image startcheck;

    [SerializeField] List<GameObject> objtype;
    [SerializeField] GameObject objplayer1_1;
    [SerializeField] GameObject objplayer1_2;
    [SerializeField] GameObject objplayer2_1;
    [SerializeField] GameObject objplayer2_2;
    public static Gamemanager Instance;

    public int Gameplayertype = 0;//1은 1번 차례일때 2는 2번 차례일때
    //시작할때 누가 먼저 윷을 던지는지 확인하는 부분
    float changecheck = 0;
    float changetimer = 0.1f;//교체될때 텀;
    bool starttype;
    float startturnyut = 5;
    float Maxstartturnyut;
    //끝
    //윷을 던지는 부분을 관리 ThrowYut부분
    public bool throwyutbutton = false;//버튼을 나오게 관리하는것 및
    [SerializeField, Tooltip("윷 타이머 작동 부분")] GameObject Playtimemanager;
    [SerializeField, Tooltip("윷 타이머가 줄어드는 막대기")] GameObject Yuttimer;
    [SerializeField] GameObject Yutbox;
    [SerializeField] Button throwbutton;
    //끝
    //캐릭터 선택 및 이동 부분 SelectCharacter부분
    [SerializeField] GameObject playerbox;//플레이어블 캐릭터들을 보이게 해주는 오브젝트
    //끝


    private Player player;
    private Numberroom numberroom;

    public enum eRule
    {

        Preferencetime,//먼저 윷을 던지는 우선권 시간
        ThrowYut,
        SelectCharacter,//캐릭터를 선택
        MoveCharacter,
        CheckRule,
        TurnOver,
    }
    private eRule curState = eRule.Preferencetime;

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

        Animation anim = GetComponent<Animation>();
        Animator animator = GetComponent<Animator>();
        Maxstartturnyut = startturnyut;
    }

    //public bool playertouch;//클릭 했을때 on으로 전환 클릭이 끝나면 off로 전환

    void Start()
    {
        startcheck.gameObject.SetActive(true);
        changecheck = 0;
    }

    void Update()
    {
        Onclickplayer();

        //testcode();
        if(curState == eRule.Preferencetime)
        {
            startturn();
            startchageturn();
        }
        else if (curState == eRule.ThrowYut)
        {
            Throwtime();
            //Playtimer playtime = 
        }
        else if (curState == eRule.SelectCharacter)
        {
            playertypechoice();
        }
    }

    private void Onclickplayer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Gameplayertype == 1)
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
            else if (Gameplayertype == 2)
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


    //private void testcode()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        GameObject tags = GameObject.FindGameObjectWithTag("player");
    //        Player player = tags.GetComponent<Player>();
    //        player.tests = true;
    //        Debug.Log("작동");
    //    }
    //}

    private void playertypechoice()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.transform != null)
            {
                Player selPlayer = rayHit.transform.GetComponent<Player>();
                selPlayer.selectedcheck = true;
            }
        }
    }

    private bool isFindData(out int value)
    {
        value = 10;
        return true;
    }

    private void startturn()//처음에 누가 먼저 시작하는지 알려주는 코드
    {
        animator = startcheck.gameObject.GetComponentInChildren<Animator>();
        Maxstartturnyut -= Time.deltaTime;
        if(Maxstartturnyut < 2)//3초가 지난 경우
        {
            if(starttype == false)
            {
                changecheck = Random.Range(0, 2);
                starttype = true;
            }
            else
            {
                animator.SetFloat("CharacterChange", changecheck);
            }

            if(Maxstartturnyut < 0)//0보다 작을 경우
            {
                Maxstartturnyut = startturnyut;
                starttype = false;
                startcheck.gameObject.SetActive(false);
                curState = eRule.ThrowYut;
                throwyutbutton = true;
            }

        }
        else//3초가 안 지난 경우
        {
            if (changecheck  == 0 && changetimer == 0.1f)//3초가 지난 경우
            {
                changecheck = 1;
                animator.SetFloat("CharacterChange", changecheck);
                
            }
            else if(changecheck == 1 && changetimer == 0.1f)
            {
                changecheck = 0;
                animator.SetFloat("CharacterChange", changecheck);
            }
        }
    }

    private void startchageturn()//순서를 정할때 0.1초마다 캐릭 터가 변경되도록 설정
    {
        if(Maxstartturnyut < 2)
        {
            return;
        }
        changetimer -= Time.deltaTime;
        if(changetimer < 0)
        {
            changetimer = 0.1f;
        }
    }

    /// <summary>
    /// 파랑이 플레이어블 핑크가 자동
    /// 파랑 == 1 // 핑크 ==0
    /// </summary>
    private void Throwtime()//윷을 던지는 부분을 관리하는 코드
    {
        if(throwyutbutton == true)
        {
            throwbutton.gameObject.SetActive(true);
            Playtimemanager.SetActive(true);
            Yuttimer.SetActive(true);
            Yutbox.SetActive(true);
            playerbox.SetActive(true);
        }
        else if(throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            curState = eRule.SelectCharacter;
        }
    }
}
