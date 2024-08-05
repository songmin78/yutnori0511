using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [Header("������ ������ �� �κ� ��ư")]
    [SerializeField] Button AgainButton;//�ٽ��ϱ� ��ư
    [SerializeField] Button LobiButton;//�κ�� ���ư��� ��ư
    [Header("�޴� ��ư")]
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

        LobiButton.onClick.AddListener(() =>//�κ�� ���ư��� �ڵ�
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
