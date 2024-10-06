using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Timeline;
using System.Collections;
using System.Collections.Generic;


public class ObjectDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image unitImage;
    [SerializeField]
    private TextMeshProUGUI textName;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRank;
    [SerializeField]
    private TextMeshProUGUI textType;
    [SerializeField]
    private ObjectDetector objectDetector;
    [SerializeField]
    private TextMeshProUGUI textMixA;
    [SerializeField]
    private TextMeshProUGUI textMixB;
    [SerializeField]
    private GameObject toolTip;
    [SerializeField]
    private DataProducer dataProducer;
    [SerializeField]
    private TextMeshProUGUI textCombine;
    [SerializeField]
    private GameObject toolTip2;
    [SerializeField]
    private TextMeshProUGUI textCombine2;

    [SerializeField]
    private Button CombineA;
    [SerializeField]
    private Button CombineB;


    [SerializeField]
    private Upgrade upgrade;
    private float rankDamage;
    private float attackSpeed;

    private Weapon weapon;  
    private ObjectData unitData;

    private void Awake()
    {
        
        OffPanel();
        HideToolTip();
        HideToolTip2();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
            objectDetector.marker.SetActive(false);
        }
    }

    public void OnPanel(Transform unit)
    {
        unitData = unit.GetComponent<ObjectData>();
        weapon = unit.GetComponent<Weapon>();
        gameObject.SetActive(true);
        
        UpdateUnitData();

        
    }
    public void OffPanel()
    {
        HideToolTip();
        HideToolTip2();
        gameObject.SetActive(false);
        
    }
    public void UpdateUnitData()
    {
        textName.text = "Name : " + unitData.Name;
        textRank.text = "Rank : " + unitData.Rank;
        if(unitData.Type == "Melee")
        {
            switch(unitData.Rank)
            {
                case "Common":
                    rankDamage = upgrade.commonUpgrade;
                    break;
                case "Uncommon":
                    rankDamage = upgrade.uncommonUpgrade;
                    break;
                case "Rare":
                    rankDamage = upgrade.rareUpgrade;
                    break;
            }
            float finalDamage = unitData.Damage + upgrade.meleeUpgrade + rankDamage ;
            textDamage.text = "Damage : " + finalDamage + "+" + weapon.AddedDamage;
        }
        else if(unitData.Type == "Range" || unitData.Type == "Bullet")
        {
            switch (unitData.Rank)
            {
                case "Common":
                    rankDamage = upgrade.commonUpgrade;
                    break;
                case "Uncommon":
                    rankDamage = upgrade.uncommonUpgrade;
                    break;
                case "Rare":
                    rankDamage = upgrade.rareUpgrade;
                    break;
            }
            float finalDamage = unitData.Damage + upgrade.rangeUpgrade + rankDamage;
            textDamage.text = "Damage : " + finalDamage + "+" + weapon.AddedDamage;
        }
        else
        {
            textDamage.text = "Damage : " + unitData.Damage + "+" + weapon.AddedDamage;
        }
        if( unitData.Rate == 0)
        {
            attackSpeed = 0;
        }
        else 
        { 
            attackSpeed = Mathf.Round((1 / unitData.Rate) * 100f) / 100f; 
        }
        
        textRate.text = "Rate : " + attackSpeed;
        textType.text = "Type : " + unitData.Type;
        unitImage.sprite = unitData.Sprite;
        if(unitData.NextLevelA == "None")
        {
            CombineA.gameObject.SetActive(false);
            HideToolTip();
            HideToolTip2();
        }
        else
        {
            CombineA.gameObject.SetActive(true);
            textMixA.text = unitData.NextLevelA;
        }
        if(unitData.NextLevelB == "None")
        {
            CombineB.gameObject.SetActive(false);
            HideToolTip();
            HideToolTip2();
        }
        else
        {
            CombineB.gameObject.SetActive(true);
            textMixB.text = unitData.NextLevelB;
        }


    }

    public void ShowToolTip()
    {
        toolTip.SetActive(true);
        for(var i = 0; i < dataProducer.stringsA.Length; i++)
        {
            if(unitData.Name.Equals(dataProducer.stringsA[i]) )//&& unitData.NextLevelA !="None")
            {
                textCombine.text = (unitData.Name + " + " + dataProducer.stringsB[i]);
                break;
            }

        }
    }
    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    public void ShowToolTip2()
    {
        toolTip2.SetActive(true);
        for(var i = 0; i < dataProducer.stringsA.Length; i++)
        {
            if(unitData.Name.Equals(dataProducer.stringsD[i]) )//&& unitData.NextLevelB != "None")
            {
                textCombine2.text = (unitData.Name + " + " + dataProducer.stringsE[i]);
                break;
            }


        }
    }
    public void HideToolTip2()
    {
        toolTip2.SetActive(false);
    }
}
