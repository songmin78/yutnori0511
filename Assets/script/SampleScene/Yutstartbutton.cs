using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Yutstartbutton : MonoBehaviour
{
    List<TMP_Text> TextList;
    [Header("������")]
    [SerializeField] GameObject yut1;
    [SerializeField] GameObject yut2;
    [SerializeField] GameObject yut3;
    [SerializeField] GameObject yut4;
    [Header("������ �����ִ� ���� �ִ� 3������ ���� ����")]
    float yutnumber = 0;
    public float Yutnumber
    {
        get
        {
            return yutnumber;
        }
    }
    //bool yutreturncheck;
    [SerializeField, Tooltip("�������Ŀ� �����ִ� ���� ����")] public float oneyut = 0;
    [SerializeField, Tooltip("�������Ŀ� �����ִ� ���� ����")] public float twoyut = 0;
    [SerializeField, Tooltip("�������Ŀ� �����ִ� ���� ����")] public float threeyut = 0;
    public bool zeromovecheck;

    //float yuttype;
    [Header("�� ������ ��ư")]
    //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
    [SerializeField] Button startbutton;
    bool yutstart;
    bool randomcheck;//���� �� ������ Ȯ�� ���ִ� �ڵ�
    int randomcount;
    int Stickcount;
    //public bool waittime;//ĳ���͸� �����̱����� �ð��� ��ư�� ������ �۵� �ȵǰ� ����
    public bool yutstarttimer;
    [Header("�� Ÿ")]
    //[SerializeField] Image timegage;
    //���� �ո����� �޸��� Ȯ�� ���ִ� �κ�
    int chageyut;
    [SerializeField]float stayyut = 1;//�� �Ǵ� ���� �ɸ��� ��� ��ٸ��� �ð�
    bool stayyutcheck;//1�� ��ٸ��� �ð��� üũ
    [Header("��ũ��Ʈ������Ʈ�� ����")]
    [SerializeField] GameObject Playtimemanager;
    bool oneYutCheck;
    bool twoYutCheck;
    bool threeYutCheck;
    bool reCheck;//�� ���� �߸� ��� true�� ����
    [Header("�����ִ� �� �ڵ� �κ�")]
    [SerializeField] GameObject Look1;
    [SerializeField] GameObject Look2;
    [SerializeField] GameObject Look3;
    [SerializeField] GameObject Look4;
    [SerializeField] TMP_Text LookText;
    bool recycleCheck;//�� ���� �㶧 �ð� �ڵ尡 �� ���ư��� �����ִ� �ڵ�

    [Header("�� �����ֱ� ���")]
    [SerializeField] TMP_Text Blue1;
    [SerializeField] TMP_Text Blue2;
    [SerializeField] TMP_Text Blue3;
    [SerializeField] TMP_Text Red1;
    [SerializeField] TMP_Text Red2;
    [SerializeField] TMP_Text Red3;

    public enum eRule
    {
        YutStartButton1,
        YutStartButton2,
        YutStartButton3,
    }
    private eRule curButton = eRule.YutStartButton1;

    [SerializeField] List<int> yutdisposition = new List<int>();
    private void Awake()
    {
        startbutton.onClick.AddListener(() =>
        {
            //if (waittime == true)
            //{
            //    return;
            //}
            yutdisposition.Clear();
            Stickcount = 0;
            yutstart = true;

        });
    }

    private void Start()
    {
        Gamemanager.Instance.Yutstartbuttons = this;
        curButton = eRule.YutStartButton3;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Gamemanager.Instance.throwyutbutton == false)
        //{
        //    return;
        //}
        //yutplaytimer();
        //startyutnbutton();//�� ������ ��ư
        //resetyut();// ���� �� �Ǵ� ���� �ɸ� ���
        //returnyut();

        //numberzero();//�̵� �Ÿ� �ʱ�ȭ
        //moveyut();

        if (curButton == eRule.YutStartButton1)
        {
            startyutnbutton();
        }
        else if(curButton == eRule.YutStartButton2)
        {
            resetyut();
        }
        else if(curButton == eRule.YutStartButton3)
        {
            return;
        }

    }

    public void yutplaytimer()
    {
        //if (yutstarttimer == true)
        //{
        //    yutdisposition.Clear();
        //    Stickcount = 0;
        //    yutstart = true;
        //    yutstarttimer = false;
        //}

        //�ڵ� ����ȭ ��
        yutdisposition.Clear();
        Stickcount = 0;
        yutstart = true;
    }


    private void startyutnbutton()//�� ������ ��ư
    {
        if(Gamemanager.Instance.TutorialStageCheck == true && yutstart == true)
        {
            if(Gamemanager.Instance.TutorialStory.StoryNumber == 4)
            {
                yutstart = false;
                tutorialYutNumber();
            }
            else if (Gamemanager.Instance.TutorialStory.StoryNumber == 12)
            {
                yutstart = false;
                tutorialYut2();
            }
            else if(Gamemanager.Instance.TutorialStory.StoryNumber == 15)
            {
                yutstart = false;
                tutorialYut3();
            }
            else if (Gamemanager.Instance.TutorialStory.StoryNumber == 17)
            {
                yutstart = false;
                tutorialYut4();
            }
            return;
        }
        if (stayyutcheck == true)
        {
            return;
        }

        #region Ư��Ű�� ������ �߰� ����� �κ�
        //Ư�� ����ȣ�� ��� ���� �ڵ��
        //if (Input.GetKeyDown(KeyCode.N))//Ȯ�� ���� �ߵ��� ����
        //{
        //    yutnumber = 5;
        //    Stickcount = 0;
        //    for (int yutstick = 0; yutstick < 4; yutstick++)
        //    {
        //        randomcount = 1;
        //        chageyut += 1;
        //        Yutcount();
        //    }
        //    yutlist();
        //    if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        //    {
        //        Gamemanager.Instance.nextturn();
        //        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        //        return;
        //    }
        //    recycleCheck = true;
        //    Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        //    recycleCheck = false;
        //    if (yutnumber == 4 || yutnumber == 5)
        //    {
        //        //stayyutcheck = true;
        //        //playtimer.returnYut = true;
        //        curButton = eRule.YutStartButton2;
        //    }
        //}
        //if(Input.GetKeyDown(KeyCode.B))//���� �߰� ����� �κ�
        //{
        //    yutnumber = 3;
        //    Stickcount = 3;
        //    for (int yutstick = 0; yutstick < 3; yutstick++)
        //    {
        //        randomcount = 1;
        //        chageyut += 1;
        //        Yutcount();
        //    }
        //    yutlist();
        //    yutstart = false;
        //    if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        //    {
        //        Gamemanager.Instance.nextturn();
        //        return;
        //    }
        //    Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//�� ���� �ƴ϶� false�� �Է�
        //    Gamemanager.Instance.throwyutbutton = false;
        //}
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    randomcheck = true;
        //    yutnumber = -1;
        //    Stickcount = 0;
        //    for (int yutstick = 0; yutstick < 4; yutstick++)
        //    {
        //        randomcount = 1;
        //        chageyut += 1;
        //        Yutcount();
        //    }
        //    yutturnNumber();
        //    Debug.Log("����");
        //    LookText.text = "����";
        //    yutstart = false;
        //    if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        //    {
        //        Gamemanager.Instance.nextturn();//���������� �÷��̾� ���� �κ����� �̵�
        //    }
        //    else
        //    {
        //        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        //        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        //    }

        //    //Gamemanager.Instance.Playtimer.PassChange();
        //    Gamemanager.Instance.throwyutbutton = false;
        //    if(oneyut == -1)
        //    {
        //        Gamemanager.Instance.CheckBackYutPass();
        //    }
        //    else
        //    {
        //        Gamemanager.Instance.PlayTimeTurn();
        //    }
        //    randomcheck = false;
        //    return;
        //}
        //�������
        #endregion

        //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
        if (yutstart == true)
        {
            reCheck = false;
            #region
            //for(int yutstick = 0; yutstick < 4; yutstick++)//���� ������ ������ 3���� ���� ����Ʈ�� ���� �ϵ��� ����
            //{
            //    test = Random.Range(0, 2);//Random.Range�� int�� ��� ������ ���� -1�� �Ͽ� ��� (0,2)�� ��� 0��  1�� �۵���
            //    yutdisposition.Add(test);
            //    //Debug.Log(test);
            //    //yutdisposition.Add(Random.Range(0, 2));//Random.Range�� int�� ��� ������ ���� -1�� �Ͽ� ��� (0,2)�� ��� 0��  1�� �۵���
            //    randomcheck = true;
            //}
            //yutdisposition.Add(Random.Range(0, 2));
            #endregion

            for (int yutstick = 0; yutstick < 4; yutstick++)//���� ������ ������ 3���� ���� ����Ʈ�� ���� �ϵ��� ����
            {
                randomcount = Random.Range(0, 2);//Random.Range�� int�� ��� ������ ���� -1�� �Ͽ� ��� (0,2)�� ��� 0��  1�� �۵���
                if (randomcount == 1)//�޸鿡 �ɸ� ���
                {
                    if (yutstick == 3 && Stickcount == 0)//4��° ���̸鼭 ������ �� �ո��� ����
                    {
                        Stickcount += 0;
                    }
                    else
                    {
                        Stickcount += randomcount;
                    }
                    chageyut += 1;
                    Yutcount();
                }
                else// 0 �� �ո鿡 �ɸ� ���
                {
                    chageyut += 1;
                    Yutcount();
                }
                if (yutstick == 3 && Stickcount == 0 && randomcount == 1)//������ ������ ���� �ո� �̸鼭 ������ ���� �޸��� ���(����)
                {
                    #region
                    //randomcheck = true;
                    //yutnumber = -1;
                    //yutturnNumber();
                    //Debug.Log("����");
                    //if(oneyut == -1)
                    //{
                    //    Gamemanager.Instance.CheckBackYutPass();
                    //}
                    #endregion
                    backPass();
                    return;
                }
                yutdisposition.Add(randomcount);
            }
            //Debug.Log(Stickcount);
            yutlist();
            yutstart = false;
            if (oneyut != 0 && twoyut != 0 && threeyut != 0 && reCheck == true)//3���������� �� �Ǵ� ���� �� ���
            {
                Gamemanager.Instance.nextturn();
                Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
                return;
            }
            //GameObject findtimer = GameObject.Find("Playtimemanager");
            if (yutnumber == 4 || yutnumber == 5)//�� �Ǵ� ���� �� ���
            {
                curButton = eRule.YutStartButton2;
                recycleCheck = true;
                //stayyutcheck = true;
                if (threeyut != 0)
                {
                    recycleCheck = false;
                    return;
                }
                //playtimer.returnYut = true;
            }
            else//�� �Ǵ� ���� �ȶ� ���
            {
                Gamemanager.Instance.throwyutbutton = false;
            }
            Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
            Gamemanager.Instance.RecycleTurnPass(recycleCheck);
            recycleCheck = false;
        }

    }

    private void backPass()//������ �ɷ����� ���� �ѱ�� �ְ� ���ִ� �ڵ�
    {
        randomcheck = true;
        yutnumber = -1;
        Stickcount = 0;
        for (int yutstick = 0; yutstick < 4; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        Debug.Log("����");
        LookText.text = "����";
        yutturnNumber();
        yutstart = false;
        if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        {
            Gamemanager.Instance.nextturn();//���������� �÷��̾� ���� �κ����� �̵�
        }
        else
        {
            Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        }
        Gamemanager.Instance.throwyutbutton = false;
        if (oneyut == -1)
        {
            Gamemanager.Instance.CheckBackYutPass();
        }
        else
        {
            Gamemanager.Instance.PlayTimeTurn();
        }
        randomcheck = false;
    }

    #region
    //private void moveyut()//���� �� �� ��ŭ �̵��� �Ÿ��� ������
    //{
    //    if(randomcheck == true)
    //    {
    //        for(int yutcheck = 0; yutcheck < 4; yutcheck++)
    //        {
    //            //int a = yutdisposition.Find(test => test >= 0);
    //            int a = yutdisposition.Contains();
    //            if(a == 1)
    //            {
    //                Debug.Log("1");
    //            }
    //            else
    //            {
    //                Debug.Log("0");
    //            }
    //        }
    //        randomcheck = false;
    //    }
    //    //����Ʈ�� 1�� �󸶳� �ִ��� Ȯ���ϴ� �ڵ�
    //}
    #endregion

    private void yutlist()//���� ���� ������ Ȯ�� ���ִ� �ڵ�
    {
        if (randomcheck == true)//���࿡ ������ �ɽ�
        {
            randomcheck = false;
            return;
        }
        //Stickcount�� 0�ϰ�� ��, 1�� ��� �� �Ǵ� ����, 2�� ��� ��, 3�ϰ�� ��, 4�ϰ�� ��
        switch (Stickcount)//�޸���  �󸶳� ������ swich������ üũ
        {
            case 0://�޸��� 0���� ���
                yutnumber = 5;
                reCheck = true;
                Debug.Log("��");
                LookText.text = "��";
                yutturnNumber();
                break;
            case 1:
                yutnumber = 1;
                Debug.Log("��");
                LookText.text = "��";
                yutturnNumber();
                break;
            case 2:
                yutnumber = 2;
                Debug.Log("��");
                LookText.text = "��";
                yutturnNumber();
                break;
            case 3:
                yutnumber = 3;
                Debug.Log("��");
                LookText.text = "��";
                yutturnNumber();
                break;
            case 4:
                yutnumber = 4;
                reCheck = true;
                Debug.Log("��");
                LookText.text = "��";
                yutturnNumber();
                break;
        }
    }


    //�̵� ���� �� ���� 0���� ����� ������ 1,2��° �� �̵��� ���� ������ 1��° ���� �ٽ� �Է��� �ȴ�
    private void yutturnNumber()//���� �ɸ� ���ڸ�ŭ ���ڸ� ����
    {
        if (oneyut == 0 && oneYutCheck == false || oneYutCheck == false)
        {
            oneyut = yutnumber;
            if (Gamemanager.Instance.Playtimer.TeamBlue == true)
            {
                Blue1.text = LookText.text;
            }
            else
            {
                Red1.text = LookText.text;
            }
            NotCheck1();
        }
        else if (oneyut != 0 && twoyut == 0 && twoYutCheck == false || twoYutCheck == false)
        {
            twoyut = yutnumber;
            if (Gamemanager.Instance.Playtimer.TeamBlue == true)
            {
               Blue2.text = LookText.text;
            }
            else
            {
                Red2.text = LookText.text;
            }
            NotCheck2();
        }
        else if (oneyut != 0 && twoyut != 0 && threeyut == 0 && threeYutCheck == false|| threeYutCheck == false)
        {
            threeyut = yutnumber;
            if (Gamemanager.Instance.Playtimer.TeamBlue == true)
            {
               Blue3.text = LookText.text;
            }
            else
            {
                Red3.text = LookText.text;
            }
            NotCheck3();
        }
    }


    public void numberzero()//�̵� �Ÿ� �ʱ�ȭ
    {
        //if (zeromovecheck == true)
        //{
        //    oneyut = 0;
        //    twoyut = 0;
        //    threeyut = 0;
        //    zeromovecheck = false;
        //}
        oneyut = 0;
        twoyut = 0;
        threeyut = 0;
    }

    private void Yutcount()//���� �ո鿡 �߳� �޸鿡 �߳� �����ִ� �κ�
    {
        #region
        //for (int yutnumber = 0; yutnumber < 4; yutnumber++)
        //{
        //    if(yutdisposition[testd] == 0)
        //    {
        //        Debug.Log(yutdisposition);
        //    }
        //    else if(yutdisposition[testd] == 1)
        //    {
        //        //Debug.Log(yutdisposition);
        //    }
        //    if(yutdisposition.Find())
        //    {

        //    }
        //}
        #endregion
        //����Ʈ�� ����� ���� �κ� => ����Ʈ 1���� ���� 0�� ��� ���� �ո����� ������ 1�� ��� ���� �޸����� ������
        switch (chageyut)
        {
            case 1:
                if (randomcount == 1)
                {
                    yut1.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look1.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 2:
                if (randomcount == 1)
                {
                    yut2.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look2.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 3:
                if (randomcount == 1)
                {
                    yut3.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look3.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 4:
                if (randomcount == 1)
                {
                    yut4.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Look4.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
        }
    }


    private void resetyut()//���� �� �Ǵ� ���� �ɸ� ���
    {
        //if (stayyutcheck == true)
        //{
        //    //GameObject findtimer = GameObject.Find("Playtimemanager");
        //    Gamemanager.Instance.Playtimer.ReturnCheck();
        //    if (stayyut < 0)
        //    {
        //        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        //        stayyut = 1;
        //        stayyutcheck = false;
        //        Gamemanager.Instance.Playtimer.BackReturnCheck();
        //        chageyut = 0;
        //    }
        //    else
        //    {
        //        stayyut -= Time.deltaTime;
        //    }
        //}

        Gamemanager.Instance.Playtimer.ReturnCheck();
        if (stayyut < 0)
        {
            yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
            yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
            yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
            yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
            lookReturnYut();
            stayyut = 1;
            //stayyutcheck = false;
            Gamemanager.Instance.Playtimer.BackReturnCheck();
            chageyut = 0;
            curButton = eRule.YutStartButton1;
        }
        else
        {
            stayyut -= Time.deltaTime;
        }
    }

    public void getbackYut()//�� ����� �ٽ� �� ������
    {
        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        lookReturnYut();
        chageyut = 0;
    }

   public void ClaerYutCount()
    {
        //yutlist();
        Gamemanager.Instance.nextturn();//�÷��̾������� �ٲ�
        Gamemanager.Instance.Playtimer.PassChange();

        //zeromovecheck = true;
        NotCheckTrue();
        numberzero();
        //playtimer.checktime = true;
        //stayyutcheck = true;
    }

    public void ControlCheck1()
    {
        curButton = eRule.YutStartButton1;
    }

    public void CatchReTurnTurn()//�� �÷��̾ ������� �ٽ� �ѹ� ���� ���ִ� �ڵ�
    {
        if (threeyut != 0 || threeYutCheck == true)
        {
            return;
        }
        chageyut = 0;
        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        lookReturnYut();

        Gamemanager.Instance.RecycleTurnManager();
        curButton = eRule.YutStartButton1;
        Gamemanager.Instance.Playtimer.RecycleTurn();

    }

    public void NotCheck1()//�ѹ� �� ��ĭ�� �ٽ� �� ������ �����ִ� �κ�
    {
        oneYutCheck = true;
    }
    public void NotCheck2()//�ѹ� �� ��ĭ�� �ٽ� �� ������ �����ִ� �κ�
    {
        twoYutCheck = true;
    }
    public void NotCheck3()//�ѹ� �� ��ĭ�� �ٽ� �� ������ �����ִ� �κ�
    {
        threeYutCheck = true;
    }

    public void NotCheckTrue()//������ �ٽ� ���� �ְ� �����ִ� �ڵ�
    {
        oneYutCheck = false;
        twoYutCheck = false;
        threeYutCheck = false;
    }

    private void lookReturnYut()
    {
        Look1.transform.rotation = Quaternion.Euler(0, 0, 0);
        Look2.transform.rotation = Quaternion.Euler(0, 0, 0);
        Look3.transform.rotation = Quaternion.Euler(0, 0, 0);
        Look4.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TextNull()
    {
        LookText.text = "";
    }

    public void NullText()
    {
        Blue1.text = "";
        Blue2.text = "";
        Blue3.text = "";
        Red1.text = "";
        Red2.text = "";
        Red3.text = "";
    }

    private void tutorialYutNumber()//Ʃ�丮���� �����Ҷ� ���� ���������� ���� �Ҽ� �ֵ��� ���� �κ�
    {
        yutnumber = 3;
        Stickcount = 3;
        for (int yutstick = 0; yutstick < 3; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        //yutdisposition.Add(randomcount);
        yutlist();
        yutstart = false;
        Gamemanager.Instance.throwyutbutton = false;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        Gamemanager.Instance.TutorialStory.TimeOff();
    }

    private void tutorialYut2()//Ʃ�丮���� �����Ҷ� ���� ���������� ���� �Ҽ� �ֵ��� ���� �κ�
    {
        yutnumber = 2;
        Stickcount = 2;
        for (int yutstick = 0; yutstick < 2; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        //yutdisposition.Add(randomcount);
        yutlist();
        yutstart = false;
        Gamemanager.Instance.throwyutbutton = false;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        //Gamemanager.Instance.TutorialStory.TimeOff();
    }

    private void tutorialYut3()
    {
        //yutnumber = 5;
        //Stickcount = 0;
        //yutlist();
        //curButton = eRule.YutStartButton2;
        //recycleCheck = true;
        //yutstart = false;
        //Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        //recycleCheck = false;
        //Gamemanager.Instance.throwyutbutton = false;
        //Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        //Gamemanager.Instance.TutorialStory.TimeOff();
        //
        yutnumber = 5;
        Stickcount = 0;
        yutlist();
        if (oneyut != 0 && twoyut != 0 && threeyut != 0 || threeyut != 0)
        {
            Gamemanager.Instance.nextturn();
            Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
            return;
        }
        recycleCheck = true;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);
        recycleCheck = false;
        if (yutnumber == 4 || yutnumber == 5)
        {
            curButton = eRule.YutStartButton2;
        }
        Gamemanager.Instance.TutorialStory.TimeOff();
    }
    private void tutorialYut4()//Ʃ�丮���� �����Ҷ� ���� ���������� ���� �Ҽ� �ֵ��� ���� �κ�
    {
        yutnumber = 2;
        Stickcount = 2;
        for (int yutstick = 0; yutstick < 2; yutstick++)
        {
            randomcount = 1;
            chageyut += 1;
            Yutcount();
        }
        yutlist();
        yutstart = false;
        Gamemanager.Instance.throwyutbutton = false;
        Gamemanager.Instance.Playtimer.cheangeyuttime(recycleCheck);//false
        Gamemanager.Instance.RecycleTurnPass(recycleCheck);
        Gamemanager.Instance.TutorialStory.TimeOff();
    }
}
