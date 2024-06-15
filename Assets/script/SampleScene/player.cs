using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Vector3 startmypos;//������ �ٽ� ������ġ�� ���ư��� ���� ��ġ Ȯ�ο�

    [Header("�� ����")]
    [SerializeField]public bool teamred;
    [SerializeField]public bool teamblue;
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
    float MaxmoveYutcount;//�̵��� �ڽ��� ��ġ�� ����
    [SerializeField] bool movecheck;//�ڱ� ���ʸ� Ȯ���ϱ� ���� üũ
    bool touchcheck;//�÷��̾����˿� ���� �κ�
    float turntimes = 0.1f;//0.1�ʰ��� �ð��� �༭ �ٷ� �̵����� �ʵ��� ������


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
        turntime,//���� ���ϰ� �ѱ��� �ʵ��� �����ϴ°�
    }
    private eRule yutcontrol = eRule.notrecall;

    private void Awake()
    {
        //Gamemanager.Instance.Player = this;
    }

    void Start()
    {
        startmypos = transform.position;
        Gamemanager.Instance.Player = this;
        MaxmoveYutcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //checkplayermouse();
        //typeplayer();

        //testcode();
        postest();
        if (yutcontrol == eRule.notrecall)
        {
            //testcode();
            if(movecheck == true)
            {
                yutcontrol = eRule.turntime;
            }
        }
        else if(yutcontrol == eRule.turntime)
        {
            turntimes -= Time.deltaTime;
            if(turntimes < 0)
            {
                turntimes = 0.1f;
                yutcontrol = eRule.playermovecheck;
            }
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
            //Gamemanager.Instance.AgainStartPos(startmypos);
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
        //yutcontrol = eRule.playermovecheck;
    }

    private void yutmoving()//���� ������ ��ġ
    {
        moveYutcount1 += oneYut;
        moveYutcount2 += twoYut;
        moveYutcount3 += threeYut;
        Gamemanager.Instance.Footholdbox.findposition(moveYutcount1, moveYutcount2, moveYutcount3, MaxmoveYutcount);
        //GameObject obj = GameObject.Find("footholdbox");
        //Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
        //footholdbox.findposition(moveYutcount1,moveYutcount2,moveYutcount3,MaxmoveYutcount);
        //footholdbox.movecheckchange(oneYut, twoYut, threeYut);
    }


    public void countyutcheck()//���� ���ڸ� �����ϴ� �ڵ�κ�
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();

        oneYut = buttontimer.oneyut;
        twoYut = buttontimer.twoyut;
        threeYut = buttontimer.threeyut;
        if(oneYut == -1 && MaxmoveYutcount == 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
    }

    private void positionselect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true && movecheck == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);

            if (rayHit.transform != null)
            {
                GameObject obj1 = Gamemanager.Instance.Footholdbox.poscheck1;
                GameObject obj2 = Gamemanager.Instance.Footholdbox.poscheck2;
                GameObject obj3 = Gamemanager.Instance.Footholdbox.poscheck3;

                //if(obj1.transform.position == new Vector3(0,-10,0))
                //{
                //    return;
                //}

                //Debug.Log(rayHit.transform.gameObject.name);

                if (rayHit.transform.gameObject == obj1)
                {
                    transform.position = rayHit.transform.position;
                    MaxmoveYutcount = moveYutcount1;
                    Yutorder = 1;
                    //Gamemanager.Instance.MovePlayerFootHold(gameObject,(int)MaxmoveYutcount);
                    //Gamemanager.Instance.holdboxPosCheck(MaxmoveYutcount);
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else if(rayHit.transform.gameObject == obj2)
                {
                    transform.position = rayHit.transform.position;
                    MaxmoveYutcount = moveYutcount2;
                    Yutorder = 2;
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else if (rayHit.transform.gameObject == obj3)
                {
                    transform.position = rayHit.transform.position;
                    MaxmoveYutcount = moveYutcount3;
                    Yutorder = 3;
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else
                {
                    return;
                }
                //Debug.Log(rayHit.transform.gameObject);
                Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)MaxmoveYutcount);
                Gamemanager.Instance.holdboxPosCheck(MaxmoveYutcount);
            }
            //Debug.Log(rayHit.transform.gameObject.name);
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
        //posmovecheck();
        movecheck = false;
        yutcontrol = eRule.notrecall;
        //Gamemanager.Instance.Footholdbox.movecheckchange(oneYut, twoYut, threeYut);
        Gamemanager.Instance.Footholdbox.movedestory();
        Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
        findplayd.SetActive(false);
        //Debug.Log(moveYutcount1);
        //Debug.Log(moveYutcount2);
        //Debug.Log(moveYutcount3);
        //Debug.Log(MaxmoveYutcount);
    }

    private void postest()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Player player = GetComponent<Player>();
            Debug.Log(player.gameObject);
            Debug.Log(player.MaxmoveYutcount);
        }
    }

    public void AgainStartPos()//���� ������ �ٽ� ó������ ���ư��� �ڵ�
    {
        transform.position = startmypos;
        MaxmoveYutcount = 0;
    }

    //private void posmovecheck()//�̵��Ŀ� �÷��̾�� ��ġ Ȯ�� ���ִ� �κ�
    //{
    //    int count = Gamemanager.Instance.objblue.Count;
    //    for(int iNum = 0; iNum < count; iNum++)
    //    {
            
    //    }
    //}
}
