using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    Animation anim;
    Animator animator;
    [SerializeField] Image startcheck;

    [SerializeField] List<GameObject> objtype;
    [SerializeField] GameObject objplayer1_1;
    [SerializeField] GameObject objplayer1_2;
    [SerializeField] GameObject objplayer2_1;
    [SerializeField] GameObject objplayer2_2;
    public static Gamemanager Instance;

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
    [SerializeField, Tooltip("�� Ÿ�̸� �۵� �κ�")] GameObject Playtimemanager;
    [SerializeField, Tooltip("�� Ÿ�̸Ӱ� �پ��� �����")] GameObject Yuttimer;
    [SerializeField] GameObject Yutbox;
    [SerializeField] Button throwbutton;
    //��
    //ĳ���� ���� �� �̵� �κ� SelectCharacter�κ�
    [SerializeField] GameObject playerbox;//�÷��̾�� ĳ���͵��� ���̰� ���ִ� ������Ʈ
    //��


    private Player player;
    private Numberroom numberroom;

    public enum eRule
    {

        Preferencetime,//���� ���� ������ �켱�� �ð�
        ThrowYut,
        SelectCharacter,//ĳ���͸� ����
        MoveCharacter,
        CheckRule,
        TurnOver,
    }
    private eRule curState = eRule.Preferencetime;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public Numberroom Numberroom
    {
        get { return numberroom; }
        set { numberroom = value; }
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
    }

    //public bool playertouch;//Ŭ�� ������ on���� ��ȯ Ŭ���� ������ off�� ��ȯ

    void Start()
    {
        startcheck.gameObject.SetActive(true);
        changecheck = 0;
    }

    void Update()
    {
        Onclickplayer();

        //testcode();
        if(curState == eRule.Preferencetime)
        {
            startturn();
            startchageturn();
        }
        else if (curState == eRule.ThrowYut)
        {
            Throwtime();
            //Playtimer playtime = 
        }
        else if (curState == eRule.SelectCharacter)
        {
            playertypechoice();
        }
    }

    private void Onclickplayer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Gameplayertype == 1)
            {
                //GameObject playerfind = GameObject.Find("Player1");
                //Player player = playerfind.GetComponent<Player>();
                //player.playerchoice = true;
                //Debug.Log(playerfind);
                //Debug.Log(player);

                Player player = objplayer1_1.GetComponent<Player>();
                Player player2 = objplayer1_2.GetComponent<Player>();
                player.playerchoice = true;
                //player.checkobj.SetActive(true);
                //player.playertype1 = true;
                player2.playerchoice = true;
                //player2.checkobj.SetActive(true);
                //player2.playertype2 = true;
            }
            else if (Gameplayertype == 2)
            {
                //GameObject playerfind = GameObject.Find("Player2");
                //Player player = playerfind.GetComponent<Player>();
                //player.playerchoice = true;

                Player player = objplayer2_1.GetComponent<Player>();
                Player player2 = objplayer2_2.GetComponent<Player>();
                player.playerchoice = true;
                player2.playerchoice = true;
            }

            #region
            //Player player = gameObject.GetComponent<Player>();

            //GameObject playerfind = GameObject.Find("Player1");
            //Debug.Log(playerfind);
            //Player player = playerfind.GetComponent<Player>();
            //player.playerchoice = true;
            //Debug.Log("�۵�");

            //GameObject playerfind = GameObject.FindGameObjectWithTag("player");
            //Player player = playerfind.GetComponent<Player>();
            //player.playerchoice = true;
            //Debug.Log(playerfind);
            #endregion
        }
    }


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

    private void playertypechoice()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.transform != null)
            {
                Player selPlayer = rayHit.transform.GetComponent<Player>();
                selPlayer.selectedcheck = true;
            }
        }
    }

    private bool isFindData(out int value)
    {
        value = 10;
        return true;
    }

    private void startturn()//ó���� ���� ���� �����ϴ��� �˷��ִ� �ڵ�
    {
        animator = startcheck.gameObject.GetComponentInChildren<Animator>();
        Maxstartturnyut -= Time.deltaTime;
        if(Maxstartturnyut < 2)//3�ʰ� ���� ���
        {
            if(starttype == false)
            {
                changecheck = Random.Range(0, 2);
                starttype = true;
            }
            else
            {
                animator.SetFloat("CharacterChange", changecheck);
            }

            if(Maxstartturnyut < 0)//0���� ���� ���
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
            if (changecheck  == 0 && changetimer == 0.1f)//3�ʰ� ���� ���
            {
                changecheck = 1;
                animator.SetFloat("CharacterChange", changecheck);
                
            }
            else if(changecheck == 1 && changetimer == 0.1f)
            {
                changecheck = 0;
                animator.SetFloat("CharacterChange", changecheck);
            }
        }
    }

    private void startchageturn()//������ ���Ҷ� 0.1�ʸ��� ĳ�� �Ͱ� ����ǵ��� ����
    {
        if(Maxstartturnyut < 2)
        {
            return;
        }
        changetimer -= Time.deltaTime;
        if(changetimer < 0)
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
        if(throwyutbutton == true)
        {
            throwbutton.gameObject.SetActive(true);
            Playtimemanager.SetActive(true);
            Yuttimer.SetActive(true);
            Yutbox.SetActive(true);
            playerbox.SetActive(true);
        }
        else if(throwyutbutton == false)
        {
            throwbutton.gameObject.SetActive(false);
            Yuttimer.SetActive(false);
            curState = eRule.SelectCharacter;
        }
    }
}
