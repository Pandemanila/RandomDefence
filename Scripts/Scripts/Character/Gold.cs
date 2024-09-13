using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public float activeChance = 0.1f;
    public float activeChance2 = 0.1f;
    public int gold = 50;
    private Weapon weapon;
    private PlayerStat stat;
    public GameObject goldEffect;
    public bool isGoldEarning = false;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        stat = GameObject.Find("PlayerStat").GetComponent<PlayerStat>();
    }

    private void Update()
    {
        if (weapon.SkillActive && weapon.skills == Skills.Gold)
        {
            if (!isGoldEarning)
            {
                if (Random.value <= activeChance2)
                {
                    stat.summon += 1;
                    weapon.SkillActive = false;
                }
                weapon.SkillActive = false;
                stat.GoldEarn(gold);
                StartCoroutine("Wait");
                goldEffect.SetActive(false);
            }

        }

    }
    public IEnumerator Wait()
    {
        isGoldEarning = true;
        yield return new WaitForSeconds(1.5f);
        isGoldEarning = false;
    }
}
