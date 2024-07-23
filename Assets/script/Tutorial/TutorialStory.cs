using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    [SerializeField] Canvas canvasTutorial;
    [SerializeField] TMP_Text storyText;
    int storyNumber = 0;//튜토리얼 대사를 바꾸기 위한 위치 체크
    public int StoryNumber
    {
        get
        {
            return storyNumber;
        }
    }
    [SerializeField]bool storyNext = false;//연타로 바로바로 스토리를 못 넘기게 제한하는 코드
    [SerializeField]float storyStayTime = 1f;
    [SerializeField]float MaxstoryStayTime;
    bool tutorialOffCheck = false;


    public enum eRule
    {
        TutorialOn,//튜토리얼 부분이 활성화
        TutorialOff,//튜토리얼 부분이 비활성화
        StayTutorial,//투툐리얼 대사를 바로 넘길수 없게 조정
        TutorialStay,//튜토리얼화면 이 뜰때 잠시 기달려주는 코드
    }
    private eRule curStory = eRule.StayTutorial;

    private void Awake()
    {
        MaxstoryStayTime = storyStayTime;
    }

    void Start()
    {
        Gamemanager.Instance.TutorialStory = this;
        canvasTutorial.gameObject.SetActive(true);
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
        else if(curStory == eRule.TutorialStay)//튜토리얼 창이 보여지고 바로 안 넘어가지도록 설정
        {
            if (MaxstoryStayTime < 0)
            {
                MaxstoryStayTime = storyStayTime;
                storyNext = true;
                curStory = eRule.TutorialOn;
            }
            else
            {
                MaxstoryStayTime -= Time.unscaledDeltaTime;
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
                tutorialOffCheck = true;//끝낼수 있는 코드체크
                break;
            case 2://숫서가 정해졌을때 들어오는대사 (블루팀이 오도록 설정됨)
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "오 블루팀이 먼저 당첨됐잖아? 그럼 먼저 던질수가 있어";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;
            case 3:
                storyText.text = "이렇게 랜덤으로 순서가 정해지고 그 다음에 윷을 던질수가 있어";
                curStory = eRule.TutorialOn;
                break;
            case 4:
                storyText.text = "그럼 이제 '윷 던지기' 버튼을 클릭해 윷을 한번 던져보자";
                tutorialOffCheck = true;
                break;
            case 5://윷을 던져 나온후에 생기는 대사
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "걸이 나왔네? 걸은 말을 3칸 움직일수 있게 해주지";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;
            case 6:
                storyText.text = "도는 1칸, 개는 2칸, 걸은 3칸, 윷은 4칸, 모는 5칸, 빽도는 -1칸을 갈수 있어";
                curStory = eRule.TutorialOn;
                break;
            case 7:
                storyText.text = "그럼 캐릭터를 선택 해봐 캐릭터는 오른쪽 아래에 있어 선택한 캐릭터는 머리에 사과가 보일거야";
                tutorialOffCheck = true;
                break;
            case 8://캐릭터를 선택 했을 경우 //바로 클릭이 되어 case 9로 넘어가는 현상이 생김
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "캐릭터를 선택했네? 그럼 윷판을 보면 흰색 공간이 보일거야";
                storyNext = false;
                curStory = eRule.TutorialStay;//바로 case9로 안 넘어가게 조절
                break;
            case 9:
                storyText.text = "그것은 너가 선택한 말이 움직일수 있는 부분을 보여준거야 그럼 그 부분을 클릭해봐";
                tutorialOffCheck = true;
                break;
            case 10:
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "이제 레드팀의 차례야 자기팀 차례는 위에 있는 그림으로 알수있어";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;
            case 11:
                storyText.text = "그리고 윷을 던지는 시간과 캐릭터 이동 시간은 위에 게이지바로 알수 있어";
                curStory = eRule.TutorialOn;
                break;
            case 12:
                storyText.text = "윷을 던지는 시간이 다 닳면 윷이 자동으로 던져지지만 이동시간이 다 닳으면 그냥 턴이 넘어가니 조심해야되";
                tutorialOffCheck = true;
                break;
            case 13:
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "레드에서 개가 떴네";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;

        }
    }

    public void TimeOn()
    {
        canvasTutorial.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void TimeOff()
    {
        Time.timeScale = 0;
        storyNumber += 1;
        //Debug.Log(storyNumber);
        storyLine();
    }
}
