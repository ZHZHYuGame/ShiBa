using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoThread : MonoSingleton<MonoThread>
{
    [SerializeField]
    bool m_runing;

    Queue<IEnumerator> m_queue = new Queue<IEnumerator>();
    public void Excute(IEnumerator en)
    {
        m_queue.Enqueue(en);

        if (!m_runing)
            StartCoroutine(CoroutineExcute());

    }


    IEnumerator CoroutineExcute()
    {
        m_runing = true;
        while (m_queue.Count > 0)
        {
            var coroutine = m_queue.Dequeue();
            yield return StartCoroutine(coroutine);
        }
        m_runing = false;
    }
}
