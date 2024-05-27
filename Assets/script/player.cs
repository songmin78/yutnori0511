using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject playd;
    [Header("플레이어")]
    [SerializeField] bool playercheck1;//플레이어1번 부분
    [SerializeField] bool playercheck2;//플레이어 2번 부분
    [Header("플레이어를 선택 했을때")]
    [SerializeField] public GameObject checkobj;//플레이러를 체크할때 보이는 오브젝트
    [SerializeField] bool checkmask;//플레이어를 체크 할때

    [Header("기타")]
    [SerializeField]bool playertouch = false;
    [SerializeField]public bool playerchoice;
    [SerializeField]public bool tests;
    public bool playertype1;//플레이어타입1
    public bool playertype2;//플레이어타입2
    [SerializeField]bool playerchecking1;
    [SerializeField]bool playerchecking2;
    [Header("윷 이동 부분")]
    [SerializeField] public float oneYut;
    [SerializeField] public float twoYut;
    [SerializeField] public float threeYut;
    //[SerializeField] float d;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("mouse"))
        {
            playertouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("mouse"))
        {
            playertouch = false;
        }
    }

    private void Awake()
    {
        //Gamemanager.Instance.Player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Gamemanager.Instance.Player = this;
    }

    // Update is called once per frame
    void Update()
    {
        checkplayermouse();
        //typeplayer();

        //testcode();
    }


    private void checkplayermouse()//마우스로 플레이어를 선택 할때 작동
    {
        if(playertouch == true && playerchoice == true)
        {
            if(playercheck1 == true)
            {
                playerchecking1 = true;
                playerchecking2 = false;
            }
            else if(playercheck2 == true)
            {
                playerchecking1 = false;
                playerchecking2 = true;
            }
            testcode();
            playerchoice = false;
            //tests = true;
            Debug.Log("선택됨");
        }
        playerchoice = false;
    }

    private void typeplayer()//플레이어1 과 플레이어2를 동시에 못 움직이게 설정
    {
        //if(playertype1 == true)
        //{
        //    playertype2 = false;
        //    checkobj.SetActive(true);
        //}
        //else if(playertype2 == true)
        //{
        //    playertype1 = false;
        //    checkobj.SetActive(true);
        //}
        
    }

    private void testcode()
    {
        //플레이어가 가져와야 할것 윷에서 나온 숫자,발판의 숫자
        GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();   

        oneYut = buttontimer.oneyut;
        twoYut = buttontimer.twoyut;
        threeYut = buttontimer.threeyut;

        GameObject objfoot = GameObject.Find("footholdbox");
        Footholdbox footholdbox = objfoot.GetComponent<Footholdbox>();
        footholdbox.zerocheck = true;

        //Gamemanager.Instance.Numberroom.mynumber += a;
        //Gamemanager.Instance.Numberroom.zerocheck = true;

        //GameObject obj = GameObject.Find("footholdbox");
        //Numberroom numberroom = obj.GetComponentInChildren<Numberroom>();
        //numberroom.count1 = a;
        //numberroom.count2 = b;
        //numberroom.count3 = c;
    }

    private void movetest()
    {

    }

}
