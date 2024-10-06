using System.Collections;
using UnityEngine;

public class Summon : MonoBehaviour
{
    [Header("SummonLv1")]
    [SerializeField]
    private GameObject[] prefabs;

    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private TextManager textManager;

    [SerializeField]
    private EnemySpawner enemySpawner;

    public float spawnRadius = 2f;
    [SerializeField]
    private ObjectDataViewer objectDataviwer;

    
    public void SummonUnit()
    {
        // prefabs 배열의 길이가 0이 아닌지 확인
    
        if (playerStat.summon > 0)
        {
            // 랜덤 인덱스 생성
            int randomIndex = Random.Range(0, prefabs.Length);

            // 소환 위치를 고정
            Vector3 spawnPosition = new Vector3(0, 0, 0); // 원하는 위치로 변경하십시오.

 
            // 유닛 소환
            GameObject character = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);
            int index = character.name.IndexOf("(Clone)");
            if(index>0)
            {
                character.name = character.name.Substring(0 , index);
            }
            playerStat.summon--;
            character.GetComponent<Weapon>().Setup(enemySpawner);


        }
        else
        {
            textManager.CantSummon();
        }

    }

}
