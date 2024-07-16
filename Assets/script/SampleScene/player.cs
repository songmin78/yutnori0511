using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("아이들 오브젝트")]
    [SerializeField] GameObject findplayd;
    [SerializeField] GameObject Curryobj1;
    [SerializeField] GameObject Curryobj2;
    //[Header("플레이어")]
    //[SerializeField] bool playercheck1;//플레이어1번 부분
    //[SerializeField] bool playercheck2;//플레이어 2번 부분
    //[Header("플레이어를 선택 했을때")]
    //[SerializeField] public GameObject checkobj;//플레이러를 체크할때 보이는 오브젝트
    //[SerializeField] bool checkmask;//플레이어를 체크 할때
    //[SerializeField] Canvas playerselected;//플레이어가 선택될 때 체크되는걸 보여주는 오브젝트
    bool selectedcheck;//선택될때 체크

    bool buleTeam;
    public Vector3 startmypos;//죽을때 다시 시작위치로 돌아가기 위한 위치 확인용

    [Header("팀 구분")]
    [SerializeField]public bool teamred;
    [SerializeField]public bool teamblue;
    [Header("기타")]
    //[SerializeField]bool playertouch = false;
    [SerializeField]public bool playerchoice;
    [SerializeField]public bool tests;
    public bool playertype1;//플레이어타입1
    public bool playertype2;//플레이어타입2
    [Header("윷 이동 부분")]
    [SerializeField] public float oneYut;
    [SerializeField] public float twoYut;
    [SerializeField] public float threeYut;
    [SerializeField, Tooltip("플레이어가 출발을 하면 True로 전환")] bool goPlayer;
    public bool GoPlayer
    {
        get
        {
            return goPlayer;
        }
    }
    float Yutorder;//말을 움직일 수있는 수 부분
    //[SerializeField] float d;
    //[SerializeField]public bool playertypenumber;
    [Header("윷을 움직이기위한 거리 부분")]
    private bool myyutturn;//자기 차례일때만 true로 변경
    [SerializeField] float moveYutcount1;//첫번째 윷에 나온 숫자만큼 더하여 어느정도 움직일지 미리 보여주는 부분
    [SerializeField] float moveYutcount2;//두번째 윷에 나온 숫자만큼 더하여 어느정도 움직일지 미리 보여주는 부분
    [SerializeField] float moveYutcount3;//세번째 윷에 나온 숫자만큼 더하여 어느정도 움직일지 미리 보여주는 부분
    [SerializeField]float maxmoveYutcount;//이동후 자신의 위치를 저장(지름길도 저장함)
    public float MaxmoveYutcount
    {
        get 
        {
            return maxmoveYutcount;
        }
    }

    [SerializeField] bool movecheck;//자기 차례를 확인하기 위한 체크
    bool touchcheck;//플레이어접촉에 관한 부분
    float turntimes = 0.1f;//0.1초간의 시간을 줘서 바로 이동하지 않도록 조절함
    //업었는지 안 업었는지 체크하는 코드
    //bool CurryBlue;
    //bool CurryRed;
    [Header("지름길에 관련된 부분을 관리")]
    [SerializeField] bool shortcutCheck;//지름에 도착했다면 체크하는 부분
    [SerializeField] float pastYutcount1;//윷에 나온 숫자만큼 더하여 움직일 위치를 보여주는 부분(지름길에 있을 경우)
    [SerializeField] float pastYutcount2;
    [SerializeField] float pastYutcount3;
    [SerializeField]int countYut;//1 이라면 5번 지름길,2이라면 10번 지름길, 3번이라면 22번 32번 지름
    ////말이 결승점을 지나갈때 위치가 안뜨도록 설정
    //public bool Exitcheck1;
    //public bool Exitcheck2;
    //public bool Exitcheck3;
    bool NextTurnCheck;

    public enum eRule
    {
        notrecall,//자기 차례가 아닐때 작동되는 부분
        playermovecheck,//원하는 위치로 이동 되는 부분
        endcheck,//자기 차례가 끝나면 대기 하는곳
        turntime,//턴을 급하게 넘기지 않도록 조절하는곳
    }
    private eRule yutcontrol = eRule.notrecall;

    private void Awake()
    {
        //Gamemanager.Instance.Player = this;
    }

    void Start()
    {
        startmypos = transform.position;
        Gamemanager.Instance.Player = this;
        maxmoveYutcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //checkplayermouse();
        //typeplayer();

        //testcode();
        //postest();
        if (yutcontrol == eRule.notrecall)
        {
            //testcode();
            if(movecheck == true)
            {
                yutcontrol = eRule.playermovecheck;
            }
            else
            {
                movecheck = false;
                return;
            }
        }
        else if(yutcontrol == eRule.playermovecheck)
        {
            positionselect();
        }

    }


    #region
    //private void checkplayermouse()//마우스로 플레이어를 선택 할때 작동
    //{
    //    if(playertouch == true && playerchoice == true)
    //    {
    //        //if(playercheck1 == true)
    //        //{
    //        //    playerchecking1 = true;
    //        //    playerchecking2 = false;
    //        //}
    //        //else if(playercheck2 == true)
    //        //{
    //        //    playerchecking1 = false;
    //        //    playerchecking2 = true;
    //        //}
    //        Testcode();
    //        playerchoice = false;
    //        //tests = true;
    //        Debug.Log("선택됨");
    //    }
    //    playerchoice = false;
    //}

    //private void typeplayer()//플레이어1 과 플레이어2를 동시에 못 움직이게 설정
    //{
    //    //if(playertype1 == true)
    //    //{
    //    //    playertype2 = false;
    //    //    checkobj.SetActive(true);
    //    //}
    //    //else if(playertype2 == true)
    //    //{
    //    //    playertype1 = false;
    //    //    checkobj.SetActive(true);
    //    //}
    //}

    //public void testcode()//위치선택하는 부분
    //{
    //    //플레이어가 가져와야 할것 윷에서 나온 숫자,발판의 숫자
    //    GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
    //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();   

    //    oneYut = buttontimer.oneyut;
    //    twoYut = buttontimer.twoyut;
    //    threeYut = buttontimer.threeyut;

    //    GameObject objfoot = GameObject.Find("footholdbox");
    //    Footholdbox footholdbox = objfoot.GetComponent<Footholdbox>();
    //    footholdbox.zerocheck = true;

    //    //Gamemanager.Instance.Numberroom.mynumber += a;
    //    //Gamemanager.Instance.Numberroom.zerocheck = true;

    //    //GameObject obj = GameObject.Find("footholdbox");
    //    //Numberroom numberroom = obj.GetComponentInChildren<Numberroom>();
    //    //numberroom.count1 = a;
    //    //numberroom.count2 = b;
    //    //numberroom.count3 = c;
    //}

    //private void playertypechoice()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Physics.Raycast(ray, );
    //}
    #endregion
    public void Playselectedcheck(bool _value)
    {
        findplayd.SetActive(_value);
        if(_value == true)
        {
            movecheck = true;
            moveyutcount();
            //Gamemanager.Instance.AgainStartPos(startmypos);
        }
        else if(_value == false)
        {
            movecheck = false;
        }

    }

    //public void Selectlocation()//플레이어가 선택될때 움직일수 있는 칸을 보여주는 부분
    //{
    //    GameObject obj = GameObject.Find("footholdbox");
    //    Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
    //}


    public void moveyutcount()//자신의 위치를 이동 할 부분에 분배
    {
        //한번만 체크
        #region
        //if(myyutturn == true)
        //{
        //    myyutturn = false;
        //    moveYutcount1 = MaxmoveYutcount;
        //    moveYutcount2 = MaxmoveYutcount;
        //    moveYutcount3 = MaxmoveYutcount;
        //    yutcontrol = eRule.turncheck;
        //    Debug.Log(MaxmoveYutcount);
        //}
        #endregion
        if(shortcutCheck == true)//지름길에 존재하고 있을 경우
        {
            pastYutcount1 = maxmoveYutcount;
            pastYutcount2 = maxmoveYutcount;
            pastYutcount3 = maxmoveYutcount;
        }
        moveYutcount1 = maxmoveYutcount;
        moveYutcount2 = maxmoveYutcount;
        moveYutcount3 = maxmoveYutcount;
        //yutcontrol = eRule.turncheck;
        countyutcheck();
        yutmoving();

        //Debug.Log(MaxmoveYutcount);
        //yutcontrol = eRule.playermovecheck;
    }

    private void yutmoving()//윷이 움직일 위치
    {
        if (shortcutCheck == true)
        {
            countShortcut();
            moveYutcount1 = maxmoveYutcount;
            moveYutcount2 = maxmoveYutcount;
            moveYutcount3 = maxmoveYutcount;
        }
        yutPosCount1();
        yutPosCount2();
        yutPosCount3();
        //moveYutcount1 += oneYut;
        //moveYutcount2 += twoYut;
        //moveYutcount3 += threeYut;
        Gamemanager.Instance.Footholdbox.findposition(moveYutcount1, moveYutcount2, moveYutcount3, maxmoveYutcount);
        //GameObject obj = GameObject.Find("footholdbox");
        //Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
        //footholdbox.findposition(moveYutcount1,moveYutcount2,moveYutcount3,MaxmoveYutcount);
        //footholdbox.movecheckchange(oneYut, twoYut, threeYut);
    }

    public void ChangeYutPos(float _Pos1, float _Pos2, float _Pos3)//중앙에서 빽도로 빠질때 값을 정상으로 넣기 위한 작업
    {
        moveYutcount1 = _Pos1;
        moveYutcount2 = _Pos2;
        moveYutcount3 = _Pos3;
    }
   


    public void countyutcheck()//윷의 숫자를 대입하는 코드부분
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();

        oneYut = buttontimer.oneyut;
        twoYut = buttontimer.twoyut;
        threeYut = buttontimer.threeyut;
        if(oneYut == -1 && maxmoveYutcount == 0)//돌린 윷이 빽도인데 대기중인 플레이어를 선택할 경우 그냥 체크를 안함
        {
            //Gamemanager.Instance.Footholdbox.positiondestory();
            //Yutorder = 1;
            //changeYutzero();
            return;
        }
    }

    private void positionselect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true && movecheck == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, LayerMask.GetMask("Ground"));

            if (rayHit.transform != null)
            {
                if(shortcutCheck == true)
                {
                    fastpositionselect(rayHit);
                }
                GameObject obj1 = Gamemanager.Instance.Footholdbox.poscheck1;
                GameObject obj2 = Gamemanager.Instance.Footholdbox.poscheck2;
                GameObject obj3 = Gamemanager.Instance.Footholdbox.poscheck3;

                //if(obj1.transform.position == new Vector3(0,-10,0))
                //{
                //    return;
                //}

                //Debug.Log(rayHit.transform.gameObject.name);

                if (rayHit.transform.gameObject == obj1)
                {
                    transform.position = rayHit.transform.position;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    maxmoveYutcount = moveYutcount1;
                    Yutorder = 1;
                    //Gamemanager.Instance.MovePlayerFootHold(gameObject,(int)MaxmoveYutcount);
                    //Gamemanager.Instance.holdboxPosCheck(MaxmoveYutcount);
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else if(rayHit.transform.gameObject == obj2)
                {
                    transform.position = rayHit.transform.position;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    maxmoveYutcount = moveYutcount2;
                    Yutorder = 2;
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else if (rayHit.transform.gameObject == obj3)
                {
                    transform.position = rayHit.transform.position;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    maxmoveYutcount = moveYutcount3;
                    Yutorder = 3;
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                //Debug.Log(rayHit.transform.gameObject);
                Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)maxmoveYutcount);
                Gamemanager.Instance.holdboxPosCheck(maxmoveYutcount,gameObject);
                //Gamemanager.Instance.DebugTest();
                //Gamemanager.Instance.PastlLoadCheck((int)MaxmoveYutcount, gameObject);
                if(NextTurnCheck == true)
                {
                    NextTurnCheck = false;
                    Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
                }
                movecheck = false;
            }
            //Debug.Log(rayHit.transform.gameObject.name);
        }
    }

    private void fastpositionselect(RaycastHit2D rayHit)//지름길에 들어올경우
    {
        GameObject obj1 = Gamemanager.Instance.Footholdbox.shortcutcheck1;
        GameObject obj2 = Gamemanager.Instance.Footholdbox.shortcutcheck2;
        GameObject obj3 = Gamemanager.Instance.Footholdbox.shortcutcheck3;

        if (rayHit.transform.gameObject == obj1)
        {
            transform.position = rayHit.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            maxmoveYutcount = pastYutcount1;
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
        else if (rayHit.transform.gameObject == obj2)
        {
            transform.position = rayHit.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            maxmoveYutcount = pastYutcount2;
            Yutorder = 2;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
        else if (rayHit.transform.gameObject == obj3)
        {
            transform.position = rayHit.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            maxmoveYutcount = pastYutcount3;
            Yutorder = 3;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
        else
        {
            return;
        }
        //Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)MaxmoveYutcount);
        //Gamemanager.Instance.holdboxPosCheck(MaxmoveYutcount, gameObject);
    }

    public void ManagerYutorderCheck(float _ClearNumber)//나가기 버튼을 누를때 이동을 한 윷의 숫자를 초기화
    {
        //Debug.Log(gameObject);
        Yutorder = _ClearNumber;
        changeYutzero();
    }

    private void changeYutzero()//말을 움직인 후에 이동한 숫자를 0으로 만드는 코드
    {
        //GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        //Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        Yutstartbutton buttontimer = Gamemanager.Instance.Yutstartbuttons;
        switch(Yutorder)
        {
            case 1:
                buttontimer.oneyut = 0;
                oneYut = buttontimer.oneyut;
                //buttontimer.NotCheck1();
                break;
            case 2:
                buttontimer.twoyut = 0;
                twoYut = buttontimer.twoyut;
                //buttontimer.NotCheck2();
                break;
            case 3:
                buttontimer.threeyut = 0;
                threeYut = buttontimer.threeyut;
                //buttontimer.NotCheck3();
                break;
        }
        //posmovecheck();
        MoveCheckControl();
        #region
        //pointCombine();//갈림길에서 빽도가 뜨면 이상한데에 표시가 뜨는 오류를 고치기 위한 코드
        //pointCombine2();
        //goPlayer = true;//움직이기 시작하면 true
        //movecheck = false;
        //shortcutCheck = false;
        //yutcontrol = eRule.notrecall;
        ////Gamemanager.Instance.Footholdbox.movecheckchange(oneYut, twoYut, threeYut);
        //Gamemanager.Instance.Footholdbox.movedestory();
        ////Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
        //findplayd.SetActive(false);
        #endregion
        //Debug.Log(moveYutcount1);
        //Debug.Log(moveYutcount2);
        //Debug.Log(moveYutcount3);
        //Debug.Log(MaxmoveYutcount);
        if(oneYut + twoYut + threeYut == 0)
        {
            NextTurnCheck = true;
        }
    }

    public void MoveCheckControl()
    {
        pointCombine();//갈림길에서 빽도가 뜨면 이상한데에 표시가 뜨는 오류를 고치기 위한 코드
        pointCombine2();
        goPlayer = true;//움직이기 시작하면 true
        //movecheck = false;
        shortcutCheck = false;
        yutcontrol = eRule.notrecall;
        Gamemanager.Instance.Footholdbox.movedestory();
        findplayd.SetActive(false);
    }

    private void pointCombine()//딱 결승점에 도착 할 경우 결승점의 maxmoveYutcount를 50번으로 통일 시킨다
    {
        float point = maxmoveYutcount;
        if (point == 20 || point == 31 || point == 37 || point == 41)
        {
            maxmoveYutcount = 49;
        }
    }

    private void pointCombine2()//3번째 위치를 통일 시키기
    {
        float point = maxmoveYutcount;
        if(point == 15 || point == 26 || point == 44)
        {
            maxmoveYutcount = 44;
        }
    }

    private void postest()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Player player = GetComponent<Player>();
            Debug.Log(player.gameObject);
            Debug.Log(player.maxmoveYutcount);
        }
    }

    public void AgainStartPos()//말이 잡히고 다시 처음으로 돌아가는 코드
    {
        goPlayer = false;
        transform.position = startmypos;
        maxmoveYutcount = 0;
        NotShortcutArrive();
    }

    //private void posmovecheck()//이동후에 플레이어들 위치 확인 해주는 부분
    //{
    //    int count = Gamemanager.Instance.objblue.Count;
    //    for(int iNum = 0; iNum < count; iNum++)
    //    {
            
    //    }
    //}

    public void DesTeam()//업는 경우 잠시 사라지게 만드는 코드 부분
    {
        goPlayer = false;
        transform.position = startmypos;
        maxmoveYutcount = 0;
        gameObject.SetActive(false);
        Curryobj1.SetActive(false);
        NotShortcutArrive();
    }

    /// <summary>
    /// 말을 업기위해 작동하는 코드
    /// </summary>
    /// <param name="_Currycheck">이미 말을 하나 업었다면 true</param>
    /// <param name="_teamtype">팀 타입 즉 레드팀이 들어오는지 블루팀이 들어오는지 확인</param>
    public void CurryTeam(bool _Currycheck)//업을 말을 생성
    {
        //if(_teamtype == 1)
        //{
        //    CurryBlue = true;
        //}
        //else if(_teamtype == 2)
        //{
        //    CurryRed = true;
        //}

        if(_Currycheck == true)
        {
            Curryobj1.SetActive(true);
            Curryobj2.SetActive(true);
            Gamemanager.Instance.WithdrawalCheck();
        }
        else
        {
            if (Curryobj1.activeSelf == true)//activeSelf == 오브젝트가 켜저있는지 꺼져있는지 확인해주는 코드
            {
                Curryobj2.SetActive(true);
            }
            else if (Curryobj1.activeSelf == false)
            {
                Curryobj1.SetActive(true);
            }
            Gamemanager.Instance.CheckCurry();
        }
    }

    public void DesCurryTeam()//업은 말을 삭제
    {
        Curryobj1.SetActive(false);
        Curryobj2.SetActive(false);
    }


    public void ShortcutArrive(int _countYut)//지름길에 없을때 작동되는 코드
    {
        countYut = _countYut;
        shortcutCheck = true;
        if(countYut == 3)
        {
            maxmoveYutcount = 38;
        }
        else if(countYut == 4)
        {
            maxmoveYutcount = 44;
        }
    }

    public void NotShortcutArrive()//지름길에 없을때 작동되는 코드
    {
        shortcutCheck = false;
    }

    //지름길만 따로 관리해주는 코드 부분들 (복잡해질까봐 따로 빼놓음)

    private void countShortcut()//지름길 부분을 따로 관리
    {
        switch (countYut)
        {
            case 1://첫번째 줄에 있는 지름길
                pastYutcount1 = pastYutcount1 + 15 + oneYut;
                pastYutcount2 = pastYutcount2 + 15 + twoYut;
                pastYutcount3 = pastYutcount3 + 15 + threeYut;
                BackYutCheck();
                Gamemanager.Instance.Footholdbox.fastfindposition(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                break;
            case 2://2번째 줄에 있는 지름길
                pastYutcount1 = pastYutcount1 + 21 + oneYut;
                pastYutcount2 = pastYutcount2 + 21 + twoYut;
                pastYutcount3 = pastYutcount3 + 21 + threeYut;
                BackYutCheck();
                Gamemanager.Instance.Footholdbox.Centerfindposition(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                break;
            case 3://지금 가장 중앙에 있을 경우
                maxmoveYutcount = 38;
                pastYutcount1 = 41 + oneYut;
                pastYutcount2 = 41 + twoYut;
                pastYutcount3 = 41 + threeYut;
                Gamemanager.Instance.Footholdbox.lastfindposition(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                break;
            case 4:
                if (MaxmoveYutcount == 44)
                {
                    if (oneYut == -1)
                    {
                        pastYutcount1 -= 1;
                    }
                    else if (twoYut == -1)
                    {
                        pastYutcount2 -= 1;
                    }
                    else if (threeYut == -1)
                    {
                        pastYutcount3 -= 1;
                    }
                    Gamemanager.Instance.Footholdbox.DesPos(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                }
                break;
        }
    }

    public void LastChangeYutPos(float _Pos1, float _Pos2, float _Pos3)//중앙에서 빽도로 빠질때 값을 정상으로 넣기 위한 작업
    {
        pastYutcount1 = _Pos1;
        pastYutcount2 = _Pos2;
        pastYutcount3 = _Pos3;
    }

    public void BackYutCheck()
    {
        if(oneYut == -1)
        {
            pastYutcount1 += 1;
        }
        else if(twoYut == -1)
        {
            pastYutcount2 += 1;
        }
        else if(threeYut == -1)
        {
            pastYutcount3 += 1;
        }
    }

    //리스트로 말 이동 테스트코드 부분
    private void yutPosCount1()
    {
        if (maxmoveYutcount == 49 && oneYut > 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.ExitPlayer1();
            Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
            return;
        }
        //현재 찾고싶은 리스트 속 이름 => chagefoothold
        for (int iNum = 0; iNum < oneYut; iNum++)
        {
            Transform moveYut = Gamemanager.Instance.Footholdbox.findYut(Gamemanager.Instance.Footholdbox.Yutfoothold[(int)moveYutcount1 + iNum].gameObject);
            Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[0].transform;
            //Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform;
            //문제: 하나하나씩 올리다 보니 결승점을  지나는리스트와 겹치니 그대로 들어감 => 해결
            if (names == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount1 += iNum;
                Yutorder = 1;
                Gamemanager.Instance.Footholdbox.ExitPlayer1();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
            else if(Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount1 += iNum;
                Yutorder = 1;
                Gamemanager.Instance.Footholdbox.ExitPlayer1();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
        }
        moveYutcount1 += oneYut;
        if(moveYutcount1 == 0 && oneYut == -1)//빽도 변수 처리들
        {
            moveYutcount1 = 49;
        }
        else if(maxmoveYutcount == 21 && oneYut == -1)
        {
            moveYutcount1 = 5;
        }
        else if(maxmoveYutcount == 32 && oneYut == -1)
        {
            moveYutcount1 = 10;
        }
        else if(maxmoveYutcount == 44  && oneYut == -1)
        {
            moveYutcount1 = 14;
        }

        if(maxmoveYutcount == 42)
        {
            maxmoveYutcount = 24;
        }
        //else if (Gamemanager.Instance.Footholdbox.Yutfoothold[15].gameObject && oneYut == -1)
        //{
        //    Debug.Log("테스트");
        //}
    }
    private void yutPosCount2()
    {
        if (maxmoveYutcount == 49 && twoYut > 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.ExitPlayer1();
            Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
            return;
        }
        //현재 찾고싶은 리스트 속 이름 => chagefoothold
        for (int iNum = 0; iNum < twoYut; iNum++)
        {
            Transform moveYut = Gamemanager.Instance.Footholdbox.findYut(Gamemanager.Instance.Footholdbox.Yutfoothold[(int)moveYutcount2 + iNum].gameObject);
            Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[0].transform;
            //Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform;
            if (names == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount2 += iNum;
                Yutorder = 2;
                Gamemanager.Instance.Footholdbox.ExitPlayer2();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
            else if (Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount2 += iNum;
                Yutorder = 2;
                Gamemanager.Instance.Footholdbox.ExitPlayer2();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
        }
        moveYutcount2 += twoYut;
        if (moveYutcount2 == 0 && twoYut == -1)
        {
            moveYutcount2 = 49;
        }
        else if (maxmoveYutcount == 21 && twoYut == -1)
        {
            moveYutcount2 = 5;
        }
        else if (maxmoveYutcount == 32 && twoYut == -1)
        {
            moveYutcount2 = 10;
        }
        else if (maxmoveYutcount == 44 && twoYut == -1)
        {
            moveYutcount2 = 14;
        }
        if (maxmoveYutcount == 42)
        {
            maxmoveYutcount = 24;
        }

    }
    private void yutPosCount3()
    {
        if (maxmoveYutcount == 49 && threeYut > 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.ExitPlayer1();
            Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
            return;
        }
        //현재 찾고싶은 리스트 속 이름 => chagefoothold
        for (int iNum = 0; iNum < threeYut; iNum++)
        {
            Transform moveYut = Gamemanager.Instance.Footholdbox.findYut(Gamemanager.Instance.Footholdbox.Yutfoothold[(int)moveYutcount3 + iNum].gameObject);
            Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[0].transform;
            //Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform;
            if (names == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount3 += iNum;
                Yutorder = 3;
                Gamemanager.Instance.Footholdbox.ExitPlayer3();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
            else if (Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount3 += iNum;
                Yutorder = 3;
                Gamemanager.Instance.Footholdbox.ExitPlayer3();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
        }
        moveYutcount3 += threeYut;
        if (moveYutcount3 == 0 && threeYut == -1)
        {
            moveYutcount3 = 49;
        }
        else if (maxmoveYutcount == 21 && threeYut == -1)
        {
            moveYutcount3 = 5;
        }
        else if (maxmoveYutcount == 32 && threeYut == -1)
        {
            moveYutcount3 = 14;
        }
        else if (maxmoveYutcount == 44 && threeYut == -1)
        {
            moveYutcount3 = 14;
        }
        if (maxmoveYutcount == 42)
        {
            maxmoveYutcount = 24;
        }
    }

    public void DesObj(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        //findplayd.SetActive(false);
        Gamemanager.Instance.Footholdbox.movedestory();
    }

    public void ExitTurnPass()//탈출 버튼을 눌러 나갔을 경우 턴을 넘기는 것
    {
        oneYut = Gamemanager.Instance.Yutstartbuttons.oneyut;
        twoYut = Gamemanager.Instance.Yutstartbuttons.twoyut;
        threeYut = Gamemanager.Instance.Yutstartbuttons.threeyut;
        changeYutzero();
        Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)maxmoveYutcount);
        Gamemanager.Instance.holdboxPosCheck(maxmoveYutcount, gameObject);
        if (NextTurnCheck == true)
        {
            NextTurnCheck = false;
            Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
        }
        movecheck = false;
    }

    public void DesYutPlayer()
    {
        if (Curryobj1.activeSelf == true)
        {
            if (Curryobj2.activeSelf == true)
            {
                Gamemanager.Instance.ListClear();
            }
            else
            {
                Gamemanager.Instance.LookAtYutPlayer();
            }
        }
    }
}