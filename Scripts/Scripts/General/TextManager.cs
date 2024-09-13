using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textWave;
    [SerializeField]
    private TextMeshProUGUI textTime;
    [SerializeField]
    private TextMeshProUGUI textCount;
    [SerializeField]
    private WaveSystem waveSystem;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private TextMeshProUGUI textGold;
    [SerializeField]
    private TextMeshProUGUI textSummon;
    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private TextMeshProUGUI warningMessage;
    private float warningTime = 2f;
    private Color color;

    public void Awake()
    {
        color = warningMessage.color;
        color.a = 0;
        warningMessage.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        textWave.text = "Wave - " + waveSystem.CurrentWave;
        textGold.text = playerStat.gold.ToString();
        textSummon.text = playerStat.summon.ToString();
        textTime.text = waveSystem.timeText;
        if(enemySpawner.EnemyList.Count < 81)
        {
            textCount.text = "적 유닛 : " + (enemySpawner.EnemyList.Count-1).ToString();
        }
        else if(enemySpawner.EnemyList.Count >= 81)
        {
            textCount.text = "적 유닛 : 0";
        }
    }

    public void WarningCombine(string character)
    {
        color = warningMessage.color;
        color.a = 100;
        warningMessage.color = color;
        warningMessage.text = "부족합니다 " + "(" + character+")";
        StartCoroutine(FadeOut());
        
    }

    IEnumerator FadeOut()
    {
        float startAlpha = warningMessage.alpha;

        float targetAlpha = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < warningTime)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / warningTime);
            Color color = warningMessage.color;
            color.a = alpha;
            warningMessage.color = color;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    public void CantSummon()
    {
        color = warningMessage.color;
        color.a = 100;
        warningMessage.color = color;
        warningMessage.text = "재화가 부족합니다.";
        StartCoroutine(FadeOut());
    }
}
