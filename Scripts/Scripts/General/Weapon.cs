using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum WeaponState {SearchTarget, RangeAttack, MeleeAttack, OnBuff,BulletAttack }
public enum WeaponType { Range, Melee, Buff,Bullet }
public enum Skills { None, Slow, Rage, Summon, Gold }


public class Weapon : MonoBehaviour
{
    [Header("Bow")]
    [SerializeField]
    private GameObject projectilePrefab;

    [Header("Common")]
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private WeaponType weaponType;

    public Skills skills;
    private Slow slowEffect;
    private SlowAreaController slowAreaController;

    [SerializeField]
    private AudioClip attackSound;
    private AudioSource audioSource;

    private Upgrade upgrade;
    private Animator animator;
    private ObjectData objectData;
    private Transform attackTarget;
    private EnemySpawner enemySpawner;
    private WeaponState weaponState = WeaponState.SearchTarget;
    private Quaternion rotation;
    private bool isAttack = false;
    private Movement movement;
    private float addedDamage;


    private float meleeUpgradeDamage;
    private float rangeUpgradeDamage;
    private float commonUpgradeDamage;
    private float uncommonUpgradeDamage;
    private float rareUpgradeDamage;
    private float rankDamage;
    private float rareBuff = 0.5f;
    private float uncommonBuff = 0.2f;
    public float buffDamage;

    private bool canAttack = true;
    private float lastAttackTime = 0;
    private float attackCooldown;
    private bool isAttacking = false;
    public bool IsAttack => isAttack;
    public float AddedDamage
    {
        set => addedDamage = Mathf.Max(0, value);
        get => addedDamage;
    }

    private Rage rage;
    public bool SkillActive { get; set; }
    private Gold gold;


    private void Awake()
    {
        SkillActive = false;
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        objectData = GetComponent<ObjectData>();
        GameObject upgradeObject = GameObject.Find("UpgradeSystem");
        upgrade = upgradeObject.GetComponent<Upgrade>();
        if(objectData.Rank == "Rare")
        {
            buffDamage = rareBuff;
        }
        else if (objectData.Rank == "Uncommon")
        {
            buffDamage = uncommonBuff;
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = attackSound;
        slowEffect = GetComponent<Slow>();
        slowAreaController = GetComponentInChildren<SlowAreaController>(true);
        rage = GetComponent<Rage>();
        gold = GetComponent<Gold>();
    }


    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }
    private void Update()
    {
        if(attackTarget != null&& !movement.IsWalk)
        {
            FaceToTarget();
        }
        meleeUpgradeDamage = upgrade.meleeUpgrade;
        rangeUpgradeDamage = upgrade.rangeUpgrade;
        commonUpgradeDamage = upgrade.commonUpgrade;
        uncommonUpgradeDamage = upgrade.uncommonUpgrade;
        rareUpgradeDamage = upgrade.rareUpgrade;
        //Debug.Log(attackTarget.ToString());
        if(weaponType == WeaponType.Buff)
        {
            ChangeState(WeaponState.OnBuff);
        }

    }

    private void FaceToTarget()
    {
        if(transform.position.x-attackTarget.position.x >0)
        {
            Vector2 characterScale =  transform.localScale;
            characterScale.x = -Mathf.Abs(characterScale.x);
            transform.localScale = characterScale;
        }
        else if(transform.position.x-attackTarget.position.x < 0)
        {
            Vector2 characterScale =  transform.localScale;
            characterScale.x = Mathf.Abs(characterScale.x);
            transform.localScale = characterScale;
        }
    }

    private IEnumerator SearchTarget()
    {
        while(true)
        {

            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null&& !movement.IsWalk)
            {
                switch (weaponType)
                {
                    case WeaponType.Melee:
                        ChangeState(WeaponState.MeleeAttack);
                        break;
                    case WeaponType.Range:
                        ChangeState(WeaponState.RangeAttack);
                        break;
                    case WeaponType.Bullet:
                        ChangeState(WeaponState.BulletAttack);
                        break;
                    case WeaponType.Buff:
                        ChangeState(WeaponState.OnBuff);
                        break;

                }


            }

            yield return null;

        }



    }

    private Transform FindClosestAttackTarget()
    {

            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector2.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

                if (distance <= objectData.Range && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;

                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            return attackTarget;

    }

    private bool IsPossibleAttackTarget()
    {
        if(attackTarget == null)
        {
            return false;
        }

        float distance = Vector2.Distance(attackTarget.transform.position, transform.position);
        if(distance > objectData.Range)
        {
            attackTarget = null;
            return false;
        }

        return true;
    }
    void PlayAudio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
        private IEnumerator RangeAttack()
    {
        while(true)
        {
            if(IsPossibleAttackTarget() == false )
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            /*if(movement.IsWalk)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }*/
            isAttack = true;

            switch (objectData.Rank)
            {
                case "Common":
                    rankDamage = commonUpgradeDamage;
                    break;
                case "Uncommon":
                    rankDamage = uncommonUpgradeDamage;
                    break;
                case "Rare":
                    rankDamage = rareUpgradeDamage;
                    break;
            }
            animator.SetBool("IsAttack", IsAttack);
            PlayAudio();
            animator.Play("Attack");
            yield return new WaitForSeconds(0.5f);
            SpawnProjectile(rankDamage);
            isAttack = false;
            animator.SetBool("IsAttack", IsAttack);
            yield return new WaitForSeconds(objectData.Rate - 0.5f);


        }
    }

    private IEnumerator MeleeAttack()
    {
        while (true)
        {
            if (IsPossibleAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            isAttack = true;
            switch (objectData.Rank)
            {
                case "Common":
                    rankDamage = commonUpgradeDamage;
                    break;
                case "Uncommon":
                    rankDamage = uncommonUpgradeDamage;
                    break;
                case "Rare":
                    rankDamage = rareUpgradeDamage;
                    break;
            }
            animator.SetBool("IsAttack", IsAttack);
            animator.Play("Attack", -1, 0f);
            PlayAudio();
            float damage = objectData.Damage + addedDamage + meleeUpgradeDamage + rankDamage;
            attackTarget.GetComponent<EnemyHP>().TakeDamage(damage);
            SkillCheck();

            yield return new WaitForSeconds(0.5f);
            if (slowAreaController != null)
            {
                slowAreaController.gameObject.SetActive(false);
            }
            isAttack = false;
            animator.SetBool("IsAttack", IsAttack);
            yield return new WaitForSeconds(objectData.Rate - 0.5f);

        }
    }
        private IEnumerator BulletAttack()
    {
        while (true)
        {
            if (IsPossibleAttackTarget() == false )
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            /*if (movement.IsWalk)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }*/
            isAttack = true;
            switch (objectData.Rank)
            {
                case "Common":
                    rankDamage = commonUpgradeDamage;
                    break;
                case "Uncommon":
                    rankDamage = uncommonUpgradeDamage;
                    break;
                case "Rare":
                    rankDamage = rareUpgradeDamage;
                    break;
            }
            animator.SetBool("IsAttack", IsAttack);
            animator.Play("Attack", -1, 0f);
            animator.Play("Attack");
            PlayAudio();
            yield return new WaitForSeconds(0.2f);
            transform.Find("Flash").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);

            transform.Find("Flash").gameObject.SetActive(false);
            float damage = objectData.Damage + addedDamage+rangeUpgradeDamage+rankDamage;
            attackTarget.GetComponent<EnemyHP>().TakeDamage(damage);

            isAttack = false;
            animator.SetBool("IsAttack", IsAttack);
            yield return new WaitForSeconds(objectData.Rate - 0.5f);
        }
    }

    private void SpawnProjectile(float rankdamage)
    { 
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        float damage = objectData.Damage + AddedDamage+rangeUpgradeDamage+rankdamage;
        clone.GetComponent<Projectile>().Setup(attackTarget, damage);
    }


    private IEnumerator OnBuff()
    {

        while (true)
        {
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

            for (int i = 0; i < characters.Length; i++)
            {
                Weapon weapon = characters[i].GetComponent<Weapon>();
                if (weapon != null)
                {
                    float distance = Vector2.Distance(weapon.transform.position, transform.position);
                    if (distance <= objectData.Range)
                    {
                        weapon.AddedDamage = (weapon.objectData.Damage * buffDamage);
                    }
                    else
                    {
                        weapon.AddedDamage = 0;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f); 
        }

    }
    private void SkillCheck()
    {
        switch(skills)
        {
            case Skills.None:
                break;
            case Skills.Slow:
                if (Random.value < slowEffect.slowProbability)
                {
                    if (slowAreaController != null)
                    {
                        // SlowArea를 활성화하여 효과를 발동
                        slowAreaController.gameObject.SetActive(true);
                    }

                }
                break;
            case Skills.Rage:
                if(!SkillActive &&Random.value <= rage.activeChance)
                {
                    SkillActive = true;
                }
                break;
            case Skills.Gold:
                if(!SkillActive && Random.value<=gold.activeChance)
                {
                    gold.goldEffect.SetActive(true);
                    SkillActive = true;
                }
                break;
        }
    }


}
