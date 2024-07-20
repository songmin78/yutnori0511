using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonmanager : MonoBehaviour
{
    [Header("시작버튼 및 창 관리")]
    [SerializeField] Button startbutton;
    [SerializeField] Button endtbutton;
    [SerializeField] Image startwarning;//게임을 시작할지 튜토리얼을 들을지 확인하는 부분
    [SerializeField] Button startstage;//게임 화면으로 전환
    [SerializeField] Button tutorialstage;//튜토리얼화면  전환
    [SerializeField] Button beforestage;
    [SerializeField] Image endwarning;//게임을 끄기전에 띄우는 창
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
            SceneManager.LoadSceneAsync(1);//게임 시작을 전환
        });

        tutorialstage.onClick.AddListener(() =>
        {
            //튜토리얼 씬으로 전환(아직 제작 안함)
            SceneManager.LoadSceneAsync(2);//게임 시작을 전환
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
            Application.Quit();//어플을 종료
        });

        beforegame.onClick.AddListener(() =>
        {
            endwarning.gameObject.SetActive(false);
        });
    }

}
