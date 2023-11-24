using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRace : MonoBehaviour
{
    [SerializeField] private int level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<TimerController>().stopTimer();
            GAManager.instance.onLevelComplete(level, other.gameObject.GetComponentInParent<TimerController>().getTime());
        }
    }
}
