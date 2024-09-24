using System;
using System.Collections.Generic;
using UnityEngine;

class Unsubscriber : IDisposable
{
    //���s�惊�X�g
    private List<IObserver<int>> m_observers;
    //Dispose���ꂽ�Ƃ���Remove����IObserver<int>
    private IObserver<int> m_observer;

    public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
    {
        m_observers = observers;
        m_observer = observer;
    }

    public void Dispose()
    {
        //Dispose���ꂽ�甭�s�惊�X�g����Ώۂ̔��s����폜����
        m_observers.Remove(m_observer);
    }
}
