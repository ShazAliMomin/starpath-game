using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtonNew : MonoBehaviour
{

    public int cost, upgradeIndex;
    public string infoBlurb;
    public bool unlocked, available;
    public bool selected;

    public Image buttonBackground;
    public TMP_Text upgradeText;

    public UpgradeButtonNew[] nextInLine;

    public UpgradeControllerNew uc;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;

        if (upgradeIndex != -2)
        {
            if (available && !unlocked) buttonBackground.color = new Color(.005f, 0f, 0.89f, 1f);
            else if (!available) buttonBackground.color = new Color(.4f, .4f, .4f, 1f);
        }


        if(upgradeIndex >= 0 && upgradeIndex < 60) unlocked = AbilityManager.AbilityManagerInstance.getUpgradeUIStatus(upgradeIndex);
        if (unlocked)
        {
            if (nextInLine.Length > 0)
            {
                for (int i = 0; i < nextInLine.Length; i++)
                {
                    nextInLine[i].available = true;
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {


        if(upgradeIndex != -2)
        {
            if (uc.selectedIndex == upgradeIndex && !unlocked && available) buttonBackground.color = new Color(.94f, 1f, .2f, 1f);
            else if (uc.selectedIndex == upgradeIndex && !unlocked && !available) buttonBackground.color = new Color(.7f, .7f, .7f, 1f);
            else if (unlocked) buttonBackground.color = new Color(1f, .66f, .14f, 1f);
            else if (available && !unlocked) buttonBackground.color = new Color(.005f, 0f, 0.89f, 1f);
            else if (!available && !unlocked) buttonBackground.color = new Color(.4f, .4f, .4f, 1f);
        }



        else
        {
            if(uc.selectedCanUnlock == true)
            {
                buttonBackground.color = new Color(.85f, .85f, .85f, 1f);
                upgradeText.text = "Upgrade";
            }
            else
            {
                //buttonBackground.color = new Color(.3f, .3f, .3f, 1f);
                upgradeText.text = "Unavailable";
            }
        }

    }

    public void updateStatus()
    {
        if (upgradeIndex >= 0 && upgradeIndex < 60) unlocked = AbilityManager.AbilityManagerInstance.getUpgradeUIStatus(upgradeIndex);
        if (unlocked)
        {
            if (nextInLine.Length > 0)
            {
                for (int i = 0; i < nextInLine.Length; i++)
                {
                    nextInLine[i].available = true;
                }
            }
        }
    }

    public void upgradeButtonClicked()
    {
        if(upgradeIndex == -2)
        {
            uc.UpgradeClicked();
        }
    }

    public void highlighted()
    {
        if (upgradeIndex == -2) return;
        
        uc.selectedIndex = upgradeIndex;
        uc.selectedCost = cost;
        uc.selectedInfoBlurb = infoBlurb;

        if (available && !unlocked)
        {
            uc.selectedCanUnlock = true;
        }
        else uc.selectedCanUnlock = false;

       // Debug.Log("highlighted index : " + upgradeIndex);

    }

    public void UpgradeSelected(int index)
    {
        if(upgradeIndex == index && available)
        {
            if (unlocked == true) return;
            buttonBackground.color = new Color(1f, .66f, .14f, 1f);
            unlocked = true;
           
            if(nextInLine.Length > 0)
            {
                for(int i=0; i<nextInLine.Length; i++)
                {
                    nextInLine[i].available = true;
                }
            }
            AbilityManager.AbilityManagerInstance.updateUpgradeUI(upgradeIndex, unlocked);
            AbilityManager.AbilityManagerInstance.UnlockUpgradeFromMenu(upgradeIndex);
            AbilityManager.AbilityManagerInstance.SpendUpgradePoints(cost);
        }

        
    }

}
