using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playtimer : MonoBehaviour
{
    [SerializeField] public bool teamred;
    [SerializeField] public bool teamblue;
    [Header("윷을 던지기 까지의 남은 시간 정리")]
    [SerializeField] float throwtime;
    [SerializeField]float Maxthrowtime;//확인 용
    bool throwwaitcheck;//던지기는 것을 기다림
    [Header("윷을 던진후 캐릭터 이동을 위한 지속 시간 정리")]
    [SerializeField] float waitmovetime;
    [SerializeField]float Maxwaitmovetime;//확인용
    bool playermovecheck;//윷을 던진후 움직이는것을 기다리는 부분
    [Header("기타")]
    [SerializeField] public bool checktime;//윷을 던졌을때 true로 전환(밖에서 받아옴)
    [SerializeField] Image timegage;//시간초 줄어드는 게이지
    public bool returnYut;//모나 윷이 뜨면 true로 전환
    //[SerializeField] GameObject poscheck1;//첫번째 윷의 이동 범위
    public bool returnyut;//모나 윷이 뜰때 바로 시간이 돌아가는것을 방지

    public enum eRule
    {
        Throwtime,//던지는 시간을 다루는 부분
        Movetime,//움직이는 시간을 다루는 부분
        Returnthrowtime,//말을 잡았을때 다시 던질 수 있는 부분
    }
    private eRule curState = eRule.Throwtime;

    private void Awake()
    {
        Maxthrowtime = throwtime;
        Maxwaitmovetime = waitmovetime;
    }

    void Update()
    {
        waityuttime();//윷 던지기 버튼을 안 누를때 작동
        cheangeyuttime();//윷 던지기 버튼을 누를때 작동
        movewaittimer();
        //playercheck();

        timecalculate();

        yuttest();
        changeteam();

        //if(curState == eRule.Throwtime)
        //{

        //}
        //else if(curState == eRule.Movetime)
        //{

        //}
        //else if(curState == eRule.Returnthrowtime)
        //{

        //}
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
        if(throwwaitcheck == true || checktime == true || returnyut == true)//윷을 던진후에 true로 전환하여 못 돌리게 만드는 코드
        {
            return;
        }
        if(Maxthrowtime <= 0 )
        {
            Maxthrowtime = throwtime;//초 초기화
            throwwaitcheck = true;//더이상 작동하지 않게 변경
            playermovecheck = true;//작동하게 변경
            GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.yutstarttimer = true;
            Debug.Log("이동으로 변경");
        }
        else
        {
            //Maxthrowtime -= Time.deltaTime;//윷을 던지기를 기다릴는 코드
        }
    }

    private void cheangeyuttime()
    {
        if(checktime == true)//윷 던지기 버튼을 누를 경우
        {
            Maxthrowtime = throwtime;//초 초기화
            throwwaitcheck = true;//더이상 작동하지 않게 변경
            playermovecheck = true;//작동하게 변경
            checktime = false;
            //Debug.Log("이동으로 변경");
        }
    }

    private void movewaittimer()//말이 움직이는것을 기다리는 코드
    {
        if(playermovecheck == false || returnYut == true)
        {
            return;
        }
        if(Maxwaitmovetime <= 0)
        {
            Maxwaitmovetime = waitmovetime;
            playermovecheck = false;//더이상 작동 안하게 변경
            throwwaitcheck = false;//윷 던지는 부분을 작동하게 변경
            if (teamred == true)
            {
                teamred = false;
                teamblue = true;
            }
            else if (teamblue == true)
            {
                teamred = true;
                teamblue = false;
            }
            Debug.Log("던지기로 변경");

            //poscheck1.SetActive(false);
            GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = false;
            buttontimer.zeromovecheck = true;

        }
        else
        {
            //Maxwaitmovetime -= Time.deltaTime;
            GameObject findyut = GameObject.Find("Yutstartbutton");
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = true;
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

    private void yuttest()
    {
        if(returnYut == true)
        {
            playermovecheck = false;//더이상 작동 안하게 변경
            throwwaitcheck = false;//윷 던지는 부분을 작동하게 변경

            GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = false;
            returnYut = false;
        }
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
}
