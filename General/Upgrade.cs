using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private PlayerStat stat;
    [SerializeField]
    private ObjectDataViewer viewer;
    [SerializeField]
    private TextManager textManager;

    public float meleeUpgrade = 0;
    public float rangeUpgrade = 0;
    public float commonUpgrade = 0;
    public float uncommonUpgrade = 0;
    public float rareUpgrade = 0;



    public void ActivePanel()
    {
        upgradePanel.SetActive(true);
    }
    public void InactivePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void MeleeUpgradeBtn()
    {
        if(stat.gold >= 10)
        {
            meleeUpgrade += 3;
            viewer.UpdateUnitData();
            stat.gold -= 10;
        }
        else
        {
            textManager.CantSummon();
        }
    }
    public void RangeUpgradeBtn()
    {
        if (stat.gold >= 10)
        {
            rangeUpgrade += 2;
            viewer.UpdateUnitData();
            stat.gold -= 10;
        }
        else
        {
            textManager.CantSummon();
        }
    }
    public void CommonUpgradeBtn()
    {
        if(stat.gold >= 15)
        { 
            commonUpgrade += 3;
            viewer.UpdateUnitData();
            stat.gold -= 15;
        }
        else
        {
            textManager.CantSummon();
        }
    }
    public void UncommonUpgradeBtn()
    {
        if(stat.gold >= 25)
        { 
            uncommonUpgrade += 5;
            viewer.UpdateUnitData();
            stat.gold -= 25;
        }
        else
        {
            textManager.CantSummon();
        }
    }
    public void RareUpgradeBtn()
    {
        rareUpgrade += 7;
        viewer.UpdateUnitData();
    }
}
