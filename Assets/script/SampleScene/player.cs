using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject findplayd;
    //[Header("�÷��̾�")]
    //[SerializeField] bool playercheck1;//�÷��̾�1�� �κ�
    //[SerializeField] bool playercheck2;//�÷��̾� 2�� �κ�
    //[Header("�÷��̾ ���� ������")]
    //[SerializeField] public GameObject checkobj;//�÷��̷��� üũ�Ҷ� ���̴� ������Ʈ
    //[SerializeField] bool checkmask;//�÷��̾ üũ �Ҷ�
    //[SerializeField] Canvas playerselected;//�÷��̾ ���õ� �� üũ�Ǵ°� �����ִ� ������Ʈ
    bool selectedcheck;//���õɶ� üũ

    bool buleTeam;

    [Header("�� ����")]
    [SerializeField]bool teamred;
    [SerializeField]bool teamblue;
    [Header("��Ÿ")]
    //[SerializeField]bool playertouch = false;
    [SerializeField]public bool playerchoice;
    [SerializeField]public bool tests;
    public bool playertype1;//�÷��̾�Ÿ��1
    public bool playertype2;//�÷��̾�Ÿ��2
    [Header("�� �̵� �κ�")]
    [SerializeField] public float oneYut;
    [SerializeField] public float twoYut;
    [SerializeField] public float threeYut;
    float Yutorder;//���� ������ ���ִ� �� �κ�
    //[SerializeField] float d;
    //[SerializeField]public bool playertypenumber;
    [Header("���� �����̱����� �Ÿ� �κ�")]
    private bool myyutturn;//�ڱ� �����϶��� true�� ����
    [SerializeField] float moveYutcount1;//ù��° ���� ���� ���ڸ�ŭ ���Ͽ� ������� �������� �̸� �����ִ� �κ�
    [SerializeField] float moveYutcount2;//�ι�° ���� ���� ���ڸ�ŭ ���Ͽ� ������� �������� �̸� �����ִ� �κ�
    [SerializeField] float moveYutcount3;//����° ���� ���� ���ڸ�ŭ ���Ͽ� ������� �������� �̸� �����ִ� �κ�
    [SerializeField] float MaxmoveYutcount;//�̵��� �ڽ��� ��ġ�� ����
    [SerializeField] bool movecheck;
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    public enum eRule
    {
        notrecall,//�ڱ� ���ʰ� �ƴҶ� �۵��Ǵ� �κ�
        playermovecheck,//���ϴ� ��ġ�� �̵� �Ǵ� �κ�
        endcheck,//�ڱ� ���ʰ� ������ ��� �ϴ°�
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
    //private void checkplayermouse()//���콺�� �÷��̾ ���� �Ҷ� �۵�
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
    //        Debug.Log("���õ�");
    //    }
    //    playerchoice = false;
    //}

    //private void typeplayer()//�÷��̾�1 �� �÷��̾�2�� ���ÿ� �� �����̰� ����
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

    //public void testcode()//��ġ�����ϴ� �κ�
    //{
    //    //�÷��̾ �����;� �Ұ� ������ ���� ����,������ ����
    //    GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
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

    //public void Selectlocation()//�÷��̾ ���õɶ� �����ϼ� �ִ� ĭ�� �����ִ� �κ�
    //{
    //    GameObject obj = GameObject.Find("footholdbox");
    //    Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
    //}


    public void moveyutcount()//�ڽ��� ��ġ�� �̵� �� �κп� �й�
    {
        //�ѹ��� üũ
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

    private void yutmoving()//���� ������ ��ġ
    {
        moveYutcount1 += oneYut;
        moveYutcount2 += twoYut;
        moveYutcount3 += threeYut;
        GameObject obj = GameObject.Find("footholdbox");
        Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
        footholdbox.findposition(moveYutcount1,moveYutcount2,moveYutcount3,MaxmoveYutcount);
    }

    public void countyutcheck()//���� ���ڸ� �����ϴ� �ڵ�κ�
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
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

    private void changeYutzero()//���� ������ �Ŀ� �̵��� ���ڸ� 0���� ����� �ڵ�
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
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

    private void outcheckplayer()//�÷��̾ �ٸ� �÷��̾� ���� ����� ���
    {

    }
}
