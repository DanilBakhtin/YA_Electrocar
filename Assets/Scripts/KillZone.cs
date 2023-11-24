using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class KillZone : MonoBehaviour
{
    [SerializeField] private UI_Controller controller;
    [SerializeField] private int level;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            GAManager.instance.onLevelFail(level, other.transform.position);
            controller.failTrace();
        }
    }
}
