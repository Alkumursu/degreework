using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.black;
        img.DOFade(0f, 1f);
    }
}
