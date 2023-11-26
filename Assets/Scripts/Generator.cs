using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private float energyGive;
    [SerializeField] private float timeEnergyGive;
    public bool isCharge = false;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private AudioSource generatorSound;

    private void Start()
    {
        smoke.Stop();
        generatorSound.Stop();
    }
    public void startCharging(BatteryController player)
    {
        StartCoroutine(GiveEnergyCoroutine(player));
        isCharge = true;
        smoke.Play();
        generatorSound.Play();
    }

    public void stopCharging()
    {
        isCharge = false;
        smoke.Stop();
        generatorSound.Stop();
    }
    IEnumerator GiveEnergyCoroutine(BatteryController player)
    {
        yield return new WaitForSeconds(timeEnergyGive);

        if (isCharge)
        {
            player.addEnergy(energyGive);
            StartCoroutine(GiveEnergyCoroutine(player));
        }
    }
}
