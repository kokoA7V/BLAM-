using System;
using System.Collections.Generic;
using UnityEngine;

class Unsubscriber : IDisposable
{
    //発行先リスト
    private List<IObserver<int>> m_observers;
    //DisposeされたときにRemoveするIObserver<int>
    private IObserver<int> m_observer;

    public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
    {
        m_observers = observers;
        m_observer = observer;
    }

    public void Dispose()
    {
        //Disposeされたら発行先リストから対象の発行先を削除する
        m_observers.Remove(m_observer);
    }
}
