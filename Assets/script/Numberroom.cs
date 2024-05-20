using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    [Header("윷 발판의 번호")]
    [SerializeField,Range(0,19)] float yutButton;

    [Header("지름길 번호 및 체크")]
    [SerializeField,Tooltip("지름길에 멈추었을때")] bool yutshortcut;
    [SerializeField] float shortcutButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
