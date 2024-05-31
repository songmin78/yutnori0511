using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberroom : MonoBehaviour
{
    [SerializeField] GameObject parentobj;
    [Header("윷 발판의 번호")]
    [SerializeField,Range(0,19)] float yutButton;//각각의 고유 위치
    [SerializeField] List<float> Yutnumber = new List<float>();//발판의 위치를 받는 부분

    [Header("지름길 번호 및 체크")]
    [SerializeField,Tooltip("지름길에 멈추었을때")] bool yutshortcut;
    [SerializeField] float shortcutButton;
    public float MaxyutButton;

    [Header("위치 확인")]
    [SerializeField] GameObject poscheck;

    [Header("기타")]
    public float count1;
    public float count2;
    public float count3;
    [SerializeField]Vector3 trs;
    Vector3 mytrs;
    public float mynumber;//윷에 의해 바뀐위치
    bool numbercheck;
    public bool zerocheck;//판 밖에있는 말을 선택 했을경우
    [SerializeField, Tooltip("현재  있는 위치")] float playerposition;
    Vector3 vec3;
    float testpp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("player"))//플레이어가 해당 발판에 있는 경우
        {
            trs = transform.position;
            numbercheck = true;
        }
    }

    private void Awake()
    {
        MaxyutButton = yutButton;
    }

    private void Start()
    {
        myposition();

        //Yutnumber.Add(yutButton);
        Footholdbox footholdbox = parentobj.GetComponent<Footholdbox>();
        footholdbox.yutnumber = transform.position;
        footholdbox.passyut(MaxyutButton);
        starttest();

        Gamemanager.Instance.Numberroom = this; 
    }

    private void Update()
    {
        movestart();
        //testtouch();
        testcode();

        //testclick();
    }

    private void testtouch()//플레이어가 몇칸 움직이는지 확인하려고 테스트
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(count1);
            Debug.Log(count2);
            Debug.Log(count3);
        }
    }

    private void myposition()//발판이 자기 위치를 알고있는 코드 한번만 받도록 함
    {
        mytrs = transform.position;
        mynumber = 0;
    }

    private void testcode()
    {

        if (numbercheck == true)
        {
            numbercheck = false;
            trs = mytrs;
            tests();
            //poscheck.transform.position = trs;
        }
    }

    private void tests()
    {
        //if(mynumber != yutButton)
        //{
        //    Yutnumber.Exists(numbercount => numbercount == mynumber);
        //    {
        //        Debug.Log(transform.position);
        //    }
        //}
    }

    private void movestart()//칸에 없는 말을 선택 할때
    {
        if(zerocheck == true)
        {
            #region
            //Yutnumber.Exists(numbercount => numbercount == mynumber);
            //{
            //    Vector3 moving = new Vector3(transform.position.x, transform.position.y, 0);
            //    Debug.Log(transform.position);
            //    Debug.Log(mynumber);
            //    poscheck.transform.position = moving;
            //}
            //zerocheck = false;


            //Yutnumber.Contains(yutButton);
            //Debug.Log(Yutnumber.Contains(yutButton));
            //if (Yutnumber.Contains(mynumber) == true)
            //{
            //    Vector3 moving = new Vector3(transform.position.x, transform.position.y, 0);
            //    poscheck.transform.position = moving;
            //}
            //else
            //{
            //    Yutnumber[0] += 1;
            //    Debug.Log(Yutnumber[0]);
            //}

            //if(Yutnumber == mynumber)
            //{
            //    vec3 = new Vector3(transform.position.x,transform.position.y,0);
            //}
            //Debug.Log(Yutnumber[0]);
            //poscheck.transform.position = vec3;
            //Footholdbox footholdbox = parentobj.GetComponent<Footholdbox>();
            //footholdbox.Yutfoothold.Add(yutButton);

            //Footholdbox footholdbox = parentobj.GetComponent<Footholdbox>();
            //testpp = footholdbox.Yutfoothold.FindIndex(testd => testd == 15);
            //Debug.Log(testpp);
            //poscheck.transform.position = testpp;
            #endregion



            zerocheck = false;
        }
    }

    private void testclick()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    GameObject findyut = GameObject.Find("footholdbox");//해당 이름의 오브젝트를 찾는다
        //    Footholdbox footholdbox = findyut.GetComponent<Footholdbox>();
        //    Debug.Log(footholdbox.Yutfoothold);
        //}
    }

    private void starttest()
    {
        //Footholdbox footholdbox = parentobj.GetComponent<Footholdbox>();
    }
}
