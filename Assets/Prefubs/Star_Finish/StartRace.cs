using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    [SerializeField] private int level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<TimerController>().startTimer();
            GAManager.instance.onLevelStart(level);
        }
    }
}
