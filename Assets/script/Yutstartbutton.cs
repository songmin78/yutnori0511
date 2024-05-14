using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Yutstartbutton : MonoBehaviour
{
    [Header("�� ������ ��ư")]
    //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
    [SerializeField] Button startbutton;
    bool yutstart;
    bool randomcheck;//���� �� ������ Ȯ�� ���ִ� �ڵ�
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


    private void startyutnbutton()//�� ������ ��ư
    {
        //0�� �ո� 1�� �޸� �� ������ 0 0 0 1�� ������
        if(yutstart == true)
        {
            for(int yutstick = 0; yutstick < 4; yutstick++)//���� ������ ������ 3���� ���� ����Ʈ�� ���� �ϵ��� ����
            {
                test = Random.Range(0, 2);//Random.Range�� int�� ��� ������ ���� -1�� �Ͽ� ��� (0,2)�� ��� 0��  1�� �۵���
                yutdisposition.Add(test);
                //Debug.Log(test);
                //yutdisposition.Add(Random.Range(0, 2));//Random.Range�� int�� ��� ������ ���� -1�� �Ͽ� ��� (0,2)�� ��� 0��  1�� �۵���
                randomcheck = true;
            }
            //yutdisposition.Add(Random.Range(0, 2));
            yutstart = false;
        }
    }

    private void moveyut()//���� �� �� ��ŭ �̵��� �Ÿ��� ������
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
        //����Ʈ�� 1�� �󸶳� �ִ��� Ȯ���ϴ� �ڵ�
    }
}
