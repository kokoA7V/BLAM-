using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver<in T>
{
    //�f�[�^�̔��s�������������Ƃ�ʒm����
    void OnCompleted();
    //�f�[�^�̔��s���ŃG���[�������������Ƃ�ʒm����
    void OnError(Exception error);
    //�f�[�^��ʒm����
    void OnNext(T value);
}
