using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonmanager : MonoBehaviour
{
    [Header("시작버튼 및 창 관리")]
    [SerializeField] Button startbutton;
    [SerializeField] Button endtbutton;
    [SerializeField] Image startwarning;//게임을 시작할지 튜토리얼을 들을지 확인하는 부분
    [SerializeField] Button startstage;//게임 화면으로 전환
    [SerializeField] Button tutorialstage;//튜토리얼화면  전환


    
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
