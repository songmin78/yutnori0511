using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject findplayd;
    [Header("�÷��̾�")]
    [SerializeField] bool playercheck1;//�÷��̾�1�� �κ�
    [SerializeField] bool playercheck2;//�÷��̾� 2�� �κ�
    [Header("�÷��̾ ���� ������")]
    //[SerializeField] public GameObject checkobj;//�÷��̷��� üũ�Ҷ� ���̴� ������Ʈ
    [SerializeField] bool checkmask;//�÷��̾ üũ �Ҷ�
    //[SerializeField] Canvas playerselected;//�÷��̾ ���õ� �� üũ�Ǵ°� �����ִ� ������Ʈ
    bool selectedcheck;//���õɶ� üũ

    bool buleTeam;

    [Header("��Ÿ")]
    [SerializeField]bool playertouch = false;
    [SerializeField]public bool playerchoice;
    [SerializeField]public bool tests;
    public bool playertype1;//�÷��̾�Ÿ��1
    public bool playertype2;//�÷��̾�Ÿ��2
    [SerializeField]public bool playerchecking1;
    [SerializeField]public bool playerchecking2;
    [Header("�� �̵� �κ�")]
    [SerializeField] public float oneYut;
    [SerializeField] public float twoYut;
    [SerializeField] public float threeYut;
    //[SerializeField] float d;
    [SerializeField]public bool playertypenumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("mouse"))
        {
            playertouch = true;
            playertypenumber = true;
            #region
            //if(playercheck1 == true)//�÷��̾� Ÿ���� 1���� ���
            //{
            //    playertype1 = true;//�÷��̾� Ÿ�� 1���� Ȯ��
            //    playertype2 = false;
            //}
            //if(playercheck2 == true)
            //{
            //    playertype1 = false;
            //    playertype2 = true;
            //}
            #endregion
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("mouse"))
        {
            playertouch = false;
            //if (playercheck1 == true)
            //{
            //    playertype1 = false;
            //    playertype2 = false;
            //}
            //else if (playercheck2 == true)
            //{
            //    playertype1 = false;
            //    playertype2 = false;
            //}
        }
    }

    private void Awake()
    {
        //Gamemanager.Instance.Player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Gamemanager.Instance.Player = this;
    }

    // Update is called once per frame
    void Update()
    {
        //checkplayermouse();
        //typeplayer();

        //testcode();
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
    #endregion

    private void typeplayer()//�÷��̾�1 �� �÷��̾�2�� ���ÿ� �� �����̰� ����
    {
        //if(playertype1 == true)
        //{
        //    playertype2 = false;
        //    checkobj.SetActive(true);
        //}
        //else if(playertype2 == true)
        //{
        //    playertype1 = false;
        //    checkobj.SetActive(true);
        //}
    }

    public void testcode()//��ġ�����ϴ� �κ�
    {
        //�÷��̾ �����;� �Ұ� ������ ���� ����,������ ����
        GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();   

        oneYut = buttontimer.oneyut;
        twoYut = buttontimer.twoyut;
        threeYut = buttontimer.threeyut;

        GameObject objfoot = GameObject.Find("footholdbox");
        Footholdbox footholdbox = objfoot.GetComponent<Footholdbox>();
        footholdbox.zerocheck = true;

        //Gamemanager.Instance.Numberroom.mynumber += a;
        //Gamemanager.Instance.Numberroom.zerocheck = true;

        //GameObject obj = GameObject.Find("footholdbox");
        //Numberroom numberroom = obj.GetComponentInChildren<Numberroom>();
        //numberroom.count1 = a;
        //numberroom.count2 = b;
        //numberroom.count3 = c;
    }

    //private void playertypechoice()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Physics.Raycast(ray, );
    //}

    public void Playselectedcheck(bool _value)
    {
            findplayd.SetActive(_value);
    }

    public void Selectlocation()//�÷��̾ ���õɶ� �����ϼ� �ִ� ĭ�� �����ִ� �κ�
    {
        GameObject obj = GameObject.Find("footholdbox");
        Footholdbox footholdbox = obj.GetComponent<Footholdbox>();
    }
}
