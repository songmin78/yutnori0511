using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yutstartbutton : MonoBehaviour
{
    [Header("윷 던지기 버튼")]
    //0은 앞면 1은 뒷면 즉 빽도는 0 0 0 1이 떠야함
    [SerializeField] Button startbutton;
    bool yutstart;
    bool randomcheck;//윷이 뭘 떴는지 확인 해주는 코드
    int randomcount;
    int Stickcount;
    public bool waittime;//캐릭터를 움직이기위한 시간에 버튼을 눌러도 작동 안되게 설정


    [SerializeField]List<int> yutdisposition = new List<int>();
    private void Awake()
    {
        startbutton.onClick.AddListener(() =>
        {
            if (waittime == true)
            {
                return;
            }
            yutdisposition.Clear();
            Stickcount = 0;
            yutstart = true;
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startyutnbutton();
        //moveyut();
    }


    private void startyutnbutton()//윷 던지기 버튼
    {
        //0은 앞면 1은 뒷면 즉 빽도는 0 0 0 1이 떠야함
        if(yutstart == true)
        {
            #region
            //for(int yutstick = 0; yutstick < 4; yutstick++)//윷을 빽도를 제외한 3개를 던져 리스트에 나열 하도록 설정
            //{
            //    test = Random.Range(0, 2);//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
            //    yutdisposition.Add(test);
            //    //Debug.Log(test);
            //    //yutdisposition.Add(Random.Range(0, 2));//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
            //    randomcheck = true;
            //}
            //yutdisposition.Add(Random.Range(0, 2));
            #endregion

            for (int yutstick = 0; yutstick < 4; yutstick++)//윷을 빽도를 제외한 3개를 던져 리스트에 나열 하도록 설정
            {
                randomcount = Random.Range(0, 2);//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
                if(randomcount == 1)//뒷면에 걸릴 경우
                {
                    if(yutstick == 3 && Stickcount == 0)//4번째 윷이면서 이전에 다 앞면이 뜰경우
                    {
                        Stickcount += 0;
                    }
                    else
                    {
                        Stickcount += randomcount;
                    }
                }
                if(yutstick == 3 && Stickcount == 0 &&randomcount == 1)//마지막 윷에서 전부 앞면 이면서 마지막 윷만 뒷면일 경우
                {
                    randomcheck = true;
                    Debug.Log("빽도");
                }
                yutdisposition.Add(randomcount);
            }
            //Debug.Log(Stickcount);
            yutlist();
            yutstart = false;
            GameObject findtimer = GameObject.Find("Playtimemanager");
            Playtimer playtimer = findtimer.GetComponent<Playtimer>();
            playtimer.checktime = true;
        }
    }

    #region
    //private void moveyut()//윷이 뜬 수 만큼 이동할 거리를 정해줌
    //{
    //    if(randomcheck == true)
    //    {
    //        for(int yutcheck = 0; yutcheck < 4; yutcheck++)
    //        {
    //            //int a = yutdisposition.Find(test => test >= 0);
    //            int a = yutdisposition.Contains();
    //            if(a == 1)
    //            {
    //                Debug.Log("1");
    //            }
    //            else
    //            {
    //                Debug.Log("0");
    //            }
    //        }
    //        randomcheck = false;
    //    }
    //    //리스트에 1이 얼마나 있는지 확인하는 코드
    //}
    #endregion

    private void yutlist()//윷에 뭐가 떴는지 확인 해주는 코드
    {
        if (randomcheck == true)//만약에 빽도가 될시
        {
            randomcheck = false;
            return;
        }
        //Stickcount가 0일경우 모, 1일 경우 도 또는 빽도, 2일 경우 개, 3일경우 걸, 4일경우 윷
        switch (Stickcount)//뒷면이  얼마나 떴는지 swich문으로 체크
        {
            case 0://뒷면이 0개일 경우
                Debug.Log("모");
                break;
            case 1:
                Debug.Log("도");
                break;
            case 2:
                Debug.Log("개");
                break;
            case 3:
                Debug.Log("걸");
                break;
            case 4:
                Debug.Log("윷");
                break;
        }
    }
}
