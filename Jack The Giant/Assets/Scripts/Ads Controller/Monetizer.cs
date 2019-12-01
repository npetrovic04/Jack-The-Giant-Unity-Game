using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monetizer : MonoBehaviour
{

    public bool timedBanner;
    public float bannerDuration;


    void Start()
    {
        AdsControll.instance.ShowBanner();
    }

    void OnDisable()
    {
        if (!timedBanner)
            AdsControll.instance.HideBanner();
        else
            AdsControll.instance.HideBanner(bannerDuration);
    }
}
