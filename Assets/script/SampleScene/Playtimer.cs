using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playtimer : MonoBehaviour
{
    [SerializeField] public bool teamred;
    [SerializeField] public bool teamblue;
    [Header("���� ������ ������ ���� �ð� ����")]
    [SerializeField] float throwtime;
    [SerializeField]float Maxthrowtime;//Ȯ�� ��
    bool throwwaitcheck;//������� ���� ��ٸ�
    [Header("���� ������ ĳ���� �̵��� ���� ���� �ð� ����")]
    [SerializeField] float waitmovetime;
    [SerializeField]float Maxwaitmovetime;//Ȯ�ο�
    bool playermovecheck;//���� ������ �����̴°��� ��ٸ��� �κ�
    [Header("��Ÿ")]
    [SerializeField] public bool checktime;//���� �������� true�� ��ȯ(�ۿ��� �޾ƿ�)
    [SerializeField] Image timegage;//�ð��� �پ��� ������
    public bool returnYut;//�� ���� �߸� true�� ��ȯ
    //[SerializeField] GameObject poscheck1;//ù��° ���� �̵� ����
    public bool returnyut;//�� ���� �㶧 �ٷ� �ð��� ���ư��°��� ����

    public enum eRule
    {
        Throwtime,//������ �ð��� �ٷ�� �κ�
        Movetime,//�����̴� �ð��� �ٷ�� �κ�
        Returnthrowtime,//���� ������� �ٽ� ���� �� �ִ� �κ�
    }
    private eRule curState = eRule.Throwtime;

    private void Awake()
    {
        Maxthrowtime = throwtime;
        Maxwaitmovetime = waitmovetime;
    }

    void Update()
    {
        waityuttime();//�� ������ ��ư�� �� ������ �۵�
        cheangeyuttime();//�� ������ ��ư�� ������ �۵�
        movewaittimer();
        //playercheck();

        timecalculate();

        yuttest();
        changeteam();

        //if(curState == eRule.Throwtime)
        //{

        //}
        //else if(curState == eRule.Movetime)
        //{

        //}
        //else if(curState == eRule.Returnthrowtime)
        //{

        //}
    }

    public void startturn(int _startteam)//���� ������ �� ����
    {
        switch(_startteam)
        {
            case 0:
                teamred = true; break;
            case 1:
                teamblue = true; break;
        }
    }

    private void waityuttime()//���� ������ ���� ��ٸ��� �ð� �ڵ�
    {
        if(throwwaitcheck == true || checktime == true || returnyut == true)//���� �����Ŀ� true�� ��ȯ�Ͽ� �� ������ ����� �ڵ�
        {
            return;
        }
        if(Maxthrowtime <= 0 )
        {
            Maxthrowtime = throwtime;//�� �ʱ�ȭ
            throwwaitcheck = true;//���̻� �۵����� �ʰ� ����
            playermovecheck = true;//�۵��ϰ� ����
            GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.yutstarttimer = true;
            Debug.Log("�̵����� ����");
        }
        else
        {
            //Maxthrowtime -= Time.deltaTime;//���� �����⸦ ��ٸ��� �ڵ�
        }
    }

    private void cheangeyuttime()
    {
        if(checktime == true)//�� ������ ��ư�� ���� ���
        {
            Maxthrowtime = throwtime;//�� �ʱ�ȭ
            throwwaitcheck = true;//���̻� �۵����� �ʰ� ����
            playermovecheck = true;//�۵��ϰ� ����
            checktime = false;
            //Debug.Log("�̵����� ����");
        }
    }

    private void movewaittimer()//���� �����̴°��� ��ٸ��� �ڵ�
    {
        if(playermovecheck == false || returnYut == true)
        {
            return;
        }
        if(Maxwaitmovetime <= 0)
        {
            Maxwaitmovetime = waitmovetime;
            playermovecheck = false;//���̻� �۵� ���ϰ� ����
            throwwaitcheck = false;//�� ������ �κ��� �۵��ϰ� ����
            if (teamred == true)
            {
                teamred = false;
                teamblue = true;
            }
            else if (teamblue == true)
            {
                teamred = true;
                teamblue = false;
            }
            Debug.Log("������� ����");

            //poscheck1.SetActive(false);
            GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = false;
            buttontimer.zeromovecheck = true;

        }
        else
        {
            //Maxwaitmovetime -= Time.deltaTime;
            GameObject findyut = GameObject.Find("Yutstartbutton");
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = true;
        }
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

    private void yuttest()
    {
        if(returnYut == true)
        {
            playermovecheck = false;//���̻� �۵� ���ϰ� ����
            throwwaitcheck = false;//�� ������ �κ��� �۵��ϰ� ����

            GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();
            buttontimer.waittime = false;
            returnYut = false;
        }
    }

    public void  changeteam()
    {
        if(teamblue == true)
        {
            Gamemanager.Instance.Chageplayteam(1);
            //Gamemanager.Instance.teamfalsecheck();
        }
        else if(teamred == true)
        {
            Gamemanager.Instance.Chageplayteam(2);
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
}
