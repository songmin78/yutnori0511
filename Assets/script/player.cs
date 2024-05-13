using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] GameObject playercheck;

    [SerializeField]bool playertouch = false;
    [SerializeField]public bool tests;

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
    }


    private void checkplayermouse()//마우스로 플레이어를 선택 할때 작동
    {
        if(playertouch == true && tests == true)
        {
            tests = false;
            Debug.Log("선택됨");
        }
        tests = false;
    }

}
