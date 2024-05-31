using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasemouse : MonoBehaviour
{

    void Update()
    {
        mousecheck();
    }

    private void mousecheck()//마우스를 쫓아가게 만드는 코드
    {
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = 0;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseposition);
        worldPos.z = 0;

        transform.position = worldPos;
    }
}
