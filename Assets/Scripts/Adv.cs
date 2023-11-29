using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Adv : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void ShowAdv();

    [DllImport("__Internal")]
    public static extern void ShowReward();

    // Показываем полноэкранную рекламу на старте
    private void Start(){
        ShowAdv();
    }

    // Fullscreen
    public void OnOpen(){
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public void OnClose(){
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }

    public void OnError(){
        OnClose();
    }

    public void OnOffline(){
        OnClose();
    }

    // Reward
    public void OnOpenReward(){
        AudioListener.volume = 0;
    }

    public void OnRewarded(){
        // Вознаграждаем
    }

    public void OnCloseReward(){
        AudioListener.volume = 1;
    }

    public void OnErrorReward(){
        OnCloseReward();
    }
}
