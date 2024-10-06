using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : MonoBehaviour
{
    [SerializeField]
    private Story[] story;
    private EnemyHP enemyHp;
    private int storyNumber = 0;
    private Vector3 position = new Vector3(0.5f, -14, 0);
    [SerializeField]
    private EnemySpawner enemySpawner;

    public void Awake()
    {

    }
    public void Start()
    {
        StartCoroutine(StorySetting());
    }

    private IEnumerator StorySetting()
    {
        while (storyNumber < story.Length)
        {
            GameObject clone = Instantiate(story[storyNumber].StoryPrefab, position, Quaternion.identity);
            Enemy enemy = clone.GetComponent<Enemy>();
            enemySpawner.enemyList.Add(enemy);
            enemyHp = clone.GetComponent<EnemyHP>();
            enemySpawner.SpawnEnemyHPSlider(clone);


            yield return new WaitUntil(() => enemyHp.CurrentHP<=0);

            storyNumber++;
        }
    }
}


[System.Serializable]
public struct Story
{
    public GameObject StoryPrefab;
}
