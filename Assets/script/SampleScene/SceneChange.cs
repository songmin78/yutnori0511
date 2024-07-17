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

    private void Awake()
    {
        AgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(1);
            //Gamemanager.Instance.ReStart();
        });

        LobiButton.onClick.AddListener(() =>//로비로 돌아가는 코드
        {
            SceneManager.LoadSceneAsync(0);
        });
    }
}
