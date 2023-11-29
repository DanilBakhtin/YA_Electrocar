using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using System.Globalization;

public class GAManager : MonoBehaviour
{
    public static GAManager instance;

    private void Awake()
    {
        if (instance) Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void onLevelComplete(int _level, float _time)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level " + _level, convertFloat(_time));
    }

    public void onLevelStart(int _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level " + _level);
    }

    public void onLevelFail(int _level, Vector3 pos)
    {
        string str_pos = "(" + convertFloat(pos.x) + "_" + convertFloat(pos.y) + "_" + convertFloat(pos.z) + ")";
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level " + _level,"Position " + str_pos);
    }

    private string convertFloat(float number)
    {
        return number.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
    }
}
