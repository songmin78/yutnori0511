using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public bool playertouch;//Ŭ�� ������ on���� ��ȯ Ŭ���� ������ off�� ��ȯ

    void Start()
    {
        
    }

    void Update()
    {
        Onclickplayer();
    }

    private void Onclickplayer()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playertouch = true;
            Debug.Log("�۵�");
        }
        playertouch = false;
    }
}
