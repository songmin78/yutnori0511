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

    private void testtouch()//�÷��̾ ��ĭ �����̴��� Ȯ���Ϸ��� �׽�Ʈ
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(count1);
            Debug.Log(count2);
            Debug.Log(count3);
        }
    }
}
