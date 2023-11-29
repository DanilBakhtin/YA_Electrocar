using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusSoundController : MonoBehaviour
{
    public static FocusSoundController instance;

    private void Awake()
    {
        if (instance) Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(this);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        AudioListener.volume = silence ? 0 : 1;
    }
}
