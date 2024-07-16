using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("���̵� ������Ʈ")]
    [SerializeField] GameObject findplayd;
    [SerializeField] GameObject Curryobj1;
    [SerializeField] GameObject Curryobj2;
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
    [SerializeField, Tooltip("�÷��̾ ����� �ϸ� True�� ��ȯ")] bool goPlayer;
    public bool GoPlayer
    {
        get
        {
            return goPlayer;
        }
    }
    float Yutorder;//���� ������ ���ִ� �� �κ�
    //[SerializeField] float d;
    //[SerializeField]public bool playertypenumber;
    [Header("���� �����̱����� �Ÿ� �κ�")]
    private bool myyutturn;//�ڱ� �����϶��� true�� ����
    [SerializeField] float moveYutcount1;//ù��° ���� ���� ���ڸ�ŭ ���Ͽ� ������� �������� �̸� �����ִ� �κ�
    [SerializeField] float moveYutcount2;//�ι�° ���� ���� ���ڸ�ŭ ���Ͽ� ������� �������� �̸� �����ִ� �κ�
    [SerializeField] float moveYutcount3;//����° ���� ���� ���ڸ�ŭ ���Ͽ� ������� �������� �̸� �����ִ� �κ�
    [SerializeField]float maxmoveYutcount;//�̵��� �ڽ��� ��ġ�� ����(�����浵 ������)
    public float MaxmoveYutcount
    {
        get 
        {
            return maxmoveYutcount;
        }
    }

    [SerializeField] bool movecheck;//�ڱ� ���ʸ� Ȯ���ϱ� ���� üũ
    bool touchcheck;//�÷��̾����˿� ���� �κ�
    float turntimes = 0.1f;//0.1�ʰ��� �ð��� �༭ �ٷ� �̵����� �ʵ��� ������
    //�������� �� �������� üũ�ϴ� �ڵ�
    //bool CurryBlue;
    //bool CurryRed;
    [Header("�����濡 ���õ� �κ��� ����")]
    [SerializeField] bool shortcutCheck;//������ �����ߴٸ� üũ�ϴ� �κ�
    [SerializeField] float pastYutcount1;//���� ���� ���ڸ�ŭ ���Ͽ� ������ ��ġ�� �����ִ� �κ�(�����濡 ���� ���)
    [SerializeField] float pastYutcount2;
    [SerializeField] float pastYutcount3;
    [SerializeField]int countYut;//1 �̶�� 5�� ������,2�̶�� 10�� ������, 3���̶�� 22�� 32�� ����
    ////���� ������� �������� ��ġ�� �ȶߵ��� ����
    //public bool Exitcheck1;
    //public bool Exitcheck2;
    //public bool Exitcheck3;
    bool NextTurnCheck;

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
        maxmoveYutcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //checkplayermouse();
        //typeplayer();

        //testcode();
        //postest();
        if (yutcontrol == eRule.notrecall)
        {
            //testcode();
            if(movecheck == true)
            {
                yutcontrol = eRule.playermovecheck;
            }
            else
            {
                movecheck = false;
                return;
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
        if(shortcutCheck == true)//�����濡 �����ϰ� ���� ���
        {
            pastYutcount1 = maxmoveYutcount;
            pastYutcount2 = maxmoveYutcount;
            pastYutcount3 = maxmoveYutcount;
        }
        moveYutcount1 = maxmoveYutcount;
        moveYutcount2 = maxmoveYutcount;
        moveYutcount3 = maxmoveYutcount;
        //yutcontrol = eRule.turncheck;
        countyutcheck();
        yutmoving();

        //Debug.Log(MaxmoveYutcount);
        //yutcontrol = eRule.playermovecheck;
    }

    private void yutmoving()//���� ������ ��ġ
    {
        if (shortcutCheck == true)
        {
            countShortcut();
            moveYutcount1 = maxmoveYutcount;
            moveYutcount2 = maxmoveYutcount;
            moveYutcount3 = maxmoveYutcount;
        }
        yutPosCount1();
        yutPosCount2();
        yutPosCount3();
        //moveYutcount1 += oneYut;
        //moveYutcount2 += twoYut;
        //moveYutcount3 += threeYut;
        Gamemanager.Instance.Footholdbox.findposition(moveYutcount1, moveYutcount2, moveYutcount3, maxmoveYutcount);
        //GameObject obj = GameObject.Find("footholdbox");
        //Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
        //footholdbox.findposition(moveYutcount1,moveYutcount2,moveYutcount3,MaxmoveYutcount);
        //footholdbox.movecheckchange(oneYut, twoYut, threeYut);
    }

    public void ChangeYutPos(float _Pos1, float _Pos2, float _Pos3)//�߾ӿ��� ������ ������ ���� �������� �ֱ� ���� �۾�
    {
        moveYutcount1 = _Pos1;
        moveYutcount2 = _Pos2;
        moveYutcount3 = _Pos3;
    }
   


    public void countyutcheck()//���� ���ڸ� �����ϴ� �ڵ�κ�
    {
        GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();

        oneYut = buttontimer.oneyut;
        twoYut = buttontimer.twoyut;
        threeYut = buttontimer.threeyut;
        if(oneYut == -1 && maxmoveYutcount == 0)//���� ���� �����ε� ������� �÷��̾ ������ ��� �׳� üũ�� ����
        {
            //Gamemanager.Instance.Footholdbox.positiondestory();
            //Yutorder = 1;
            //changeYutzero();
            return;
        }
    }

    private void positionselect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true && movecheck == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, LayerMask.GetMask("Ground"));

            if (rayHit.transform != null)
            {
                if(shortcutCheck == true)
                {
                    fastpositionselect(rayHit);
                }
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
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    maxmoveYutcount = moveYutcount1;
                    Yutorder = 1;
                    //Gamemanager.Instance.MovePlayerFootHold(gameObject,(int)MaxmoveYutcount);
                    //Gamemanager.Instance.holdboxPosCheck(MaxmoveYutcount);
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else if(rayHit.transform.gameObject == obj2)
                {
                    transform.position = rayHit.transform.position;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    maxmoveYutcount = moveYutcount2;
                    Yutorder = 2;
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                else if (rayHit.transform.gameObject == obj3)
                {
                    transform.position = rayHit.transform.position;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    maxmoveYutcount = moveYutcount3;
                    Yutorder = 3;
                    Gamemanager.Instance.Footholdbox.positiondestory();
                    changeYutzero();
                }
                //Debug.Log(rayHit.transform.gameObject);
                Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)maxmoveYutcount);
                Gamemanager.Instance.holdboxPosCheck(maxmoveYutcount,gameObject);
                //Gamemanager.Instance.DebugTest();
                //Gamemanager.Instance.PastlLoadCheck((int)MaxmoveYutcount, gameObject);
                if(NextTurnCheck == true)
                {
                    NextTurnCheck = false;
                    Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
                }
                movecheck = false;
            }
            //Debug.Log(rayHit.transform.gameObject.name);
        }
    }

    private void fastpositionselect(RaycastHit2D rayHit)//�����濡 ���ð��
    {
        GameObject obj1 = Gamemanager.Instance.Footholdbox.shortcutcheck1;
        GameObject obj2 = Gamemanager.Instance.Footholdbox.shortcutcheck2;
        GameObject obj3 = Gamemanager.Instance.Footholdbox.shortcutcheck3;

        if (rayHit.transform.gameObject == obj1)
        {
            transform.position = rayHit.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            maxmoveYutcount = pastYutcount1;
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
        else if (rayHit.transform.gameObject == obj2)
        {
            transform.position = rayHit.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            maxmoveYutcount = pastYutcount2;
            Yutorder = 2;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
        else if (rayHit.transform.gameObject == obj3)
        {
            transform.position = rayHit.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            maxmoveYutcount = pastYutcount3;
            Yutorder = 3;
            Gamemanager.Instance.Footholdbox.positiondestory();
            changeYutzero();
        }
        else
        {
            return;
        }
        //Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)MaxmoveYutcount);
        //Gamemanager.Instance.holdboxPosCheck(MaxmoveYutcount, gameObject);
    }

    public void ManagerYutorderCheck(float _ClearNumber)//������ ��ư�� ������ �̵��� �� ���� ���ڸ� �ʱ�ȭ
    {
        //Debug.Log(gameObject);
        Yutorder = _ClearNumber;
        changeYutzero();
    }

    private void changeYutzero()//���� ������ �Ŀ� �̵��� ���ڸ� 0���� ����� �ڵ�
    {
        //GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        //Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        Yutstartbutton buttontimer = Gamemanager.Instance.Yutstartbuttons;
        switch(Yutorder)
        {
            case 1:
                buttontimer.oneyut = 0;
                oneYut = buttontimer.oneyut;
                //buttontimer.NotCheck1();
                break;
            case 2:
                buttontimer.twoyut = 0;
                twoYut = buttontimer.twoyut;
                //buttontimer.NotCheck2();
                break;
            case 3:
                buttontimer.threeyut = 0;
                threeYut = buttontimer.threeyut;
                //buttontimer.NotCheck3();
                break;
        }
        //posmovecheck();
        MoveCheckControl();
        #region
        //pointCombine();//�����濡�� ������ �߸� �̻��ѵ��� ǥ�ð� �ߴ� ������ ��ġ�� ���� �ڵ�
        //pointCombine2();
        //goPlayer = true;//�����̱� �����ϸ� true
        //movecheck = false;
        //shortcutCheck = false;
        //yutcontrol = eRule.notrecall;
        ////Gamemanager.Instance.Footholdbox.movecheckchange(oneYut, twoYut, threeYut);
        //Gamemanager.Instance.Footholdbox.movedestory();
        ////Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
        //findplayd.SetActive(false);
        #endregion
        //Debug.Log(moveYutcount1);
        //Debug.Log(moveYutcount2);
        //Debug.Log(moveYutcount3);
        //Debug.Log(MaxmoveYutcount);
        if(oneYut + twoYut + threeYut == 0)
        {
            NextTurnCheck = true;
        }
    }

    public void MoveCheckControl()
    {
        pointCombine();//�����濡�� ������ �߸� �̻��ѵ��� ǥ�ð� �ߴ� ������ ��ġ�� ���� �ڵ�
        pointCombine2();
        goPlayer = true;//�����̱� �����ϸ� true
        //movecheck = false;
        shortcutCheck = false;
        yutcontrol = eRule.notrecall;
        Gamemanager.Instance.Footholdbox.movedestory();
        findplayd.SetActive(false);
    }

    private void pointCombine()//�� ������� ���� �� ��� ������� maxmoveYutcount�� 50������ ���� ��Ų��
    {
        float point = maxmoveYutcount;
        if (point == 20 || point == 31 || point == 37 || point == 41)
        {
            maxmoveYutcount = 49;
        }
    }

    private void pointCombine2()//3��° ��ġ�� ���� ��Ű��
    {
        float point = maxmoveYutcount;
        if(point == 15 || point == 26 || point == 44)
        {
            maxmoveYutcount = 44;
        }
    }

    private void postest()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Player player = GetComponent<Player>();
            Debug.Log(player.gameObject);
            Debug.Log(player.maxmoveYutcount);
        }
    }

    public void AgainStartPos()//���� ������ �ٽ� ó������ ���ư��� �ڵ�
    {
        goPlayer = false;
        transform.position = startmypos;
        maxmoveYutcount = 0;
        NotShortcutArrive();
    }

    //private void posmovecheck()//�̵��Ŀ� �÷��̾�� ��ġ Ȯ�� ���ִ� �κ�
    //{
    //    int count = Gamemanager.Instance.objblue.Count;
    //    for(int iNum = 0; iNum < count; iNum++)
    //    {
            
    //    }
    //}

    public void DesTeam()//���� ��� ��� ������� ����� �ڵ� �κ�
    {
        goPlayer = false;
        transform.position = startmypos;
        maxmoveYutcount = 0;
        gameObject.SetActive(false);
        Curryobj1.SetActive(false);
        NotShortcutArrive();
    }

    /// <summary>
    /// ���� �������� �۵��ϴ� �ڵ�
    /// </summary>
    /// <param name="_Currycheck">�̹� ���� �ϳ� �����ٸ� true</param>
    /// <param name="_teamtype">�� Ÿ�� �� �������� �������� ������� �������� Ȯ��</param>
    public void CurryTeam(bool _Currycheck)//���� ���� ����
    {
        //if(_teamtype == 1)
        //{
        //    CurryBlue = true;
        //}
        //else if(_teamtype == 2)
        //{
        //    CurryRed = true;
        //}

        if(_Currycheck == true)
        {
            Curryobj1.SetActive(true);
            Curryobj2.SetActive(true);
            Gamemanager.Instance.WithdrawalCheck();
        }
        else
        {
            if (Curryobj1.activeSelf == true)//activeSelf == ������Ʈ�� �����ִ��� �����ִ��� Ȯ�����ִ� �ڵ�
            {
                Curryobj2.SetActive(true);
            }
            else if (Curryobj1.activeSelf == false)
            {
                Curryobj1.SetActive(true);
            }
            Gamemanager.Instance.CheckCurry();
        }
    }

    public void DesCurryTeam()//���� ���� ����
    {
        Curryobj1.SetActive(false);
        Curryobj2.SetActive(false);
    }


    public void ShortcutArrive(int _countYut)//�����濡 ������ �۵��Ǵ� �ڵ�
    {
        countYut = _countYut;
        shortcutCheck = true;
        if(countYut == 3)
        {
            maxmoveYutcount = 38;
        }
        else if(countYut == 4)
        {
            maxmoveYutcount = 44;
        }
    }

    public void NotShortcutArrive()//�����濡 ������ �۵��Ǵ� �ڵ�
    {
        shortcutCheck = false;
    }

    //�����游 ���� �������ִ� �ڵ� �κе� (����������� ���� ������)

    private void countShortcut()//������ �κ��� ���� ����
    {
        switch (countYut)
        {
            case 1://ù��° �ٿ� �ִ� ������
                pastYutcount1 = pastYutcount1 + 15 + oneYut;
                pastYutcount2 = pastYutcount2 + 15 + twoYut;
                pastYutcount3 = pastYutcount3 + 15 + threeYut;
                BackYutCheck();
                Gamemanager.Instance.Footholdbox.fastfindposition(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                break;
            case 2://2��° �ٿ� �ִ� ������
                pastYutcount1 = pastYutcount1 + 21 + oneYut;
                pastYutcount2 = pastYutcount2 + 21 + twoYut;
                pastYutcount3 = pastYutcount3 + 21 + threeYut;
                BackYutCheck();
                Gamemanager.Instance.Footholdbox.Centerfindposition(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                break;
            case 3://���� ���� �߾ӿ� ���� ���
                maxmoveYutcount = 38;
                pastYutcount1 = 41 + oneYut;
                pastYutcount2 = 41 + twoYut;
                pastYutcount3 = 41 + threeYut;
                Gamemanager.Instance.Footholdbox.lastfindposition(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                break;
            case 4:
                if (MaxmoveYutcount == 44)
                {
                    if (oneYut == -1)
                    {
                        pastYutcount1 -= 1;
                    }
                    else if (twoYut == -1)
                    {
                        pastYutcount2 -= 1;
                    }
                    else if (threeYut == -1)
                    {
                        pastYutcount3 -= 1;
                    }
                    Gamemanager.Instance.Footholdbox.DesPos(pastYutcount1, pastYutcount2, pastYutcount3, maxmoveYutcount);
                }
                break;
        }
    }

    public void LastChangeYutPos(float _Pos1, float _Pos2, float _Pos3)//�߾ӿ��� ������ ������ ���� �������� �ֱ� ���� �۾�
    {
        pastYutcount1 = _Pos1;
        pastYutcount2 = _Pos2;
        pastYutcount3 = _Pos3;
    }

    public void BackYutCheck()
    {
        if(oneYut == -1)
        {
            pastYutcount1 += 1;
        }
        else if(twoYut == -1)
        {
            pastYutcount2 += 1;
        }
        else if(threeYut == -1)
        {
            pastYutcount3 += 1;
        }
    }

    //����Ʈ�� �� �̵� �׽�Ʈ�ڵ� �κ�
    private void yutPosCount1()
    {
        if (maxmoveYutcount == 49 && oneYut > 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.ExitPlayer1();
            Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
            return;
        }
        //���� ã����� ����Ʈ �� �̸� => chagefoothold
        for (int iNum = 0; iNum < oneYut; iNum++)
        {
            Transform moveYut = Gamemanager.Instance.Footholdbox.findYut(Gamemanager.Instance.Footholdbox.Yutfoothold[(int)moveYutcount1 + iNum].gameObject);
            Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[0].transform;
            //Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform;
            //����: �ϳ��ϳ��� �ø��� ���� �������  �����¸���Ʈ�� ��ġ�� �״�� �� => �ذ�
            if (names == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount1 += iNum;
                Yutorder = 1;
                Gamemanager.Instance.Footholdbox.ExitPlayer1();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
            else if(Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount1 += iNum;
                Yutorder = 1;
                Gamemanager.Instance.Footholdbox.ExitPlayer1();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
        }
        moveYutcount1 += oneYut;
        if(moveYutcount1 == 0 && oneYut == -1)//���� ���� ó����
        {
            moveYutcount1 = 49;
        }
        else if(maxmoveYutcount == 21 && oneYut == -1)
        {
            moveYutcount1 = 5;
        }
        else if(maxmoveYutcount == 32 && oneYut == -1)
        {
            moveYutcount1 = 10;
        }
        else if(maxmoveYutcount == 44  && oneYut == -1)
        {
            moveYutcount1 = 14;
        }

        if(maxmoveYutcount == 42)
        {
            maxmoveYutcount = 24;
        }
        //else if (Gamemanager.Instance.Footholdbox.Yutfoothold[15].gameObject && oneYut == -1)
        //{
        //    Debug.Log("�׽�Ʈ");
        //}
    }
    private void yutPosCount2()
    {
        if (maxmoveYutcount == 49 && twoYut > 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.ExitPlayer1();
            Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
            return;
        }
        //���� ã����� ����Ʈ �� �̸� => chagefoothold
        for (int iNum = 0; iNum < twoYut; iNum++)
        {
            Transform moveYut = Gamemanager.Instance.Footholdbox.findYut(Gamemanager.Instance.Footholdbox.Yutfoothold[(int)moveYutcount2 + iNum].gameObject);
            Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[0].transform;
            //Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform;
            if (names == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount2 += iNum;
                Yutorder = 2;
                Gamemanager.Instance.Footholdbox.ExitPlayer2();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
            else if (Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount2 += iNum;
                Yutorder = 2;
                Gamemanager.Instance.Footholdbox.ExitPlayer2();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
        }
        moveYutcount2 += twoYut;
        if (moveYutcount2 == 0 && twoYut == -1)
        {
            moveYutcount2 = 49;
        }
        else if (maxmoveYutcount == 21 && twoYut == -1)
        {
            moveYutcount2 = 5;
        }
        else if (maxmoveYutcount == 32 && twoYut == -1)
        {
            moveYutcount2 = 10;
        }
        else if (maxmoveYutcount == 44 && twoYut == -1)
        {
            moveYutcount2 = 14;
        }
        if (maxmoveYutcount == 42)
        {
            maxmoveYutcount = 24;
        }

    }
    private void yutPosCount3()
    {
        if (maxmoveYutcount == 49 && threeYut > 0)
        {
            Yutorder = 1;
            Gamemanager.Instance.Footholdbox.ExitPlayer1();
            Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
            return;
        }
        //���� ã����� ����Ʈ �� �̸� => chagefoothold
        for (int iNum = 0; iNum < threeYut; iNum++)
        {
            Transform moveYut = Gamemanager.Instance.Footholdbox.findYut(Gamemanager.Instance.Footholdbox.Yutfoothold[(int)moveYutcount3 + iNum].gameObject);
            Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[0].transform;
            //Transform names = Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform;
            if (names == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount3 += iNum;
                Yutorder = 3;
                Gamemanager.Instance.Footholdbox.ExitPlayer3();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
            else if (Gamemanager.Instance.Footholdbox.Yutfoothold[49].transform == moveYut && moveYutcount1 != 0 && iNum != 0)
            {
                moveYutcount3 += iNum;
                Yutorder = 3;
                Gamemanager.Instance.Footholdbox.ExitPlayer3();
                Gamemanager.Instance.PosClearYut(gameObject, Yutorder);
                return;
            }
        }
        moveYutcount3 += threeYut;
        if (moveYutcount3 == 0 && threeYut == -1)
        {
            moveYutcount3 = 49;
        }
        else if (maxmoveYutcount == 21 && threeYut == -1)
        {
            moveYutcount3 = 5;
        }
        else if (maxmoveYutcount == 32 && threeYut == -1)
        {
            moveYutcount3 = 14;
        }
        else if (maxmoveYutcount == 44 && threeYut == -1)
        {
            moveYutcount3 = 14;
        }
        if (maxmoveYutcount == 42)
        {
            maxmoveYutcount = 24;
        }
    }

    public void DesObj(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        //findplayd.SetActive(false);
        Gamemanager.Instance.Footholdbox.movedestory();
    }

    public void ExitTurnPass()//Ż�� ��ư�� ���� ������ ��� ���� �ѱ�� ��
    {
        oneYut = Gamemanager.Instance.Yutstartbuttons.oneyut;
        twoYut = Gamemanager.Instance.Yutstartbuttons.twoyut;
        threeYut = Gamemanager.Instance.Yutstartbuttons.threeyut;
        changeYutzero();
        Gamemanager.Instance.MovePlayerFootHold(gameObject, (int)maxmoveYutcount);
        Gamemanager.Instance.holdboxPosCheck(maxmoveYutcount, gameObject);
        if (NextTurnCheck == true)
        {
            NextTurnCheck = false;
            Gamemanager.Instance.turnendcheck(oneYut, twoYut, threeYut);
        }
        movecheck = false;
    }

    public void DesYutPlayer()
    {
        if (Curryobj1.activeSelf == true)
        {
            if (Curryobj2.activeSelf == true)
            {
                Gamemanager.Instance.ListClear();
            }
            else
            {
                Gamemanager.Instance.LookAtYutPlayer();
            }
        }
    }
}