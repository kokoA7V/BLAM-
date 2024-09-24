using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaginataController : MonoBehaviour
{
    [SerializeField]
    GameObject frontKeyboard;
    [SerializeField]
    GameObject backKeyboard;

    Vector3 oldPos;
    Vector3 newPos;

    float spinSpd;

    void Start()
    {
        oldPos = transform.position;
        newPos = transform.position;

        spinSpd = 1.0f;
    }

    void Update()
    {
        SpinKeyboard();
        FollowKeyboard();
    }

    void SpinKeyboard()
    {
        frontKeyboard.transform.Rotate(new Vector3(0, spinSpd, 0));
        backKeyboard.transform.Rotate(new Vector3(0, spinSpd, 0));
    }
    void FollowKeyboard()
    {
        newPos = transform.position;


        oldPos = newPos;

    }
}
