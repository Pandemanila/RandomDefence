using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slow : MonoBehaviour
{
    [SerializeField]
    private float slowAmount = 0.2f;
    public float slowProbability = 0.3f;
    [SerializeField] 
    private float slowDuration = 2.0f;

    private HashSet<EnemyMovement> slowedEnemies = new HashSet<EnemyMovement>();


    public void ApplySlowEffect(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        EnemyMovement enemyMovement = collision.GetComponent<EnemyMovement>();

        if (enemyMovement != null && !slowedEnemies.Contains(enemyMovement))
        {
            StartCoroutine(SlowDownEnemy(enemyMovement));
        }
    }

    private IEnumerator SlowDownEnemy(EnemyMovement enemyMovement)
    {
        slowedEnemies.Add(enemyMovement);

        // 원래 속도 저장
        float originalSpeed = enemyMovement.MoveSpeed;
        // 이동 속도 감소
        enemyMovement.MoveSpeed -= originalSpeed * slowAmount;

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(slowDuration);

        // 이동 속도를 원래대로 복구
        enemyMovement.MoveSpeed = originalSpeed;

        slowedEnemies.Remove(enemyMovement);
    }
}

