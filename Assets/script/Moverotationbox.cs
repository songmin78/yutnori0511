using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverotationbox : MonoBehaviour
{
    [SerializeField] GameObject objplayer1_1;
    [SerializeField] GameObject objplayer1_2;
    [SerializeField] GameObject objplayer2_1;
    [SerializeField] GameObject objplayer2_2;
    [SerializeField]bool movecheck;
    [SerializeField]Vector3 vec3;//위치 확인용
    bool team1;
    bool team2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("mouse"))
        {
            movecheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("mouse"))
        {
            movecheck = false;
        }
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        positioncheck();
        //playerbox();
        moveposition();
        changeteamplayer();
    }

    private void playerbox()
    {
        //if (Gameplayertype == 1)
        //{
        //    Player player = objplayer1_1.GetComponent<Player>();
        //    Player player2 = objplayer1_2.GetComponent<Player>();
        //    player.playerchoice = true;
        //    player2.playerchoice = true;
        //}
        //else if (Gameplayertype == 2)
        //{
        //    Player player = objplayer2_1.GetComponent<Player>();
        //    Player player2 = objplayer2_2.GetComponent<Player>();
        //    player.playerchoice = true;
        //    player2.playerchoice = true;
        //}
    }

    private void moveposition()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && movecheck == true)
        {
            if (team1 == true)
            {
                objplayer1_1.transform.position = vec3;
                objplayer1_2.transform.position = vec3;
            }
            else if (team2 == true)
            {
                objplayer2_1.transform.position = vec3;
                objplayer2_2.transform.position = vec3;
            }
        }
    }

    private void positioncheck()
    {
        vec3 = transform.position;
    }

    private void changeteamplayer()//턴이 끝나면 다른팀으로 될때 사용 가능한걸 따로 구분
    {
        GameObject obj = GameObject.Find("Playtimemanager");
        Playtimer playtimer = obj.GetComponent<Playtimer>();
        team1 = playtimer.changeteam1;
        team2 = playtimer.changeteam2;
    }
}
