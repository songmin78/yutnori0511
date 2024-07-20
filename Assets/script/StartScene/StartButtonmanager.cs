using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonmanager : MonoBehaviour
{
    [Header("���۹�ư �� â ����")]
    [SerializeField] Button startbutton;
    [SerializeField] Button endtbutton;
    [SerializeField] Image startwarning;//������ �������� Ʃ�丮���� ������ Ȯ���ϴ� �κ�
    [SerializeField] Button startstage;//���� ȭ������ ��ȯ
    [SerializeField] Button tutorialstage;//Ʃ�丮��ȭ��  ��ȯ
    [SerializeField] Button beforestage;
    [SerializeField] Image endwarning;//������ �������� ���� â
    [SerializeField] Button gameexit;
    [SerializeField] Button beforegame;




    private void Awake()
    {
        startbutton.onClick.AddListener(() =>
        {
            startwarning.gameObject.SetActive(true);
        });

        startstage.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(1);//���� ������ ��ȯ
        });

        tutorialstage.onClick.AddListener(() =>
        {
            //Ʃ�丮�� ������ ��ȯ(���� ���� ����)
            SceneManager.LoadSceneAsync(2);//���� ������ ��ȯ
        });

        beforestage.onClick.AddListener(() =>
        {
            startwarning.gameObject.SetActive(false);
        });

        endtbutton.onClick.AddListener(() =>
        {
            endwarning.gameObject.SetActive(true);
        });

        gameexit.onClick.AddListener(() =>
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//������ ����
        });

        beforegame.onClick.AddListener(() =>
        {
            endwarning.gameObject.SetActive(false);
        });
    }

}
