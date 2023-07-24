using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHudElement : MonoBehaviour
{

    [SerializeField] private Sprite[] gunSprites;
    [SerializeField] private Sprite lockSprite;

    private PlayerController1 playerController;

    public Image image;

    public Image backgroundColor;

    [SerializeField] private Image altFireToggle;

    private int shootMode;
    private bool altFire;

    [SerializeField] private bool activeSlot;
    [SerializeField] private int slot;
    
    void Start()
    {
        shootMode = AbilityManager.AbilityManagerInstance.GetShootMode();
    }

    
    void Update()
    {
        shootMode = AbilityManager.AbilityManagerInstance.GetShootMode();

        if (activeSlot)
        {

            image.sprite = gunSprites[shootMode-1];
            if (AbilityManager.AbilityManagerInstance.GetAltFire(shootMode))
            {
                //Set altfire toggle to true;
                altFireToggle.color = new Color(1f, .55f, 0f, 1f);
            }
            else
            {
                //Set altfire toggle to false;
                altFireToggle.color = new Color(.8f, .8f, .8f, 1f);
            }
        }
        else
        {
            //Check if guns are unlocked first

            if (PlayerController1.freya)
            {
                backgroundColor.color = new Color(0.113f, 0.113f, 0.235f, 1f);
                switch (shootMode)
                {
                    case 1:
                        if (slot == 1) image.sprite = gunSprites[1];
                        else image.sprite = gunSprites[2];
                        break;
                    case 2:
                        if (slot == 1) image.sprite = gunSprites[0];
                        else image.sprite = gunSprites[2];
                        break;
                    case 3:
                        if (slot == 1) image.sprite = gunSprites[0];
                        else image.sprite = gunSprites[1];
                        break;
                    default:
                        break;
                }
            }
            else
            {
                backgroundColor.color = new Color(0.2f, 0.2f, 0.2f, 1f);
                image.sprite = lockSprite;
            }
            
        }
    }
}
