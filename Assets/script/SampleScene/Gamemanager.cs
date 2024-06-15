using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    Animation anim;
    Animator animator;
    [SerializeField] Image startcheck;

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


    private Player player;
    private Footholdbox footholdbox;

    public enum eRule
    {

        Preferencetime,//먼저 윷을 던지는 우선권 시간
        ThrowYut,
        SelectCharacter,//캐릭터를 선택
        ReturnthrowYut,//다시 윷을 던지는 부분
        waittime,//잠깜 기다리는 부분
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
        Maxtimewait = timewait;
    }

    //public bool playertouch;//클릭 했을때 on으로 전환 클릭이 끝나면 off로 전환

    void Start()
    {
        startcheck.gameObject.SetActive(true);
        changecheck = 0;
    }

    void Update()
    {
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
            //if (rayHit.transform != null && rayHit.transform.gameObject.layer == "Player")
            //{
            //    //Debug.Log(rayHit.transform.name);
            //    selectcharactor(rayHit.transform.gameObject);
            //    //Player selPlayer = rayHit.transform.GetComponent<Player>();
            //    //selPlayer.Playselectedcheck(true);
            //}
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
            }
            else
            {
                animator.SetFloat("CharacterChange", changecheck);
            }

            if (Maxstartturnyut < 0)//0보다 작을 경우
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
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            Playtimer startplayer = Playtimemanager.GetComponent<Playtimer>();
            startplayer.startturn((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
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
            Yutstartbutton yutstartbutton = Yutstartbutton.GetComponent<Yutstartbutton>();
            yutstartbutton.getbackYut();
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
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            Playtimer startplayer = Playtimemanager.GetComponent<Playtimer>();
            startplayer.turnendchange((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            curState = eRule.SelectCharacter;
        }
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
    public void holdboxPosCheck(float _MaxmoveYutcount)
    {
        //현재 말이 이동할 위치에 같은팀 말이 존재하는지
        //Footholdbox.Yutfoothold[(int)_MaxmoveYutcount];
        if (IsPositionExistPlayer((int)_MaxmoveYutcount, out GameObject player) == true)//현재 말이 이동할 위치에 같은팀 말이 존재하는지 true면 있는것
        {
            //_MaxmoveYutcount는 현재 있는 위치, 즉 이동할 위치에 있는 리스트를 가져올려면 moveYutcount 부분이 필요
            //player 아군인지 적군인지 판단 

            //지금은 이동만 하면 입력이 되는 문제가 발생함
            //if(player.gameObject.layer == )
            //{
            //    Debug.Log("레드팀이 있음");
            //}
            //else if(player.transform.tag == "blueplayer")
            //{
            //    Debug.Log("블루팀이 있음");
            //}
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
        Debug.Log(_player);//<= 지금 움직이는 플레이어가 들어 가는 현상이 아닌 해당 발판에 있는 오브젝트를 가져와야 함( player에서 작동해야되나?)
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
            Debug.Log(_player);
        }
        else//플레이어가 발판에 존재함
        {
            //지금 플레이어가 이동만 하면 이쪽이 작동 됨 => 즉 자기 위치발판에서 이동하면 작동되는 부분
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.objPlayer == _player);
            data.trsFootHold = footholdbox.Yutfoothold[_movePos];
            Debug.Log(data.trsFootHold);
        }
    }

    /// <summary>
    /// 해당 위치에 플레이어가 존재하는지 확인합니다.
    /// </summary>
    /// <param name="_pos">해당위치를 확인합니다</param>
    public bool IsPositionExistPlayer(int _pos, out GameObject _player)
    {
        _player = default;//null로 초기화
        Transform trsYutfoolhold = footholdbox.Yutfoothold[_pos];//체크할 위치 <= 이동 후 위치 확인

        bool isExist = listObjectWhereFootHold.Exists(x => x.trsFootHold == trsYutfoolhold);
        //isExist true라면 있는것
        if (isExist == true)
        { 
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.trsFootHold == trsYutfoolhold);
            _player = data.objPlayer;
        }

        Debug.Log(_player);
        return isExist;
    }
}
