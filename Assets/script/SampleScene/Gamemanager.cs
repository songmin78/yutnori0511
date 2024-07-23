using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [Header("Ʃ�丮�� �κ�(Ʃ�丮�� �������������� ����)")]
    [SerializeField,Tooltip("Ʃ�丮�� ���������� ������ True�� ����")] bool tutorialStageCheck;
    bool onlyStory1 = true;//�� �ѹ��� ��� ������ ����
    bool onlyStory2 = true;//�� �ѹ��� ��� ������ ����
    public bool TutorialStageCheck
    {
        get
        {
            return tutorialStageCheck;
        }
    }

    [Header("�Ϲݰ���")]
    [SerializeField] Button ClearButton;
    Animation anim;
    Animator animator;
    [SerializeField] Image startcheck;
    TMP_Text tmp;

    [SerializeField] List<GameObject> objblue;
    [SerializeField] List<GameObject> objred;

   public class cObjectWhereFootHold
    {
        public GameObject objPlayer;
        public Transform trsFootHold;
    }

    //�÷����ϴ� ���� �������� �ִٸ� ����Ʈ�� ����Ͽ� ����ִ��� ����ϴ� �뵵�� Ȱ��
    List<cObjectWhereFootHold> listObjectWhereFootHold = new List<cObjectWhereFootHold>();

    public static Gamemanager Instance;
    //[Header("���� �̵� �� ��ġ�� �����ִ� ������Ʈ ����")]
    //[SerializeField] public GameObject movelocation1;
    //[SerializeField] public GameObject movelocation2;
    //[SerializeField] public GameObject movelocation3;
    [Header("��Ÿ")]
    //[SerializeField] TMP_Text LookYut;
    public int Gameplayertype = 0;//1�� 1�� �����϶� 2�� 2�� �����϶�
    //�����Ҷ� ���� ���� ���� �������� Ȯ���ϴ� �κ�
    float changecheck = 0;
    float changetimer = 0.1f;//��ü�ɶ� ��;
    bool starttype;
    float startturnyut = 5;
    float Maxstartturnyut;
    //��
    //���� ������ �κ��� ���� ThrowYut�κ�
    public bool throwyutbutton = false;//��ư�� ������ �����ϴ°� ��
    [Header("�� Ÿ�̸� �κ�")]
    [SerializeField, Tooltip("�� Ÿ�̸� �۵� �κ�")] GameObject Playtimemanager;
    [SerializeField, Tooltip("�� Ÿ�̸Ӱ� �پ��� �����")] GameObject Yuttimer;
    [SerializeField, Tooltip("�÷��̾��̵��� �پ��� �����")] GameObject PlayerTimer;
    [SerializeField] GameObject Yutbox;
    [SerializeField] Button throwbutton;
    //��
    //ĳ���� ���� �� �̵� �κ� SelectCharacter�κ�
    [SerializeField] GameObject playerbox;//�÷��̾�� ĳ���͵��� ���̰� ���ִ� ������Ʈ
    [SerializeField] int teamtype = 0;//1�� ����� 2�� ������

    //bool returncheck;//�ٽ� �ǵ����� ���̴� �ڵ�
    //��
    //���� �ѱ涧 ���̴� �κ�
    [SerializeField] GameObject Yutstartbutton;
    //waittime �κ� ��� ��ٸ��� �ڵ� �κ�
    float timewait = 0.5f;
    float Maxtimewait;
    Vector3 StartPos;
    [Header("���� �����ִ� ��üũ")]
    [SerializeField] bool CurryBlue;
    [SerializeField] bool CurryRed;
    bool BlueCurryCheck;//����� 2������ ���������� true
    bool RedCurryCheck;//������ 2������ ���������� true

    private Player player;
    private Footholdbox footholdbox;
    private Yutstartbutton yutstartbutton;
    private Playtimer playtimer;
    private SceneChange sceneChange;
    private TutorialStory tutorialStory;
    //������� ����� ���� �˷��ִ� ������Ʈ
    GameObject Clearobj;
    float ClearNumber;

    bool BackCheck = false;

    bool TurnCycleCheck;//��Ҵٸ� �� ���� �ٽ� ���� �Ҽ� �ְ� �����ִ� �κ�
    [Header("����� �� ���� �ڵ� �κ�")]
    [SerializeField] Canvas WinerTeamCanvas;
    //[SerializeField] Button AgainButton;//�ٽ��ϱ� ��ư
    //[SerializeField] Button LobiButton;//�κ�� ���ư��� ��ư
    [SerializeField] TMP_Text WinerTeamString;
    bool WinerRed;
    bool WinerBlue;
    

    public enum eRule
    {

        Preferencetime,//���� ���� ������ �켱�� �ð�
        ThrowYut,
        SelectCharacter,//ĳ���͸� ����
        ReturnthrowYut,//�ٽ� ���� ������ �κ�
        waittime,//��� ��ٸ��� �κ�
        RecycleTime,//�ٽ� ���� �� �÷��̾� ��ġ �ڵ尡 �� ���ư��� ��ġ
        GameClear,//������ Ŭ���� �Ҷ� �۵��ǰ� �ϴ� �κ�
    }
    private eRule curState = eRule.Preferencetime;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }


    public Footholdbox Footholdbox
    {
        get { return footholdbox; }
        set { footholdbox = value; }
    }

    public Yutstartbutton Yutstartbuttons
    {
        get { return yutstartbutton; }
        set { yutstartbutton = value; }
    }

    public Playtimer Playtimer
    {
        get { return playtimer; }
        set { playtimer = value; }
    }

    public SceneChange SceneChange
    {
        get { return sceneChange; }
        set { sceneChange = value; }
    }

    public TutorialStory TutorialStory
    {
        get { return tutorialStory; }
        set { tutorialStory = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        TMP_Text tmp = GetComponent<TMP_Text>();
        Animation anim = GetComponent<Animation>();
        Animator animator = GetComponent<Animator>();
        Maxstartturnyut = startturnyut;
        Maxtimewait = timewait;

        #region ���� ���� ������ ��ư�� �ٷ�� �κ�
        ClearButton.onClick.AddListener(() =>
        {
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.objPlayer == Clearobj);
            listObjectWhereFootHold.Remove(data);
            if (objblue.Find(x => x.gameObject == Clearobj) != null)
            {
                objblue.Remove(Clearobj);
            }
            else if (objred.Find(x => x.gameObject == Clearobj) != null)
            {
                objred.Remove(Clearobj);
            }
            Player.DesYutPlayer();
            //Clearobj.gameObject.transform.position = new Vector3(0, -10, 0);
            Footholdbox.positiondestory();
            Player.ManagerYutorderCheck(ClearNumber);
            Clearobj.gameObject.SetActive(false);
            ClearButton.gameObject.SetActive(false);

            if(objblue.Count  == 0)
            {
                Debug.Log("����� Ŭ����");
                WinerBlue = true;
                teamWinCheck();
                curState = eRule.GameClear;
                Playtimer.StayTurnTime();
            }
            else if(objred.Count == 0)
            {
                Debug.Log("������ Ŭ����");
                WinerRed = true;
                teamWinCheck();
                curState = eRule.GameClear;
                Playtimer.StayTurnTime();
            }

            Player.ExitTurnPass();
        });
        #endregion
    }

    //public bool playertouch;//Ŭ�� ������ on���� ��ȯ Ŭ���� ������ off�� ��ȯ

    void Start()
    {
        startcheck.gameObject.SetActive(true);
        changecheck = 0;
    }

    void Update()
    {
        testText();
        //Onclickplayer();

        //testcode();
        if (curState == eRule.Preferencetime)
        {
            startturn();
            startchageturn();
        }
        else if (curState == eRule.ThrowYut)
        {
            Throwtime();
        }
        else if (curState == eRule.SelectCharacter)
        {
            Yutbox.SetActive(false);
            playertypechoice();
            //positionobjcheck();
        }
        else if (curState == eRule.ReturnthrowYut)
        {
            changeturn();
        }
        else if (curState == eRule.waittime)
        {
            waitingtimer();
        }
        else if(curState == eRule.RecycleTime)
        {
            return;
        }
        else if(curState == eRule.GameClear)
        {

        }
    }

    #region
    //private void Onclickplayer()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        if (Gameplayertype == 1)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player1");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //player.playerchoice = true;
    //            //Debug.Log(playerfind);
    //            //Debug.Log(player);

    //            Player player = objplayer1_1.GetComponent<Player>();
    //            Player player2 = objplayer1_2.GetComponent<Player>();
    //            player.playerchoice = true;
    //            //player.checkobj.SetActive(true);
    //            //player.playertype1 = true;
    //            player2.playerchoice = true;
    //            //player2.checkobj.SetActive(true);
    //            //player2.playertype2 = true;
    //        }
    //        else if (Gameplayertype == 2)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player2");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //player.playerchoice = true;

    //            Player player = objplayer2_1.GetComponent<Player>();
    //            Player player2 = objplayer2_2.GetComponent<Player>();
    //            player.playerchoice = true;
    //            player2.playerchoice = true;
    //        }

    //        #region
    //        //Player player = gameObject.GetComponent<Player>();

    //        //GameObject playerfind = GameObject.Find("Player1");
    //        //Debug.Log(playerfind);
    //        //Player player = playerfind.GetComponent<Player>();
    //        //player.playerchoice = true;
    //        //Debug.Log("�۵�");

    //        //GameObject playerfind = GameObject.FindGameObjectWithTag("player");
    //        //Player player = playerfind.GetComponent<Player>();
    //        //player.playerchoice = true;
    //        //Debug.Log(playerfind);
    //        #endregion
    //    }
    //}


    //private void testcode()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        GameObject tags = GameObject.FindGameObjectWithTag("player");
    //        Player player = tags.GetComponent<Player>();
    //        player.tests = true;
    //        Debug.Log("�۵�");
    //    }
    //}

    #endregion

    private void selectcharactor(GameObject _value)//�ٸ� ������Ʈ�� ������ ������ ������Ʈ�� ���� �ڵ�
    {
        //Debug.Log(teamtype);
        if (teamtype == 1)//������� ���
        {
            int count = objblue.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objblue[iNum].GetComponent<Player>();
                selPlayer.Playselectedcheck(_value == objblue[iNum]);
                //if (objred[iNum] == true)
                //{
                //    selPlayer.lookobj();
                //}
            }
        }
        else if (teamtype == 2)
        {
            int count = objred.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objred[iNum].GetComponent<Player>();
                selPlayer.Playselectedcheck(_value == objred[iNum]);
                //if (objred[iNum] == true)
                //{
                //    selPlayer.lookobj();
                //}
            }
        }
    }

    //public void teamfalsecheck()//���� ��ũ�� ������� ����� �ڵ� �κ�
    //{
    //    if (teamtype == 1)//������� ���
    //    {
    //        int count = objred.Count;
    //        for (int iNum = 0; iNum < count; ++iNum)
    //        {
    //            Player selPlayer = objred[iNum].GetComponent<Player>();
    //            selPlayer.Playselectedcheck(false);
    //        }
    //    }
    //    else if (teamtype == 2)
    //    {
    //        int count = objblue.Count;
    //        for (int iNum = 0; iNum < count; ++iNum)
    //        {
    //            Player selPlayer = objblue[iNum].GetComponent<Player>();
    //            selPlayer.Playselectedcheck(false);
    //        }
    //    }
    //}


    private void playertypechoice()//�÷��̾ �������� ���
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.transform != null && rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //Debug.Log(rayHit.transform.name);
                DesYutButton();//������ �ִ� ��ư�� �����ϴ� �κ� �ٸ� �÷��̾ ���� �Ҷ� �����ϵ��� ����
                Footholdbox.movedestory();//�̵� ǥ���� �� ������ �̵�
                Footholdbox.ExitPlayerFalse();// ���� ������ ������ �� ��ġ�� �̵� ǥ�ð� �ߴ°��� �����ϱ� ���� �ڵ�� �̵���
                selectcharactor(rayHit.transform.gameObject);
                if (tutorialStageCheck == true && onlyStory1 == true)
                {
                    onlyStory1 = false;
                    TutorialStory.TimeOff();
                    return;
                }
                //Player selPlayer = rayHit.transform.GetComponent<Player>();
                //selPlayer.Playselectedcheck(true);

            }
        }
    }

    //private void findplayerteam()//������ �÷��̾�ĳ���� Ȯ��?
    //{
    //    GameObject startteam = GameObject.Find("Playtimemanager");
    //    Playtimer startplayer = startteam.GetComponent<Playtimer>();
    //    startplayer.changeteam();
    //}

    /// <summary>
    /// team = 1 <= �����, team = 2 <= ������
    /// </summary>
    /// <param name="team"></param>
    public void Chageplayteam(int team)
    {
        switch (team)
        {
            case 1:
                teamtype = team;
                break;
            case 2:
                teamtype = team;
                break;
        }

    }

    //private bool isFindData(out int value)
    //{
    //    value = 10;
    //    return true;
    //}

    //�����ϰ� �� ü������ �ȵǴ� ������ ���� 07/05����
    private void startturn()//ó���� ���� ���� �����ϴ��� �˷��ִ� �ڵ�
    {
        animator = startcheck.gameObject.GetComponentInChildren<Animator>();
        Maxstartturnyut -= Time.deltaTime;
        if (Maxstartturnyut < 2)//3�ʰ� ���� ���
        {
            if (starttype == false)
            {
                changecheck = Random.Range(0, 2);
                starttype = true;
                if (tutorialStageCheck == true)
                {
                    changecheck = 1;
                }
            }
            else
            {
                animator.SetFloat("CharacterChange", changecheck);
            }

            if (Maxstartturnyut < 0)//0���� ���� ��� �� �ð��� �� �� ���
            {
                if (tutorialStageCheck == true)
                {
                    changecheck = 1;
                    TutorialStory.TimeOff();
                }
                Maxstartturnyut = startturnyut;
                starttype = false;
                startcheck.gameObject.SetActive(false);
                curState = eRule.ThrowYut;
                throwyutbutton = true;
                Playtimer.LookScene();
                startTeamTurn();
            }

        }
        else//3�ʰ� �� ���� ���
        {
            if (changecheck == 0 && changetimer == 0.1f)//3�ʰ� ���� ���
            {
                changecheck = 1;
                animator.SetFloat("CharacterChange", changecheck);

            }
            else if (changecheck == 1 && changetimer == 0.1f)
            {
                changecheck = 0;
                animator.SetFloat("CharacterChange", changecheck);
            }
        }
    }

    private void startchageturn()//������ ���Ҷ� 0.1�ʸ��� ĳ�� �Ͱ� ����ǵ��� ����
    {
        if (Maxstartturnyut < 2)
        {
            return;
        }
        changetimer -= Time.deltaTime;
        if (changetimer < 0)
        {
            changetimer = 0.1f;
        }
    }

    /// <summary>
    /// �Ķ��� �÷��̾�� ��ũ�� �ڵ�
    /// �Ķ� == 1 // ��ũ ==0
    /// </summary>
    private void Throwtime()//���� ������ �κ��� �����ϴ� �ڵ�
    {
        if (throwyutbutton == true)
        {
            throwbutton.gameObject.SetActive(true);
            Playtimemanager.SetActive(true);
            Yuttimer.SetActive(true);
            PlayerTimer.SetActive(false);
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            //Playtimer startplayer = Playtimemanager.GetComponent<Playtimer>();
            //startplayer.startturn((int)changecheck);
            Playtimer.startturn((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yutbox.SetActive(false);
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
            curState = eRule.SelectCharacter;
        }
    }

    #region
    //private void positionobjcheck()//�̵��� ������Ʈ ��ġ üũ
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0) == true)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
    //        if (rayHit.transform != null && rayHit.transform.gameObject == movelocation1)
    //        {
    //            //movepositioncheck();
    //        }
    //    }
    //}

    //private void movepositioncheck()//ĳ���Ͱ� �̵��� �ڵ�
    //{
    //    if (movelocation1)
    //    {
    //        transform.position = movelocation1.transform.position;
    //    }
    //}
    #endregion

    public void turnendcheck(float _oneYut, float _twoYut, float _thrYut)
    {
        if (TurnCycleCheck == true)
        {
            //TurnCycleCheck = false;
            return;
        }
        if (_oneYut + _twoYut + _thrYut == 0)
        {
            curState = eRule.ReturnthrowYut;
            throwyutbutton = true;
            if (changecheck == 0)
            {
                changecheck = 1;
            }
            else
            {
                changecheck = 0;
            }
            Yutstartbuttons.getbackYut();
            Playtimer.TurnChangeCheck();
            //Yutstartbutton yutstartbutton = Yutstartbutton.GetComponent<Yutstartbutton>();
            //yutstartbutton.getbackYut();
            //Player.MoveCheckControl();
            Yutstartbuttons.NotCheckTrue();
            if(tutorialStageCheck == true && onlyStory2 == true)
            {
                onlyStory2 = false;
                TutorialStory.TimeOff();
            }
        }
        else//���� ���� 0�� �ƴ� ���
        {
            curState = eRule.waittime;
        }
    }

    private void changeturn()//�÷��̾� ���� �����ϴ� �ڵ�
    {
        if (throwyutbutton == true)
        {
            throwbutton.gameObject.SetActive(true);
            Playtimemanager.SetActive(true);
            Yuttimer.SetActive(true);
            PlayerTimer.SetActive(false);
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            Playtimer.turnendchange((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            Yutbox.SetActive(false);
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
            curState = eRule.SelectCharacter;
        }
    }

    private void startTeamTurn()
    {
        throwbutton.gameObject.SetActive(true);
        //Playtimemanager.SetActive(true);
        Yuttimer.SetActive(true);
        PlayerTimer.SetActive(false);
        Yutbox.SetActive(true);
        playerbox.SetActive(true);

        //Playtimer.startturn((int)changecheck);
        Playtimer.StartTurnTime();
        Playtimer.turnendchange((int)changecheck);
        Yutstartbuttons.ControlCheck1();
    }


    private void waitingtimer()
    {
        Maxtimewait -= Time.deltaTime;
        if (Maxtimewait < 0)
        {
            Maxtimewait = timewait;
            curState = eRule.SelectCharacter;
        }
    }

    public void nextturn()
    {
        throwyutbutton = false;
        changeturn();
    }

    //���� �����϶� ������  ���� ���� ��ȣ�ۿ�
    //Footholdbox���� list���ִ� ��ġ�� � ������Ʈ�� �ִ��� Ȯ���� �ʿ�
    //���� ��ȣ�ۿ��� �۵��� �� �׷��� list���� ������Ʈ�� �ִ� �κп��� ������ ����
    public void holdboxPosCheck(float _MaxmoveYutcount,GameObject dplayer)
    {
        //���� ���� �̵��� ��ġ�� ������ ���� �����ϴ���
        //Footholdbox.Yutfoothold[(int)_MaxmoveYutcount];
        if (IsPositionExistPlayer((int)_MaxmoveYutcount, out GameObject outplayer) == true)//���� ���� �̵��� ��ġ�� ������ ���� �����ϴ��� true�� �ִ°�
        {
            //_MaxmoveYutcount�� ���� �ִ� ��ġ, �� �̵��� ��ġ�� �ִ� ����Ʈ�� �����÷��� moveYutcount �κ��� �ʿ�
            //player�� �Ʊ����� �������� �Ǵ� 
            #region if���� Ȱ���Ͽ� ������ ������� Ȯ�� ���ִ� �ڵ�
            //if (outplayer.gameObject.tag == "RedTeam" && teamtype == 1)//����� �ð��϶�(teamtype == 1) �������� ���� ���
            //{
            //    //outplayer�� �´� ���� ��ġ�� �� �������ϴ� �κ�
            //    Player players = outplayer.GetComponent<Player>();//�� �ڸ��� �ִ� ������Ʈ�� �ִ� �÷��̾� ��ũ��Ʈ�� �ҷ��´�
            //    players.AgainStartPos();
            //}
            //else if(outplayer.gameObject.tag == "BlueTeam" && teamtype == 1)//����� �ð��϶�(teamtype == 1)�϶� ������� ���� ��� ���´�
            //{
            //    Player players = outplayer.GetComponent<Player>();
            //    //���� ���� ����� ���� �ȵ�
            //}
            #endregion

            #region switch ������ ������� �ڵ� ���� ���� ����� �ڵ� �κ�
            ////Debug.Log(outplayer);
            //Player players = outplayer.GetComponent<Player>();//�� �ڸ��� �ִ� ������Ʈ�� �ִ� �÷��̾� ��ũ��Ʈ�� �ҷ��´�
            ////Debug.Log(cObjectWhereFootHold.Equals(outplayer, _MaxmoveYutcount));
            ////teamtype == 1�̸� ����� ����,teamtype == 2�̸� ������ ����
            //switch (teamtype)
            //{
            //    //ó������ ��� ��쿡�� �� ���� �׷��� ������ ��� ���� �������� �õ��� �ϸ� ������ <- �ذ�
            //    //���� � ����� ���� ���� ��� ���� �� ������ ��찡 ���� => RemoveAt(0)�� ���� ����Ʈ�� ���� �ɽ� ���� ���� ������� ������ ����
            //    case 1: // ����� ���϶� 
            //        if(outplayer.gameObject.tag == "RedTeam")//�������� ���� ���
            //        {
            //            //Player players = outplayer.GetComponent<Player>();//�� �ڸ��� �ִ� ������Ʈ�� �ִ� �÷��̾� ��ũ��Ʈ�� �ҷ��´�
            //            players.AgainStartPos();
            //            listObjectWhereFootHold.RemoveAt(0);
            //            //if(cObjectWhereFootHold.Equals(_MaxmoveYutcount, outplayer) == true)
            //            //{

            //            //}

            //            //listObjectWhereFootHold.Remove()
            //            //Debug.Log("�������� ���");
            //        }
            //        else if(outplayer.gameObject.tag == "BlueTeam")//������� ���� ���
            //        {
            //            //Debug.Log("��������� ����");
            //            //���� ���� ����� ���� �ȵ�
            //            //Player players = outplayer.GetComponent<Player>();
            //        }
            //        break;
            //    case 2://������ �����϶�
            //        if (outplayer.gameObject.tag == "RedTeam")//�������� ���� ���
            //        {
            //            //Debug.Log("���������� ����");
            //            //Player players = outplayer.GetComponent<Player>();//�� �ڸ��� �ִ� ������Ʈ�� �ִ� �÷��̾� ��ũ��Ʈ�� �ҷ��´�
            //        }
            //        else if (outplayer.gameObject.tag == "BlueTeam")//������� ��� ���
            //        {
            //            //Debug.Log("��������� ���");
            //            //���� ���� ����� ���� �ȵ�
            //            //Player players = outplayer.GetComponent<Player>();
            //            players.AgainStartPos();
            //            listObjectWhereFootHold.RemoveAt(0);

            //        }
            //        break;
            //}
            //Debug.Log(outplayer);
            #endregion

            //��� ���� => 1.�̵��� ���� ��ġ�� �����´� 2.������ ��ġ�� ���� ����Ǿ��ִ� ��ġ�� �񱳸��Ͽ� ���� ��ġ�� ���� ����Ʈ�� �˾ƿ´�
            //3.����Ʈ�� �˾Ƴ��� �� ����Ʈ�� ���� ��Ų��
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.trsFootHold == footholdbox.Yutfoothold[(int)_MaxmoveYutcount]);
            //���� = > data�� ��ġ�� �ִ� ����Ʈ�ӿ��ִ� ������Ʈ�� �̵���Ű�� ���� �ƴ� ������ ���� �Ǿ����� 
            int MaxCount = listObjectWhereFootHold.Count;
            //Debug.Log(listObjectWhereFootHold[1].trsFootHold);
            for (int iNum = 0; iNum < MaxCount; iNum++)
            //data <= ���� �����̴� ������
            //listObjectWhereFootHold <= �� ������ �ִ� ��
            {
                if (data.trsFootHold == listObjectWhereFootHold[iNum].trsFootHold && dplayer != listObjectWhereFootHold[iNum].objPlayer)
                {
                    //Debug.Log(teamtype);
                    checkCount(data, dplayer, iNum);
                    break;
                }
                else
                {
                    continue;
                }
            }
            //Debug.Log(data.trsFootHold);
        }
    }

    private void checkCount(cObjectWhereFootHold data, GameObject _dplayer, int iNum)//���� ���� ���� ��쿡 ����Ǵ� �ڵ�
    {
        //Debug.Log(teamtype);
        //data <= ���� �����̴� ������
        //listObjectWhereFootHold <= �� ������ �ִ� ��

        //Player players = listObjectWhereFootHold[iNum].objPlayer.GetComponent<Player>();//�� �ڸ��� �ִ� ������Ʈ�� �ִ� �÷��̾� ��ũ��Ʈ�� �ҷ��´�
        player = listObjectWhereFootHold[iNum].objPlayer.GetComponent<Player>();
        GameObject ObjBackUp = listObjectWhereFootHold[iNum].objPlayer;//���� �� �ڸ����ִ� ���� ��� �صα����� ���� �ڵ�
        Player objs = ObjBackUp.gameObject.GetComponent<Player>();
        //Player players = data.objPlayer.GetComponent<Player>();
        #region
        //switch (teamtype)
        //{
        //    case 1: // ����� ���϶� 
        //        if (outplayer.gameObject.tag == "RedTeam")//�������� ���� ���
        //        {
        //            players.AgainStartPos();
        //            listObjectWhereFootHold.RemoveAt(iNum);
        //        }
        //        else if (outplayer.gameObject.tag == "BlueTeam")//������� ���� ���
        //        {
        //        }
        //        break;
        //    case 2://������ �����϶�
        //        if (outplayer.gameObject.tag == "RedTeam")//�������� ���� ���
        //        {
        //        }
        //        else if (outplayer.gameObject.tag == "BlueTeam")//������� ��� ���
        //        {
        //            players.AgainStartPos();
        //            listObjectWhereFootHold.RemoveAt(iNum);
        //        }
        //        break;
        //}
        #endregion
        switch (teamtype)//���� �����ϼ��ִ� �� ��
        {
            case 1:// ����� ���϶�
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//������ ���� �������� �±׸� �ް� ������ ��´�
                {
                    data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    player.AgainStartPos();//�������� ��ġ�� �ʱ�ȭ ��Ŵ
                    listObjectWhereFootHold.Remove(data);//��ġ�� �ʱ�ȭ ���Ŀ� ����Ʈ ����
                    assistantPlayerRed(data.objPlayer);//������ ������ �ٽ� �����ִ� �ڵ�
                    //player.DesCurryTeam();//�������� ������ �������� �����ϴ� �ڵ�
                    Yutstartbuttons.CatchReTurnTurn();
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//������ ���� ����� �±׸� �ް������� ���´�
                {
                    #region ����
                    //player.DesTeam();//��������  ���� ���̸� �Ⱥ��̰� �����ϴ� �ڵ�
                    //data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    //listObjectWhereFootHold.Remove(data);

                    //player = _dplayer.GetComponent<Player>();
                    //player.CurryTeam();
                    //CurryBlue = true;
                    #endregion
                    //NotCarryPrevention(_dplayer);//�������� �����
                    player = _dplayer.GetComponent<Player>();//��¸��� �����´�
                    if (player.GoPlayer == false)//player.MaxmoveYutcount != objs.MaxmoveYutcount
                    {
                        return;
                    }
                    player.DesTeam();//ó������ ������ ������Ʈ�� ����
                    data = listObjectWhereFootHold.Find(x => x.objPlayer == _dplayer);
                    listObjectWhereFootHold.Remove(data);
                    //���� �ڸ����ִ� ������Ʈ�� ������ �޾ƾ��Ѵ�
                    player = ObjBackUp.GetComponent<Player>();
                    //BlueCurryCheck 2������ ���������� 1���������� ���� ��쿡 2������ �����ٴ°��� �˷��ֱ� ���� ����
                    player.CurryTeam(BlueCurryCheck);

                    //1���� ���� 2���� ������ �������� ����X 2���� ���� 1���� ������ ������ ������ ����
                }
                break;
            case 2://������ ���϶�
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//������ ���� ������ �±׸� �ް��� ���� ���´�
                {
                    player = _dplayer.GetComponent<Player>();//��¸��� �����´�
                    //if (player.GoPlayer == false || player.MaxmoveYutcount != objs.MaxmoveYutcount)
                    //{
                    //    return;
                    //}
                    //������ ���ݱ��� ����Ʈ���� ���� �̸����� ó���� �ϴٺ��� �̰� ���� ��� ��ġ�� ����Ʈ������ �� ������ �ȴ�
                    if (player.GoPlayer == false)//�Ⱥ��̴� ������ �ϳ� �� �����ؼ� �ذ��
                    {
                        return;
                    }
                    player.DesTeam();//ó������ ������ ������Ʈ�� ����
                    data = listObjectWhereFootHold.Find(x => x.objPlayer == _dplayer);
                    listObjectWhereFootHold.Remove(data);
                    //���� �ڸ����ִ� ������Ʈ�� ������ �޾ƾ��Ѵ�
                    player = ObjBackUp.GetComponent<Player>();
                    player.CurryTeam(RedCurryCheck);
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//������ ���� ����� �±׸� �ް������� ��´�
                {
                    data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    player.AgainStartPos();
                    listObjectWhereFootHold.Remove(data);
                    assistantPlayerBlue(data.objPlayer);//������� ��´�
                    //player.DesCurryTeam();
                    Yutstartbuttons.CatchReTurnTurn();
                }
                break;
        }
        //turnendcheck(Player.oneYut, Player.twoYut, Player.threeYut);
    }

    public void CheckCurry()
    {
        if(teamtype == 1)
        {
            BlueCurryCheck = true;
        }
        else if(teamtype == 2)
        {
            RedCurryCheck = true;
        }
    }

    public void WithdrawalCheck()
    {
        if (teamtype == 1)
        {
            BlueCurryCheck = false;
        }
        else if (teamtype == 2)
        {
            RedCurryCheck = false;
        }
    }

    //���� �����϶� �ٸ��� ���� ���� ��ȣ�ۿ�
    //�����濡 ���� �� ������� �� �� �ִ� ��ȣ�ۿ�
    //�������� ���������� ������ �ִ� ��ȣ�ۿ�

    /// <summary>
    /// �÷��̾ �������� �̵���ŵ�ϴ�.
    /// </summary>
    /// <param name="_player">�̵��� �÷��̾�</param>
    /// <param name="_movePos">�̵��� ��ġ</param>
    public void MovePlayerFootHold(GameObject _player, int _movePos)
    {
        bool isExsitPlayer = listObjectWhereFootHold.Exists(x => x.objPlayer == _player);//����Ʈ�� �ش� �÷��̾ �����ϴ��� 
        //Debug.Log(_player);
        //<= ���� �����̴� �÷��̾ ��� ���� ������ �ƴ� �ش� ���ǿ� �ִ� ������Ʈ�� �����;� ��( player���� �۵��ؾߵǳ�?)
        //���Ӱ� �̵��Ҷ� �÷��̾ �����Ѵٰ� �� -> �̵��Ŀ� �� ���ǿ� �÷��̾ �ִ� ���� Ȯ������ �ٲ�ߵ�
        //�÷��̾ �̵��Ŀ� ��ġ�� �����ϰ� ���ο� �÷��̾ ���� �� ���ǿ� �ִ��� Ȯ�� ���ִ� �κ��� �ʿ���
        

        if (isExsitPlayer == false)//�÷��̾�� ���ǿ� ������ �����Ǿ�� ��(�ش� ���ǿ� �÷��̾ ���ٸ�)
        {
            cObjectWhereFootHold data = new cObjectWhereFootHold()
            {
                objPlayer = _player,
                trsFootHold = footholdbox.Yutfoothold[_movePos]
            };

            listObjectWhereFootHold.Add(data);
            PastlLoadCheck(data.trsFootHold, _player);
            //midcheck(_movePos);
            //Debug.Log(data.objPlayer);
            //Debug.Log(_player);
        }
        else//�÷��̾ ���ǿ� ������
        {
            //���� �÷��̾ �̵��� �ϸ� ������ �۵� �� => �� �ڱ� ��ġ���ǿ��� �̵��ϸ� �۵��Ǵ� �κ�
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.objPlayer == _player);
            data.objPlayer = _player;
            data.trsFootHold = footholdbox.Yutfoothold[_movePos];
            PastlLoadCheck(data.trsFootHold, _player);
            //midcheck(_movePos);
            //Debug.Log(_player,data.trsFootHold);
            //Debug.Log(data.objPlayer);
        }

        //Debug.Log(_player);
    }

    /// <summary>
    /// �ش� ��ġ�� �÷��̾ �����ϴ��� Ȯ���մϴ�.
    /// </summary>
    /// <param name="_pos">�ش���ġ�� Ȯ���մϴ�</param>
    public bool IsPositionExistPlayer(int _pos, out GameObject _player)
    {
        //���� ��ƹ����� ���� �ٸ� ���̶� ������ �ȵ�
        _player = default;//null�� �ʱ�ȭ
        Transform trsYutfoolhold = footholdbox.Yutfoothold[_pos];//üũ�� ��ġ <= �̵� �� ��ġ Ȯ��

        bool isExist = listObjectWhereFootHold.Exists(x => x.trsFootHold == trsYutfoolhold);
        //isExist true��� �ִ°�
        if (isExist == true)
        {
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.trsFootHold == trsYutfoolhold);//��ġ�� �� ����
            _player = data.objPlayer;//���⿡�� ������Ʈ�� ��ȯ�� �ȵ�
        }

        //Debug.Log(_player);
        return isExist;
    }

    private void assistantPlayerBlue(GameObject subplayer)
    {
        //GameObject subchild = subplayer.transform.GetChild(1).gameObject;//�������� �ڽĿ�����Ʈ�� �����´�
        if (subplayer.transform.GetChild(1).gameObject.activeSelf == false)//�� �ڽ� ������Ʈ�� �����ִٸ� ������ ���Ѵ�
        {
            return;
        }
        else
        {
            for (int iNum = 0; iNum < objblue.Count; iNum++)
            {
                GameObject subobj = objblue[iNum];
                if (subobj.activeSelf == false)
                {
                    subobj.SetActive(true);
                    BlueCurryCheck = false;
                }
            }
            player.DesCurryTeam();//�������� ������ �������� �����ϴ� �ڵ�
        }
    }
    
    private void assistantPlayerRed(GameObject subplayer)
    {
        //GameObject subchild = subplayer.transform.GetChild(1).gameObject;//�������� �ڽĿ�����Ʈ�� �����´�
        if(subplayer.transform.GetChild(1).gameObject.activeSelf == false)//�� �ڽ� ������Ʈ�� �����ִٸ� ������ ���Ѵ�
        {
            return;
        }
        else
        {
            for (int iNum = 0; iNum < objred.Count; iNum++)
            {
                GameObject subobj = objred[iNum];
                if (subobj.activeSelf == false)
                {
                    subobj.SetActive(true);
                    RedCurryCheck = false;
                }
            }
            player.DesCurryTeam();//�������� ������ �������� �����ϴ� �ڵ�
        }
    }

    //�������� ����� ���� ���̴� �ڵ���ġ
    public void PastlLoadCheck(Transform _data, GameObject _player)
    {
        //0 ~ 19������ �ܰ����� �κ� �������� 5,10�κ�
        //20~29������ ������ [5]���� �� ���� �������� ���� ��
        //30 ~ 34������ ������ [10]���� �� ���� �������� ���� ��
        //
        if (_data == Footholdbox.Yutfoothold[5] || _data == Footholdbox.Yutfoothold[10] || _data == Footholdbox.Yutfoothold[34]
            || _data == Footholdbox.Yutfoothold[23] || _data == Footholdbox.Yutfoothold[44])
        {
            player = _player.GetComponent<Player>();
            if(_data == Footholdbox.Yutfoothold[5])
            {
                player.ShortcutArrive(1);
            }
            else if(_data == Footholdbox.Yutfoothold[10])
            {
                player.ShortcutArrive(2);
            }
            else if (_data == Footholdbox.Yutfoothold[23] || _data == Footholdbox.Yutfoothold[34])
            {
                player.ShortcutArrive(3);
            }
            else if(_data == Footholdbox.Yutfoothold[44])
            {
                player.ShortcutArrive(4);
            }
        }
    }

    //���� ������ �� ���� ������ ������ �۵��ϵ��� ���� �κе�
    public void PosClearYut(GameObject _obj, float _Yutorder)//������ �ִ� ��ư�� �������ִ� �κ�
    {
        ClearButton.gameObject.SetActive(true);
        Clearobj = _obj;
        ClearNumber = _Yutorder;
    }

    public void DesYutButton()//������ �ִ� ��ư�� �����ϴ� �κ� �ٸ� �÷��̾ ���� �Ҷ� �Ǵ� ������ �ʾ����� ����
    {
        ClearButton.gameObject.SetActive(false);
    }

    public void CheckBackYutPass()//���� �����ε� �ʵ忡 ���� �ϳ��� ���� ���
    {
        int MaxCount = listObjectWhereFootHold.Count;
        if(teamtype == 1 )//����� �϶�
        {
            if (listObjectWhereFootHold.Count == 0)
            {
                Yutstartbuttons.ClaerYutCount();
                return;
            }
            BackCheck = true;//������ ã�� ������ ���� �� �ѱ�� �����ִ� �ڵ�
            for (int iNum = 0; iNum < MaxCount; iNum++)//�� ����Ʈ�� ���ؼ� ã�ƺ���
            {
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//�ʵ忡 ���� ���
                {
                    BackCheck = false;
                    return;
                }
                else if(listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam" )//for���� �������� ���� ���
                {
                    continue;
                }
            }
            if (BackCheck == true)//�ʵ忡 ���� ���� ��쿡 true�� ������
            {
                BackCheck = false;
                Yutstartbuttons.ClaerYutCount();
            }
            else//�ʵ忡 ���� ���� ���
            {
                throwbutton.gameObject.SetActive(false);
                Yuttimer.SetActive(false);
                PlayerTimer.SetActive(true);
                curState = eRule.SelectCharacter;
            }
        }
        else if(teamtype == 2)
        {
            if (listObjectWhereFootHold.Count == 0)
            {
                Yutstartbuttons.ClaerYutCount();
                return;
            }
            BackCheck = true;
            for (int iNum = 0; iNum < MaxCount; iNum++)//�� ����Ʈ�� ���ؼ� ã�ƺ���
            {
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//�ʵ忡 ���� ���
                {
                    BackCheck = false;
                    return;
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//for���� �������� ���� ���
                {
                    continue;
                }
            }
            if (BackCheck == true)//�ʵ忡 ���� ��쿡 true�� ������
            {
                BackCheck = false;
                Yutstartbuttons.ClaerYutCount();
            }
            else//�ʵ忡 ���� ���� ���
            {
                throwbutton.gameObject.SetActive(false);
                Yuttimer.SetActive(false);
                PlayerTimer.SetActive(true);
                curState = eRule.SelectCharacter;
            }
        }
    }

    public void PlayTimeTurn()//������ �ɸ��� �÷��� Ÿ������ �ѱ��
    {
        throwbutton.gameObject.SetActive(false);
        Yuttimer.SetActive(false);
        PlayerTimer.SetActive(true);
        curState = eRule.SelectCharacter;
    }

    public void TimeOverChange()//�ð��ʳ��� �̵����� ������ ���� ����ǰ� �ϴ� �κ�
    {
        throwbutton.gameObject.SetActive(true);
        Playtimemanager.SetActive(true);
        Yuttimer.SetActive(true);
        PlayerTimer.SetActive(false);
        Yutbox.SetActive(true);
        playerbox.SetActive(true);

        Playtimer.turnendchange((int)changecheck);
    }
    public void PlayerTimeChange()
    {
        Yutbox.gameObject.SetActive(false);
        throwbutton.gameObject.SetActive(false);
        Yuttimer.SetActive(false);
        PlayerTimer.SetActive(true);
        curState = eRule.SelectCharacter;
    }

    public void DebugTest()
    {
        Debug.Log(teamtype);
    }

    public void EndTurnCheck()
    {
        curState = eRule.ReturnthrowYut;
        throwyutbutton = true;
        desChangePos();
        if (changecheck == 0)
        {
            changecheck = 1;
        }
        else
        {
            changecheck = 0;
        }
        Yutstartbuttons.getbackYut();
        Playtimer.TurnChangeCheck();

        
    }

    private void desChangePos()
    {
        if (teamtype == 1)//������� ���
        {
            int count = objblue.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objblue[iNum].GetComponent<Player>();
                selPlayer.DesObj(objblue[iNum].gameObject);
            }
        }
        else if (teamtype == 2)
        {
            int count = objred.Count;
            for (int iNum = 0; iNum < count; ++iNum)
            {
                Player selPlayer = objred[iNum].GetComponent<Player>();
                selPlayer.DesObj(objblue[iNum].gameObject);
            }
        }
    }

    public void RecycleTurnManager()
    {
        TurnCycleCheck = true;
        //curState = eRule.ThrowYut;
        throwbutton.gameObject.SetActive(true);
        Playtimemanager.SetActive(true);
        PlayerTimer.SetActive(false);//�׽�Ʈ �κ�
        Yuttimer.SetActive(true);
        Yutbox.SetActive(true);
        playerbox.SetActive(true);
        curState = eRule.RecycleTime;
    }

    /// <summary>
    /// TurnCycleCheck == ���� ��� �ٽ� �������� true�� �ǵ��� ���� �Ǿ�����
    /// </summary>
    /// <param name="_recycleCheck"></param>
    public void RecycleTurnPass( bool _recycleCheck)//���⼭ ���� �߻� 07/20����
    {
        if(_recycleCheck == true && TurnCycleCheck == false)//�� ���� �� �� ���
        {
            return;
        }
        if(TurnCycleCheck == true && _recycleCheck == false)//���� ��� �ٽ� ������ ���ε� �� ���� �� �ɸ����
        {
            TurnCycleCheck = false;
            //Yutbox.SetActive(false);
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
            curState = eRule.SelectCharacter;
        }
        else if(TurnCycleCheck == true && _recycleCheck == true)//���� ��� ���� ������ �� ���� �� ���
        {
            return;
        }
    }

    public void ListClear()//���� ��� ���� �� ���� ������ ������� ������ ���� ���� ó���ϵ��� �ϴ� �ڵ�
    {
        if(Clearobj.gameObject.tag == "RedTeam")
        {
            objred.Clear();
        }
        else if(Clearobj.gameObject.tag == "BlueTeam")
        {
            objblue.Clear();
        }
    }

    public void LookAtYutPlayer()
    {
        if (Clearobj.gameObject.tag == "RedTeam")
        {
            for(int iNum = 0; iNum < objred.Count; iNum++)
            {
                if (objred[iNum].activeSelf == false)//���࿡ ������Ʈ�� �����ִٸ�
                {
                    objred.RemoveAt(iNum);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
        else if (Clearobj.gameObject.tag == "BlueTeam")
        {
            for (int iNum = 0; iNum < objblue.Count; iNum++)
            {
                if (objblue[iNum].activeSelf == false)//���࿡ ������Ʈ�� �����ִٸ�
                {
                    objblue.RemoveAt(iNum);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    //==================================���� Ŭ���� ���� �κ��� ���� ����

    private void teamWinCheck()
    {
        if (WinerBlue == true)
        {
            WinerBlue = false;
            WinerTeamCanvas.gameObject.SetActive(true);
            WinerTeamString.text = "������� �¸��߽��ϴ�";
        }
        else if (WinerRed == true)
        {
            WinerRed = false;
            WinerTeamCanvas.gameObject.SetActive(true);
            WinerTeamString.text = "�������� �¸��߽��ϴ�";
        }
    }
    #region
    //public void ReStart()//�ٽ� �����ϱ� ��ư�� �������� �۵�
    //{
    //    //WinerTeamCanvas.gameObject.SetActive(false);
    //    //startcheck.gameObject.SetActive(true);
    //    //changecheck = 0;
    //    //Yutstartbuttons.YutReStartCheck();
    //    //footholdbox.movedestory();
    //    //returnPosPlayer();
    //    //curState = eRule.Preferencetime;
    //}

    //private void returnPosPlayer()//�÷��̾ �ڱ� �ڸ��� ���� �ڵ�
    //{
    //    for(int iNum = 0; iNum < objblue.Count; iNum++)
    //    {
    //        Player.AgainPlayerStart();
    //    }
    //    for (int iNum = 0; iNum < objred.Count; iNum++)
    //    {
    //        Player.AgainPlayerStart();
    //    }

    //}
    #endregion

    private void testText()
    {
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //    WinerTeamString.text = "�׽�Ʈ";
        //}
    }

    public void ChangeGageBar(int _check)
    {
        if(_check == 0)//���� ������ �κ�
        {
            Yuttimer.SetActive(true);
            PlayerTimer.SetActive(false);
        }
        else if(_check == 1)//�÷��̾ �̵��ϴ� �κ�
        {
            Yuttimer.SetActive(false);
            PlayerTimer.SetActive(true);
        }
    }
}
