using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    [SerializeField] TMP_Text storyText;

    public enum eRule
    {
        TutorialOn,//튜토리얼 부분이 활성화
        TutorialOff,//튜토리얼 부분이 비활성화
        StayTutorial,//투툐리얼 대사를 바로 넘길수 없게 조정
    }

    void Start()
    {
        storyText.text = "안녕 플레이어 난 이게임을 설명할 블루라고해 반가워!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void nextStory()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            storyText.text = "";
        }
    }
}
