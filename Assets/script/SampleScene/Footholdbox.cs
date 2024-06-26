using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Footholdbox : MonoBehaviour
{
    [SerializeField] public List<Transform> Yutfoothold;//발판의 위치를 받는 부분
    [SerializeField] bool testcheck;
    float yutButton;
    public Vector3 yutnumber;
    float myposition;//자신이 있는 위치를 체크
    public bool zerocheck;//판 밖에있는 말을 선택 했을경우
    [SerializeField] public GameObject poscheck1;//첫번째 윷의 이동 범위
    [SerializeField] public GameObject poscheck2;//2번째 윷의 이동 범위
    [SerializeField] public GameObject poscheck3;//3번째 윷의 이동 범위
    [SerializeField] public GameObject shortcutcheck1;//지름길에 들었을때만 보이는 범위
    [SerializeField] public GameObject shortcutcheck2;//지름길에 들었을때만 보이는 범위
    [SerializeField] public GameObject shortcutcheck3;//지름길에 들었을때만 보이는 범위

    [Header("윷 이동 부분")]//윷이 나온수 만큼 이동 할수있는 부분을 생성
    [SerializeField] float oneYut;
    [SerializeField] float twoYut;
    [SerializeField] float threeYut;

    [Header("들어와있는 블록 체크")]
    [SerializeField]bool redcheck;
    [SerializeField] bool bluecheck;

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

   public Transform findYut(Transform chagefoothold)
    {
        return Yutfoothold.Find(x => x.transform == chagefoothold);
    }
    
    public Transform MidFoothold(Transform chagefoothold4)
    {
        return Yutfoothold.Find(x => x.transform == chagefoothold4);
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
        //poscheck1.transform.position = new Vector3(poscheck1.transform.position.x, poscheck1.transform.position.y - 0.1f, poscheck1.transform.position.z);
        poscheck1.SetActive(true);
        if(_maxposition == 0 && _position2 == -1)
        {
            return;
        }
        poscheck2.transform.position = Yutfoothold[(int)_position2].transform.position;
        //poscheck2.transform.position = new Vector3(poscheck2.transform.position.x, poscheck2.transform.position.y - 0.1f, poscheck2.transform.position.z);
        poscheck2.SetActive(true);
        if (_maxposition == 0 && _position3 == -1)
        {
            return;
        }
        poscheck3.transform.position = Yutfoothold[(int)_position3].transform.position;
        //poscheck3.transform.position = new Vector3(poscheck3.transform.position.x, poscheck3.transform.position.y - 0.1f, poscheck3.transform.position.z);
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

    public void fastfindposition(float _fastposition1, float _fastposition2, float _fastposition3, float _fastmaxposition)
    {
        shortcutcheck1.transform.position = Yutfoothold[(int)_fastposition1].transform.position;
        shortcutcheck1.SetActive(true);
        if (_fastmaxposition == 0 && _fastposition2 == -1)
        {
            return;
        }
        shortcutcheck2.transform.position = Yutfoothold[(int)_fastposition2].transform.position;
        shortcutcheck2.SetActive(true);
        if (_fastmaxposition == 0 && _fastposition3 == -1)
        {
            return;
        }
        shortcutcheck3.transform.position = Yutfoothold[(int)_fastposition3].transform.position;
        shortcutcheck3.SetActive(true);
        if (_fastposition1 == _fastmaxposition + 15)
        {
            shortcutcheck1.transform.position = new Vector3(0, -10, 0);
        }
        if (_fastposition2 == _fastmaxposition + 15)
        {
            shortcutcheck2.transform.position = new Vector3(0, -10, 0);
        }
        if (_fastposition3 == _fastmaxposition + 15)
        {
            shortcutcheck3.transform.position = new Vector3(0, -10, 0);
        }
    }

    public void Centerfindposition(float _centerposition1, float _centerposition2, float _centerposition3, float _centermaxposition)
    {
        shortcutcheck1.transform.position = Yutfoothold[(int)_centerposition1].transform.position;
        shortcutcheck1.SetActive(true);
        if (_centermaxposition == 0 && _centerposition2 == -1)
        {
            return;
        }
        shortcutcheck2.transform.position = Yutfoothold[(int)_centerposition2].transform.position;
        shortcutcheck2.SetActive(true);
        if (_centermaxposition == 0 && _centerposition3 == -1)
        {
            return;
        }
        shortcutcheck3.transform.position = Yutfoothold[(int)_centerposition3].transform.position;
        shortcutcheck3.SetActive(true);
        if (_centerposition1 == _centermaxposition + 21)
        {
            shortcutcheck1.transform.position = new Vector3(0, -10, 0);
        }
        if (_centerposition2 == _centermaxposition + 21)
        {
            shortcutcheck2.transform.position = new Vector3(0, -10, 0);
        }
        if (_centerposition3 == _centermaxposition + 21)
        {
            shortcutcheck3.transform.position = new Vector3(0, -10, 0);
        }
    }

    public void lastfindposition(float _lastPosition1, float _lastPosition2, float _lastPosition3, float _lastMaxposition)
    {
        shortcutcheck1.transform.position = Yutfoothold[(int)_lastPosition1].transform.position;
        shortcutcheck1.SetActive(true);
        if (_lastMaxposition == 0 && _lastPosition2 == -1)
        {
            return;
        }
        shortcutcheck2.transform.position = Yutfoothold[(int)_lastPosition2].transform.position;
        shortcutcheck2.SetActive(true);
        if (_lastMaxposition == 0 && _lastPosition3 == -1)
        {
            return;
        }
        shortcutcheck3.transform.position = Yutfoothold[(int)_lastPosition3].transform.position;
        shortcutcheck3.SetActive(true);
        if (_lastPosition1 == _lastMaxposition + 3)
        {
            shortcutcheck1.transform.position = new Vector3(0, -10, 0);
        }
        if (_lastPosition2 == _lastMaxposition + 3)
        {
            shortcutcheck2.transform.position = new Vector3(0, -10, 0);
        }
        if (_lastPosition3 == _lastMaxposition + 3)
        {
            shortcutcheck3.transform.position = new Vector3(0, -10, 0);
        }
    }

    public void positiondestory()//같은 숫자가 걸릴 경우
    {
        poscheck1.SetActive(false);
        poscheck2.SetActive(false);
        poscheck3.SetActive(false);
        shortcutcheck1.SetActive(false);
        shortcutcheck2.SetActive(false);
        shortcutcheck3.SetActive(false);
    }

    //public void movecheckchange(float _oneYut, float _twoYut, float _threeYut)//이미 이동한 위치 표시를 밖으로 빼버리는 코드
    //{
    //    if(_oneYut == 0)
    //    {
    //        poscheck1.transform.position = new Vector3(0, -10, 0);
    //    }
    //    else if(_twoYut == 0)
    //    {
    //        poscheck2.transform.position = new Vector3(0, -10, 0);
    //    }
    //    else if(_threeYut == 0)
    //    {
    //        poscheck3.transform.position = new Vector3(0, -10, 0);
    //    }
    //}
    public void movedestory()//표식을 맵 밖으로 이동
    {
        poscheck1.transform.position = new Vector3(0, -10, 0);
        poscheck2.transform.position = new Vector3(0, -10, 0);
        poscheck3.transform.position = new Vector3(0, -10, 0);
        shortcutcheck1.transform.position = new Vector3(0, -10, 0);
        shortcutcheck2.transform.position = new Vector3(0, -10, 0);
        shortcutcheck3.transform.position = new Vector3(0, -10, 0);
    }

    //public void posplayercheck(float _MaxmoveYutcount)//위치 리스트에 위치 값을 넣는다
    //{
    //    Player player = GetComponent<Player>();
    //    //player.transform.position = Yutfoothold[(int)_MaxmoveYutcount].transform.position;
    //    if (Yutfoothold[(int)_MaxmoveYutcount] && Gamemanager.Instance.Player.teamblue == true)
    //    {
    //        bluecheck = true;
    //    }
    //    else if (Yutfoothold[(int)_MaxmoveYutcount] && Gamemanager.Instance.Player.teamred == true)
    //    {
    //        redcheck = true;
    //    }
    //}
}
