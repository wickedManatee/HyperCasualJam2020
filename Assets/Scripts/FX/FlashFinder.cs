using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFinder : MonoBehaviour
{
    public static CanvasGroup FlashCanvasGroup;
    public CanvasGroup m_CanvasGroup;

    protected virtual void Awake()
    {
        FlashCanvasGroup = m_CanvasGroup;
    }
}
