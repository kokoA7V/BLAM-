using UnityEngine;
using System;

public class MonoObservar : MonoBehaviour
{

    Observer observerA = new Observer("A‚³‚ñ");
    [SerializeField]
    AnimationPlayerObserver observerB;
    Observer observerC = new Observer("C‚³‚ñ");

    Observable observable = new Observable();


    // Start is called before the first frame update
    void Start()
    {
        IDisposable disposableA = observable.Subscribe(observerA);
        IDisposable disposableB = observable.Subscribe(observerB);
        IDisposable disposableC = observable.Subscribe(observerC);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            observable.SendNotice();
        }
    }
}
