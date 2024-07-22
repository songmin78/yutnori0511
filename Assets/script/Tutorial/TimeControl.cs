using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TimeStop();
    }

    private void TimeStop()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 1;
        }
    }

}
