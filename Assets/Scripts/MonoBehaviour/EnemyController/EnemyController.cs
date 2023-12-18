using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class MeleeAttack
    {
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
    public class Audios
    {
        [Header("Audios")]
        public RandomAudio noticePlayer;
        public RandomAudio stab;
        public RandomAudio damage;
        public RandomAudio death;
    }

    [System.Serializable]
    public class AnimationTrigger
    {
        [Header("Animation Trigger's Name")]
        public string idle;
        public string walk;
        public string run;
        [Space(5)]

        public string melee;
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
        public float distanceToStop;
        public float distanceToAttack;
        public float distanceToFollow;

        [Header("Random Travel")]
        public float distanceRandomWalk;
        public float minTime;
        public float maxTime;
    }

    // -------------------------
    // Variables
    // -------------------------

    [Header("Enemy Controller")]
    public int health;
    public int exp;
    public int points;

    // Nav Mesh Agent
    GameObject player;
    PlayerInfos playerInfos;
    bool chasingPlayer;
    bool attackingPlayer;
    Vector3 destinationPos;

    [Header("Combat's Infos")]
    [SerializeField] MeleeAttack stabAttack;
    [Space(5)]

    [SerializeField] GameObject deathParticle;
    float stunTime;
    bool stopEverything = false;
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

    // Start Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        player = GameObject.Find("Player");
        playerInfos = player.GetComponent<PlayerController>().playerInfos;

        if(animator == null)
        animator = GetComponent<Animator>();

        // Invoke RandomTravel
        Invoke("RandomTravel", RandomFloat(navMeshSettings.minTime, navMeshSettings.maxTime));
    }

    // Update Functions
    // -------------------------

    private void FixedUpdate()
    {
        // Call Functions
        if(!stopEverything)
        CheckPlayerDistance();
    }

    // NavMesh Functions
    // -------------------------

    void CheckPlayerDistance()
    {
        // Check Player's distance
        float distanceBetweenPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(!attackingPlayer && distanceBetweenPlayer <= navMeshSettings.distanceToAttack)
        StartCoroutine("AttackPlayer");

        else if(!attackingPlayer && distanceBetweenPlayer <= navMeshSettings.distanceToFollow)
        ChasePlayer();

        else if(!chasingPlayer && Vector3.Distance(destinationPos, transform.position) <= navMeshSettings.distanceToStop)
        StopTravel();

        else if(chasingPlayer)
        StopTravel();
    }

    IEnumerator AttackPlayer()
    {
        // Set Variables
        attackingPlayer = true;

        // Call Functions
        StopTravel();

        // Play Animation & Audio
        audioSettings.stab.PlayRandomAudio();
        animator.SetTrigger(animationTrigger.melee);

        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPos);

        yield return new WaitForSeconds(stabAttack.attackStartup);

        // Set Hitbox
        BoxCollider hitbox = stabAttack.hitbox.GetComponent<BoxCollider>();
        stabAttack.hitbox.GetComponent<Hitbox>().attackInfos = stabAttack.attackInfos;

        hitbox.size = stabAttack.size;
        hitbox.center = stabAttack.center;
        stabAttack.hitbox.SetActive(true);

        yield return new WaitForSeconds(stabAttack.attackDuration);

        // Set Variables
        stabAttack.hitbox.SetActive(false);

        yield return new WaitForSeconds(stabAttack.attackCooldown);

        // Set Variables
        attackingPlayer = false;

        StopTravel();
    }

    void ChasePlayer()
    {
        // Play Animation & Audio
        if(!chasingPlayer && !attackingPlayer)
        {
            animator.SetTrigger(animationTrigger.run);
            audioSettings.noticePlayer.PlayRandomAudio();
        }

        navMeshAgent.isStopped = false;
        chasingPlayer = true;
        navMeshAgent.SetDestination(player.transform.position);
        destinationPos = player.transform.position;
    }

    void StopTravel()
    {
        if(!stopEverything && !navMeshAgent.isStopped)
        animator.SetTrigger(animationTrigger.idle);

        chasingPlayer = false;
        navMeshAgent.isStopped = true;
    }

    void RandomTravel()
    {
        if(!chasingPlayer)
        {
            if(navMeshAgent.isStopped)
            animator.SetTrigger(animationTrigger.walk);

            navMeshAgent.isStopped = false;
            navMeshAgent.speed = navMeshSettings.normalSpeed;
            navMeshAgent.SetDestination(GetRandomLocation());
            destinationPos = navMeshAgent.destination;
        }

        // Invoke RandomTravel
        Invoke("RandomTravel", RandomFloat(navMeshSettings.minTime, navMeshSettings.maxTime));
    }

    Vector3 GetRandomLocation()
    {
        // Set Variables
        Vector3 randomPosition = Vector3.zero;
        Vector3 centerOfRadius = transform.position;

        randomPosition = centerOfRadius + (Vector3)(Random.Range(navMeshSettings.distanceRandomWalk/3, navMeshSettings.distanceRandomWalk) * UnityEngine.Random.insideUnitCircle);
        randomPosition.z = randomPosition.y;
        randomPosition.y = centerOfRadius.y;

        Debug.Log(randomPosition);
        return randomPosition; 
    }

    // Combat Functions
    // -------------------------

    public void TakeDamage(AttackInfos attackInfos)
    {
        // Set Variables
        stopEverything = true;
        health -= playerInfos.RoundDamage(attackInfos.damage, playerInfos.strength, .15f);
        stunTime = attackInfos.stunTime;

        // Call Functions
        StopTravel();

        // Stop Current Attack
        StopCoroutine("AttackPlayer");

        attackingPlayer = false;
        stabAttack.hitbox.SetActive(false);

        if(!dead && health <= 0)
        StartCoroutine("Death");

        else if(!dead)
        StartCoroutine("LoseHealth");

    }

    IEnumerator Death()
    {
        dead = true;

        // Call Functions
        playerInfos.ExpGain(exp);
        playerInfos.player.MoneyUp(points);

        // Play Animation & Audio
        animator.SetTrigger(animationTrigger.die);
        audioSettings.death.PlayRandomAudio();

        float waitFor = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(waitFor);

        // Destroy and Instantiate Particles
        if(deathParticle != null)
        Instantiate(deathParticle, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    IEnumerator LoseHealth()
    {
        // Play Animation & Audio
        animator.SetTrigger(animationTrigger.damage);
        audioSettings.damage.PlayRandomAudio();

        yield return new WaitForSeconds(stunTime);

        // Set Variables
        stopEverything = false;
        animator.SetTrigger(animationTrigger.idle);
    }
}
