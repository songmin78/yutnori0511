using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberroom : MonoBehaviour
{
    [Header("�� ������ ��ȣ")]
    [SerializeField,Range(0,19)] float yutButton;//������ ���� ��ġ
    [SerializeField] List<float> Yutnumber = new List<float>();//������ ��ġ�� �޴� �κ�

    [Header("������ ��ȣ �� üũ")]
    [SerializeField,Tooltip("�����濡 ���߾�����")] bool yutshortcut;
    [SerializeField] float shortcutButton;
    public float MaxyutButton;

    [Header("��ġ Ȯ��")]
    [SerializeField] GameObject poscheck;

    [Header("��Ÿ")]
    public float count1;
    public float count2;
    public float count3;
    [SerializeField]Vector3 trs;
    Vector3 mytrs;
    public float mynumber;//���� ���� �ٲ���ġ
    bool numbercheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("player"))//�÷��̾ �ش� ���ǿ� �ִ� ���
        {
            trs = transform.position;
            numbercheck = true;
        }
    }

    private void Awake()
    {
        MaxyutButton = yutButton;
    }

    private void Start()
    {
        myposition();

        Yutnumber.Add(yutButton);

        Gamemanager.Instance.Numberroom = this; 
    }

    private void Update()
    {
        testtouch();
        testcode();
    }

    private void testtouch()//�÷��̾ ��ĭ �����̴��� Ȯ���Ϸ��� �׽�Ʈ
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(count1);
            Debug.Log(count2);
            Debug.Log(count3);
        }
    }

    private void myposition()//������ �ڱ� ��ġ�� �˰��ִ� �ڵ� �ѹ��� �޵��� ��
    {
        mytrs = transform.position;
        mynumber = 0;
    }

    private void testcode()
    {
        if(numbercheck == true)
        {
            numbercheck = false;
            trs = mytrs;
            tests();
            //poscheck.transform.position = trs;
        }
    }

    private void tests()
    {
        if(mynumber != yutButton)
        {
            Yutnumber.Exists(numbercount => numbercount == mynumber);
            {

            }
        }
    }

}
