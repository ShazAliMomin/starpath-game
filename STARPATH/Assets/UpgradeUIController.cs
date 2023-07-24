using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUIController : MonoBehaviour
{

    [SerializeField] private GameObject pistol, smg, shotgun, dash, aoe, turret, orbs;
    [SerializeField] private TMP_Text upgradePtsCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLevelText()
    {
        TMP_Text pistolLv, smgLv, shotgunLv, aoeLv, dashLv, turretLv, orbsLv;
        pistolLv = pistol.GetComponent<TMP_Text>();
        pistolLv.text = (AbilityManager.AbilityManagerInstance.GetAbility(1)).ToString();

        smgLv = smg.GetComponent<TMP_Text>();
        smgLv.text = (AbilityManager.AbilityManagerInstance.GetAbility(2)).ToString();

        shotgunLv = shotgun.GetComponent<TMP_Text>();
        shotgunLv.text = (AbilityManager.AbilityManagerInstance.GetAbility(3)).ToString();

        aoeLv = aoe.GetComponent<TMP_Text>();
        aoeLv.text = (AbilityManager.AbilityManagerInstance.GetAbility(4)).ToString();

        dashLv = dash.GetComponent<TMP_Text>();
        dashLv.text = (AbilityManager.AbilityManagerInstance.GetAbility(5)).ToString();

        turretLv = turret.GetComponent<TMP_Text>();
        turretLv.text = (AbilityManager.AbilityManagerInstance.GetAbility(6)).ToString();

        upgradePtsCounter.text = (AbilityManager.AbilityManagerInstance.GetUpgradePointsAvailable()).ToString();
    }
}
