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

    public float count1;
    public float count2;
    public float count3;

    private void Awake()
    {
        MaxyutButton = yutButton;
    }

    private void Update()
    {
        testtouch();
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
}
