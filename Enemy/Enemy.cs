using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private EnemyMovement enemyMovement;
    private EnemySpawner enemySpawner;


    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {

        enemyMovement = GetComponent<EnemyMovement>();
        
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while(true)
        {
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f *enemyMovement.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }  
    }
    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount -1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            if(direction.x == 1)
            {
                transform.rotation = Quaternion.Euler(0,180f,0);
            }
            else if (direction.x == 0)
            {
                if (direction.y == -1)
                {
                    transform.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            enemyMovement.MoveTo(direction);
        }
        else
        {   Vector3 direction = (wayPoints[0].position - transform.position).normalized;
            if (direction.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (direction.x == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            enemyMovement.MoveTo(direction);
            currentIndex = 0;
        }
    }

    public void OnDie()
    {
        enemySpawner.DestroyEnemy(this);
    }

}
