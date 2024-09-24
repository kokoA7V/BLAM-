using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable<out T>
{
    //データの発行を購読する
    IDisposable Subscribe(IObserver<T> observer);
}
