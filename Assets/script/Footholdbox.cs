using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footholdbox : MonoBehaviour
{
    [SerializeField] public List<float> Yutfoothold = new List<float>();//������ ��ġ�� �޴� �κ�
    float yutButton;
    public Vector3 yutnumber;
    [SerializeField] GameObject poscheck;

    // Start is called before the first frame update
    void Start()
    {
        //Maxyutfoot();
        //Debug.Log(yutnumber);
    }

    // Update is called once per frame
    void Update()
    {
        Maxyutfoot();
    }

    private void Maxyutfoot()//������ �ִ� ������ ����Ʈ�� �˸���
    {
        //for (int foothold = 0; foothold < 19; foothold++)
        //{
        //    yutButton += 1;
        //    Yutfoothold.Add(yutButton);
        //}

        //Yutfoothold.Add()
        if(Input.GetKeyDown(KeyCode.P))
        {
            //float item = Gamemanager.Instance.Numberroom.MaxyutButton;
            //int yy = Yutfoothold.FindIndex(a => a == item);


            //Vector3 pp = 
            //poscheck.transform.position = transform.position;
        }
    }

    public void passyut(float _yutButton)
    {
        Yutfoothold.Add(_yutButton);
        Yutfoothold.Sort();
    }
}
