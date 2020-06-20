using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public float duration = .4f;
    public bool fadeOnStart = true;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
            Debug.LogError("canvas group not found");

        if (fadeOnStart)
            StartCoroutine(DoFade());
    }

    public void Update()
    {
        if (canvasGroup.alpha == 0)
            Destroy(gameObject);
    }

    public void ClickToFade()
    {
        StartCoroutine(DoFade());
    }

    public IEnumerator DoFade()
    {
        float counter = 0f;
        float start = canvasGroup.alpha;
        float end = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }
    }
}
