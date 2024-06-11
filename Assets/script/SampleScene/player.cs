using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject findplayd;
    //[Header("플레이어")]
    //[SerializeField] bool playercheck1;//플레이어1번 부분
    //[SerializeField] bool playercheck2;//플레이어 2번 부분
    //[Header("플레이어를 선택 했을때")]
    //[SerializeField] public GameObject checkobj;//플레이러를 체크할때 보이는 오브젝트
    //[SerializeField] bool checkmask;//플레이어를 체크 할때
    //[SerializeField] Canvas playerselected;//플레이어가 선택될 때 체크되는걸 보여주는 오브젝트
    bool selectedcheck;//선택될때 체크

    bool buleTeam;

    [Header("팀 구분")]
    [SerializeField]bool teamred;
    [SerializeField]bool teamblue;
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
    float Yutorder;//말을 움직일 수있는 수 부분
    //[SerializeField] float d;
    //[SerializeField]public bool playertypenumber;
    [Header("윷을 움직이기위한 거리 부분")]
    private bool myyutturn;//자기 차례일때만 true로 변경
    [SerializeField] float moveYutcount1;//첫번째 윷에 나온 숫자만큼 더하여 어느정도 움직일지 미리 보여주는 부분
    [SerializeField] float moveYutcount2;//두번째 윷에 나온 숫자만큼 더하여 어느정도 움직일지 미리 보여주는 부분
    [SerializeField] float moveYutcount3;//세번째 윷에 나온 숫자만큼 더하여 어느정도 움직일지 미리 보여주는 부분
    [SerializeField] float MaxmoveYutcount;//이동후 자신의 위치를 저장
    [SerializeField] bool movecheck;
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    public enum eRule
    {
        notrecall,//자기 차례가 아닐때 작동되는 부분
        playermovecheck,//원하는 위치로 이동 되는 부분
        endcheck,//자기 차례가 끝나면 대기 하는곳
    }
    private eRule yutcontrol = eRule.notrecall;

    private void Awake()
    {
        //Gamemanager.Instance.Player = this;
        
    }

    void Start()
    {
        Gamemanager.Instance.Player = this;
        MaxmoveYutcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //checkplayermouse();
        //typeplayer();

        //testcode();
        if(yutcontrol == eRule.notrecall)
        {
            return;
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
        moveYutcount1 = MaxmoveYutcount;
        moveYutcount2 = MaxmoveYutcount;
        moveYutcount3 = MaxmoveYutcount;
        //yutcontrol = eRule.turncheck;
        countyutcheck();
        yutmoving();
        //Debug.Log(MaxmoveYutcount);
        yutcontrol = eRule.playermovecheck;
    }

    private void yutmoving()//윷이 움직일 위치
    {
        moveYutcount1 += oneYut;
        moveYutcount2 += twoYut;
        moveYutcount3 += threeYut;
        GameObject obj = GameObject.Find("footholdbox");
        Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
        footholdbox.findposition(moveYutcount1,moveYutcount2,moveYutcount3,MaxmoveYutcount);
    }

    public void countyutcheck()//윷의 숫자를 대입하는 코드부분
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();

        oneYut = buttontimer.oneyut;
        twoYut = buttontimer.twoyut;
        threeYut = buttontimer.threeyut;
    }

    private void positionselect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true && movecheck == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.transform != null)
            {
                GameObject obj1 = Gamemanager.Instance.movelocation1;
                GameObject obj2 = Gamemanager.Instance.movelocation2;
                GameObject obj3 = Gamemanager.Instance.movelocation3;

                if (rayHit.transform.gameObject == obj1 && obj1.gameObject == true)
                {
                    transform.position = rayHit.transform.position;
                    MaxmoveYutcount = moveYutcount1;
                    Yutorder = 1;
                    changeYutzero();
                    obj1.SetActive(false);
                    obj2.SetActive(false);
                    obj3.SetActive(false);
                }
                else if(rayHit.transform.gameObject == obj2 && obj2.gameObject ==true)
                {
                    transform.position = rayHit.transform.position;
                    MaxmoveYutcount = moveYutcount2;
                    Yutorder = 2;
                    changeYutzero();
                    obj1.SetActive(false);
                    obj2.SetActive(false);
                    obj3.SetActive(false);
                }
                else if (rayHit.transform.gameObject == obj3 && obj3.gameObject == true)
                {
                    transform.position = rayHit.transform.position;
                    MaxmoveYutcount = moveYutcount3;
                    Yutorder = 3;
                    changeYutzero();
                    obj1.SetActive(false);
                    obj2.SetActive(false);
                    obj3.SetActive(false);
                }
                //movepositioncheck();
            }
        }
    }

    private void changeYutzero()//말을 움직인 후에 이동한 숫자를 0으로 만드는 코드
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();

        switch(Yutorder)
        {
            case 1:
                buttontimer.oneyut = 0;
                oneYut = buttontimer.oneyut;
                break;
            case 2:
                buttontimer.twoyut = 0;
                twoYut = buttontimer.twoyut;
                break;
            case 3:
                buttontimer.threeyut = 0;
                threeYut = buttontimer.threeyut;
                break;
        }
        findplayd.SetActive(false);
        movecheck = false;
        yutcontrol = eRule.notrecall;
        Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
    }


    //private void objcheck()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
    //    if (rayHit.transform.tag == "player" && movecheck == false)
    //    {
    //        movecheck = true;
    //    }
    //    else if (movecheck == true)
    //    {
    //        movecheck = false;
    //    }
    //}

    private void outcheckplayer()//플레이어가 다른 플레이어 말을 잡았을 경우
    {

    }
}
