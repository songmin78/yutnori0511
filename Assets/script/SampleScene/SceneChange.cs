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

    private void Awake()
    {
        AgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(1);
            //Gamemanager.Instance.ReStart();
        });

        LobiButton.onClick.AddListener(() =>//�κ�� ���ư��� �ڵ�
        {
            SceneManager.LoadSceneAsync(0);
        });
    }
}
