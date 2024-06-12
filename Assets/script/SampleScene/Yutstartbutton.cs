using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yutstartbutton : MonoBehaviour
{
    [Header("������")]
    [SerializeField] GameObject yut1;
    [SerializeField] GameObject yut2;
    [SerializeField] GameObject yut3;
    [SerializeField] GameObject yut4;
    [Header("������ �����ִ� ���� �ִ� 3������ ���� ����")]
    float yutnumber = 0;
    bool yutreturncheck;
    [SerializeField, Tooltip("�������Ŀ� �����ִ� ���� ����")] public float oneyut = 0;
    [SerializeField, Tooltip("�������Ŀ� �����ִ� ���� ����")] public float twoyut = 0;
    [SerializeField, Tooltip("�������Ŀ� �����ִ� ���� ����")] public float threeyut = 0;
    public bool zeromovecheck;

    float yuttype;
    [Header("�� ������ ��ư")]
    //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
    [SerializeField] Button startbutton;
    bool yutstart;
    bool randomcheck;//���� �� ������ Ȯ�� ���ִ� �ڵ�
    int randomcount;
    int Stickcount;
    public bool waittime;//ĳ���͸� �����̱����� �ð��� ��ư�� ������ �۵� �ȵǰ� ����
    public bool yutstarttimer;
    [Header("�� Ÿ")]
    //[SerializeField] Image timegage;
    //���� �ո����� �޸��� Ȯ�� ���ִ� �κ�
    int chageyut;
    [SerializeField]float stayyut = 1;//�� �Ǵ� ���� �ɸ��� ��� ��ٸ��� �ð�
    bool stayyutcheck;//1�� ��ٸ��� �ð��� üũ
    [Header("��ũ��Ʈ������Ʈ�� ����")]
    [SerializeField] GameObject Playtimemanager;


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

    // Update is called once per frame
    void Update()
    {
        //if(Gamemanager.Instance.throwyutbutton == false)
        //{
        //    return;
        //}
        yutplaytimer();
        startyutnbutton();
        resetyut();
        //returnyut();

        numberzero();
        //moveyut();

    }

    private void yutplaytimer()
    {
        if (yutstarttimer == true)
        {
            yutdisposition.Clear();
            Stickcount = 0;
            yutstart = true;
            yutstarttimer = false;
        }
    }


    private void startyutnbutton()//�� ������ ��ư
    {
        if (stayyutcheck == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.N))//Ȯ�� ���� �ߵ��� ����
        {
            yutnumber = 4;
            Stickcount = 4;
            for (int yutstick = 0; yutstick < 4; yutstick++)
            {
                randomcount = 1;
                chageyut += 1;
                Yutcount();
            }
            yutlist();
            if (oneyut != 0 && twoyut != 0 && threeyut != 0)
            {
                Gamemanager.Instance.nextturn();
                return;
            }
            //GameObject findtimer = GameObject.Find("Playtimemanager");
            Playtimer playtimer = Playtimemanager.GetComponent<Playtimer>();
            playtimer.checktime = true;
            if (yutnumber == 4 || yutnumber == 5)
            {
                stayyutcheck = true;
                playtimer.returnYut = true;
            }
        }
        //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
        if (yutstart == true)
        {
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
                if (yutstick == 3 && Stickcount == 0 && randomcount == 1)//������ ������ ���� �ո� �̸鼭 ������ ���� �޸��� ���
                {
                    randomcheck = true;
                    yutnumber = -1;
                    yutturnNumber();
                    Debug.Log("����");
                }
                yutdisposition.Add(randomcount);
            }
            //Debug.Log(Stickcount);
            yutlist();
            if (oneyut != 0 && twoyut != 0 && threeyut != 0)
            {
                Gamemanager.Instance.nextturn();
                return;
            }
            yutstart = false;
            //GameObject findtimer = GameObject.Find("Playtimemanager");
            Playtimer playtimer = Playtimemanager.GetComponent<Playtimer>();
            playtimer.checktime = true;
            if (yutnumber == 4 || yutnumber == 5)//�� �Ǵ� ���� �� ���
            {
                stayyutcheck = true;
                if (threeyut != 0)
                {
                    return;
                }
                playtimer.returnYut = true;
            }
            else//�� �Ǵ� ���� �ȶ� ���
            {
                Gamemanager.Instance.throwyutbutton = false;
            }
        }

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
                yutreturncheck = true;
                yutturnNumber();
                Debug.Log("��");
                break;
            case 1:
                yutnumber = 1;
                yutturnNumber();
                Debug.Log("��");
                break;
            case 2:
                yutnumber = 2;
                yutturnNumber();
                Debug.Log("��");
                break;
            case 3:
                yutnumber = 3;
                yutturnNumber();
                Debug.Log("��");
                break;
            case 4:
                yutnumber = 4;
                yutreturncheck = true;
                yutturnNumber();
                Debug.Log("��");
                break;
        }
    }


    private void yutturnNumber()//���� �ɸ� ���ڸ�ŭ ���ڸ� ����
    {
        if (oneyut == 0)
        {
            oneyut = yutnumber;
        }
        else if (oneyut != 0 && twoyut == 0)
        {
            twoyut = yutnumber;
        }
        else if (oneyut != 0 && twoyut != 0 && threeyut == 0)
        {
            threeyut = yutnumber;
        }
    }


    private void numberzero()//�̵� �Ÿ� �ʱ�ȭ
    {
        if (zeromovecheck == true)
        {
            oneyut = 0;
            twoyut = 0;
            threeyut = 0;
            zeromovecheck = false;
        }
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
                }
                break;
            case 2:
                if (randomcount == 1)
                {
                    yut2.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 3:
                if (randomcount == 1)
                {
                    yut3.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case 4:
                if (randomcount == 1)
                {
                    yut4.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
        }
    }


    private void resetyut()//���� �� �Ǵ� ���� �ɸ� ���
    {
        if (stayyutcheck == true)
        {
            //GameObject findtimer = GameObject.Find("Playtimemanager");
            Playtimer playtimer = Playtimemanager.GetComponent<Playtimer>();
            playtimer.returnyut = true;
            if (stayyut < 0)
            {
                yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
                yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
                yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
                yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
                stayyut = 1;
                stayyutcheck = false;
                playtimer.returnyut = false;
                chageyut = 0;
            }
            else
            {
                stayyut -= Time.deltaTime;
            }
        }
    }

    public void getbackYut()
    {
        yut1.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut2.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut3.transform.rotation = Quaternion.Euler(0, 0, 0);
        yut4.transform.rotation = Quaternion.Euler(0, 0, 0);
        chageyut = 0;
    }
}
