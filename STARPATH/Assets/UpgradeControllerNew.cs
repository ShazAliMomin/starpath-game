using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeControllerNew : MonoBehaviour
{
    public int selectedIndex;
    public int selectedCost;
    public string selectedInfoBlurb;
    public bool selectedCanUnlock;

    public Image upgradeButtonColor;

    public TMP_Text costText;
    public TMP_Text infoBlurbText;

    public TMP_Text upgradePointsAvailable;

    private int pointsAvailable;

    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = -1;
        selectedCost = -1;
        selectedInfoBlurb = "";
        selectedCanUnlock = false;

        AbilityManager.AbilityManagerInstance.setBaseUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedIndex >= 0)
        {
            costText.text = selectedCost.ToString();
            infoBlurbText.text = selectedInfoBlurb;
        }

        pointsAvailable = AbilityManager.AbilityManagerInstance.GetUpgradePointsAvailable();
        if (pointsAvailable < selectedCost && selectedCanUnlock == true) selectedCanUnlock = false;
        else if(selectedCanUnlock && pointsAvailable >= selectedCost) selectedCanUnlock = true;
        upgradePointsAvailable.text = pointsAvailable.ToString();

        //Debug.Log("can unlock: " + selectedCanUnlock);
    }

    public void UpgradeClicked()
    {
        if (selectedCanUnlock == false) return;
        BroadcastMessage("UpgradeSelected", selectedIndex);
    }

    public void allButtonsUpdate()
    {
        BroadcastMessage("updateStatus");
    }

}
