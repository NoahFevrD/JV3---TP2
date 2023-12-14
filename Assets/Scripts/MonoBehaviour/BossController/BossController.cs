using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class BossController : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class MeleeAttack
    {
        public float lookAtPlayerFor;
        public float timeBeforeAttack;
        public float attackStartup;
        public float attackDuration;
        public float attackCooldown;

        [Header("Hitbox")]
        public GameObject hitbox;
        public AttackInfos attackInfos;
        [Space(5)]

        public Vector3 center;
        public Vector3 size;
    }

    [System.Serializable]
    public class DistantAttack
    {
        public float attackStartup;
        public float attackCooldown;

        [Header("Bullet")]
        public Transform spawnTransform;
        public GameObject bullet;
        public AttackInfos attackInfos;
        [Space(5)]

        public int minBullet;
        public int maxBullet;
    }

    [System.Serializable]
    public class Audios
    {
        [Header("Audios")]
        public RandomAudio idle;
        [Space(5)]

        public RandomAudio beforeAttack;
        public RandomAudio biteAttack;
        public RandomAudio tailAttack;
        public RandomAudio distantAttack;
        [Space(5)]

        public RandomAudio damage;
        public RandomAudio death;
    }

    [System.Serializable]
    public class AnimationTrigger
    {
        [Header("Animation Trigger's Name")]
        public string idle;
        public string idle2;
        public string walk;
        public string run;
        [Space(5)]

        public string biteAttack;
        public string tailAttack;
        public string spell;
        public string damage;
        public string die;
    }

    [System.Serializable]
    public class NavMeshSettings
    {
        [Header("Speed")]
        public float normalSpeed;
        public float chaseSpeed;

        [Header("Distance")]
        public float meleeAttackDistance;
        public float projectileAttackDistance;
    }

    // -------------------------
    // Variables
    // -------------------------

    [Header("Boss' Controller")]
    public int health;
    public int exp;
    public int points;
    [Space(10)]

    [SerializeField] GameObject deathParticle;
    public UnityEvent onDeath;

    // Nav Mesh Agent
    bool lookAtPlayer;
    GameObject player;
    PlayerInfos playerInfos;
    Vector3 destinationPos;
    bool attackingPlayer;

    [Header("Combat's Infos")]
    [SerializeField] Vector2 firstAttackCooldown;
    [SerializeField] Vector2 attackInterval;
    [Space(10)]

    [SerializeField] MeleeAttack biteAttack;
    [SerializeField] MeleeAttack tailAttack;
    [Space(5)]

    [SerializeField] DistantAttack fireballAttack;

    bool dead = false;

    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationTrigger animationTrigger;
    [Space(5)]

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] NavMeshSettings navMeshSettings;
    [Space(5)]

    [SerializeField] Audios audioSettings;

    // -------------------------
    // Functions
    // -------------------------

    float RandomFloat(float minFloat, float maxFloat)
    {
        float randomFloat = Random.Range(minFloat, maxFloat);
        return randomFloat;
    }

    int RandomInt(int minInt, int maxInt)
    {
        int randomInt = Random.Range(minInt, maxInt);
        return randomInt;
    }

    // Start Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        player = GameObject.Find("Player");
        playerInfos = player.GetComponent<PlayerController>().playerInfos;

        if(animator != null)
        animator = GetComponent<Animator>();

        // Call Functions
        Invoke("DecideAttack", RandomFloat(firstAttackCooldown.x, firstAttackCooldown.y));
        Invoke("Scream", RandomFloat(20, 50));
    }

    // Update Functions
    // -------------------------

    void FixedUpdate()
    {
        // Check Distance
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if(playerDistance <= navMeshAgent.stoppingDistance + 5)
        StopTravel();

        // Look At Player
        if(lookAtPlayer)
        {
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(playerPos);
        }
    }

    // Nav Mesh Functions
    // -------------------------

    void ComeCloser()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);

        animator.SetTrigger(animationTrigger.walk);
    }

    void StopTravel()
    {
        if(!navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = true;
            animator.SetTrigger(animationTrigger.idle);
        }
    }

    void Scream()
    {
        if(!dead && !attackingPlayer && navMeshAgent.isStopped)
        {
            // Set Variables
            lookAtPlayer = true;

            // Call Functions
            audioSettings.idle.PlayRandomAudio();
            animator.SetTrigger(animationTrigger.idle2);
            StartCoroutine(ResetAnimation(animationTrigger.idle));
        }

        Invoke("Scream", RandomFloat(20, 50));
    }

    // Combat Functions
    // -------------------------

    void DecideAttack()
    {
        // Play Audio
        audioSettings.beforeAttack.PlayRandomAudio();

        // Choose Attack
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if(playerDistance <= navMeshSettings.meleeAttackDistance)
        {
            int randomIndex = RandomInt(0, 2);
            if(randomIndex == 0)
            StartCoroutine("BiteAttack");

            else
            StartCoroutine("TailAttack");
        }

        else if(playerDistance <= navMeshSettings.projectileAttackDistance)
        StartCoroutine("ProjectileAttack");

        else
        ComeCloser();

        Invoke("DecideAttack", RandomFloat(attackInterval.x, attackInterval.y));
    }

    IEnumerator ResetAnimation(string trigger)
    {
        // Set Variables
        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(animationLength);

        animator.SetTrigger(trigger);
        lookAtPlayer = false;
    }

    IEnumerator BiteAttack()
    {
        // Set Variables
        lookAtPlayer = true;
        attackingPlayer = true;

        yield return new WaitForSeconds(biteAttack.lookAtPlayerFor);

        // Set Variables
        lookAtPlayer = false;

        yield return new WaitForSeconds(biteAttack.timeBeforeAttack - biteAttack.lookAtPlayerFor);

        // Play Animation & Audio
        audioSettings.biteAttack.PlayRandomAudio();
        animator.SetTrigger(animationTrigger.biteAttack);

        StopCoroutine("ResetAnimation");
        StartCoroutine(ResetAnimation(animationTrigger.idle));

        yield return new WaitForSeconds(biteAttack.attackStartup);

        // Set Hitbox
        BoxCollider hitbox = biteAttack.hitbox.GetComponent<BoxCollider>();
        biteAttack.hitbox.GetComponent<Hitbox>().attackInfos = biteAttack.attackInfos;

        hitbox.size = biteAttack.size;
        hitbox.center = biteAttack.center;
        biteAttack.hitbox.SetActive(true);

        yield return new WaitForSeconds(biteAttack.attackDuration);

        // Set Variables
        biteAttack.hitbox.SetActive(false);

        yield return new WaitForSeconds(biteAttack.attackCooldown);

        // Set Variables
        attackingPlayer = false;
    }

    IEnumerator TailAttack()
    {
        // Set Variables
        lookAtPlayer = true;
        attackingPlayer = true;

        yield return new WaitForSeconds(tailAttack.lookAtPlayerFor);

        // Set Variables
        lookAtPlayer = false;

        yield return new WaitForSeconds(tailAttack.timeBeforeAttack - tailAttack.lookAtPlayerFor);

        // Play Animation & Audio
        audioSettings.tailAttack.PlayRandomAudio();
        animator.SetTrigger(animationTrigger.tailAttack);

        StopCoroutine("ResetAnimation");
        StartCoroutine(ResetAnimation(animationTrigger.idle));

        yield return new WaitForSeconds(tailAttack.attackStartup);

        // Set Hitbox
        BoxCollider hitbox = tailAttack.hitbox.GetComponent<BoxCollider>();
        tailAttack.hitbox.GetComponent<Hitbox>().attackInfos = tailAttack.attackInfos;

        hitbox.size = tailAttack.size;
        hitbox.center = tailAttack.center;
        tailAttack.hitbox.SetActive(true);

        yield return new WaitForSeconds(tailAttack.attackDuration);

        // Set Variables
        tailAttack.hitbox.SetActive(false);

        yield return new WaitForSeconds(tailAttack.attackCooldown);

        // Set Variables
        attackingPlayer = false;
    }

    IEnumerator ProjectileAttack()
    {
        // Set Variables
        lookAtPlayer = true;
        attackingPlayer = true;
        int numberOfBullet = RandomInt(fireballAttack.minBullet, fireballAttack.maxBullet + 1);

        for(int i = 0;i < numberOfBullet;i++)
        {
            // Play Animation & Audio
            audioSettings.distantAttack.PlayRandomAudio();
            animator.SetTrigger(animationTrigger.spell);

            StopCoroutine("ResetAnimation");
            StartCoroutine(ResetAnimation(animationTrigger.idle));

            yield return new WaitForSeconds(fireballAttack.attackStartup);

            // Set Hitbox
            fireballAttack.spawnTransform.LookAt(player.transform.position);
            var bullet = Instantiate(fireballAttack.bullet, fireballAttack.spawnTransform.position, fireballAttack.spawnTransform.rotation);
            bullet.GetComponent<Hitbox>().attackInfos = fireballAttack.attackInfos;

            yield return new WaitForSeconds(fireballAttack.attackCooldown);
        }

        // Set Variables
        attackingPlayer = false;
        lookAtPlayer = false;
    }

    public void TakeDamage(AttackInfos attackInfos)
    {
        // Set Variables
        health -= playerInfos.RoundDamage(attackInfos.damage, playerInfos.strength, .15f);

        if(!dead && health <= 0)
        StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        dead = true;

        // Call Functions
        playerInfos.ExpGain(exp);
        playerInfos.player.MoneyUp(points);

        StopCoroutine("BiteAttack");
        StopCoroutine("TailAttack");
        StopCoroutine("ProjectileAttack");

        StopTravel();

        biteAttack.hitbox.SetActive(false);
        tailAttack.hitbox.SetActive(false);

        // Play Animation & Audio
        animator.SetTrigger(animationTrigger.die);
        audioSettings.death.PlayRandomAudio();

        float waitFor = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 2.5f;
        yield return new WaitForSeconds(waitFor);

        // Destroy and Instantiate Particles
        if(deathParticle != null)
        Instantiate(deathParticle, transform.position, transform.rotation);

        onDeath.Invoke();
        Destroy(gameObject);
    }
}