using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberroom : MonoBehaviour
{
    [Header("�� ������ ��ȣ")]
    [SerializeField,Range(0,19)] float yutButton;

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
    float mynumber;
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
        mynumber = yutButton;
    }

    private void testcode()
    {
        if(numbercheck == true)
        {
            numbercheck = false;
            trs = mytrs;
            //poscheck.transform.position = trs;
        }
    }

    private void tests()
    {
    }

}
