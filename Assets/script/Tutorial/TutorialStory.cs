using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    [SerializeField] Canvas canvasTutorial;
    [SerializeField] TMP_Text storyText;
    int storyNumber = 0;//Ʃ�丮�� ��縦 �ٲٱ� ���� ��ġ üũ
    public int StoryNumber
    {
        get
        {
            return storyNumber;
        }
    }
    [SerializeField]bool storyNext = false;//��Ÿ�� �ٷιٷ� ���丮�� �� �ѱ�� �����ϴ� �ڵ�
    [SerializeField]float storyStayTime = 1f;
    [SerializeField]float MaxstoryStayTime;
    bool tutorialOffCheck = false;


    public enum eRule
    {
        TutorialOn,//Ʃ�丮�� �κ��� Ȱ��ȭ
        TutorialOff,//Ʃ�丮�� �κ��� ��Ȱ��ȭ
        StayTutorial,//�������� ��縦 �ٷ� �ѱ�� ���� ����
        TutorialStay,//Ʃ�丮��ȭ�� �� �㶧 ��� ��޷��ִ� �ڵ�
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
        else if(curStory == eRule.TutorialStay)//Ʃ�丮�� â�� �������� �ٷ� �� �Ѿ������ ����
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
            case 1://�����ϰ� �ٷ� �翩�� ȭ�鿡�� Ŭ�� ������ ���� ���
                storyText.text = "�� ���� �Ȱ��� ���� ������̾� �׷� ������ ���� �غ���";
                tutorialOffCheck = true;//������ �ִ� �ڵ�üũ
                break;
            case 2://������ ���������� �����´�� (������� ������ ������)
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "�� ������� ���� ��÷���ݾ�? �׷� ���� �������� �־�";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;
            case 3:
                storyText.text = "�̷��� �������� ������ �������� �� ������ ���� �������� �־�";
                curStory = eRule.TutorialOn;
                break;
            case 4:
                storyText.text = "�׷� ���� '�� ������' ��ư�� Ŭ���� ���� �ѹ� ��������";
                tutorialOffCheck = true;
                break;
            case 5://���� ���� �����Ŀ� ����� ���
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "���� ���Գ�? ���� ���� 3ĭ �����ϼ� �ְ� ������";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;
            case 6:
                storyText.text = "���� 1ĭ, ���� 2ĭ, ���� 3ĭ, ���� 4ĭ, ��� 5ĭ, ������ -1ĭ�� ���� �־�";
                curStory = eRule.TutorialOn;
                break;
            case 7:
                storyText.text = "�׷� ĳ���͸� ���� �غ� ĳ���ʹ� ������ �Ʒ��� �־� ������ ĳ���ʹ� �Ӹ��� ����� ���ϰž�";
                tutorialOffCheck = true;
                break;
            case 8://ĳ���͸� ���� ���� ��� //�ٷ� Ŭ���� �Ǿ� case 9�� �Ѿ�� ������ ����
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "ĳ���͸� �����߳�? �׷� ������ ���� ��� ������ ���ϰž�";
                storyNext = false;
                curStory = eRule.TutorialStay;//�ٷ� case9�� �� �Ѿ�� ����
                break;
            case 9:
                storyText.text = "�װ��� �ʰ� ������ ���� �����ϼ� �ִ� �κ��� �����ذž� �׷� �� �κ��� Ŭ���غ�";
                tutorialOffCheck = true;
                break;
            case 10:
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "���� �������� ���ʾ� �ڱ��� ���ʴ� ���� �ִ� �׸����� �˼��־�";
                storyNext = false;
                curStory = eRule.TutorialStay;
                break;
            case 11:
                storyText.text = "�׸��� ���� ������ �ð��� ĳ���� �̵� �ð��� ���� �������ٷ� �˼� �־�";
                curStory = eRule.TutorialOn;
                break;
            case 12:
                storyText.text = "���� ������ �ð��� �� ��� ���� �ڵ����� ���������� �̵��ð��� �� ������ �׳� ���� �Ѿ�� �����ؾߵ�";
                tutorialOffCheck = true;
                break;
            case 13:
                canvasTutorial.gameObject.SetActive(true);
                storyText.text = "���忡�� ���� ����";
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
