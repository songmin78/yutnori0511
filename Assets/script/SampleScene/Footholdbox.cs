using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footholdbox : MonoBehaviour
{
    [SerializeField] List<Transform> Yutfoothold;//발판의 위치를 받는 부분
    [SerializeField] bool testcheck;
    float yutButton;
    public Vector3 yutnumber;
    float myposition;//자신이 있는 위치를 체크
    public bool zerocheck;//판 밖에있는 말을 선택 했을경우
    [SerializeField] public GameObject poscheck1;//첫번째 윷의 이동 범위
    [SerializeField] public GameObject poscheck2;//2번째 윷의 이동 범위
    [SerializeField] public GameObject poscheck3;//3번째 윷의 이동 범위

    [Header("윷 이동 부분")]//윷이 나온수 만큼 이동 할수있는 부분을 생성
    [SerializeField] float oneYut;
    [SerializeField] float twoYut;
    [SerializeField] float threeYut;

    //int testd;//윷이 나온수 만큼 이동 할수있는 부분을 생성

    private void Start()
    {
        Gamemanager.Instance.Footholdbox = this;
    }

    #region
    //private void movefoothold()//이동하는 부분의 위치를 확인
    //{
    //    #region
    //    //for (int foothold = 0; foothold < 19; foothold++)
    //    //{
    //    //    yutButton += 1;
    //    //    Yutfoothold.Add(yutButton);
    //    //}

    //    //Yutfoothold.Add()
    //    //if (Input.GetKeyDown(KeyCode.P))
    //    //{
    //    //    //float item = Gamemanager.Instance.Numberroom.MaxyutButton;
    //    //    //int yy = Yutfoothold.FindIndex(a => a == item);


    //    //    //Vector3 pp = 
    //    //    //poscheck.transform.position = transform.position;

    //    //    //Yutfoothold.
    //    //}
    //    #endregion
    //    if(zerocheck == true)
    //    {
    //        GameObject findyut = GameObject.Find("Yutstartbutton");//해당 이름의 오브젝트를 찾는다
    //        Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();


    //        oneYut = buttontimer.oneyut;
    //        twoYut = buttontimer.twoyut;
    //        threeYut = buttontimer.threeyut;
    //        if(oneYut == -1)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            poscheck1.SetActive(true);
    //            poscheck1.transform.position = Yutfoothold[(int)oneYut].transform.position;
    //        }
    //        if (twoYut == 0)
    //        {
    //            poscheck2.SetActive(false);
    //            return;
    //        }
    //        else
    //        {
    //            poscheck2.SetActive(true);
    //            poscheck2.transform.position = Yutfoothold[(int)twoYut].transform.position;
    //        }
    //        if(threeYut == 0)
    //        {
    //            poscheck3.SetActive(false);
    //            return;
    //        }
    //        else
    //        {
    //            poscheck3.SetActive(true);
    //            poscheck3.transform.position = Yutfoothold[(int)threeYut].transform.position;
    //        }

    //        //zerocheck = false;
    //    }
    //    zerocheck = false;
    //}
    #endregion

    public void passyut(float _yutButton)
    {
        //Yutfoothold.Add(_yutButton);
        //Yutfoothold.Sort();
    }
    #region

    //public float GetAt(int index)
    //{
    //    return Yutfoothold[index];
    //}

    //private void findfoothold()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        //Debug.Log(Yutfoothold[testd]);
    //        //poscheck1.transform.position = Yutfoothold[testd].transform.position;
    //    }

    //    //    if (Input.GetKeyDown(KeyCode.M))
    //    //    {
    //    //        testd += 1;
    //    //        //yutnumber = new Vector3(Yutfoothold[testd]);
    //    //    }
    //}
    #endregion

    public void findposition(float _position1, float _position2, float _position3, float _maxposition)
    {
        poscheck1.transform.position = Yutfoothold[(int)_position1].transform.position;
        poscheck1.SetActive(true);
        poscheck2.transform.position = Yutfoothold[(int)_position2].transform.position;
        poscheck2.SetActive(true);
        poscheck3.transform.position = Yutfoothold[(int)_position3].transform.position;
        poscheck3.SetActive(true);

        //if(_position1 == _position2)
        //{
        //    poscheck2.SetActive(false);
        //}
        //else if(_position1 == _position3)
        //{
        //    poscheck3.SetActive(false);
        //}
        //else if(_position2 == _position3)
        //{
        //    poscheck3.SetActive(false);  
        //}

        //samenumber();
        if (_position1 == _maxposition)
        {
            //poscheck1.SetActive(false);
            poscheck1.transform.position = new Vector3(0, -10, 0);
        }
        if (_position2 == _maxposition)
        {
            //poscheck2.SetActive(false);
            poscheck2.transform.position = new Vector3(0, -10, 0);
        }
        if (_position3 == _maxposition)
        {
            //poscheck3.SetActive(false);
            poscheck3.transform.position = new Vector3(0, -10, 0);
        }
    }

    public void positiondestory()//같은 숫자가 걸릴 경우
    {
        poscheck1.SetActive(false);
        poscheck2.SetActive(false);
        poscheck3.SetActive(false);
    }

    public void movecheckchange(float _oneYut, float _twoYut, float _threeYut)//이미 이동한 위치 표시를 밖으로 빼버리는 코드
    {
        if(_oneYut == 0)
        {
            poscheck1.transform.position = new Vector3(0, -10, 0);
        }
        else if(_twoYut == 0)
        {
            poscheck2.transform.position = new Vector3(0, -10, 0);
        }
        else if(_threeYut == 0)
        {
            poscheck3.transform.position = new Vector3(0, -10, 0);
        }
    }
    public void movedestory()//표식을 맵 밖으로 이동
    {
        poscheck1.transform.position = new Vector3(0, -10, 0);
        poscheck2.transform.position = new Vector3(0, -10, 0);
        poscheck3.transform.position = new Vector3(0, -10, 0);
    }

}
