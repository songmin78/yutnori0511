using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject playd;
    [Header("�÷��̾�")]
    [SerializeField] bool playercheck1;//�÷��̾�1�� �κ�
    [SerializeField] bool playercheck2;//�÷��̾� 2�� �κ�
    [Header("�÷��̾ ���� ������")]
    [SerializeField] public GameObject checkobj;//�÷��̷��� üũ�Ҷ� ���̴� ������Ʈ
    [SerializeField] bool checkmask;//�÷��̾ üũ �Ҷ�

    [Header("��Ÿ")]
    [SerializeField]bool playertouch = false;
    [SerializeField]public bool playerchoice;
    [SerializeField]public bool tests;
    public bool playertype1;//�÷��̾�Ÿ��1
    public bool playertype2;//�÷��̾�Ÿ��2

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("mouse"))
        {
            playertouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("mouse"))
        {
            playertouch = false;
        }
    }

    private void Awake()
    {
        //Gamemanager.Instance.Player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkplayermouse();
        typeplayer();

        testcode();
    }


    private void checkplayermouse()//���콺�� �÷��̾ ���� �Ҷ� �۵�
    {
        if(playertouch == true && playerchoice == true)
        {
            playerchoice = false;
            //tests = true;
            Debug.Log("���õ�");
        }
        playerchoice = false;
    }

    private void typeplayer()//�÷��̾�  Ÿ�Կ� ���� �۵��Ǵ� �κ� �ȵǴ� �κ� �����ϴ°�
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

    private void testcode()
    {
        if (tests == true)
        {
            //tests = false;
            //Debug.Log("�Ϸ�");
        }

        if (playerchoice == true)
        {
            
        }
    }

    private void movetest()
    {

    }

}
