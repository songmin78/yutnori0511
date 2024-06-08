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
    [SerializeField]Vector3 vec3;//��ġ Ȯ�ο�
    bool team1;
    bool team2;
    [SerializeField]bool playertype1;//�÷��̾� 1���� �۵��ϵ��� ����
    [SerializeField]bool playertype2;//�÷��̾� 1���� �۵��ϵ��� ����


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == ("mouse"))
    //    {
    //        movecheck = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == ("mouse"))
    //    {
    //        movecheck = false;
    //    }
    //}

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
        //positioncheck();
        ////playerbox();
        //moveposition();
        //changeteamplayer();
    }

    private void playerbox()
    {

    }

    private void moveposition()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && movecheck == true)
        {
            #region
            //if (team1 == true)
            //{
            //    Player player = objplayer1_1.GetComponent<Player>();
            //    Player player2 = objplayer1_2.GetComponent<Player>();
            //    if (player.playertype1 == true)
            //    {
            //        objplayer1_1.transform.position = vec3;
            //    }
            //    else if(player2.playertype2 == true)
            //    {
            //        objplayer1_2.transform.position = vec3;
            //    }
            //}
            //else if (team2 == true)
            //{
            //    Player player = objplayer2_1.GetComponent<Player>();
            //    Player player2 = objplayer2_2.GetComponent<Player>();
            //    if (player.playertype1 == true)
            //    {
            //        objplayer2_1.transform.position = vec3;
            //    }
            //    else if(player2.playertype2 == true)
            //    {
            //        objplayer2_2.transform.position = vec3;
            //    }
            //}
            #endregion
            if(team1 == true)
            {
                Player player = objplayer1_1.GetComponent<Player>();
                Player player2 = objplayer1_2.GetComponent<Player>();
                if (objplayer1_1 == true && player.playertypenumber == true)
                {
                    objplayer1_1.transform.position = vec3;
                }
                else if (objplayer1_2 == true && player2.playertypenumber == true)
                {
                    objplayer1_2.transform.position = vec3;
                }
            }
            else if(team2 == true)
            {
                Player player = objplayer2_1.GetComponent<Player>();
                Player player2 = objplayer2_2.GetComponent<Player>();
                if (objplayer2_1 == true && player.playertypenumber == true)
                {
                    objplayer2_1.transform.position = vec3;
                }
                else if (objplayer2_2 == true && player2.playertypenumber == true)
                {
                    objplayer2_2.transform.position = vec3;
                }
            }
        }


    }

    private void positioncheck()
    {
        vec3 = transform.position;
    }

    private void changeteamplayer()//���� ������ �ٸ������� �ɶ� ��� �����Ѱ� ���� ����
    {
        GameObject obj = GameObject.Find("Playtimemanager");
        Playtimer playtimer = obj.GetComponent<Playtimer>();
        team1 = playtimer.teamred;
        team2 = playtimer.teamblue;
    }
}
