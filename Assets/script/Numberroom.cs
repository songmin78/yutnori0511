using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberroom : MonoBehaviour
{
    [SerializeField] GameObject parentobj;
    [Header("�� ������ ��ȣ")]
    [SerializeField,Range(0,19)] float yutButton;//������ ���� ��ġ
    [SerializeField] List<float> Yutnumber = new List<float>();//������ ��ġ�� �޴� �κ�

    [Header("������ ��ȣ �� üũ")]
    [SerializeField,Tooltip("�����濡 ���߾�����")] bool yutshortcut;
    [SerializeField] float shortcutButton;
    public float MaxyutButton;

    [Header("��ġ Ȯ��")]
    [SerializeField] GameObject poscheck;

    [Header("��Ÿ")]
    public float count1;
    public float count2;
    public float count3;
    [SerializeField]Vector3 trs;
    Vector3 mytrs;
    public float mynumber;//���� ���� �ٲ���ġ
    bool numbercheck;
    public bool zerocheck;//�� �ۿ��ִ� ���� ���� �������
    [SerializeField, Tooltip("����  �ִ� ��ġ")] float playerposition;
    Vector3 vec3;
    float testpp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("player"))//�÷��̾ �ش� ���ǿ� �ִ� ���
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

    private void testtouch()//�÷��̾ ��ĭ �����̴��� Ȯ���Ϸ��� �׽�Ʈ
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(count1);
            Debug.Log(count2);
            Debug.Log(count3);
        }
    }

    private void myposition()//������ �ڱ� ��ġ�� �˰��ִ� �ڵ� �ѹ��� �޵��� ��
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

    private void movestart()//ĭ�� ���� ���� ���� �Ҷ�
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
        //    GameObject findyut = GameObject.Find("footholdbox");//�ش� �̸��� ������Ʈ�� ã�´�
        //    Footholdbox footholdbox = findyut.GetComponent<Footholdbox>();
        //    Debug.Log(footholdbox.Yutfoothold);
        //}
    }

    private void starttest()
    {
        //Footholdbox footholdbox = parentobj.GetComponent<Footholdbox>();
    }
}
