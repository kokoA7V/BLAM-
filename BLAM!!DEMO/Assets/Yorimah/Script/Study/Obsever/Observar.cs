using System;

public class Observer : IObserver<int>
{
    private string m_name;
    public Observer(string name)
    {
        m_name = name;
    }

    public void OnCompleted()
    {
        Console.WriteLine($"{m_name}���ʒm�̎󂯎����������܂���");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"{m_name}�����̃G���[����M���܂���:{error.Message}");
    }

    public void OnNext(int value)
    {
        Console.WriteLine($"{m_name}��{value}���󂯎��܂���");
    }
}