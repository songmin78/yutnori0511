using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberroom : MonoBehaviour
{
    [Header("윷 발판의 번호")]
    [SerializeField,Range(0,19)] float yutButton;

    [Header("지름길 번호 및 체크")]
    [SerializeField,Tooltip("지름길에 멈추었을때")] bool yutshortcut;
    [SerializeField] float shortcutButton;
    public float MaxyutButton;

    [Header("위치 확인")]
    [SerializeField] GameObject poscheck;

    [Header("기타")]
    public float count1;
    public float count2;
    public float count3;
    [SerializeField]Vector3 trs;
    Vector3 mytrs;
    float mynumber;
    bool numbercheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("player"))//플레이어가 해당 발판에 있는 경우
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

    private void testtouch()//플레이어가 몇칸 움직이는지 확인하려고 테스트
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(count1);
            Debug.Log(count2);
            Debug.Log(count3);
        }
    }

    private void myposition()//발판이 자기 위치를 알고있는 코드 한번만 받도록 함
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
