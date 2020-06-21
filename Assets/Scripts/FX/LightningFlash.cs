using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFlash : MonoBehaviour
{
    public float FadeAwayTime = 0.5f;
    protected CanvasGroup _cg;
    protected Coroutine _flashing;

    protected virtual void Awake()
    {
        _cg = FlashFinder.FlashCanvasGroup;
    }
    protected virtual void Start()
    {
        Flash();
    }

    public virtual void Flash()
    {
        _cg.alpha = 1f;
        if (_flashing != null)
            StopCoroutine(_flashing);
        _flashing = StartCoroutine(FadeAway());
    }

    protected IEnumerator FadeAway()
    {
        var stopTime = Time.time + FadeAwayTime;
        while(Time.time < stopTime)
        {
            yield return null;
            _cg.alpha = (stopTime - Time.time) / stopTime;
        }
        _cg.alpha = 0f;
    }
}
