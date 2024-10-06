using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAreaController : MonoBehaviour
{
    [SerializeField] private Slow slowEffect;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && slowEffect != null)
        {
            animator.Play("Skill");
            slowEffect.ApplySlowEffect(collision);
        }
    }
}
