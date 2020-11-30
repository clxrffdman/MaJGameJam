using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject soundSpawn;
    public AudioClip playerDamage;
    public AudioClip playerDeath;
    public Animator anim;
    public LevelController levelControl;
    public Shoot rootShoot;

    public GameObject[] hearts;

    [Header("Player Stats")]
    public int maxHealth;
    public int currentHealth;
    public float invulverabilityTimer;

    [Header("Player States")]
    public bool alive;
    public bool invulnerable;


    // Start is called before the first frame update
    void Start()
    {
        if(rootShoot == null)
        {
            rootShoot = transform.GetChild(transform.childCount - 1).GetComponent<Shoot>();
        }
        alive = true;
        currentHealth = maxHealth;
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if (levelControl == null)
        {
            if (GameObject.Find("LevelController") != null)
            {
                levelControl = GameObject.Find("LevelController").transform.GetComponent<LevelController>();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(alive && currentHealth <= 0)
        {
            alive = false;
            Death();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            currentHealth -= damage;
            var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
            sound.GetComponent<SoundSample>().SpawnSound(playerDamage, 0f, 0.4f);
            Invoke("RemoveInvulnerable", invulverabilityTimer);

            for(int i = hearts.Length-1; i >= 0; i--)
            {
                if (hearts[i].GetComponent<Heart>().alive)
                {
                    hearts[i].GetComponent<Heart>().Destroyed();
                    i = -1;
                }
                
            }
            

        }
        
        
    }

    void RemoveInvulnerable()
    {
        if (invulnerable)
        {
            invulnerable = false;
        }
        
    }

    public void Death()
    {
        print("player has died!");
        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(playerDeath, 0f, 0.4f);
        GetComponent<PlayerController>().moveEnabled = false;
        GetComponent<PlayerController>().movement.x = 0f;
        GetComponent<PlayerController>().movement.y = 0f;
        transform.GetChild(0).gameObject.SetActive(false);
        rootShoot.currentGun.SetActive(false);
        rootShoot.currentGunIndex = -1;
        anim.Play("death");
        GetComponent<CapsuleCollider2D>().enabled = false;
        

        Invoke("EndCard",2.5f);
        
    }

    void EndCard()
    {
        levelControl.EndLevel();
    }
}
