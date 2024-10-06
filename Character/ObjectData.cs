
using System;
using UnityEngine;

public class ObjectData : MonoBehaviour
{

    [SerializeField]
    private string objectName;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private string unitRank;
    [SerializeField]
    private string unitType;
    [SerializeField]
    private Sprite unitSprite;
    [SerializeField]
    private int unitNumber;
    [SerializeField]
    private string nextLevelA = "None";
    [SerializeField]
    private string nextLevelB = "None";


    public string Name => objectName;
    public float Damage => attackDamage;
    public float Rate 
    {
        get { return attackRate; }
        set { attackRate = value; }
    }
    public float Range => attackRange;
    public string Rank => unitRank;
    public string Type => unitType;
    public Sprite Sprite => unitSprite;    
    public string NextLevelA => nextLevelA;
    public string NextLevelB => nextLevelB;

}
