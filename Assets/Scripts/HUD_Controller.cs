using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    private bool touchController;
    [SerializeField] private GameObject touchPanel;

    [SerializeField] private Image batteryIcon;
    [SerializeField] private Text energyText;

    [SerializeField] private GameObject actionIcon;
    [SerializeField] private GameObject costEnergyText;

    [SerializeField] private Sprite sprite_flash_on;
    [SerializeField] private Sprite sprite_flash_off;

    [SerializeField] private Button flash_btn;
    [SerializeField] private Button action_btn;

    private BatteryController player;
    private bool isFlash;

    private void Start()
    {   
        flash_btn.onClick.AddListener(flash);
        action_btn.onClick.AddListener(activateTrap);
    }
    public void setPlayer(BatteryController input, bool useTouch)
    {
        player = input;
        touchController = useTouch;

        if (!touchController)
            touchPanel.SetActive(false);
    }
    public void actionON(float cost)
    {
        actionIcon.SetActive(true);
        costEnergyText.SetActive(true);
        costEnergyText.GetComponent<Text>().text = cost.ToString();
    }
    public void actionOFF()
    {
        actionIcon.SetActive(false);
        costEnergyText.SetActive(false);
    }
    public void flash()
    {
        if (!player) return;

        if (isFlash)
        {
            flash_btn.GetComponent<Image>().sprite = sprite_flash_on;
            player.LightHeadOff();
        }
        else
        {
            flash_btn.GetComponent<Image>().sprite = sprite_flash_off;
            player.LightHeadOn();
        }

        isFlash = !isFlash;
        
    }
    public void activateTrap()
    {
        player.activateTrap();
    }

    public void updateIconBattery(float percent)
    {
        batteryIcon.fillAmount = percent;
    }

    public void updateTextEnergy(int input)
    {
        energyText.text = input.ToString();
    }
}

