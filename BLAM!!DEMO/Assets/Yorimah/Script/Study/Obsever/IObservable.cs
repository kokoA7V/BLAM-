using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable<out T>
{
    //�f�[�^�̔��s���w�ǂ���
    IDisposable Subscribe(IObserver<T> observer);
}
