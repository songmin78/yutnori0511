using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    [SerializeField] TMP_Text storyText;

    public enum eRule
    {
        TutorialOn,//Ʃ�丮�� �κ��� Ȱ��ȭ
        TutorialOff,//Ʃ�丮�� �κ��� ��Ȱ��ȭ
        StayTutorial,//�������� ��縦 �ٷ� �ѱ�� ���� ����
    }

    void Start()
    {
        storyText.text = "�ȳ� �÷��̾� �� �̰����� ������ ������� �ݰ���!";
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
