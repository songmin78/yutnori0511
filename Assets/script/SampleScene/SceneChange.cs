using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [Header("게임이 끝났을 때 부분 버튼")]
    [SerializeField] Button AgainButton;//다시하기 버튼
    [SerializeField] Button LobiButton;//로비로 돌아가는 버튼
    [Header("메뉴 버튼")]
    [SerializeField] Button menuButton;
    [SerializeField] Canvas MenuScene;
    [SerializeField] Button LobbyScene;
    [SerializeField] Button PlayButton;

    private void Awake()
    {
        AgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(1);
            Time.timeScale = 1;
            //Gamemanager.Instance.ReStart();
        });

        LobiButton.onClick.AddListener(() =>//로비로 돌아가는 코드
        {
            SceneManager.LoadSceneAsync(0);
        });

        menuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            MenuScene.gameObject.SetActive(true);
        });

        LobbyScene.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(0);
        });

        PlayButton.onClick.AddListener(() =>
        {
            MenuScene.gameObject.SetActive(false);
            Time.timeScale = 1;
        });

    }
}
