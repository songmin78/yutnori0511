using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footholdbox : MonoBehaviour
{
    [SerializeField] List<Transform> Yutfoothold;//������ ��ġ�� �޴� �κ�
    [SerializeField] bool testcheck;
    float yutButton;
    public Vector3 yutnumber;
    float myposition;//�ڽ��� �ִ� ��ġ�� üũ
    public bool zerocheck;//�� �ۿ��ִ� ���� ���� �������
    [SerializeField] GameObject poscheck1;//ù��° ���� �̵� ����
    [SerializeField] GameObject poscheck2;//2��° ���� �̵� ����
    [SerializeField] GameObject poscheck3;//3��° ���� �̵� ����

    [Header("�� �̵� �κ�")]//���� ���¼� ��ŭ �̵� �Ҽ��ִ� �κ��� ����
    [SerializeField] float oneYut;
    [SerializeField] float twoYut;
    [SerializeField] float threeYut;

    //int testd;//���� ���¼� ��ŭ �̵� �Ҽ��ִ� �κ��� ����


    // Start is called before the first frame update
    void Start()
    {
        //Maxyutfoot();
        //Debug.Log(yutnumber);
    }

    // Update is called once per frame
    void Update()
    {
        movefoothold();
        findfoothold();
    }

    private void movefoothold()//�̵��ϴ� �κ��� ��ġ�� Ȯ��
    {
        #region
        //for (int foothold = 0; foothold < 19; foothold++)
        //{
        //    yutButton += 1;
        //    Yutfoothold.Add(yutButton);
        //}

        //Yutfoothold.Add()
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    //float item = Gamemanager.Instance.Numberroom.MaxyutButton;
        //    //int yy = Yutfoothold.FindIndex(a => a == item);


        //    //Vector3 pp = 
        //    //poscheck.transform.position = transform.position;

        //    //Yutfoothold.
        //}
        #endregion
        if(zerocheck == true)
        {
            GameObject findyut = GameObject.Find("Yutstartbutton");//�ش� �̸��� ������Ʈ�� ã�´�
            Yutstartbutton buttontimer = findyut.GetComponent<Yutstartbutton>();

            
            oneYut = buttontimer.oneyut;
            twoYut = buttontimer.twoyut;
            threeYut = buttontimer.threeyut;
            if(oneYut == -1)
            {
                return;
            }
            else
            {
                poscheck1.SetActive(true);
                poscheck1.transform.position = Yutfoothold[(int)oneYut].transform.position;
            }
            if (twoYut == 0)
            {
                poscheck2.SetActive(false);
                return;
            }
            else
            {
                poscheck2.SetActive(true);
                poscheck2.transform.position = Yutfoothold[(int)twoYut].transform.position;
            }
            if(threeYut == 0)
            {
                poscheck3.SetActive(false);
                return;
            }
            else
            {
                poscheck3.SetActive(true);
                poscheck3.transform.position = Yutfoothold[(int)threeYut].transform.position;
            }

            //zerocheck = false;
        }
        zerocheck = false;
    }

    public void passyut(float _yutButton)
    {
        //Yutfoothold.Add(_yutButton);
        //Yutfoothold.Sort();
    }

    //public float GetAt(int index)
    //{
    //    return Yutfoothold[index];
    //}

    private void findfoothold()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log(Yutfoothold[testd]);
            //poscheck1.transform.position = Yutfoothold[testd].transform.position;
        }

        //    if (Input.GetKeyDown(KeyCode.M))
        //    {
        //        testd += 1;
        //        //yutnumber = new Vector3(Yutfoothold[testd]);
        //    }
    }
}
