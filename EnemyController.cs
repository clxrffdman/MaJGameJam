using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public bool left;
    public Transform target;
    public SpriteRenderer spriteRenderer;
    public AILerp aiLerp;
    public Animator anim;
    public float deathAnimationDuration;
    public GameObject damageBullet;
    public LevelController levelControl;
    public GameObject soundSpawn;
    public AudioClip deathSound;

    [Header("Enemy Stats")]
    public int maxHealth;
    public int currentHealth;
    public float movementSpeed;
    public float speedVariation;
    public float stopDistance;
    public float attackWindupDuration;
    public float attackDuration;

    [Header("Enemy State")]
    public bool alive;
    public bool canAttack;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        canMove = true;
        canAttack = true;
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
        if(target == null)
        {
            target = GameObject.Find("Player").transform;
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (aiLerp == null)
        {
            aiLerp = GetComponent<AILerp>();
        }
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if(levelControl == null)
        {
            if(GameObject.Find("LevelController") != null)
            {
                levelControl = GameObject.Find("LevelController").transform.GetComponent<LevelController>();
            }
        }
        aiLerp.repathRate = Random.Range(0.3f, 1.2f);
        aiLerp.speed = movementSpeed + Random.Range(-speedVariation, speedVariation);

        currentHealth = maxHealth;

        SpawnFadeIn();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive && !levelControl.paused)
        {
            if (currentHealth <= 0)
            {
                Death();
            }


            if (target.position.x > transform.position.x)
            {
                left = false;
            }
            else
            {
                left = true;
            }

            if (left)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            if (Vector3.Distance(aiLerp.position, target.position) <= stopDistance)
            {
                if (canAttack)
                {
                    StartCoroutine(Attack());


                }
            }
        }

    }

    public IEnumerator Attack()
    {
        aiLerp.isStopped = true;
        canAttack = false;
        canMove = false;

        
        anim.Play("zombieAttack");

        //Insert attack here
        if(damageBullet != null)
        {
            var enemyBullet = Instantiate(damageBullet, transform.position, Quaternion.identity);
            if (left)
            {
                enemyBullet.GetComponent<CircleCollider2D>().offset = new Vector2(-enemyBullet.GetComponent<CircleCollider2D>().offset.x, enemyBullet.GetComponent<CircleCollider2D>().offset.y);
            }
        }
        

        yield return new WaitForSeconds(attackDuration);

        aiLerp.isStopped = false;
        canAttack = true;
        canMove = true;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

    }

    void StopMove()
    {
        alive = false;
        aiLerp.isStopped = true;
        canAttack = false;
        canMove = false;
        StopAllCoroutines();
        GetComponent<AILerp>().canMove = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    void Death()
    {
        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(deathSound, 0f, 0.15f);

        StopMove();
        anim.Play("zombieDeath");
        levelControl.currentKills++;
        levelControl.totalKills++;
        LeanTween.color(gameObject, new Color(0.5f, 0.5f, 0.5f, 0.7f), 0.5f);
        Invoke("DecayToDestroy", 0.5f);
        //Destroy(gameObject,deathAnimationDuration);
    }

    void DecayToDestroy()
    {
        LeanTween.color(gameObject, new Color(0.5f, 0.5f, 0.5f, 0f), 3f);
        Destroy(gameObject, 3.1f);
    }

    void SpawnFadeIn()
    {
        LeanTween.color(gameObject, new Color(1f, 1f, 1f, 1f), 0.25f);
    }
}
