using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int summon= 4;
    public int gold = 0;

    public void FinshWave()
    {
        summon += 2;
    }

    public void GoldEarn(int storyGold)
    {
        gold += storyGold;    
    }
}
