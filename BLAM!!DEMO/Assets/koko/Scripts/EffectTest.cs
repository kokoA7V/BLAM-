using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
    [SerializeField]
    GameObject effectObj;

    ParticleSystem particle;

    private void Start()
    {
        GameObject temp = Instantiate(effectObj);
        particle = temp.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            particle.Play();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            particle.Pause();
        }

    }


}
