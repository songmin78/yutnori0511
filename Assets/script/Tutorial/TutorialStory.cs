using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    [SerializeField] TMP_Text storyText;
    int storyNumber = 0;//튜토리얼 대사를 바꾸기 위한 위치 체크
    public int StoryNumber
    {
        get
        {
            return storyNumber;
        }
    }
    bool storyNext = false;//연타로 바로바로 스토리를 못 넘기게 제한하는 코드
    [SerializeField]float storyStayTime = 1f;
    [SerializeField]float MaxstoryStayTime;
    bool tutorialOffCheck = false;


    public enum eRule
    {
        TutorialOn,//튜토리얼 부분이 활성화
        TutorialOff,//튜토리얼 부분이 비활성화
        StayTutorial,//투툐리얼 대사를 바로 넘길수 없게 조정
    }
    private eRule curStory = eRule.StayTutorial;

    private void Awake()
    {
        MaxstoryStayTime = storyStayTime;
    }

    void Start()
    {
        Gamemanager.Instance.TutorialStory = this;
        storyText.text = "안녕 플레이어 난 이게임을 설명할 블루라고해 반가워!";
    }

    // Update is called once per frame
    void Update()
    {
        if(curStory == eRule.TutorialOn)
        {
            nextStory();
        }
        else if(curStory == eRule.TutorialOff)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                TimeOn();
            }
        }
        else if(curStory == eRule.StayTutorial)
        {
            if(MaxstoryStayTime < 0)
            {
                MaxstoryStayTime = storyStayTime;
                storyNext = true;
                if(tutorialOffCheck == true)
                {
                    tutorialOffCheck = false;
                    curStory = eRule.TutorialOff;
                }
                else
                {
                    curStory = eRule.TutorialOn;
                }
            }
            else
            {
                MaxstoryStayTime -= Time.unscaledDeltaTime;
                //Time.unscaledDeltaTime <- timescale에 영향을 받지 않는다
            }
        }
    }

    private void nextStory()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && storyNext == true)
        {
            storyNumber += 1;
            storyNext = false;
            storyLine();
            curStory = eRule.StayTutorial;
        }
    }

    private void storyLine()
    {
        switch(storyNumber)
        {
            case 1://시작하고 바로 띄여진 화면에서 클릭 했을때 오는 대사
                storyText.text = "넌 나랑 똑같이 생긴 블루팀이야 그럼 게임을 시작 해보자";
                tutorialOffCheck = true;
                break;
            case 2://숫서가 정해졌을때 들어오는대사 (블루팀이 오도록 설정됨)
                storyText.text = "오 블루팀이 먼저 당첨되어서 먼저 던질수가 있어";
                curStory = eRule.TutorialOn;
                break;
            case 3://case2에서 클릭했을때 들어오는 대사
                storyText.text = "이렇게 랜덤으로 숫서가 정해지고 그 다음에 윷을 던질수가 있어";
                curStory = eRule.TutorialOn;
                break;
            case 4:
                storyText.text = "그럼 이제 '윷 던지기' 버튼을 클릭해 윷을 한번 던져보자";
                tutorialOffCheck = true;
                break;
            case 5://윷을 던져 나온후에 생기는 대사
                storyText.text = "걸이 나왔네? 걸은 말을 3칸 움직일수 있게 해주지";
                curStory = eRule.TutorialOn;
                break;
            case 6:
                storyText.text = "도는 1칸, 개는 2칸, 걸은 3칸, 윷은 4칸, 모는 5칸, 빽도는 -1칸을 갈수 있어";
                tutorialOffCheck = true;
                break;
        }
    }

    public void TimeOn()
    {
        Time.timeScale = 1;
    }

    public void TimeOff()
    {
        Time.timeScale = 0;
        storyNumber += 1;
        storyLine();
    }
}
