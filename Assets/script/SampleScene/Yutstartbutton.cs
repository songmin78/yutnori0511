using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Yutstartbutton : MonoBehaviour
{
    List<TMP_Text> TextList;
    [Header("윷가락")]
    [SerializeField] GameObject yut1;
    [SerializeField] GameObject yut2;
    [SerializeField] GameObject yut3;
    [SerializeField] GameObject yut4;
    [Header("윷으로 갈수있는 숫자 최대 3번까지 저장 가능")]
    float yutnumber = 0;
    public float Yutnumber
    {
        get
        {
            return yutnumber;
        }
    }
    //bool yutreturncheck;
    [SerializeField, Tooltip("윷던진후에 갈수있는 수를 저장")] public float oneyut = 0;
    [SerializeField, Tooltip("윷던진후에 갈수있는 수를 저장")] public float twoyut = 0;
    [SerializeField, Tooltip("윷던진후에 갈수있는 수를 저장")] public float threeyut = 0;
    public bool zeromovecheck;

    //float yuttype;
    [Header("윷 던지기 버튼")]
    //0은 앞면 1은 뒷면 즉 빽도는 0 0 0 1이 떠야함
    [SerializeField] Button startbutton;
    bool yutstart;
    bool randomcheck;//윷이 뭘 떴는지 확인 해주는 코드
    int randomcount;
    int Stickcount;
    //public bool waittime;//캐릭터를 움직이기위한 시간에 버튼을 눌러도 작동 안되게 설정
    public bool yutstarttimer;
    [Header("기 타")]
    //[SerializeField] Image timegage;
    //윷이 앞면인지 뒷면인 확인 해주는 부분
    int chageyut;
    [SerializeField]float stayyut = 1;//모 또는 윷에 걸릴때 잠깐 기다리는 시간
    bool stayyutcheck;//1초 기다리는 시간을 체크
    [Header("스크립트오브젝트를 관리")]
    [SerializeField] GameObject Playtimemanager;
    bool oneYutCheck;
    bool twoYutCheck;
    bool threeYutCheck;
    bool reCheck;//모나 윷이 뜨면 잠깐 true로 변경
    [Header("보여주는 윷 코드 부분")]
    [SerializeField] GameObject Look1;
    [SerializeField] GameObject Look2;
    [SerializeField] GameObject Look3;
    [SerializeField] GameObject Look4;
    [SerializeField] TMP_Text LookText;
    bool recycleCheck;//모나 윷이 뜰때 시간 코드가 안 돌아가게 도와주는 코드

    [Header("윷 보여주기 등록")]
    [SerializeField] TMP_Text Blue1;
    [SerializeField] TMP_Text Blue2;
    [SerializeField] TMP_Text Blue3;
    [SerializeField] TMP_Text Red1;
    [SerializeField] TMP_Text Red2;
    [SerializeField] TMP_Text Red3;

    public enum eRule
    {
        YutStartButton1,
        YutStartButton2,
        YutStartButton3,
    }
    private eRule curButton = eRule.YutStartButton1;

    [SerializeField] List<int> yutdisposition = new List<int>();
    private void Awake()
    {
        startbutton.onClick.AddListener(() =>
        {
            //if (waittime == true)
            //{
            //    return;
            //}
            yutdisposition.Clear();
            Stickcount = 0;
            yutstart = true;

        });
    }

    private void Start()
    {
        Gamemanager.Instance.Yutstartbuttons = this;
        curButton = eRule.YutStartButton3;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Gamemanager.Instance.throwyutbutton == false)
        //{
        //    return;
        //}
        //yutplaytimer();
        //startyutnbutton();//윷 던지기 버튼
        //resetyut();// 윷이 모 또는 윷에 걸릴 경우
        //returnyut();

        //numberzero();//이동 거리 초기화
        //moveyut();

        if (curButton == eRule.YutStartButton1)
        {
            startyutnbutton();
        }
        else if(curButton == eRule.YutStartButton2)
        {
            resetyut();
        }
        else if(curButton == eRule.YutStartButton3)
        {
            return;
        }

    }

    public void yutplaytimer()
    {
        //if (yutstarttimer == true)
        //{
        //    yutdisposition.Clear();
        //    Stickcount = 0;
        //    yutstart = true;
        //    yutstarttimer = false;
        //}

        //코드 최적화 중
        yutdisposition.Clear();
        Stickcount = 0;
        yutstart = true;
    }


    private void startyutnbutton()//윷 던지기 버튼
    {
        if(Gamemanager.Instance.TutorialStageCheck == true && yutstart == true)
        {
            if(Gamemanager.Instance.TutorialStory.StoryNumber == 4)
            {
                yutstart = false;
                tutorialYutNumber();
            }
            else if (Gamemanager.Instance.TutorialStory.StoryNumber == 12)
            {
                yutstart = false;
                tutorialYut2();
            }
            else if(Gamemanager.Instance.TutorialStory.StoryNumber == 15)
            {
                yutstart = false;
                tutorialYut3();
            }
            else if (Gamemanager.Instance.TutorialStory.StoryNumber == 17)
            {
                yutstart = false;
                tutorialYut4();
            }
            return;
        }
        if (stayyutcheck == true)
        {
            return;
        }

        #region 특정키를 누르면 뜨게 만드는 부분
        //특정 윷번호를 얻기 위한 코드들
        //if (Input.GetKeyDown(KeyCode.N))//확정 윷가 뜨도록 변경
        //{
        //    yutnumber = 5;
        //    Stickcount = 0;
        //    for (int yutstick = 0; yutstick < 4; yutstick++)
        //    {
        //        randomcount = 1;
        //        chageyut += 1;
        //        Yutcount();
        //    }
        //    yutlist();
        //    if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        //    {
        //        Gamemanager.Instance.nextturn();
        //        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        //        return;
        //    }
        //    recycleCheck = true;
        //    Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        //    recycleCheck = false;
        //    if (yutnumber == 4 || yutnumber == 5)
        //    {
        //        //stayyutcheck = true;
        //        //playtimer.returnYut = true;
        //        curButton = eRule.YutStartButton2;
        //    }
        //}
        //if(Input.GetKeyDown(KeyCode.B))//걸이 뜨게 만드는 부분
        //{
        //    yutnumber = 3;
        //    Stickcount = 3;
        //    for (int yutstick = 0; yutstick < 3; yutstick++)
        //    {
        //        randomcount = 1;
        //        chageyut += 1;
        //        Yutcount();
        //    }
        //    yutlist();
        //    yutstart = false;
        //    if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        //    {
        //        Gamemanager.Instance.nextturn();
        //        return;
        //    }
        //    Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//모나 윷이 아니라서 false가 입력
        //    Gamemanager.Instance.throwyutbutton = false;
        //}
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    randomcheck = true;
        //    yutnumber = -1;
        //    Stickcount = 0;
        //    for (int yutstick = 0; yutstick < 4; yutstick++)
        //    {
        //        randomcount = 1;
        //        chageyut += 1;
        //        Yutcount();
        //    }
        //    yutturnNumber();
        //    Debug.Log("빽도");
        //    LookText.text = "빽도";
        //    yutstart = false;
        //    if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        //    {
        //        Gamemanager.Instance.nextturn();//강제적으로 플레이어 선택 부분으로 이동
        //    }
        //    else
        //    {
        //        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        //        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        //    }

        //    //Gamemanager.Instance.Playtimer.PassChange();
        //    Gamemanager.Instance.throwyutbutton = false;
        //    if(oneyut == -1)
        //    {
        //        Gamemanager.Instance.CheckBackYutPass();
        //    }
        //    else
        //    {
        //        Gamemanager.Instance.PlayTimeTurn();
        //    }
        //    randomcheck = false;
        //    return;
        //}
        //여기까지
        #endregion

        //0은 앞면 1은 뒷면 즉 빽도는 0 0 0 1이 떠야함
        if (yutstart == true)
        {
            reCheck = false;
            #region
            //for(int yutstick = 0; yutstick < 4; yutstick++)//윷을 빽도를 제외한 3개를 던져 리스트에 나열 하도록 설정
            //{
            //    test = Random.Range(0, 2);//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
            //    yutdisposition.Add(test);
            //    //Debug.Log(test);
            //    //yutdisposition.Add(Random.Range(0, 2));//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
            //    randomcheck = true;
            //}
            //yutdisposition.Add(Random.Range(0, 2));
            #endregion

            for (int yutstick = 0; yutstick < 4; yutstick++)//윷을 빽도를 제외한 3개를 던져 리스트에 나열 하도록 설정
            {
                randomcount = Random.Range(0, 2);//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
                if (randomcount == 1)//뒷면에 걸릴 경우
                {
                    if (yutstick == 3 && Stickcount == 0)//4번째 윷이면서 이전에 다 앞면이 뜰경우
                    {
                        Stickcount += 0;
                    }
                    else
                    {
                        Stickcount += randomcount;
                    }
                    chageyut += 1;
                    Yutcount();
                }
                else// 0 즉 앞면에 걸릴 경우
                {
                    chageyut += 1;
                    Yutcount();
                }
                if (yutstick == 3 && Stickcount == 0 && randomcount == 1)//마지막 윷에서 전부 앞면 이면서 마지막 윷만 뒷면일 경우(빽도)
                {
                    #region
                    //randomcheck = true;
                    //yutnumber = -1;
                    //yutturnNumber();
                    //Debug.Log("빽도");
                    //if(oneyut == -1)
                    //{
                    //    Gamemanager.Instance.CheckBackYutPass();
                    //}
                    #endregion
                    backPass();
                    return;
                }
                yutdisposition.Add(randomcount);
            }
            //Debug.Log(Stickcount);
            yutlist();
            yutstart = false;
            if (oneyut != 0 && twoyut != 0 && threeyut != 0 && reCheck == true)//3번연속으로 모 또는 윷이 뜰 경우
            {
                Gamemanager.Instance.nextturn();
                Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
                return;
            }
            //GameObject findtimer = GameObject.Find("Playtimemanager");
            if (yutnumber == 4 || yutnumber == 5)//모 또는 윷이 뜰 경우
            {
                curButton = eRule.YutStartButton2;
                recycleCheck = true;
                //stayyutcheck = true;
                if (threeyut != 0)
                {
                    recycleCheck = false;
                    return;
                }
                //playtimer.returnYut = true;
            }
            else//모 또는 윷이 안뜰 경우
            {
                Gamemanager.Instance.throwyutbutton = false;
            }
            Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
            Gamemanager.Instance.RecycleTurnPass(recycleCheck);
            recycleCheck = false;
        }

    }

    private void backPass()//빽도에 걸렸을때 턴을 넘길수 있게 해주는 코드
    {
        randomcheck = true;
        yutnumber = -1;
        Stickcount = 0;
        for (int yutstick = 0; yutstick < 4; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        Debug.Log("빽도");
        LookText.text = "빽도";
        yutturnNumber();
        yutstart = false;
        if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        {
            Gamemanager.Instance.nextturn();//강제적으로 플레이어 선택 부분으로 이동
        }
        else
        {
            Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        }
        Gamemanager.Instance.throwyutbutton = false;
        if (oneyut == -1)
        {
            Gamemanager.Instance.CheckBackYutPass();
        }
        else
        {
            Gamemanager.Instance.PlayTimeTurn();
        }
        randomcheck = false;
    }

    #region
    //private void moveyut()//윷이 뜬 수 만큼 이동할 거리를 정해줌
    //{
    //    if(randomcheck == true)
    //    {
    //        for(int yutcheck = 0; yutcheck < 4; yutcheck++)
    //        {
    //            //int a = yutdisposition.Find(test => test >= 0);
    //            int a = yutdisposition.Contains();
    //            if(a == 1)
    //            {
    //                Debug.Log("1");
    //            }
    //            else
    //            {
    //                Debug.Log("0");
    //            }
    //        }
    //        randomcheck = false;
    //    }
    //    //리스트에 1이 얼마나 있는지 확인하는 코드
    //}
    #endregion

    private void yutlist()//윷에 뭐가 떴는지 확인 해주는 코드
    {
        if (randomcheck == true)//만약에 빽도가 될시
        {
            randomcheck = false;
            return;
        }
        //Stickcount가 0일경우 모, 1일 경우 도 또는 빽도, 2일 경우 개, 3일경우 걸, 4일경우 윷
        switch (Stickcount)//뒷면이  얼마나 떴는지 swich문으로 체크
        {
            case 0://뒷면이 0개일 경우
                yutnumber = 5;
                reCheck = true;
                Debug.Log("모");
                LookText.text = "모";
                yutturnNumber();
                break;
            case 1:
                yutnumber = 1;
                Debug.Log("도");
                LookText.text = "도";
                yutturnNumber();
                break;
            case 2:
                yutnumber = 2;
                Debug.Log("개");
                LookText.text = "개";
                yutturnNumber();
                break;
            case 3:
                yutnumber = 3;
                Debug.Log("걸");
                LookText.text = "걸";
                yutturnNumber();
                break;
            case 4:
                yutnumber = 4;
                reCheck = true;
                Debug.Log("윷");
                LookText.text = "윷";
                yutturnNumber();
                break;
        }
    }


    //이동 직후 그 윷을 0으로 만들어 버리니 1,2번째 윷 이동을 쓰고 잡을때 1번째 윷이 다시 입력이 된다
    private void yutturnNumber()//윷에 걸린 숫자만큼 숫자를 대입
    {
        if (oneyut == 0 && oneYutCheck == false || oneYutCheck == false)
        {
            oneyut = yutnumber;
            if (Gamemanager.Instance.Playtimer.TeamBlue == true)
            {
                Blue1.text = LookText.text;
            }
            else
            {
                Red1.text = LookText.text;
            }
            NotCheck1();
        }
        else if (oneyut != 0 && twoyut == 0 && twoYutCheck == false || twoYutCheck == false)
        {
            twoyut = yutnumber;
            if (Gamemanager.Instance.Playtimer.TeamBlue == true)
            {
               Blue2.text = LookText.text;
            }
            else
            {
                Red2.text = LookText.text;
            }
            NotCheck2();
        }
        else if (oneyut != 0 && twoyut != 0 && threeyut == 0 && threeYutCheck == false|| threeYutCheck == false)
        {
            threeyut = yutnumber;
            if (Gamemanager.Instance.Playtimer.TeamBlue == true)
            {
               Blue3.text = LookText.text;
            }
            else
            {
                Red3.text = LookText.text;
            }
            NotCheck3();
        }
    }


    public void numberzero()//이동 거리 초기화
    {
        //if (zeromovecheck == true)
        //{
        //    oneyut = 0;
        //    twoyut = 0;
        //    threeyut = 0;
        //    zeromovecheck = false;
        //}
        oneyut = 0;
        twoyut = 0;
        threeyut = 0;
    }

    private void Yutcount()//윷이 앞면에 뜨냐 뒷면에 뜨냐 보여주는 부분
    {
        #region
        //for (int yutnumber = 0; yutnumber < 4; yutnumber++)
        //{
        //    if(yutdisposition[testd] == 0)
        //    {
        //        Debug.Log(yutdisposition);
        //    }
        //    else if(yutdisposition[testd] == 1)
        //    {
        //        //Debug.Log(yutdisposition);
        //    }
        //    if(yutdisposition.Find())
        //    {

        //    }
        //}
        #endregion
        //리스트로 만들고 싶은 부분 => 리스트 1번의 값이 0일 경우 윷을 앞면으로 돌리기 1일 경우 윷을 뒷면으로 돌리기
        switch (chageyut)
        {
            case 1:
                if (randomcount == 1)
                {
                    yut1.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look1.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 2:
                if (randomcount == 1)
                {
                    yut2.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look2.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 3:
                if (randomcount == 1)
                {
                    yut3.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look3.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 4:
                if (randomcount == 1)
                {
                    yut4.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look4.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
        }
    }


    private void resetyut()//윷이 모 또는 윷에 걸릴 경우
    {
        //if (stayyutcheck == true)
        //{
        //    //GameObject findtimer = GameObject.Find("Playtimemanager");
        //    Gamemanager.Instance.Playtimer.ReturnCheck();
        //    if (stayyut < 0)
        //    {
        //        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        stayyut = 1;
        //        stayyutcheck = false;
        //        Gamemanager.Instance.Playtimer.BackReturnCheck();
        //        chageyut = 0;
        //    }
        //    else
        //    {
        //        stayyut -= Time.deltaTime;
        //    }
        //}

        Gamemanager.Instance.Playtimer.ReturnCheck();
        if (stayyut < 0)
        {
            yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
            yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
            yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
            yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
            lookReturnYut();
            stayyut = 1;
            //stayyutcheck = false;
            Gamemanager.Instance.Playtimer.BackReturnCheck();
            chageyut = 0;
            curButton = eRule.YutStartButton1;
        }
        else
        {
            stayyut -= Time.deltaTime;
        }
    }

    public void getbackYut()//윷 모양을 다시 되 돌린다
    {
        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        lookReturnYut();
        chageyut = 0;
    }

   public void ClaerYutCount()
    {
        //yutlist();
        Gamemanager.Instance.nextturn();//플레이어턴으로 바꿈
        Gamemanager.Instance.Playtimer.PassChange();

        //zeromovecheck = true;
        NotCheckTrue();
        numberzero();
        //playtimer.checktime = true;
        //stayyutcheck = true;
    }

    public void ControlCheck1()
    {
        curButton = eRule.YutStartButton1;
    }

    public void CatchReTurnTurn()//적 플레이어를 잡았을때 다시 한번 던질 수있는 코드
    {
        if (threeyut != 0 || threeYutCheck == true)
        {
            return;
        }
        chageyut = 0;
        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        lookReturnYut();

        Gamemanager.Instance.RecycleTurnManager();
        curButton = eRule.YutStartButton1;
        Gamemanager.Instance.Playtimer.RecycleTurn();

    }

    public void NotCheck1()//한번 쓴 윷칸에 다시 못 들어오게 도와주는 부분
    {
        oneYutCheck = true;
    }
    public void NotCheck2()//한번 쓴 윷칸에 다시 못 들어오게 도와주는 부분
    {
        twoYutCheck = true;
    }
    public void NotCheck3()//한번 쓴 윷칸에 다시 못 들어오게 도와주는 부분
    {
        threeYutCheck = true;
    }

    public void NotCheckTrue()//윷값을 다시 쓸수 있게 도와주는 코드
    {
        oneYutCheck = false;
        twoYutCheck = false;
        threeYutCheck = false;
    }

    private void lookReturnYut()
    {
        Look1.transform.rotation = Quaternion.Euler(0, 0, 0);
        Look2.transform.rotation = Quaternion.Euler(0, 0, 0);
        Look3.transform.rotation = Quaternion.Euler(0, 0, 0);
        Look4.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TextNull()
    {
        LookText.text = "";
    }

    public void NullText()
    {
        Blue1.text = "";
        Blue2.text = "";
        Blue3.text = "";
        Red1.text = "";
        Red2.text = "";
        Red3.text = "";
    }

    private void tutorialYutNumber()//튜토리얼을 진행할때 윷을 강제적으로 제한 할수 있도록 만든 부분
    {
        yutnumber = 3;
        Stickcount = 3;
        for (int yutstick = 0; yutstick < 3; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        //yutdisposition.Add(randomcount);
        yutlist();
        yutstart = false;
        Gamemanager.Instance.throwyutbutton = false;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        Gamemanager.Instance.TutorialStory.TimeOff();
    }

    private void tutorialYut2()//튜토리얼을 진행할때 윷을 강제적으로 제한 할수 있도록 만든 부분
    {
        yutnumber = 2;
        Stickcount = 2;
        for (int yutstick = 0; yutstick < 2; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        //yutdisposition.Add(randomcount);
        yutlist();
        yutstart = false;
        Gamemanager.Instance.throwyutbutton = false;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        //Gamemanager.Instance.TutorialStory.TimeOff();
    }

    private void tutorialYut3()
    {
        //yutnumber = 5;
        //Stickcount = 0;
        //yutlist();
        //curButton = eRule.YutStartButton2;
        //recycleCheck = true;
        //yutstart = false;
        //Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        //recycleCheck = false;
        //Gamemanager.Instance.throwyutbutton = false;
        //Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        //Gamemanager.Instance.TutorialStory.TimeOff();
        //
        yutnumber = 5;
        Stickcount = 0;
        yutlist();
        if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        {
            Gamemanager.Instance.nextturn();
            Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
            return;
        }
        recycleCheck = true;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        recycleCheck = false;
        if (yutnumber == 4 || yutnumber == 5)
        {
            curButton = eRule.YutStartButton2;
        }
        Gamemanager.Instance.TutorialStory.TimeOff();
    }
    private void tutorialYut4()//튜토리얼을 진행할때 윷을 강제적으로 제한 할수 있도록 만든 부분
    {
        yutnumber = 2;
        Stickcount = 2;
        for (int yutstick = 0; yutstick < 2; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        yutlist();
        yutstart = false;
        Gamemanager.Instance.throwyutbutton = false;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        Gamemanager.Instance.TutorialStory.TimeOff();
    }
}
