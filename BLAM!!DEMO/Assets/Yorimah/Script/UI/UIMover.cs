using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField, Header("LightPause")]
    private GameObject lightGameObject;

    private Vector3 enterVector3;
    void Start()
    {
        enterVector3 = lightGameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �|�[�Y��ʂɓ������ꍇ�ɋN������
    private void EnterPause()
    {

    }
}
