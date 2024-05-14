using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Yutstartbutton : MonoBehaviour
{
    [Header("윷 던지기 버튼")]
    //0은 앞면 1은 뒷면 즉 빽도는 0 0 0 1이 떠야함
    [SerializeField] Button startbutton;
    bool yutstart;
    bool randomcheck;//윷이 뭘 떴는지 확인 해주는 코드
    int test;


    [SerializeField]List<int> yutdisposition = new List<int>();
    private void Awake()
    {
        startbutton.onClick.AddListener(() =>
        {
            yutdisposition.Clear();
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
        moveyut();
    }


    private void startyutnbutton()//윷 던지기 버튼
    {
        //0은 앞면 1은 뒷면 즉 빽도는 0 0 0 1이 떠야함
        if(yutstart == true)
        {
            for(int yutstick = 0; yutstick < 4; yutstick++)//윷을 빽도를 제외한 3개를 던져 리스트에 나열 하도록 설정
            {
                test = Random.Range(0, 2);//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
                yutdisposition.Add(test);
                //Debug.Log(test);
                //yutdisposition.Add(Random.Range(0, 2));//Random.Range는 int일 경우 마지막 숫자 -1를 하여 계산 (0,2)일 경우 0과  1만 작동함
                randomcheck = true;
            }
            //yutdisposition.Add(Random.Range(0, 2));
            yutstart = false;
        }
    }

    private void moveyut()//윷이 뜬 수 만큼 이동할 거리를 정해줌
    {
        if(randomcheck == true)
        {
            for(int yutcheck = 0; yutcheck < 4; yutcheck++)
            {
                int a = yutdisposition.Find(test => test >= 0);
                if(a == 1)
                {
                    Debug.Log("1");
                }
                else
                {
                    Debug.Log("0");
                }
            }
            randomcheck = false;
        }
        //리스트에 1이 얼마나 있는지 확인하는 코드
    }
}
