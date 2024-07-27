using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoButton : MonoBehaviour
{
    [SerializeField] Button GameStartButton;
    [SerializeField] Button TutoRecycleButton;
    [SerializeField] Button LobbyButton;
    // Start is called before the first frame update

    private void Awake()
    {
        GameStartButton.onClick.AddListener(() =>//게임 시작
        {
            SceneManager.LoadSceneAsync(1);
        });
        TutoRecycleButton.onClick.AddListener(() =>//튜토다시하기
        {
            SceneManager.LoadSceneAsync(2);
        });
        LobbyButton.onClick.AddListener(() =>//로비로 가기
        {
            SceneManager.LoadSceneAsync(0);
        });

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
