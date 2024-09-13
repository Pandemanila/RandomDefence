using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPref : MonoBehaviour
{
    private PlayerStat playerStat;
    public int gold;
    private EnemyHP enemyHp;


    public void Start()
    {
        enemyHp = GetComponent<EnemyHP>();
        playerStat = FindObjectOfType<PlayerStat>();

    }

    public void Update()
    {
        if (enemyHp.CurrentHP <= 0)
        {
            playerStat.GoldEarn(gold);
            Debug.Log("Kill");
        }
    }
}
