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
        GameStartButton.onClick.AddListener(() =>//���� ����
        {
            SceneManager.LoadSceneAsync(1);
        });
        TutoRecycleButton.onClick.AddListener(() =>//Ʃ��ٽ��ϱ�
        {
            SceneManager.LoadSceneAsync(2);
        });
        LobbyButton.onClick.AddListener(() =>//�κ�� ����
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
