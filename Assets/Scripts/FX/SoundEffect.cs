using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource m_AudioSource;
    public float StartTime;
    public float PlayTime;

    protected Coroutine _stop;

    protected virtual void Start()
    {
        Play();
    }

    public void Play()
    {
        m_AudioSource.time = StartTime;
        m_AudioSource.Play();
        if (_stop != null)
            StopCoroutine(_stop);

        _stop = StartCoroutine(StopIn(PlayTime));
    }

    protected IEnumerator StopIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_AudioSource.Stop();
        _stop = null;
    }
}
