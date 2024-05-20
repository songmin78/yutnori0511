using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playtimer : MonoBehaviour
{
    [SerializeField] bool testteam1;
    [SerializeField] bool testteam2;
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

    private void Awake()
    {
        Maxthrowtime = throwtime;
        Maxwaitmovetime = waitmovetime;
    }

    void Start()
    {
        startturn();
    }

    void Update()
    {
        waityuttime();//윷 던지기 버튼을 안 누를때 작동
        cheangeyuttime();//윷 던지기 버튼을 누를때 작동
        movewaittimer();
        playercheck();
    }

    private void startturn()//먼저 시작할 팀 설정
    {
        int startplayer =  Random.Range(0, 2);
        switch(startplayer)
        {
            case 0:
                testteam1 = true; break;
            case 1:
                testteam2 = true; break;
        }
    }

    private void waityuttime()//윷을 던지기 위해 기다리는 시간 코드
    {
        if(throwwaitcheck == true || checktime == true)//윷을 던진후에 true로 전환하여 못 돌리게 만드는 코드
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
            Maxthrowtime -= Time.deltaTime;//윷을 던지기를 기다릴는 코드
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
            Debug.Log("이동으로 전환");
        }
    }

    private void movewaittimer()//움직이는것을 기다리는 코드
    {
        if(playermovecheck == false)
        {
            return;
        }
        if(Maxwaitmovetime <= 0)
        {
            Maxwaitmovetime = waitmovetime;
            playermovecheck = false;//더이상 작동 안하게 변경
            throwwaitcheck = false;//윷 던지는 부분을 작동하게 변경
            if (testteam1 == true)
            {
                testteam1 = false;
                testteam2 = true;
            }
            else if (testteam2 == true)
            {
                testteam1 = true;
                testteam2 = false;
            }
            Debug.Log("던지기로 변경");

            GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = false;

        }
        else
        {
            Maxwaitmovetime -= Time.deltaTime;
            GameObject findyut = GameObject.Find("Yutstartbutton");
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = true;
        }
    }

    private void playercheck()
    {
        if(playermovecheck == true)
        {
            if(testteam1 == true)
            {
                //GameObject playerfind = GameObject.Find("Player1");
                //Player player = playerfind.GetComponent<Player>();
                //GameObject playerfind2 = GameObject.Find("Player2");
                //Player player2 = playerfind2.GetComponent<Player>();
                //player.playertype1 = true;
                //player.playertype2 = false;

                GameObject obj = GameObject.Find("Gamemanager");
                Gamemanager gamemanager = obj.GetComponent<Gamemanager>();
                gamemanager.Gameplayertype = 1;

            }
            else if(testteam2 == true)
            {
                //GameObject playerfind = GameObject.Find("Player2");
                //Player player = playerfind.GetComponent<Player>();
                //player.playertype1 = false;
                //player.playertype2 = true;

                GameObject obj = GameObject.Find("Gamemanager");
                Gamemanager gamemanager = obj.GetComponent<Gamemanager>();
                gamemanager.Gameplayertype = 2;
            }
        }

    }

    private void timecalculate()//시간 계산 코드
    {

    }
}
