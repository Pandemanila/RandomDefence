using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType {Arrow, Skull,}
public class Projectile : MonoBehaviour
{

    private EnemyMovement movement;
    private Transform target;
    private float damage;
    [SerializeField]
    private ProjectileType type;

    public void Setup(Transform target, float damage)
    {
        movement = GetComponent<EnemyMovement>();
        this.target = target;
        this.damage = damage;
    }
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 135);
    }
    private void Update()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            if(target.position.x-transform.position.x >= 0)
            {   
                switch(type)
                {
                    case ProjectileType.Arrow:
                        transform.rotation = Quaternion.Euler(0, 0, -45);
                        break;
                    case ProjectileType.Skull:
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }

            }
            else
            {
                switch (type)
                {
                    case ProjectileType.Arrow:
                        transform.rotation = Quaternion.Euler(0, 0, 135);
                        break;
                    case ProjectileType.Skull:
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                }
            }
            movement.MoveTo(direction);

        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("Story")) return;
        if(collision.transform != target) return;
        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
