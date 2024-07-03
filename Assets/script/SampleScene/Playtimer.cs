using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playtimer : MonoBehaviour
{
    [SerializeField] bool teamred;
    [SerializeField] bool teamblue;
    [Header("윷을 던지기 까지의 남은 시간 정리")]
    [SerializeField] float throwtime;
    [SerializeField]float Maxthrowtime;//확인 용
    bool throwwaitcheck;//던지기는 것을 기다림
    [Header("윷을 던진후 캐릭터 이동을 위한 지속 시간 정리")]
    [SerializeField] float waitmovetime;
    [SerializeField]float Maxwaitmovetime;//확인용
    bool playermovecheck;//윷을 던진후 움직이는것을 기다리는 부분
    [Header("기타")]
    //[SerializeField] public bool checktime;//윷을 던졌을때 true로 전환(밖에서 받아옴)
    [SerializeField] Image timegage;//시간초 줄어드는 게이지
    //public bool returnYut;//모나 윷이 뜨면 true로 전환
    //[SerializeField] GameObject poscheck1;//첫번째 윷의 이동 범위
    //public bool returnyut;//모나 윷이 뜰때 바로 시간이 돌아가는것을 방지

    public enum eRule
    {
        Throwtime,
        Movetime,
        Returnthrowtime,
        ReturnTimeStay,//윷타이머가 안 돌아가게 관리하는 코드
    }
    private eRule curTimer = eRule.Throwtime;

    private void Awake()
    {
        Maxthrowtime = throwtime;
        Maxwaitmovetime = waitmovetime;
    }

    private void Start()
    {
        Gamemanager.Instance.Playtimer = this;
    }

    void Update()
    {

        //waityuttime();//윷 던지기 버튼을 안 누를때 작동
        //cheangeyuttime();//윷 던지기 버튼을 누를때 작동
        //movewaittimer();//말이 움직이는것을 기다리는 코드

        //timecalculate();//시간 계산 코드 게이지바 관리

        //yuttest();//윷을 다시 던지기 위한 코드 윷 또는 모가 뜰 경우<- 굳이 업데이트 돌려야하나?
        //changeteam();//차례가 끝나면 팀 변경 <- 굳이 업데이트문으로 돌릴 이유가 없을


        if (curTimer == eRule.Throwtime)//던지기 버튼을 안 누르면 자동으로 던져지도록하는 부분
        {
            waityuttime();
            timecalculate();
            //cheangeyuttime();//윷 던지기 버튼을 누를때 작동
        }
        else if(curTimer == eRule.Movetime)
        {
            movewaittimer();//말이 움직이는것을 기다리는 코드
            //timecalculate();
        }
        else if(curTimer == eRule.ReturnTimeStay)
        {
            return;
        }
    }

    public void startturn(int _startteam)//먼저 시작할 팀 설정
    {
        switch(_startteam)
        {
            case 0:
                teamred = true; break;
            case 1:
                teamblue = true; break;
        }
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
            Gamemanager.Instance.PlayerTimeChange();
            Debug.Log("이동으로 변경");
        }
        else
        {
            Maxthrowtime -= Time.deltaTime;//윷을 던지기를 기다릴는 코드
        }
    }

    public void cheangeyuttime()
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

        Maxthrowtime = throwtime;//초 초기화
        curTimer = eRule.Movetime;
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
            Gamemanager.Instance.Yutstartbuttons.zeromovecheck = true;

            changeteam();//변경된 팀을 게임 메니저에 넣을수 있도록 도와주는 코드
            curTimer = eRule.Throwtime;//다시 윷을 던질수 있는 부분으로 변경
            Gamemanager.Instance.TimeOverChange();
            Debug.Log("던지기로 변경");
        }
        else
        {
            Maxwaitmovetime -= Time.deltaTime;
            //Gamemanager.Instance.Yutstartbuttons.waittime = true;
        }


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

    public void  changeteam()
    {
        if(teamblue == true)
        {
            Gamemanager.Instance.Chageplayteam(1);
            //Gamemanager.Instance.teamfalsecheck();
        }
        else if(teamred == true)
        {
            Gamemanager.Instance.Chageplayteam(2);
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
}
