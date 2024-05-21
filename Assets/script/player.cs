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
    [Header("테스트 부분")]
    [SerializeField] public float a;
    [SerializeField] public float b;
    [SerializeField] public float c;
    [SerializeField] float d;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        checkplayermouse();
        typeplayer();

        //testcode();
    }


    private void checkplayermouse()//마우스로 플레이어를 선택 할때 작동
    {
        if(playertouch == true && playerchoice == true)
        {
            testcode();
            playerchoice = false;
            //tests = true;
            Debug.Log("선택됨");
        }
        playerchoice = false;
    }

    private void typeplayer()//플레이어  타입에 따라 작동되는 부분 안되는 부분 구분하는거
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

        a = buttontimer.oneyut;
        b = buttontimer.twoyut;
        c = buttontimer.threeyut;

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
