using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMover : MonoBehaviour
{
    [SerializeField, Header("LightPause")]
    private Image lightImage;

    private Vector3 enterVector3;
    void Start()
    {
        enterVector3 = lightImage.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        EnterPause();
    }

    // �|�[�Y��ʂɓ������ꍇ�ɋN������
    private void EnterPause()
    {
        lightImage.transform.position += new Vector3(1, 0, 0);
    }
}
