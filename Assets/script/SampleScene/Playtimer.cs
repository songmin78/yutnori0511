using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Playtimer : MonoBehaviour
{
    [Header("튜토리얼 부분(튜토리얼 스테이지에서만 쓰임)")]
    [SerializeField, Tooltip("튜토리얼 스테이지에 있을때 True로 쓰기")] bool tutorialStageCheck;
    float tutorialTime = 1.5f;
    float MaxtutorialTime;
    //bool buttonTutorial = true;
    float yutThrow = 1.5f;
    [SerializeField] float MaxyutThrow;
    bool tutorialCheck1 = true;
    bool tutorialCheck2 = true;

    Animator animator;
    [Header("일반게임")]
    [SerializeField] GameObject TestObj;
    [SerializeField] bool teamred;
    [SerializeField] bool teamblue;
    public bool TeamBlue
    {
        get
        {
            return teamblue;
        }
    }
    [Header("윷을 던지기 까지의 남은 시간 정리")]
    [SerializeField] float throwtime;
    [SerializeField] float Maxthrowtime;//확인 용
    bool throwwaitcheck;//던지기는 것을 기다림
    [Header("윷을 던진후 캐릭터 이동을 위한 지속 시간 정리")]
    [SerializeField] float waitmovetime;
    [SerializeField] float Maxwaitmovetime;//확인용
    bool playermovecheck;//윷을 던진후 움직이는것을 기다리는 부분
    [Header("기타")]
    //[SerializeField] public bool checktime;//윷을 던졌을때 true로 전환(밖에서 받아옴)
    [SerializeField] Image timegage;//시간초 줄어드는 게이지
    [SerializeField] Image timegagePlayer;//플레이어가 조종하는 시간초 줄어드는 게이지
    [SerializeField, Tooltip("던지는 턴인지 이동하는 턴인지 알려주는 텍스트")] TMP_Text Text;
    //public bool returnYut;//모나 윷이 뜨면 true로 전환
    //[SerializeField] GameObject poscheck1;//첫번째 윷의 이동 범위
    //public bool returnyut;//모나 윷이 뜰때 바로 시간이 돌아가는것을 방지
    [Header("빽도 관련 부분")]
    [SerializeField] float BackTime;
    [SerializeField] float MaxBackTime;

    public enum eRule
    {
        Throwtime,
        Movetime,
        BackChangeTimer,
        ReturnTimeStay,//윷타이머가 안 돌아가게 관리하는 코드
        TutorialTime,//튜토리얼 레드팀일때 들어가는 코드
        TutorialMove,//튜토리얼 레드팀일때 들어가는 코드
        AutoRed,//레드팀AI
    }
    private eRule curTimer = eRule.Throwtime;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();

        Maxthrowtime = throwtime;
        Maxwaitmovetime = waitmovetime;
        MaxBackTime = BackTime;
        MaxtutorialTime = tutorialTime;
        MaxyutThrow = yutThrow;
    }

    private void Start()
    {
        Gamemanager.Instance.Playtimer = this;
        TestObj.gameObject.SetActive(false);
        curTimer = eRule.ReturnTimeStay;
    }

    public void LookScene()
    {
        TestObj.gameObject.SetActive(true);
    }

    void Update()
    {
        if (teamred == true && tutorialStageCheck == true && tutorialCheck1 == true)
        {
            curTimer = eRule.TutorialTime;
        }
        else if (teamred == true && tutorialStageCheck == true && tutorialCheck2 == true)
        {
            curTimer = eRule.TutorialMove;
        }
        //waityuttime();//윷 던지기 버튼을 안 누를때 작동
        //cheangeyuttime();//윷 던지기 버튼을 누를때 작동
        //movewaittimer();//말이 움직이는것을 기다리는 코드

        //timecalculate();//시간 계산 코드 게이지바 관리

        //yuttest();//윷을 다시 던지기 위한 코드 윷 또는 모가 뜰 경우<- 굳이 업데이트 돌려야하나?
        //changeteam();//차례가 끝나면 팀 변경 <- 굳이 업데이트문으로 돌릴 이유가 없을
        if (Input.GetKeyDown(KeyCode.R))
        {
            Maxwaitmovetime = 1;
        }

        ChangeAnimator();
        if (curTimer == eRule.Throwtime)//던지기 버튼을 안 누르면 자동으로 던져지도록하는 부분
        {
            if (tutorialStageCheck == false && teamred == true)
            {
                Gamemanager.Instance.AutoClickRed();
            }
            waityuttime();
            timecalculate();
            //cheangeyuttime();//윷 던지기 버튼을 누를때 작동
        }
        else if (curTimer == eRule.Movetime)
        {
            movewaittimer();//말이 움직이는것을 기다리는 코드
            PlayTimeCalCulate();
            //timecalculate();
        }
        else if (curTimer == eRule.BackChangeTimer)//필드에 자기말이 없을때 빽도가 뜨면 턴을 넘기도록 만든 curTime
        {
            if (MaxBackTime < 0)
            {
                MaxBackTime = BackTime;
                BackChangeTurn();
            }
            else
            {
                MaxBackTime -= Time.deltaTime;
            }
        }
        else if (curTimer == eRule.ReturnTimeStay)//이 스크립트가 작동 안하게 대기하는 코드부분
        {
            return;
        }
        else if (curTimer == eRule.TutorialTime)
        {
            //if(buttonTutorial == true)
            //{
            //    buttonTutorial = false;
            //    Gamemanager.Instance.ButtonOff();
            //}
            Gamemanager.Instance.ButtonOff();
            TutorialTeam();
        }
        else if (curTimer == eRule.TutorialMove)
        {
            automaticMove();
        }
    }

    public void StayTurnTime()//이 스크립트가 작동 안되게 바꾸는 코드 
    {
        Time.timeScale = 0;
        curTimer = eRule.ReturnTimeStay;
    }

    public void StartTurnTime()//시작타이머가 돌아가도록 하는 코드
    {
        Text.text = "윷 던지기 턴";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
        Gamemanager.Instance.Yutstartbuttons.NullText();
        curTimer = eRule.Throwtime;
    }

    public void startturn(int _startteam)//먼저 시작할 팀 설정
    {
        switch (_startteam)
        {
            case 0:
                teamred = true; break;
            case 1:
                teamblue = true; break;
        }
        changeteam();
    }

    private void waityuttime()//윷을 던지기 위해 기다리는 시간 코드
    {
        #region 과거에 만든 코드들
        //if (throwwaitcheck == true || checktime == true || returnyut == true)//윷을 던진후에 true로 전환하여 못 돌리게 만드는 코드
        //{
        //    return;
        //}
        //if(Maxthrowtime <= 0 )
        //{
        //    Maxthrowtime = throwtime;//초 초기화
        //    throwwaitcheck = true;//더이상 작동하지 않게 변경
        //    playermovecheck = true;//작동하게 변경
        //    GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.yutstarttimer = true;
        //    Debug.Log("이동으로 변경");
        //}
        //else
        //{
        //    //Maxthrowtime -= Time.deltaTime;//윷을 던지기를 기다릴는 코드
        //}
        #endregion

        //코드를  다시 리메이크
        if (Maxthrowtime <= 0)//던지기 기다리는 시간이 다 지날 경우 말을 움직일수 있는 부분으로 체인지
        {
            Maxthrowtime = throwtime;//초 초기화
            //Gamemanager.Instance.Yutstartbuttons.yutstarttimer = true;
            Gamemanager.Instance.Yutstartbuttons.yutplaytimer();
            curTimer = eRule.Movetime;
            Text.text = "이동 턴";
            Gamemanager.Instance.PlayerTimeChange();
            Debug.Log("이동으로 변경");
        }
        else
        {
            Maxthrowtime -= Time.deltaTime;//윷을 던지기를 기다릴는 코드
        }
    }

    private void ChangeAnimator()
    {
        animator = TestObj.gameObject.GetComponentInChildren<Animator>();
        if (teamblue == true)
        {
            animator.SetFloat("TurnCheck", 1);
        }
        else if (teamred == true)
        {
            animator.SetFloat("TurnCheck", 0);
        }
    }
    public void cheangeyuttime(bool _recycleCheck)
    {
        #region 과거에 만든 코드들
        //if (checktime == true)//윷 던지기 버튼을 누를 경우
        //{
        //    Maxthrowtime = throwtime;//초 초기화
        //    throwwaitcheck = true;//더이상 작동하지 않게 변경
        //    playermovecheck = true;//작동하게 변경
        //    checktime = false;
        //    //Debug.Log("이동으로 변경");
        //}
        #endregion
        //float yutnumber = Gamemanager.Instance.Yutstartbuttons.Yutnumber;
        Maxthrowtime = throwtime;//초 초기화
        Maxwaitmovetime = waitmovetime;//이동하는 부분 초 초기화
        //if (Yutnumber == 4 || Yutnumber == 5)
        //{
        //    curButton = eRule.YutStartButton2;
        //}
        if (_recycleCheck == true)
        {
            return;
        }
        curTimer = eRule.Movetime;
        Text.text = "이동 턴";
    }

    public void PassChange()
    {
        Maxthrowtime = throwtime;//초 초기화
        curTimer = eRule.BackChangeTimer;
    }

    private void movewaittimer()//말이 움직이는것을 기다리는 코드
    {
        #region  과거에 만든 코드들
        //if (playermovecheck == false || returnYut == true)
        //{
        //    return;
        //}
        //if(Maxwaitmovetime <= 0)
        //{
        //    Maxwaitmovetime = waitmovetime;
        //    playermovecheck = false;//더이상 작동 안하게 변경
        //    throwwaitcheck = false;//윷 던지는 부분을 작동하게 변경
        //    if (teamred == true)
        //    {
        //        teamred = false;
        //        teamblue = true;
        //    }
        //    else if (teamblue == true)
        //    {
        //        teamred = true;
        //        teamblue = false;
        //    }
        //    Debug.Log("던지기로 변경");

        //    //poscheck1.SetActive(false);
        //    GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.waittime = false;
        //    buttontimer.zeromovecheck = true;

        //}
        //else
        //{
        //    //Maxwaitmovetime -= Time.deltaTime;
        //    GameObject findyut = GameObject.Find("Yutstartbutton");
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.waittime = true;
        //}
        #endregion
        //코드 리메이크
        if (Maxwaitmovetime <= 0)//유저가 말을 안 움직였을때 그냥 턴을 넘기도록 설정
        {
            Maxwaitmovetime = waitmovetime;
            if (teamred == true)//이동시간이 다 끝났을 경우 팀 변경
            {
                teamred = false;
                teamblue = true;
            }
            else if (teamblue == true)
            {
                teamred = true;
                teamblue = false;
            }

            //Gamemanager.Instance.Yutstartbuttons.waittime = false;//나중에 다시 건들 예정
            //Gamemanager.Instance.Yutstartbuttons.zeromovecheck = true;
            Gamemanager.Instance.Yutstartbuttons.numberzero();

            changeteam();//변경된 팀을 게임 메니저에 넣을수 있도록 도와주는 코드
            curTimer = eRule.Throwtime;//다시 윷을 던질수 있는 부분으로 변경
            Text.text = "윷 던지기 턴";
            Gamemanager.Instance.Yutstartbuttons.TextNull();
            Gamemanager.Instance.Yutstartbuttons.NullText();
            Gamemanager.Instance.Yutstartbuttons.ClaerYutCount();
            Gamemanager.Instance.TimeOverChange();
            Gamemanager.Instance.EndTurnCheck();//턴 체인지
            Debug.Log("던지기로 변경");
        }
        else
        {
            Maxwaitmovetime -= Time.deltaTime;
            //Gamemanager.Instance.Yutstartbuttons.waittime = true;
        }
    }

    public void BackChangeTurn()//빽도가 뜰때 턴 체인지 부분
    {
        if (teamred == true)//이동시간이 다 끝났을 경우 팀 변경
        {
            teamred = false;
            teamblue = true;
        }
        else if (teamblue == true)
        {
            teamred = true;
            teamblue = false;
        }
        Gamemanager.Instance.Yutstartbuttons.numberzero();
        changeteam();//변경된 팀을 게임 메니저에 넣을수 있도록 도와주는 코드
        curTimer = eRule.Throwtime;//다시 윷을 던질수 있는 부분으로 변경
        Text.text = "윷 던지기 턴";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
        Gamemanager.Instance.Yutstartbuttons.NullText();
        Gamemanager.Instance.TimeOverChange();
        Gamemanager.Instance.EndTurnCheck();//턴 체인지
    }

    #region
    //private void playercheck()
    //{
    //    if(playermovecheck == true)
    //    {
    //        if(teamblue == true)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player1");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //GameObject playerfind2 = GameObject.Find("Player2");
    //            //Player player2 = playerfind2.GetComponent<Player>();
    //            //player.playertype1 = true;
    //            //player.playertype2 = false;

    //            GameObject obj = GameObject.Find("Gamemanager");
    //            Gamemanager gamemanager = obj.GetComponent<Gamemanager>();
    //            gamemanager.Gameplayertype = 1;

    //        }
    //        else if(teamred == true)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player2");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //player.playertype1 = false;
    //            //player.playertype2 = true;

    //            GameObject obj = GameObject.Find("Gamemanager");
    //            Gamemanager gamemanager = obj.GetComponent<Gamemanager>();
    //            gamemanager.Gameplayertype = 2;
    //        }
    //    }

    //}
    #endregion

    private void timecalculate()//시간 계산 코드
    {
        timegage.fillAmount = Maxthrowtime / throwtime;
    }

    private void PlayTimeCalCulate()
    {
        timegagePlayer.fillAmount = Maxwaitmovetime / waitmovetime;
    }

    private void yuttest()//윷을 다시 던지기 위한 코드
    {
        #region 옛날에 만드 ㄴ코드들
        //if (returnYut == true)
        //{
        //    playermovecheck = false;//더이상 작동 안하게 변경
        //    throwwaitcheck = false;//윷 던지는 부분을 작동하게 변경

        //    GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.waittime = false;
        //    returnYut = false;
        //}
        #endregion

    }

    public void changeteam()
    {
        if (teamblue == true)
        {
            Gamemanager.Instance.Chageplayteam(1);
            //animator.SetFloat("CharacterChange", 0);
            //Gamemanager.Instance.teamfalsecheck();
        }
        else if (teamred == true)
        {
            Gamemanager.Instance.Chageplayteam(2);
            //animator.SetFloat("CharacterChange", 1);
            //Gamemanager.Instance.teamfalsecheck();
        }
    }

    public void turnendchange(int _startteam)//턴이 종료 될때 블루팀 레드팀을 변경하는 코드부분
    {
        switch (_startteam)
        {
            case 0:
                teamblue = false;
                teamred = true; break;
            case 1:
                teamred = false;
                teamblue = true; break;
        }
    }

    public void ReturnCheck()//모나 윷이 걸릴때 바로 돌아가지 않도록 조절하는 코드
    {
        curTimer = eRule.ReturnTimeStay;
    }
    public void BackReturnCheck()//모나 윷이 걸릴때 바로 돌아가지 않도록 조절하는 코드
    {
        curTimer = eRule.Throwtime;
    }
    public void TurnChangeCheck()
    {
        Maxwaitmovetime = waitmovetime;
        if (teamred == true)//이동시간이 다 끝났을 경우 팀 변경
        {
            teamred = false;
            teamblue = true;
        }
        else if (teamblue == true)
        {
            teamred = true;
            teamblue = false;
        }
        Gamemanager.Instance.Yutstartbuttons.numberzero();

        changeteam();//변경된 팀을 게임 메니저에 넣을수 있도록 도와주는 코드
        curTimer = eRule.Throwtime;//다시 윷을 던질수 있는 부분으로 변경
        Text.text = "윷 던지기 턴";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
        Gamemanager.Instance.Yutstartbuttons.NullText();
        Gamemanager.Instance.TimeOverChange();
    }

    public void RecycleTurn()
    {
        curTimer = eRule.Throwtime;
        Text.text = "윷 던지기 턴";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
    }

    //튜토리얼 부분
    public void TutorialTeam()//레드팀 차례일때 자동으로 윷을 던지 도록 설정
    {
        if (MaxtutorialTime < 0)
        {
            MaxtutorialTime = tutorialTime;
            Gamemanager.Instance.Yutstartbuttons.yutplaytimer();
            //curTimer = eRule.Movetime;
            Gamemanager.Instance.PlayerTimeChange();
            tutorialCheck1 = false;
            curTimer = eRule.TutorialMove;
        }
        else
        {
            MaxtutorialTime -= Time.deltaTime;
        }
    }

    private void automaticMove()
    {
        if (tutorialStageCheck == true)
        {
            if (MaxyutThrow < 0)
            {
                tutorialCheck2 = false;
                MaxyutThrow = yutThrow;
                Gamemanager.Instance.selectTeam();
            }
            else
            {
                MaxyutThrow -= Time.deltaTime;
            }
        }
    }


    //RedTeam AI
    public void RedSelect()
    {
        Maxthrowtime = throwtime;//초 초기화
        Gamemanager.Instance.Yutstartbuttons.YutThrowClick();
        if (Gamemanager.Instance.Yutstartbuttons.Yutnumber > 3)
        {
            return;
        }
        curTimer = eRule.Movetime;
        Text.text = "이동 턴";
            Gamemanager.Instance.SelectRed();
    }
}
