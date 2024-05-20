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
    [SerializeField,Tooltip("�������Ŀ� �����ִ� ���� ����")] public float oneyut = 0;
    [SerializeField,Tooltip("�������Ŀ� �����ִ� ���� ����")] public float twoyut = 0;
    [SerializeField,Tooltip("�������Ŀ� �����ִ� ���� ����")] public float threeyut = 0;
    public bool zerocheck;

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


    [SerializeField]List<int> yutdisposition = new List<int>();
    private void Awake()
    {
        startbutton.onClick.AddListener(() =>
        {
            if (waittime == true)
            {
                return;
            }
            yutdisposition.Clear();
            Stickcount = 0;
            yutstart = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        yutplaytimer();
        startyutnbutton();
        returnyut();

        numberzero();

        //moveyut();
    }

    private void yutplaytimer()
    {
        if(yutstarttimer == true)
        {
            yutdisposition.Clear();
            Stickcount = 0;
            yutstart = true;
            yutstarttimer = false;
        }
    }


    private void startyutnbutton()//�� ������ ��ư
    {
        //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
        if(yutstart == true)
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
                if(randomcount == 1)//�޸鿡 �ɸ� ���
                {
                    if(yutstick == 3 && Stickcount == 0)//4��° ���̸鼭 ������ �� �ո��� ����
                    {
                        Stickcount += 0;
                    }
                    else
                    {
                        Stickcount += randomcount;
                    }
                }
                if(yutstick == 3 && Stickcount == 0 &&randomcount == 1)//������ ������ ���� �ո� �̸鼭 ������ ���� �޸��� ���
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
            yutstart = false;
            GameObject findtimer = GameObject.Find("Playtimemanager");
            Playtimer playtimer = findtimer.GetComponent<Playtimer>();
            playtimer.checktime = true;
            if(yutnumber == 4 || yutnumber == 5)
            {
                playtimer.returnYut = true;
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

    private void yutrotation()
    {
        
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
        else if(oneyut != 0 && twoyut != 0 && threeyut == 0)
        {
            threeyut = yutnumber;
        }
        else
        {

        }
    }

    private void returnyut()//�� �Ǵ� ���� ������ ��� �ѹ��� ������ �ִ� �ڵ�
    {
        if (yutreturncheck == true)
        {
            //yutstart = true;
            //yutreturncheck = false;
            //waittime = false;
        }

    }

    private void numberzero()//�̵� �Ÿ� �ʱ�ȭ
    {
        if(zerocheck == true)
        {
            oneyut = 0;
            twoyut = 0;
            threeyut = 0;
            zerocheck = false;
        }
    }

}
