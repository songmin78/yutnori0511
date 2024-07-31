using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Playtimer : MonoBehaviour
{
    [Header("Ʃ�丮�� �κ�(Ʃ�丮�� �������������� ����)")]
    [SerializeField, Tooltip("Ʃ�丮�� ���������� ������ True�� ����")] bool tutorialStageCheck;
    float tutorialTime = 1.5f;
    float MaxtutorialTime;
    //bool buttonTutorial = true;
    float yutThrow = 1.5f;
    [SerializeField] float MaxyutThrow;
    bool tutorialCheck1 = true;
    bool tutorialCheck2 = true;

    Animator animator;
    [Header("�Ϲݰ���")]
    [SerializeField] GameObject TestObj;
    [SerializeField] bool teamred;
    [SerializeField] bool teamblue;
    public bool TeamBlue
    {
        get
        {
            return teamblue;
        }
    }
    [Header("���� ������ ������ ���� �ð� ����")]
    [SerializeField] float throwtime;
    [SerializeField] float Maxthrowtime;//Ȯ�� ��
    bool throwwaitcheck;//������� ���� ��ٸ�
    [Header("���� ������ ĳ���� �̵��� ���� ���� �ð� ����")]
    [SerializeField] float waitmovetime;
    [SerializeField] float Maxwaitmovetime;//Ȯ�ο�
    bool playermovecheck;//���� ������ �����̴°��� ��ٸ��� �κ�
    [Header("��Ÿ")]
    //[SerializeField] public bool checktime;//���� �������� true�� ��ȯ(�ۿ��� �޾ƿ�)
    [SerializeField] Image timegage;//�ð��� �پ��� ������
    [SerializeField] Image timegagePlayer;//�÷��̾ �����ϴ� �ð��� �پ��� ������
    [SerializeField, Tooltip("������ ������ �̵��ϴ� ������ �˷��ִ� �ؽ�Ʈ")] TMP_Text Text;
    //public bool returnYut;//�� ���� �߸� true�� ��ȯ
    //[SerializeField] GameObject poscheck1;//ù��° ���� �̵� ����
    //public bool returnyut;//�� ���� �㶧 �ٷ� �ð��� ���ư��°��� ����
    [Header("���� ���� �κ�")]
    [SerializeField] float BackTime;
    [SerializeField] float MaxBackTime;

    public enum eRule
    {
        Throwtime,
        Movetime,
        BackChangeTimer,
        ReturnTimeStay,//��Ÿ�̸Ӱ� �� ���ư��� �����ϴ� �ڵ�
        TutorialTime,//Ʃ�丮�� �������϶� ���� �ڵ�
        TutorialMove,//Ʃ�丮�� �������϶� ���� �ڵ�
        AutoRed,//������AI
    }
    private eRule curTimer = eRule.Throwtime;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();

        Maxthrowtime = throwtime;
        Maxwaitmovetime = waitmovetime;
        MaxBackTime = BackTime;
        MaxtutorialTime = tutorialTime;
        MaxyutThrow = yutThrow;
    }

    private void Start()
    {
        Gamemanager.Instance.Playtimer = this;
        TestObj.gameObject.SetActive(false);
        curTimer = eRule.ReturnTimeStay;
    }

    public void LookScene()
    {
        TestObj.gameObject.SetActive(true);
    }

    void Update()
    {
        if (teamred == true && tutorialStageCheck == true && tutorialCheck1 == true)
        {
            curTimer = eRule.TutorialTime;
        }
        else if (teamred == true && tutorialStageCheck == true && tutorialCheck2 == true)
        {
            curTimer = eRule.TutorialMove;
        }
        //waityuttime();//�� ������ ��ư�� �� ������ �۵�
        //cheangeyuttime();//�� ������ ��ư�� ������ �۵�
        //movewaittimer();//���� �����̴°��� ��ٸ��� �ڵ�

        //timecalculate();//�ð� ��� �ڵ� �������� ����

        //yuttest();//���� �ٽ� ������ ���� �ڵ� �� �Ǵ� �� �� ���<- ���� ������Ʈ �������ϳ�?
        //changeteam();//���ʰ� ������ �� ���� <- ���� ������Ʈ������ ���� ������ ����
        if (Input.GetKeyDown(KeyCode.R))
        {
            Maxwaitmovetime = 1;
        }

        ChangeAnimator();
        if (curTimer == eRule.Throwtime)//������ ��ư�� �� ������ �ڵ����� �����������ϴ� �κ�
        {
            if (tutorialStageCheck == false && teamred == true)
            {
                Gamemanager.Instance.AutoClickRed();
            }
            waityuttime();
            timecalculate();
            //cheangeyuttime();//�� ������ ��ư�� ������ �۵�
        }
        else if (curTimer == eRule.Movetime)
        {
            movewaittimer();//���� �����̴°��� ��ٸ��� �ڵ�
            PlayTimeCalCulate();
            //timecalculate();
        }
        else if (curTimer == eRule.BackChangeTimer)//�ʵ忡 �ڱ⸻�� ������ ������ �߸� ���� �ѱ⵵�� ���� curTime
        {
            if (MaxBackTime < 0)
            {
                MaxBackTime = BackTime;
                BackChangeTurn();
            }
            else
            {
                MaxBackTime -= Time.deltaTime;
            }
        }
        else if (curTimer == eRule.ReturnTimeStay)//�� ��ũ��Ʈ�� �۵� ���ϰ� ����ϴ� �ڵ�κ�
        {
            return;
        }
        else if (curTimer == eRule.TutorialTime)
        {
            //if(buttonTutorial == true)
            //{
            //    buttonTutorial = false;
            //    Gamemanager.Instance.ButtonOff();
            //}
            Gamemanager.Instance.ButtonOff();
            TutorialTeam();
        }
        else if (curTimer == eRule.TutorialMove)
        {
            automaticMove();
        }
    }

    public void StayTurnTime()//�� ��ũ��Ʈ�� �۵� �ȵǰ� �ٲٴ� �ڵ� 
    {
        Time.timeScale = 0;
        curTimer = eRule.ReturnTimeStay;
    }

    public void StartTurnTime()//����Ÿ�̸Ӱ� ���ư����� �ϴ� �ڵ�
    {
        Text.text = "�� ������ ��";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
        Gamemanager.Instance.Yutstartbuttons.NullText();
        curTimer = eRule.Throwtime;
    }

    public void startturn(int _startteam)//���� ������ �� ����
    {
        switch (_startteam)
        {
            case 0:
                teamred = true; break;
            case 1:
                teamblue = true; break;
        }
        changeteam();
    }

    private void waityuttime()//���� ������ ���� ��ٸ��� �ð� �ڵ�
    {
        #region ���ſ� ���� �ڵ��
        //if (throwwaitcheck == true || checktime == true || returnyut == true)//���� �����Ŀ� true�� ��ȯ�Ͽ� �� ������ ����� �ڵ�
        //{
        //    return;
        //}
        //if(Maxthrowtime <= 0 )
        //{
        //    Maxthrowtime = throwtime;//�� �ʱ�ȭ
        //    throwwaitcheck = true;//���̻� �۵����� �ʰ� ����
        //    playermovecheck = true;//�۵��ϰ� ����
        //    GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.yutstarttimer = true;
        //    Debug.Log("�̵����� ����");
        //}
        //else
        //{
        //    //Maxthrowtime -= Time.deltaTime;//���� �����⸦ ��ٸ��� �ڵ�
        //}
        #endregion

        //�ڵ带  �ٽ� ������ũ
        if (Maxthrowtime <= 0)//������ ��ٸ��� �ð��� �� ���� ��� ���� �����ϼ� �ִ� �κ����� ü����
        {
            Maxthrowtime = throwtime;//�� �ʱ�ȭ
            //Gamemanager.Instance.Yutstartbuttons.yutstarttimer = true;
            Gamemanager.Instance.Yutstartbuttons.yutplaytimer();
            curTimer = eRule.Movetime;
            Text.text = "�̵� ��";
            Gamemanager.Instance.PlayerTimeChange();
            Debug.Log("�̵����� ����");
        }
        else
        {
            Maxthrowtime -= Time.deltaTime;//���� �����⸦ ��ٸ��� �ڵ�
        }
    }

    private void ChangeAnimator()
    {
        animator = TestObj.gameObject.GetComponentInChildren<Animator>();
        if (teamblue == true)
        {
            animator.SetFloat("TurnCheck", 1);
        }
        else if (teamred == true)
        {
            animator.SetFloat("TurnCheck", 0);
        }
    }
    public void cheangeyuttime(bool _recycleCheck)
    {
        #region ���ſ� ���� �ڵ��
        //if (checktime == true)//�� ������ ��ư�� ���� ���
        //{
        //    Maxthrowtime = throwtime;//�� �ʱ�ȭ
        //    throwwaitcheck = true;//���̻� �۵����� �ʰ� ����
        //    playermovecheck = true;//�۵��ϰ� ����
        //    checktime = false;
        //    //Debug.Log("�̵����� ����");
        //}
        #endregion
        //float yutnumber = Gamemanager.Instance.Yutstartbuttons.Yutnumber;
        Maxthrowtime = throwtime;//�� �ʱ�ȭ
        Maxwaitmovetime = waitmovetime;//�̵��ϴ� �κ� �� �ʱ�ȭ
        //if (Yutnumber == 4 || Yutnumber == 5)
        //{
        //    curButton = eRule.YutStartButton2;
        //}
        if (_recycleCheck == true)
        {
            return;
        }
        curTimer = eRule.Movetime;
        Text.text = "�̵� ��";
    }

    public void PassChange()
    {
        Maxthrowtime = throwtime;//�� �ʱ�ȭ
        curTimer = eRule.BackChangeTimer;
    }

    private void movewaittimer()//���� �����̴°��� ��ٸ��� �ڵ�
    {
        #region  ���ſ� ���� �ڵ��
        //if (playermovecheck == false || returnYut == true)
        //{
        //    return;
        //}
        //if(Maxwaitmovetime <= 0)
        //{
        //    Maxwaitmovetime = waitmovetime;
        //    playermovecheck = false;//���̻� �۵� ���ϰ� ����
        //    throwwaitcheck = false;//�� ������ �κ��� �۵��ϰ� ����
        //    if (teamred == true)
        //    {
        //        teamred = false;
        //        teamblue = true;
        //    }
        //    else if (teamblue == true)
        //    {
        //        teamred = true;
        //        teamblue = false;
        //    }
        //    Debug.Log("������� ����");

        //    //poscheck1.SetActive(false);
        //    GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.waittime = false;
        //    buttontimer.zeromovecheck = true;

        //}
        //else
        //{
        //    //Maxwaitmovetime -= Time.deltaTime;
        //    GameObject findyut = GameObject.Find("Yutstartbutton");
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.waittime = true;
        //}
        #endregion
        //�ڵ� ������ũ
        if (Maxwaitmovetime <= 0)//������ ���� �� ���������� �׳� ���� �ѱ⵵�� ����
        {
            Maxwaitmovetime = waitmovetime;
            if (teamred == true)//�̵��ð��� �� ������ ��� �� ����
            {
                teamred = false;
                teamblue = true;
            }
            else if (teamblue == true)
            {
                teamred = true;
                teamblue = false;
            }

            //Gamemanager.Instance.Yutstartbuttons.waittime = false;//���߿� �ٽ� �ǵ� ����
            //Gamemanager.Instance.Yutstartbuttons.zeromovecheck = true;
            Gamemanager.Instance.Yutstartbuttons.numberzero();

            changeteam();//����� ���� ���� �޴����� ������ �ֵ��� �����ִ� �ڵ�
            curTimer = eRule.Throwtime;//�ٽ� ���� ������ �ִ� �κ����� ����
            Text.text = "�� ������ ��";
            Gamemanager.Instance.Yutstartbuttons.TextNull();
            Gamemanager.Instance.Yutstartbuttons.NullText();
            Gamemanager.Instance.Yutstartbuttons.ClaerYutCount();
            Gamemanager.Instance.TimeOverChange();
            Gamemanager.Instance.EndTurnCheck();//�� ü����
            Debug.Log("������� ����");
        }
        else
        {
            Maxwaitmovetime -= Time.deltaTime;
            //Gamemanager.Instance.Yutstartbuttons.waittime = true;
        }
    }

    public void BackChangeTurn()//������ �㶧 �� ü���� �κ�
    {
        if (teamred == true)//�̵��ð��� �� ������ ��� �� ����
        {
            teamred = false;
            teamblue = true;
        }
        else if (teamblue == true)
        {
            teamred = true;
            teamblue = false;
        }
        Gamemanager.Instance.Yutstartbuttons.numberzero();
        changeteam();//����� ���� ���� �޴����� ������ �ֵ��� �����ִ� �ڵ�
        curTimer = eRule.Throwtime;//�ٽ� ���� ������ �ִ� �κ����� ����
        Text.text = "�� ������ ��";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
        Gamemanager.Instance.Yutstartbuttons.NullText();
        Gamemanager.Instance.TimeOverChange();
        Gamemanager.Instance.EndTurnCheck();//�� ü����
    }

    #region
    //private void playercheck()
    //{
    //    if(playermovecheck == true)
    //    {
    //        if(teamblue == true)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player1");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //GameObject playerfind2 = GameObject.Find("Player2");
    //            //Player player2 = playerfind2.GetComponent<Player>();
    //            //player.playertype1 = true;
    //            //player.playertype2 = false;

    //            GameObject obj = GameObject.Find("Gamemanager");
    //            Gamemanager gamemanager = obj.GetComponent<Gamemanager>();
    //            gamemanager.Gameplayertype = 1;

    //        }
    //        else if(teamred == true)
    //        {
    //            //GameObject playerfind = GameObject.Find("Player2");
    //            //Player player = playerfind.GetComponent<Player>();
    //            //player.playertype1 = false;
    //            //player.playertype2 = true;

    //            GameObject obj = GameObject.Find("Gamemanager");
    //            Gamemanager gamemanager = obj.GetComponent<Gamemanager>();
    //            gamemanager.Gameplayertype = 2;
    //        }
    //    }

    //}
    #endregion

    private void timecalculate()//�ð� ��� �ڵ�
    {
        timegage.fillAmount = Maxthrowtime / throwtime;
    }

    private void PlayTimeCalCulate()
    {
        timegagePlayer.fillAmount = Maxwaitmovetime / waitmovetime;
    }

    private void yuttest()//���� �ٽ� ������ ���� �ڵ�
    {
        #region ������ ���� ���ڵ��
        //if (returnYut == true)
        //{
        //    playermovecheck = false;//���̻� �۵� ���ϰ� ����
        //    throwwaitcheck = false;//�� ������ �κ��� �۵��ϰ� ����

        //    GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        //    Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
        //    buttontimer.waittime = false;
        //    returnYut = false;
        //}
        #endregion

    }

    public void changeteam()
    {
        if (teamblue == true)
        {
            Gamemanager.Instance.Chageplayteam(1);
            //animator.SetFloat("CharacterChange", 0);
            //Gamemanager.Instance.teamfalsecheck();
        }
        else if (teamred == true)
        {
            Gamemanager.Instance.Chageplayteam(2);
            //animator.SetFloat("CharacterChange", 1);
            //Gamemanager.Instance.teamfalsecheck();
        }
    }

    public void turnendchange(int _startteam)//���� ���� �ɶ� ����� �������� �����ϴ� �ڵ�κ�
    {
        switch (_startteam)
        {
            case 0:
                teamblue = false;
                teamred = true; break;
            case 1:
                teamred = false;
                teamblue = true; break;
        }
    }

    public void ReturnCheck()//�� ���� �ɸ��� �ٷ� ���ư��� �ʵ��� �����ϴ� �ڵ�
    {
        curTimer = eRule.ReturnTimeStay;
    }
    public void BackReturnCheck()//�� ���� �ɸ��� �ٷ� ���ư��� �ʵ��� �����ϴ� �ڵ�
    {
        curTimer = eRule.Throwtime;
    }
    public void TurnChangeCheck()
    {
        Maxwaitmovetime = waitmovetime;
        if (teamred == true)//�̵��ð��� �� ������ ��� �� ����
        {
            teamred = false;
            teamblue = true;
        }
        else if (teamblue == true)
        {
            teamred = true;
            teamblue = false;
        }
        Gamemanager.Instance.Yutstartbuttons.numberzero();

        changeteam();//����� ���� ���� �޴����� ������ �ֵ��� �����ִ� �ڵ�
        curTimer = eRule.Throwtime;//�ٽ� ���� ������ �ִ� �κ����� ����
        Text.text = "�� ������ ��";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
        Gamemanager.Instance.Yutstartbuttons.NullText();
        Gamemanager.Instance.TimeOverChange();
    }

    public void RecycleTurn()
    {
        curTimer = eRule.Throwtime;
        Text.text = "�� ������ ��";
        Gamemanager.Instance.Yutstartbuttons.TextNull();
    }

    //Ʃ�丮�� �κ�
    public void TutorialTeam()//������ �����϶� �ڵ����� ���� ���� ���� ����
    {
        if (MaxtutorialTime < 0)
        {
            MaxtutorialTime = tutorialTime;
            Gamemanager.Instance.Yutstartbuttons.yutplaytimer();
            //curTimer = eRule.Movetime;
            Gamemanager.Instance.PlayerTimeChange();
            tutorialCheck1 = false;
            curTimer = eRule.TutorialMove;
        }
        else
        {
            MaxtutorialTime -= Time.deltaTime;
        }
    }

    private void automaticMove()
    {
        if (tutorialStageCheck == true)
        {
            if (MaxyutThrow < 0)
            {
                tutorialCheck2 = false;
                MaxyutThrow = yutThrow;
                Gamemanager.Instance.selectTeam();
            }
            else
            {
                MaxyutThrow -= Time.deltaTime;
            }
        }
    }


    //RedTeam AI
    public void RedSelect()
    {
        Maxthrowtime = throwtime;//�� �ʱ�ȭ
        Gamemanager.Instance.Yutstartbuttons.YutThrowClick();
        if (Gamemanager.Instance.Yutstartbuttons.Yutnumber > 3)
        {
            return;
        }
        curTimer = eRule.Movetime;
        Text.text = "�̵� ��";
            Gamemanager.Instance.SelectRed();
    }
}
