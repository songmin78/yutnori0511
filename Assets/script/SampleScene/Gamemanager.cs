using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    Animation anim;
    Animator animator;
    [SerializeField] Image startcheck;

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


    private Player player;
    private Footholdbox footholdbox;

    public enum eRule
    {

        Preferencetime,//���� ���� ������ �켱�� �ð�
        ThrowYut,
        SelectCharacter,//ĳ���͸� ����
        ReturnthrowYut,//�ٽ� ���� ������ �κ�
        waittime,//��� ��ٸ��� �κ�
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

        Animation anim = GetComponent<Animation>();
        Animator animator = GetComponent<Animator>();
        Maxstartturnyut = startturnyut;
        Maxtimewait = timewait;
    }

    //public bool playertouch;//Ŭ�� ������ on���� ��ȯ Ŭ���� ������ off�� ��ȯ

    void Start()
    {
        startcheck.gameObject.SetActive(true);
        changecheck = 0;
    }

    void Update()
    {
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
                selectcharactor(rayHit.transform.gameObject);
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
            }
            else
            {
                animator.SetFloat("CharacterChange", changecheck);
            }

            if (Maxstartturnyut < 0)//0���� ���� ���
            {
                Maxstartturnyut = startturnyut;
                starttype = false;
                startcheck.gameObject.SetActive(false);
                curState = eRule.ThrowYut;
                throwyutbutton = true;
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
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            Playtimer startplayer = Playtimemanager.GetComponent<Playtimer>();
            startplayer.startturn((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
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
            Yutstartbutton yutstartbutton = Yutstartbutton.GetComponent<Yutstartbutton>();
            yutstartbutton.getbackYut();
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
            Yutbox.SetActive(true);
            playerbox.SetActive(true);

            Playtimer startplayer = Playtimemanager.GetComponent<Playtimer>();
            startplayer.turnendchange((int)changecheck);
        }
        else if (throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            curState = eRule.SelectCharacter;
        }
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
    public void holdboxPosCheck(float _MaxmoveYutcount)
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

            #region �׽�Ʈ �ڵ带 ����� �κ�
            //��� ���� => 1.�̵��� ���� ��ġ�� �����´� 2.������ ��ġ�� ���� ����Ǿ��ִ� ��ġ�� �񱳸��Ͽ� ���� ��ġ�� ���� ����Ʈ�� �˾ƿ´�
            //3.����Ʈ�� �˾Ƴ��� �� ����Ʈ�� ���� ��Ų��
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.trsFootHold == footholdbox.Yutfoothold[(int)_MaxmoveYutcount]);
            int MaxCount = listObjectWhereFootHold.Count;
            //Debug.Log(listObjectWhereFootHold[1].trsFootHold);
            for (int iNum = 0; iNum < MaxCount; iNum++)
            //data <= ���� �����̴� ������
            //listObjectWhereFootHold <= �� ������ �ִ� ��
            {
                if (data.trsFootHold == listObjectWhereFootHold[iNum].trsFootHold && data.objPlayer != listObjectWhereFootHold[iNum].objPlayer)
                {
                    checkCount(data, outplayer, iNum);
                    break;
                }
                else
                {
                    continue;
                }
            }
            //Debug.Log(data.trsFootHold);
            #endregion
        }
    }

    private void checkCount(cObjectWhereFootHold data, GameObject outplayer, int iNum)//���� ���� ���� ��쿡 ����Ǵ� �ڵ�
    {
        //data <= ���� �����̴� ������
        //listObjectWhereFootHold <= �� ������ �ִ� ��

        Player players = listObjectWhereFootHold[iNum].objPlayer.GetComponent<Player>();//�� �ڸ��� �ִ� ������Ʈ�� �ִ� �÷��̾� ��ũ��Ʈ�� �ҷ��´�
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
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//������ ���� �������� �±׸� �ް� ������
                {
                    data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    players.AgainStartPos();
                    listObjectWhereFootHold.Remove(data);
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//������ ���� ����� �±׸� �ް������� ���´�
                {
                }
                break;
            case 2://������ ���϶�
                if (listObjectWhereFootHold[iNum].objPlayer.tag == "RedTeam")//������ ���� ������ �±׸� �ް��� ���� ���´�
                {
                }
                else if (listObjectWhereFootHold[iNum].objPlayer.tag == "BlueTeam")//������ ���� ����� �±׸� �ް������� ��´�
                {
                    data.objPlayer = listObjectWhereFootHold[iNum].objPlayer;
                    players.AgainStartPos();
                    listObjectWhereFootHold.Remove(data);
                }
                break;
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
            //Debug.Log(data.objPlayer);
            //Debug.Log(_player);
        }
        else//�÷��̾ ���ǿ� ������
        {
            //���� �÷��̾ �̵��� �ϸ� ������ �۵� �� => �� �ڱ� ��ġ���ǿ��� �̵��ϸ� �۵��Ǵ� �κ�
            cObjectWhereFootHold data = listObjectWhereFootHold.Find(x => x.objPlayer == _player);
            data.objPlayer = _player;
            data.trsFootHold = footholdbox.Yutfoothold[_movePos];
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


}
