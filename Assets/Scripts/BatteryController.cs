using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float Energyonsumption = 1f;
    private float currnetEnergy;
    public ParticleSystem electrocity;

    public Light headLight;
    private bool isLight;
    [SerializeField] private float costLight;

    [SerializeField] private float jumpForce;
    private bool isGround;

    [SerializeField] private Generator generator;

    private PrometeoCarController CarController;
    private Rigidbody rb;

    private bool isLossEnergy;

    private Trap trap;

    private HUD_Controller hud;

    void Start()
    {
        CarController = GetComponent<PrometeoCarController>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD_Controller>();
        hud.setPlayer(this, CarController.useTouchControls);

        rb = GetComponent<Rigidbody>();

        currnetEnergy = maxEnergy;
        actionOff();
        electrocity.Stop();

        LightHeadOff();

        isLossEnergy = false; 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        int absoluteCarSpeed = Mathf.RoundToInt(Mathf.Abs(CarController.carSpeed));
        
        if (absoluteCarSpeed > 0 && !CarController.deceleratingCar && !generator.isCharge)
        {
            addEnergy(Time.deltaTime * Energyonsumption * -1f);
        }

        if (absoluteCarSpeed > 0 && CarController.deceleratingCar && !generator.isCharge)
        {
            addEnergy(Time.deltaTime * Energyonsumption / 5f);
        }

        if (isLight)
        {
            addEnergy(Time.deltaTime * costLight * -1f);
        }

        if (currnetEnergy <= 0)
        {   
            LightHeadOff();
            rb.isKinematic = true;
            isLossEnergy = true;
            
        }

        if (isLossEnergy && !generator.isCharge && currnetEnergy > 0f)
        {
            rb.isKinematic = false;
            isLossEnergy = false;
        }

        hud.updateTextEnergy(Mathf.RoundToInt(currnetEnergy));

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            activateTrap();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isLight)
                LightHeadOff();
            else LightHeadOn();
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGround)
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            activateGenerator();
        }
    }
    private void actionOn(float cost)
    {
        hud.actionON(cost);
    }
    private void actionOff()
    {
        hud.actionOFF();
    }
    private void updateIconBattery()
    {   
        if (currnetEnergy >= 0)
            hud.updateIconBattery(currnetEnergy / maxEnergy);
    }
    public void addEnergy(float energyInput)
    {
        currnetEnergy += energyInput;

        if (currnetEnergy > maxEnergy)
            currnetEnergy = maxEnergy;

        if (currnetEnergy < 0)
            currnetEnergy = 0;

        updateIconBattery();
    }
    public void activateTrap()
    {
        if (trap != null)
        {
            if (currnetEnergy - trap.costEnergy >= 0)
            {   
                currnetEnergy -= trap.costEnergy;
                trap.activate();
                actionOff();
                StartCoroutine(electrocityCoroutine());
                trap = null;
            }
        }

    }
    public void setTrap(Trap input)
    {   
        if (input != null)
        {
            trap = input;
            actionOn(trap.costEnergy);
        }
        else
        {
            trap = null;
            actionOff();
        }
    }
    IEnumerator electrocityCoroutine()
    {
        electrocity.Play();

        yield return new WaitForSeconds(2);

        electrocity.Stop();
    }
    public void LightHeadOn()
    {
        headLight.enabled = true;
        isLight = true;
    }
    public void LightHeadOff()
    {
        headLight.enabled = false;
        isLight = false;
    }

    private void jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void activateGenerator()
    {

        if (!generator.isCharge)
        {
            rb.isKinematic = true;
            generator.startCharging(this);
        }
        else
        {
            rb.isKinematic = false;
            generator.stopCharging();
        }
    }


}
