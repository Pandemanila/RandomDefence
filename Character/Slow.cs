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

        // ���� �ӵ� ����
        float originalSpeed = enemyMovement.MoveSpeed;
        // �̵� �ӵ� ����
        enemyMovement.MoveSpeed -= originalSpeed * slowAmount;

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(slowDuration);

        // �̵� �ӵ��� ������� ����
        enemyMovement.MoveSpeed = originalSpeed;

        slowedEnemies.Remove(enemyMovement);
    }
}

