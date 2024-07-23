using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [Header("튜토리얼 부분(튜토리얼 스테이지에서만 쓰임)")]
    [SerializeField,Tooltip("튜토리얼 스테이지에 있을때 True로 쓰기")] bool tutorialStageCheck;
    bool onlyStory1 = true;//딱 한번만 들어 오도록 설정
    bool onlyStory2 = true;//딱 한번만 들어 오도록 설정
    public bool TutorialStageCheck
    {
        get
        {
            return tutorialStageCheck;
        }
    }

    [Header("일반게임")]
    [SerializeField] Button ClearButton;
    Animation anim;
    Animator animator;
    [SerializeField] Image startcheck;
    TMP_Text tmp;

    [SerializeField] List<GameObject> objblue;
    [SerializeField] List<GameObject> objred;

   public class cObjectWhereFootHold
    {
        public GameObject objPlayer;
        public Transform trsFootHold;
    }

    //플레이하는 말이 발판위에 있다면 리스트에 등록하여 어디있는지 출력하는 용도로 활용
    List<cObjectWhereFootHold> listObjectWhereFootHold = new List<cObjectWhereFootHold>();

    public static Gamemanager Instance;
    //[Header("말을 이동 할 위치를 보여주는 오브젝트 관리")]
    //[SerializeField] public GameObject movelocation1;
    //[SerializeField] public GameObject movelocation2;
    //[SerializeField] public GameObject movelocation3;
    [Header("기타")]
    //[SerializeField] TMP_Text LookYut;
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
    [Header("윷 타이머 부분")]
    [SerializeField, Tooltip("윷 타이머 작동 부분")] GameObject Playtimemanager;
    [SerializeField, Tooltip("윷 타이머가 줄어드는 막대기")] GameObject Yuttimer;
    [SerializeField, Tooltip("플레이어이동이 줄어드는 막대기")] GameObject PlayerTimer;
    [SerializeField] GameObject Yutbox;
    [SerializeField] Button throwbutton;
    //끝
    //캐릭터 선택 및 이동 부분 SelectCharacter부분
    [SerializeField] GameObject playerbox;//플레이어블 캐릭터들을 보이게 해주는 오브젝트
    [SerializeField] int teamtype = 0;//1은 블루팀 2는 레드팀

    //bool returncheck;//다시 되돌릴때 쓰이는 코드
    //끝
    //턴을 넘길때 쓰이는 부분
    [SerializeField] GameObject Yutstartbutton;
    //waittime 부분 잠깐 기다리는 코드 부분
    float timewait = 0.5f;
    float Maxtimewait;
    Vector3 StartPos;
    [Header("현재 업고있는 팀체크")]
    [SerializeField] bool CurryBlue;
    [SerializeField] bool CurryRed;
    bool BlueCurryCheck;//블루팀 2마리가 업혀있을때 true
    bool RedCurryCheck;//레드팀 2마리가 업혀있을때 true

    private Player player;
    private Footholdbox footholdbox;
    private Yutstartbutton yutstartbutton;
    private Playtimer playtimer;
    private SceneChange sceneChange;
    private TutorialStory tutorialStory;
    //결승점에 통과한 말을 알려주는 오브젝트
    GameObject Clearobj;
    float ClearNumber;

    bool BackCheck = false;

    bool TurnCycleCheck;//잡았다면 그 턴을 다시 실핼 할수 있게 도와주는 부분
    [Header("우승한 팀 관련 코드 부분")]
    [SerializeField] Canvas WinerTeamCanvas;
    //[SerializeField] Button AgainButton;//다시하기 버튼
    //[SerializeField] Button LobiButton;//로비로 돌아가는 버튼
    [SerializeField] TMP_Text WinerTeamString;
    bool WinerRed;
    bool WinerBlue;
    

    public enum eRule
    {

        Preferencetime,//먼저 윷을 던지는 우선권 시간
        ThrowYut,
        SelectCharacter,//캐릭터를 선택
        ReturnthrowYut,//다시 윷을 던지는 부분
        waittime,//잠깜 기다리는 부분
        RecycleTime,//다시 잡을 때 플레이어 터치 코드가 안 돌아가게 조치
        GameClear,//게임이 클리어 할때 작동되게 하는 부분
    }
    private eRule curState = eRule.Preferencetime;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }


    public Footholdbox Footholdbox
    {
        get { return footholdbox; }
        set { footholdbox = value; }
    }

    public Yutstartbutton Yutstartbuttons
    {
        get { return yutstartbutton; }
        set { yutstartbutton = value; }
    }

    public Playtimer Playtimer
    {
        get { return playtimer; }
        set { playtimer = value; }
    }

    public SceneChange SceneChange
    {
        get { return sceneChange; }
        set { sceneChange = value; }
    }

    public TutorialStory TutorialStory
    {
        get { return tutorialStory; }
        set { tutorialStory = value; }
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

        TMP_Text tmp = GetComponent<TMP_Text>();
        Animation anim = GetComponent<Animation>();
        Animator animator = GetComponent<Animator>();
        Maxstartturnyut = startturnyut;
        Maxtimewait = timewait;

        #region 게임 말이 나가기 버튼을 다루는 부분
        ClearButton.onClick.AddListener(() =>
        {
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.objPlayer == Clearobj);
            listObjectWhereFootHold.Remove(data);
            if (objblue.Find(x => x.gameObject == Clearobj) != null)
            {
                objblue.Remove(Clearobj);
            }
            else if (objred.Find(x => x.gameObject == Clearobj) != null)
            {
                objred.Remove(Clearobj);
            }
            Player.DesYutPlayer();
            //Clearobj.gameObject.transform.position = new Vector3(0, -10, 0);
            Footholdbox.positiondestory();
            Player.ManagerYutorderCheck(ClearNumber);
            Clearobj.gameObject.SetActive(false);
            ClearButton.gameObject.SetActive(false);

            if(objblue.Count  == 0)
            {
                Debug.Log("블루팀 클리어");
                WinerBlue = true;
                teamWinCheck();
                curState = eRule.GameClear;
                Playtimer.StayTurnTime();
            }
            else if(objred.Count == 0)
            {
                Debug.Log("레드팀 클리어");
                WinerRed = true;
                teamWinCheck();
                curState = eRule.GameClear;
                Playtimer.StayTurnTime();
            }

            Player.ExitTurnPass();
        });
        #endregion
    }

    //public bool playertouch;//클릭 했을때 on으로 전환 클릭이 끝나면 off로 전환

    void Start()
    {
        startcheck.gameObject.SetActive(true);
        changecheck = 0;
    }

    void Update()
    {
        testText();
        //Onclickplayer();

        //testcode();
        if (curState == eRule.Preferencetime)
        {
            startturn();
            startchageturn();
        }
        else if (curState == eRule.ThrowYut)
        {
            Throwtime();
        }
        else if (curState == eRule.SelectCharacter)
        {
            Yutbox.SetActive(false);
            playertypechoice();
            //positionobjcheck();
        }
        else if (curState == eRule.ReturnthrowYut)
        {
            changeturn();
        }
        else if (curState == eRule.waittime)
        {
            waitingtimer();
        }
        else if(curState == eRule.RecycleTime)
        {
            return;
        }
        else if(curState == eRule.GameClear)
        {

        }
    }

    #region
    //private void Onclickplayer()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        if (Gameplayertype == 1)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player1");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //player.playerchoice = true;
    //            //Debug.Log(playerfind);
    //            //Debug.Log(player);

    //            Player player = objplayer1_1.GetComponent<Player>();
    //            Player player2 = objplayer1_2.GetComponent<Player>();
    //            player.playerchoice = true;
    //            //player.checkobj.SetActive(true);
    //            //player.playertype1 = true;
    //            player2.playerchoice = true;
    //            //player2.checkobj.SetActive(true);
    //            //player2.playertype2 = true;
    //        }
    //        else if (Gameplayertype == 2)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player2");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //player.playerchoice = true;

    //            Player player = objplayer2_1.GetComponent<Player>();
    //            Player player2 = objplayer2_2.GetComponent<Player>();
    //            player.playerchoice = true;
    //            player2.playerchoice = true;
    //        }

    //        #region
    //        //Player player = gameObject.GetComponent<Player>();

    //        //GameObject playerfind = GameObject.Find("Player1");
    //        //Debug.Log(playerfind);
    //        //Player player = playerfind.GetComponent<Player>();
    //        //player.playerchoice = true;
    //        //Debug.Log("작동");

    //        //GameObject playerfind = GameObject.FindGameObjectWithTag("player");
    //        //Player player = playerfind.GetComponent<Player>();
    //        //player.playerchoice = true;
    //        //Debug.Log(playerfind);
    //        #endregion
    //    }
    //}


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

    #endregion

    private void selectcharactor(GameObject _value)//다른 오브젝트를 누를때 기존에 오브젝트는 끄는 코드
    {
        //Debug.Log(teamtype);
        if (teamtype == 1)//블루팀일 경우
        {
            int count = objblue.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objblue[iNum].GetComponent<Player>();
                selPlayer.Playselectedcheck(_value == objblue[iNum]);
                //if (objred[iNum] == true)
                //{
                //    selPlayer.lookobj();
                //}
            }
        }
        else if (teamtype == 2)
        {
            int count = objred.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objred[iNum].GetComponent<Player>();
                selPlayer.Playselectedcheck(_value == objred[iNum]);
                //if (objred[iNum] == true)
                //{
                //    selPlayer.lookobj();
                //}
            }
        }
    }

    //public void teamfalsecheck()//선택 마크를 사라지게 만드는 코드 부분
    //{
    //    if (teamtype == 1)//블루팀일 경우
    //    {
    //        int count = objred.Count;
    //        for (int iNum = 0; iNum < count; ++iNum)
    //        {
    //            Player selPlayer = objred[iNum].GetComponent<Player>();
    //            selPlayer.Playselectedcheck(false);
    //        }
    //    }
    //    else if (teamtype == 2)
    //    {
    //        int count = objblue.Count;
    //        for (int iNum = 0; iNum < count; ++iNum)
    //        {
    //            Player selPlayer = objblue[iNum].GetComponent<Player>();
    //            selPlayer.Playselectedcheck(false);
    //        }
    //    }
    //}


    private void playertypechoice()//플레이어를 선택했을 경우
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.transform != null && rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //Debug.Log(rayHit.transform.name);
                DesYutButton();//나갈수 있는 버튼을 삭제하는 부분 다른 플레이어를 선택 할때 삭제하도록 설정
                Footholdbox.movedestory();//이동 표식을 맵 밖으로 이동
                Footholdbox.ExitPlayerFalse();// 말이 나갈수 있을때 그 위치에 이동 표시가 뜨는것을 방지하기 위한 코드로 이동함
                selectcharactor(rayHit.transform.gameObject);
                if (tutorialStageCheck == true && onlyStory1 == true)
                {
                    onlyStory1 = false;
                    TutorialStory.TimeOff();
                    return;
                }
                //Player selPlayer = rayHit.transform.GetComponent<Player>();
                //selPlayer.Playselectedcheck(true);

            }
        }
    }

    //private void findplayerteam()//선택한 플레이어캐릭터 확인?
    //{
    //    GameObject startteam = GameObject.Find("Playtimemanager");
    //    Playtimer startplayer = startteam.GetComponent<Playtimer>();
    //    startplayer.changeteam();
    //}

    /// <summary>
    /// team = 1 <= 블루팀, team = 2 <= 레드팀
    /// </summary>
    /// <param name="team"></param>
    public void Chageplayteam(int team)
    {
        switch (team)
        {
            case 1:
                teamtype = team;
                break;
            case 2:
                teamtype = team;
                break;
        }

    }

    //private bool isFindData(out int value)
    //{
    //    value = 10;
    //    return true;
    //}

    //시작하고 팀 체인지가 안되는 현상이 있음 07/05일자
    private void startturn()//처음에 누가 먼저 시작하는지 알려주는 코드
    {
        animator = startcheck.gameObject.GetComponentInChildren<Animator>();
        Maxstartturnyut -= Time.deltaTime;
        if (Maxstartturnyut < 2)//3초가 지난 경우
        {
            if (starttype == false)
            {
                changecheck = Random.Range(0, 2);
                starttype = true;
                if (tutorialStageCheck == true)
                {
                    changecheck = 1;
                }
            }
            else
            {
                animator.SetFloat("CharacterChange", changecheck);
            }

            if (Maxstartturnyut < 0)//0보다 작을 경우 즉 시간이 다 된 경우
            {
                if (tutorialStageCheck == true)
                {
                    changecheck = 1;
                    TutorialStory.TimeOff();
                }
                Maxstartturnyut = startturnyut;
                starttype = false;
                startcheck.gameObject.SetActive(false);
                curState = eRule.ThrowYut;
                throwyutbutton = true;
                Playtimer.LookScene();
                startTeamTurn();
            }

        }
        else//3초가 안 지난 경우
        {
            if (changecheck == 0 && changetimer == 0.1f)//3초가 지난 경우
            {
                changecheck = 1;
                animator.SetFloat("CharacterChange", changecheck);

            }
            else if (changecheck == 1 && changetimer == 0.1f)
            {
                changecheck = 0;
                animator.SetFloat("CharacterChange", changecheck);
            }
        }
    }

    private void startchageturn()//순서를 정할때 0.1초마다 캐릭 터가 변경되도록 설정
    {
        if (Maxstartturnyut < 2)
        {
            return;
        }
        changetimer -= Time.deltaTime;
        if (changetimer < 0)
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
        if (throwyutbutton == true)
        {
            throwbutton.gameObject.SetActive(true);
            Playtimemanager.SetActive(true);
            Yuttimer.SetActive(true);
            PlayerTimer.SetActive(false);
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            //Playtimer startplayer = Playtimemanager.GetComponent<Playtimer>();
            //startplayer.startturn((int)changecheck);
            Playtimer.startturn((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yutbox.SetActive(false);
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
            curState = eRule.SelectCharacter;
        }
    }

    #region
    //private void positionobjcheck()//이동될 오브젝트 위치 체크
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0) == true)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
    //        if (rayHit.transform != null && rayHit.transform.gameObject == movelocation1)
    //        {
    //            //movepositioncheck();
    //        }
    //    }
    //}

    //private void movepositioncheck()//캐릭터가 이동할 코드
    //{
    //    if (movelocation1)
    //    {
    //        transform.position = movelocation1.transform.position;
    //    }
    //}
    #endregion

    public void turnendcheck(float _oneYut, float _twoYut, float _thrYut)
    {
        if (TurnCycleCheck == true)
        {
            //TurnCycleCheck = false;
            return;
        }
        if (_oneYut + _twoYut + _thrYut == 0)
        {
            curState = eRule.ReturnthrowYut;
            throwyutbutton = true;
            if (changecheck == 0)
            {
                changecheck = 1;
            }
            else
            {
                changecheck = 0;
            }
            Yutstartbuttons.getbackYut();
            Playtimer.TurnChangeCheck();
            //Yutstartbutton yutstartbutton = Yutstartbutton.GetComponent<Yutstartbutton>();
            //yutstartbutton.getbackYut();
            //Player.MoveCheckControl();
            Yutstartbuttons.NotCheckTrue();
            if(tutorialStageCheck == true && onlyStory2 == true)
            {
                onlyStory2 = false;
                TutorialStory.TimeOff();
            }
        }
        else//더한 값이 0이 아닐 경우
        {
            curState = eRule.waittime;
        }
    }

    private void changeturn()//플레이어 턴을 변경하는 코드
    {
        if (throwyutbutton == true)
        {
            throwbutton.gameObject.SetActive(true);
            Playtimemanager.SetActive(true);
            Yuttimer.SetActive(true);
            PlayerTimer.SetActive(false);
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            Playtimer.turnendchange((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            Yutbox.SetActive(false);
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
            curState = eRule.SelectCharacter;
        }
    }

    private void startTeamTurn()
    {
        throwbutton.gameObject.SetActive(true);
        //Playtimemanager.SetActive(true);
        Yuttimer.SetActive(true);
        PlayerTimer.SetActive(false);
        Yutbox.SetActive(true);
        playerbox.SetActive(true);

        //Playtimer.startturn((int)changecheck);
        Playtimer.StartTurnTime();
        Playtimer.turnendchange((int)changecheck);
        Yutstartbuttons.ControlCheck1();
    }


    private void waitingtimer()
    {
        Maxtimewait -= Time.deltaTime;
        if (Maxtimewait < 0)
        {
            Maxtimewait = timewait;
            curState = eRule.SelectCharacter;
        }
    }

    public void nextturn()
    {
        throwyutbutton = false;
        changeturn();
    }

    //말을 움직일때 같은팀  말을 잡을 상호작용
    //Footholdbox에서 list에있는 위치에 어떤 오브젝트가 있는지 확인이 필요
    //현재 상호작용은 작동이 됨 그러나 list에서 오브젝트를 넣는 부분에서 문제가 생김
    public void holdboxPosCheck(float _MaxmoveYutcount,GameObject dplayer)
    {
        //현재 말이 이동할 위치에 같은팀 말이 존재하는지
        //Footholdbox.Yutfoothold[(int)_MaxmoveYutcount];
        if (IsPositionExistPlayer((int)_MaxmoveYutcount, out GameObject outplayer) == true)//현재 말이 이동할 위치에 같은팀 말이 존재하는지 true면 있는것
        {
            //_MaxmoveYutcount는 현재 있는 위치, 즉 이동할 위치에 있는 리스트를 가져올려면 moveYutcount 부분이 필요
            //player가 아군인지 적군인지 판단 
            #region if문을 활용하여 레드팀 블루팀을 확인 해주는 코드
            //if (outplayer.gameObject.tag == "RedTeam" && teamtype == 1)//블루팀 시간일때(teamtype == 1) 레드팀을 잡을 경우
            //{
            //    //outplayer에 맞는 시작 위치로 되 돌려야하는 부분
            //    Player players = outplayer.GetComponent<Player>();//그 자리에 있는 오브젝트에 있는 플레이어 스크립트를 불러온다
            //    players.AgainStartPos();
            //}
            //else if(outplayer.gameObject.tag == "BlueTeam" && teamtype == 1)//블루팀 시간일때(teamtype == 1)일때 블루팀을 잡을 경우 업는다
            //{
            //    Player players = outplayer.GetComponent<Player>();
            //    //아직 업는 기능은 구현 안됨
            //}
            #endregion

            #region switch 문으로 만들어진 코드 현재 가장 가까운 코드 부분
            ////Debug.Log(outplayer);
            //Player players = outplayer.GetComponent<Player>();//그 자리에 있는 오브젝트에 있는 플레이어 스크립트를 불러온다
            ////Debug.Log(cObjectWhereFootHold.Equals(outplayer, _MaxmoveYutcount));
            ////teamtype == 1이면 블루팀 차례,teamtype == 2이면 레드팀 차례
            //switch (teamtype)
            //{
            //    //처음으로 잡는 경우에는 잘 잡힘 그러나 잡은후 상대 말이 잡을려고 시도를 하면 안잡힘 <- 해결
            //    //현재 어떤 경우의 수로 잡을 경우 말이 안 잡히는 경우가 존재 => RemoveAt(0)에 없는 리스트가 삭제 될시 다음 말을 잡았을때 문제가 생김
            //    case 1: // 블루팀 턴일때 
            //        if(outplayer.gameObject.tag == "RedTeam")//레드팀을 잡을 경우
            //        {
            //            //Player players = outplayer.GetComponent<Player>();//그 자리에 있는 오브젝트에 있는 플레이어 스크립트를 불러온다
            //            players.AgainStartPos();
            //            listObjectWhereFootHold.RemoveAt(0);
            //            //if(cObjectWhereFootHold.Equals(_MaxmoveYutcount, outplayer) == true)
            //            //{

            //            //}

            //            //listObjectWhereFootHold.Remove()
            //            //Debug.Log("빨강말을 잡다");
            //        }
            //        else if(outplayer.gameObject.tag == "BlueTeam")//블루팀을 업는 경우
            //        {
            //            //Debug.Log("블루팀말을 업다");
            //            //아직 업는 기능은 구현 안됨
            //            //Player players = outplayer.GetComponent<Player>();
            //        }
            //        break;
            //    case 2://레드팀 차례일때
            //        if (outplayer.gameObject.tag == "RedTeam")//레드팀을 업는 경우
            //        {
            //            //Debug.Log("레드팀말을 업다");
            //            //Player players = outplayer.GetComponent<Player>();//그 자리에 있는 오브젝트에 있는 플레이어 스크립트를 불러온다
            //        }
            //        else if (outplayer.gameObject.tag == "BlueTeam")//블루팀을 잡는 경우
            //        {
            //            //Debug.Log("블루팀말을 잡다");
            //            //아직 업는 기능은 구현 안됨
            //            //Player players = outplayer.GetComponent<Player>();
            //            players.AgainStartPos();
            //            listObjectWhereFootHold.RemoveAt(0);

            //        }
            //        break;
            //}
            //Debug.Log(outplayer);
            #endregion

            //잡는 순서 => 1.이동한 말의 위치를 가져온다 2.가져온 위치랑 현재 저장되어있는 위치랑 비교를하여 같은 위치를 가진 리스트를 알아온다
            //3.리스트를 알아내면 그 리스트를 삭제 시킨다
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.trsFootHold == footholdbox.Yutfoothold[(int)_MaxmoveYutcount]);
            //문제 = > data에 위치해 있는 리스트속에있는 오브젝트가 이동시키는 말이 아닌 잡히는 말로 되어있음 
            int MaxCount = listObjectWhereFootHold.Count;
            //Debug.Log(listObjectWhereFootHold[1].trsFootHold);
            for (int iNum = 0; iNum < MaxCount; iNum++)
            //data <= 말을 움직이는 데이터
            //listObjectWhereFootHold <= 그 공간에 있는 말
            {
                if (data.trsFootHold == listObjectWhereFootHold[iNum].trsFootHold && dplayer != listObjectWhereFootHold[iNum].objPlayer)
                {
                    //Debug.Log(teamtype);
                    checkCount(data, dplayer, iNum);
                    break;
                }
                else
                {
                    continue;
                }
            }
            //Debug.Log(data.trsFootHold);
        }
    }

    private void checkCount(cObjectWhereFootHold data, GameObject _dplayer, int iNum)//말이 말을 잡을 경우에 실행되는 코드
    {
        //Debug.Log(teamtype);
        //data <= 말을 움직이는 데이터
        //listObjectWhereFootHold <= 그 공간에 있는 말

        //Player players = listObjectWhereFootHold[iNum].objPlayer.GetComponent<Player>();//그 자리에 있는 오브젝트에 있는 플레이어 스크립트를 불러온다
        player = listObjectWhereFootHold[iNum].objPlayer.GetComponent<Player>();
        GameObject ObjBackUp = listObjectWhereFootHold[iNum].objPlayer;//원래 그 자리에있던 말을 백업 해두기위해 만든 코드
        Player objs = ObjBackUp.gameObject.GetComponent<Player>();
        //Player players = data.objPlayer.GetComponent<Player>();
        #region
        //switch (teamtype)
        //{
        //    case 1: // 블루팀 턴일때 
        //        if (outplayer.gameObject.tag == "RedTeam")//레드팀을 잡을 경우
        //        {
        //            players.AgainStartPos();
        //            listObjectWhereFootHold.RemoveAt(iNum);
        //        }
        //        else if (outplayer.gameObject.tag == "BlueTeam")//블루팀을 업는 경우
        //        {
        //        }
        //        break;
        //    case 2://레드팀 차례일때
        //        if (outplayer.gameObject.tag == "RedTeam")//레드팀을 업는 경우
        //        {
        //        }
        //        else if (outplayer.gameObject.tag == "BlueTeam")//블루팀을 잡는 경우
        //        {
        //            players.AgainStartPos();
        //            listObjectWhereFootHold.RemoveAt(iNum);
        //        }
        //        break;
        //}
        #endregion
        switch (teamtype)//현재 움직일수있는 팀 턴
        {
            case 1:// 블루팀 턴일때
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//잡히는 말이 레드팀의 태그를 달고 있을때 잡는다
                {
                    data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    player.AgainStartPos();//잡힌말의 위치를 초기화 시킴
                    listObjectWhereFootHold.Remove(data);//위치를 초기화 이후에 리스트 빼기
                    assistantPlayerRed(data.objPlayer);//업혀진 말들을 다시 보여주는 코드
                    //player.DesCurryTeam();//업힌말이 있으면 업힌말을 삭제하는 코드
                    Yutstartbuttons.CatchReTurnTurn();
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//잡히는 말이 블루팀 태그를 달고있을때 업는다
                {
                    #region 대비용
                    //player.DesTeam();//잡은말이  같은 팀이면 안보이게 설정하는 코드
                    //data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    //listObjectWhereFootHold.Remove(data);

                    //player = _dplayer.GetComponent<Player>();
                    //player.CurryTeam();
                    //CurryBlue = true;
                    #endregion
                    //NotCarryPrevention(_dplayer);//업기전에 물어보기
                    player = _dplayer.GetComponent<Player>();//잡는말을 가져온다
                    if (player.GoPlayer == false)//player.MaxmoveYutcount != objs.MaxmoveYutcount
                    {
                        return;
                    }
                    player.DesTeam();//처음으로 보내고 오브젝트를 끈다
                    data = listObjectWhereFootHold.Find(x => x.objPlayer == _dplayer);
                    listObjectWhereFootHold.Remove(data);
                    //원래 자리에있던 오브젝트에 꼬리를 달아야한다
                    player = ObjBackUp.GetComponent<Player>();
                    //BlueCurryCheck 2마리가 업혀있을때 1마리쪽으로 업을 경우에 2마리다 업었다는것을 알려주기 위해 만듬
                    player.CurryTeam(BlueCurryCheck);

                    //1개의 말이 2개의 말한테 업힐때는 문제X 2개의 말이 1개의 말한테 업히면 문제가 생김
                }
                break;
            case 2://레드팀 턴일때
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//잡히는 말이 레드팀 태그를 달고있 을때 업는다
                {
                    player = _dplayer.GetComponent<Player>();//잡는말을 가져온다
                    //if (player.GoPlayer == false || player.MaxmoveYutcount != objs.MaxmoveYutcount)
                    //{
                    //    return;
                    //}
                    //문제점 지금까지 리스트에서 같은 이름으로 처리를 하다보니 이걸 넣을 경우 겹치는 리스트끼리는 안 잡히게 된다
                    if (player.GoPlayer == false)//안보이는 발판을 하나 더 생성해서 해결됨
                    {
                        return;
                    }
                    player.DesTeam();//처음으로 보내고 오브젝트를 끈다
                    data = listObjectWhereFootHold.Find(x => x.objPlayer == _dplayer);
                    listObjectWhereFootHold.Remove(data);
                    //원래 자리에있던 오브젝트에 꼬리를 달아야한다
                    player = ObjBackUp.GetComponent<Player>();
                    player.CurryTeam(RedCurryCheck);
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//잡히는 말이 블루팀 태그를 달고있을때 잡는다
                {
                    data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    player.AgainStartPos();
                    listObjectWhereFootHold.Remove(data);
                    assistantPlayerBlue(data.objPlayer);//블루팀을 잡는다
                    //player.DesCurryTeam();
                    Yutstartbuttons.CatchReTurnTurn();
                }
                break;
        }
        //turnendcheck(Player.oneYut, Player.twoYut, Player.threeYut);
    }

    public void CheckCurry()
    {
        if(teamtype == 1)
        {
            BlueCurryCheck = true;
        }
        else if(teamtype == 2)
        {
            RedCurryCheck = true;
        }
    }

    public void WithdrawalCheck()
    {
        if (teamtype == 1)
        {
            BlueCurryCheck = false;
        }
        else if (teamtype == 2)
        {
            RedCurryCheck = false;
        }
    }

    //말을 움직일때 다른팀 말을 잡을 상호작용
    //지름길에 들어올 때 지름길로 갈 수 있는 상호작용
    //골인점에 도달했을때 나갈수 있는 상호작용

    /// <summary>
    /// 플레이어를 발판으로 이동시킵니다.
    /// </summary>
    /// <param name="_player">이동할 플레이어</param>
    /// <param name="_movePos">이동할 위치</param>
    public void MovePlayerFootHold(GameObject _player, int _movePos)
    {
        bool isExsitPlayer = listObjectWhereFootHold.Exists(x => x.objPlayer == _player);//리스트에 해당 플레이어가 존재하는지 
        //Debug.Log(_player);
        //<= 지금 움직이는 플레이어가 들어 가는 현상이 아닌 해당 발판에 있는 오브젝트를 가져와야 함( player에서 작동해야되나?)
        //새롭게 이동할때 플레이어가 존재한다고 뜸 -> 이동후에 이 발판에 플레이어가 있는 지를 확인으로 바꿔야됨
        //플레이어가 이동후에 위치를 저장하고 새로운 플레이어가 오면 그 발판에 있는지 확인 해주는 부분이 필요함
        

        if (isExsitPlayer == false)//플레이어는 발판에 없었고 생성되어야 함(해당 발판에 플레이어가 없다면)
        {
            cObjectWhereFootHold data = new cObjectWhereFootHold()
            {
                objPlayer = _player,
                trsFootHold = footholdbox.Yutfoothold[_movePos]
            };

            listObjectWhereFootHold.Add(data);
            PastlLoadCheck(data.trsFootHold, _player);
            //midcheck(_movePos);
            //Debug.Log(data.objPlayer);
            //Debug.Log(_player);
        }
        else//플레이어가 발판에 존재함
        {
            //지금 플레이어가 이동만 하면 이쪽이 작동 됨 => 즉 자기 위치발판에서 이동하면 작동되는 부분
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.objPlayer == _player);
            data.objPlayer = _player;
            data.trsFootHold = footholdbox.Yutfoothold[_movePos];
            PastlLoadCheck(data.trsFootHold, _player);
            //midcheck(_movePos);
            //Debug.Log(_player,data.trsFootHold);
            //Debug.Log(data.objPlayer);
        }

        //Debug.Log(_player);
    }

    /// <summary>
    /// 해당 위치에 플레이어가 존재하는지 확인합니다.
    /// </summary>
    /// <param name="_pos">해당위치를 확인합니다</param>
    public bool IsPositionExistPlayer(int _pos, out GameObject _player)
    {
        //말을 잡아버리는 순간 다른 말이랑 연동이 안됨
        _player = default;//null로 초기화
        Transform trsYutfoolhold = footholdbox.Yutfoothold[_pos];//체크할 위치 <= 이동 후 위치 확인

        bool isExist = listObjectWhereFootHold.Exists(x => x.trsFootHold == trsYutfoolhold);
        //isExist true라면 있는것
        if (isExist == true)
        {
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.trsFootHold == trsYutfoolhold);//위치는 잘 받음
            _player = data.objPlayer;//여기에서 오브잭트가 변환이 안됨
        }

        //Debug.Log(_player);
        return isExist;
    }

    private void assistantPlayerBlue(GameObject subplayer)
    {
        //GameObject subchild = subplayer.transform.GetChild(1).gameObject;//잡힌말의 자식오브젝트를 가져온다
        if (subplayer.transform.GetChild(1).gameObject.activeSelf == false)//그 자식 오브젝트가 꺼져있다면 실행을 안한다
        {
            return;
        }
        else
        {
            for (int iNum = 0; iNum < objblue.Count; iNum++)
            {
                GameObject subobj = objblue[iNum];
                if (subobj.activeSelf == false)
                {
                    subobj.SetActive(true);
                    BlueCurryCheck = false;
                }
            }
            player.DesCurryTeam();//업힌말이 있으면 업힌말을 삭제하는 코드
        }
    }
    
    private void assistantPlayerRed(GameObject subplayer)
    {
        //GameObject subchild = subplayer.transform.GetChild(1).gameObject;//잡힌말의 자식오브젝트를 가져온다
        if(subplayer.transform.GetChild(1).gameObject.activeSelf == false)//그 자식 오브젝트가 꺼져있다면 실행을 안한다
        {
            return;
        }
        else
        {
            for (int iNum = 0; iNum < objred.Count; iNum++)
            {
                GameObject subobj = objred[iNum];
                if (subobj.activeSelf == false)
                {
                    subobj.SetActive(true);
                    RedCurryCheck = false;
                }
            }
            player.DesCurryTeam();//업힌말이 있으면 업힌말을 삭제하는 코드
        }
    }

    //지름길을 만들기 위해 쓰이는 코드위치
    public void PastlLoadCheck(Transform _data, GameObject _player)
    {
        //0 ~ 19까지는 외각으로 부분 지름길은 5,10부분
        //20~29까지는 지름길 [5]에서 쭉 갈때 기준으로 가는 길
        //30 ~ 34까지는 지름길 [10]에서 쭉 갈때 기준으로 가는 길
        //
        if (_data == Footholdbox.Yutfoothold[5] || _data == Footholdbox.Yutfoothold[10] || _data == Footholdbox.Yutfoothold[34]
            || _data == Footholdbox.Yutfoothold[23] || _data == Footholdbox.Yutfoothold[44])
        {
            player = _player.GetComponent<Player>();
            if(_data == Footholdbox.Yutfoothold[5])
            {
                player.ShortcutArrive(1);
            }
            else if(_data == Footholdbox.Yutfoothold[10])
            {
                player.ShortcutArrive(2);
            }
            else if (_data == Footholdbox.Yutfoothold[23] || _data == Footholdbox.Yutfoothold[34])
            {
                player.ShortcutArrive(3);
            }
            else if(_data == Footholdbox.Yutfoothold[44])
            {
                player.ShortcutArrive(4);
            }
        }
    }

    //말이 윷판을 다 돌고 나갈수 있을때 작동하도록 만든 부분들
    public void PosClearYut(GameObject _obj, float _Yutorder)//나갈수 있는 버튼을 생성해주는 부분
    {
        ClearButton.gameObject.SetActive(true);
        Clearobj = _obj;
        ClearNumber = _Yutorder;
    }

    public void DesYutButton()//나갈수 있는 버튼을 삭제하는 부분 다른 플레이어를 선택 할때 또는 나가지 않았을때 삭제
    {
        ClearButton.gameObject.SetActive(false);
    }

    public void CheckBackYutPass()//윷이 빽도인데 필드에 윷이 하나도 없는 경우
    {
        int MaxCount = listObjectWhereFootHold.Count;
        if(teamtype == 1 )//블루팀 일때
        {
            if (listObjectWhereFootHold.Count == 0)
            {
                Yutstartbuttons.ClaerYutCount();
                return;
            }
            BackCheck = true;//빽도를 찾고 있을때 텀을 못 넘기게 도와주는 코드
            for (int iNum = 0; iNum < MaxCount; iNum++)//총 리스트를 비교해서 찾아본다
            {
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//필드에 있을 경우
                {
                    BackCheck = false;
                    return;
                }
                else if(listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam" )//for문에 레드팀이 잡힐 경우
                {
                    continue;
                }
            }
            if (BackCheck == true)//필드에 말이 없을 경우에 true가 유지됨
            {
                BackCheck = false;
                Yutstartbuttons.ClaerYutCount();
            }
            else//필드에 말이 있을 경우
            {
                throwbutton.gameObject.SetActive(false);
                Yuttimer.SetActive(false);
                PlayerTimer.SetActive(true);
                curState = eRule.SelectCharacter;
            }
        }
        else if(teamtype == 2)
        {
            if (listObjectWhereFootHold.Count == 0)
            {
                Yutstartbuttons.ClaerYutCount();
                return;
            }
            BackCheck = true;
            for (int iNum = 0; iNum < MaxCount; iNum++)//총 리스트를 비교해서 찾아본다
            {
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//필드에 있을 경우
                {
                    BackCheck = false;
                    return;
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//for문에 레드팀이 잡힐 경우
                {
                    continue;
                }
            }
            if (BackCheck == true)//필드에 없을 경우에 true가 유지됨
            {
                BackCheck = false;
                Yutstartbuttons.ClaerYutCount();
            }
            else//필드에 말이 있을 경우
            {
                throwbutton.gameObject.SetActive(false);
                Yuttimer.SetActive(false);
                PlayerTimer.SetActive(true);
                curState = eRule.SelectCharacter;
            }
        }
    }

    public void PlayTimeTurn()//빽도가 걸릴때 플레이 타임으로 넘기기
    {
        throwbutton.gameObject.SetActive(false);
        Yuttimer.SetActive(false);
        PlayerTimer.SetActive(true);
        curState = eRule.SelectCharacter;
    }

    public void TimeOverChange()//시간초내에 이동하지 않으면 턴이 변경되게 하는 부분
    {
        throwbutton.gameObject.SetActive(true);
        Playtimemanager.SetActive(true);
        Yuttimer.SetActive(true);
        PlayerTimer.SetActive(false);
        Yutbox.SetActive(true);
        playerbox.SetActive(true);

        Playtimer.turnendchange((int)changecheck);
    }
    public void PlayerTimeChange()
    {
        Yutbox.gameObject.SetActive(false);
        throwbutton.gameObject.SetActive(false);
        Yuttimer.SetActive(false);
        PlayerTimer.SetActive(true);
        curState = eRule.SelectCharacter;
    }

    public void DebugTest()
    {
        Debug.Log(teamtype);
    }

    public void EndTurnCheck()
    {
        curState = eRule.ReturnthrowYut;
        throwyutbutton = true;
        desChangePos();
        if (changecheck == 0)
        {
            changecheck = 1;
        }
        else
        {
            changecheck = 0;
        }
        Yutstartbuttons.getbackYut();
        Playtimer.TurnChangeCheck();

        
    }

    private void desChangePos()
    {
        if (teamtype == 1)//블루팀일 경우
        {
            int count = objblue.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objblue[iNum].GetComponent<Player>();
                selPlayer.DesObj(objblue[iNum].gameObject);
            }
        }
        else if (teamtype == 2)
        {
            int count = objred.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objred[iNum].GetComponent<Player>();
                selPlayer.DesObj(objblue[iNum].gameObject);
            }
        }
    }

    public void RecycleTurnManager()
    {
        TurnCycleCheck = true;
        //curState = eRule.ThrowYut;
        throwbutton.gameObject.SetActive(true);
        Playtimemanager.SetActive(true);
        PlayerTimer.SetActive(false);//테스트 부분
        Yuttimer.SetActive(true);
        Yutbox.SetActive(true);
        playerbox.SetActive(true);
        curState = eRule.RecycleTime;
    }

    /// <summary>
    /// TurnCycleCheck == 말을 잡고 다시 돌렸을때 true가 되도록 설정 되어있음
    /// </summary>
    /// <param name="_recycleCheck"></param>
    public void RecycleTurnPass( bool _recycleCheck)//여기서 문제 발생 07/20일자
    {
        if(_recycleCheck == true && TurnCycleCheck == false)//모나 윷이 안 뜰 경우
        {
            return;
        }
        if(TurnCycleCheck == true && _recycleCheck == false)//말을 잡고 다시 던지기 턴인데 모나 윷이 안 걸린경우
        {
            TurnCycleCheck = false;
            //Yutbox.SetActive(false);
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
            curState = eRule.SelectCharacter;
        }
        else if(TurnCycleCheck == true && _recycleCheck == true)//말을 잡고 윷을 던질떄 모나 윷이 뜬 경우
        {
            return;
        }
    }

    public void ListClear()//현재 모든 팀을 다 업고 있을때 결승점을 지나면 전부 삭제 처리하도록 하는 코드
    {
        if(Clearobj.gameObject.tag == "RedTeam")
        {
            objred.Clear();
        }
        else if(Clearobj.gameObject.tag == "BlueTeam")
        {
            objblue.Clear();
        }
    }

    public void LookAtYutPlayer()
    {
        if (Clearobj.gameObject.tag == "RedTeam")
        {
            for(int iNum = 0; iNum < objred.Count; iNum++)
            {
                if (objred[iNum].activeSelf == false)//만약에 오브젝트가 꺼져있다면
                {
                    objred.RemoveAt(iNum);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
        else if (Clearobj.gameObject.tag == "BlueTeam")
        {
            for (int iNum = 0; iNum < objblue.Count; iNum++)
            {
                if (objblue[iNum].activeSelf == false)//만약에 오브젝트가 꺼져있다면
                {
                    objblue.RemoveAt(iNum);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    //==================================게임 클리어 이후 부분을 만들 예정

    private void teamWinCheck()
    {
        if (WinerBlue == true)
        {
            WinerBlue = false;
            WinerTeamCanvas.gameObject.SetActive(true);
            WinerTeamString.text = "블루팀이 승리했습니다";
        }
        else if (WinerRed == true)
        {
            WinerRed = false;
            WinerTeamCanvas.gameObject.SetActive(true);
            WinerTeamString.text = "레드팀이 승리했습니다";
        }
    }
    #region
    //public void ReStart()//다시 시작하기 버튼을 눌렀을때 작동
    //{
    //    //WinerTeamCanvas.gameObject.SetActive(false);
    //    //startcheck.gameObject.SetActive(true);
    //    //changecheck = 0;
    //    //Yutstartbuttons.YutReStartCheck();
    //    //footholdbox.movedestory();
    //    //returnPosPlayer();
    //    //curState = eRule.Preferencetime;
    //}

    //private void returnPosPlayer()//플레이어가 자기 자리로 가는 코드
    //{
    //    for(int iNum = 0; iNum < objblue.Count; iNum++)
    //    {
    //        Player.AgainPlayerStart();
    //    }
    //    for (int iNum = 0; iNum < objred.Count; iNum++)
    //    {
    //        Player.AgainPlayerStart();
    //    }

    //}
    #endregion

    private void testText()
    {
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //    WinerTeamString.text = "테스트";
        //}
    }

    public void ChangeGageBar(int _check)
    {
        if(_check == 0)//윷을 던지는 부분
        {
            Yuttimer.SetActive(true);
            PlayerTimer.SetActive(false);
        }
        else if(_check == 1)//플레이어가 이동하는 부분
        {
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
        }
    }
}
