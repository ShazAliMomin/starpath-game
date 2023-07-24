using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    [SerializeField] private TMP_Text upgradePts;
    [SerializeField] private int abilityIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeElement()
    {
        if (AbilityManager.AbilityManagerInstance.GetUpgradePointsAvailable() > 0 && AbilityManager.AbilityManagerInstance.GetAbility(abilityIndex) < 5)
        {
            int currentLevel = AbilityManager.AbilityManagerInstance.GetAbility(abilityIndex);
            currentLevel++;
            //if (currentLevel < 5) currentLevel++;
            AbilityManager.AbilityManagerInstance.SetAbility(abilityIndex, currentLevel);
            textField.text = currentLevel.ToString();
            upgradePts.text = (AbilityManager.AbilityManagerInstance.GetUpgradePointsAvailable() - 1).ToString();
            AbilityManager.AbilityManagerInstance.SpendUpgradePoints(1);
        }
        
    }
}
