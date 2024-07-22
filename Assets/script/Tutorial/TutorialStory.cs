using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    [SerializeField] TMP_Text storyText;
    int storyNumber = 0;//Ʃ�丮�� ��縦 �ٲٱ� ���� ��ġ üũ
    public int StoryNumber
    {
        get
        {
            return storyNumber;
        }
    }
    bool storyNext = false;//��Ÿ�� �ٷιٷ� ���丮�� �� �ѱ�� �����ϴ� �ڵ�
    [SerializeField]float storyStayTime = 1f;
    [SerializeField]float MaxstoryStayTime;
    bool tutorialOffCheck = false;


    public enum eRule
    {
        TutorialOn,//Ʃ�丮�� �κ��� Ȱ��ȭ
        TutorialOff,//Ʃ�丮�� �κ��� ��Ȱ��ȭ
        StayTutorial,//�������� ��縦 �ٷ� �ѱ�� ���� ����
    }
    private eRule curStory = eRule.StayTutorial;

    private void Awake()
    {
        MaxstoryStayTime = storyStayTime;
    }

    void Start()
    {
        Gamemanager.Instance.TutorialStory = this;
        storyText.text = "�ȳ� �÷��̾� �� �̰����� ������ ������� �ݰ���!";
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
                //Time.unscaledDeltaTime <- timescale�� ������ ���� �ʴ´�
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
            case 1://�����ϰ� �ٷ� �翩�� ȭ�鿡�� Ŭ�� ������ ���� ���
                storyText.text = "�� ���� �Ȱ��� ���� ������̾� �׷� ������ ���� �غ���";
                tutorialOffCheck = true;
                break;
            case 2://������ ���������� �����´�� (������� ������ ������)
                storyText.text = "�� ������� ���� ��÷�Ǿ ���� �������� �־�";
                curStory = eRule.TutorialOn;
                break;
            case 3://case2���� Ŭ�������� ������ ���
                storyText.text = "�̷��� �������� ������ �������� �� ������ ���� �������� �־�";
                curStory = eRule.TutorialOn;
                break;
            case 4:
                storyText.text = "�׷� ���� '�� ������' ��ư�� Ŭ���� ���� �ѹ� ��������";
                tutorialOffCheck = true;
                break;
            case 5://���� ���� �����Ŀ� ����� ���
                storyText.text = "���� ���Գ�? ���� ���� 3ĭ �����ϼ� �ְ� ������";
                curStory = eRule.TutorialOn;
                break;
            case 6:
                storyText.text = "���� 1ĭ, ���� 2ĭ, ���� 3ĭ, ���� 4ĭ, ��� 5ĭ, ������ -1ĭ�� ���� �־�";
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
