using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shoot : MonoBehaviour
{
    public Transform crosshair;
    public Transform player;
    public int gunType;

    public bool shootEnable;
    public bool swapEnable;

    public bool isMelee;

    public int currentGunIndex;
    public int currentWeaponIndex;

    //GUN SPECIFIC VARIABLES
    public GameObject currentGun;
    public GameObject currentBullet;
    public AudioClip currentSound;
    public float currentSoundVolume;
    public float currentGunShakeMag;
    public float currentGunShakeDuration;
    public float currentGunVolume;
    public float gundelay;
    public float bulletQuantity;
    public GameObject barrel;
    public float ShootSpeed;
    public float ammoCostPerShot;
    public float currentFreezeFrame;

    public bool canShoot;
    public float cooldownBase = 1f;
    public float cooldown = 0.25f;
    public float timeLeft = 0.0f;
    public Animator anim;
    public float animationLength;
    public float aniTimeLeft = 0.0f;

    public bool auto;

    public AudioSource audioSource;
    public GameObject soundSpawn;

    

    //AMMO MANAGEMENT
    public float currentAmmo;
    
    



    //WEAPON LIST



    public GameObject[] weaponArray;

    //WEAPON UI ICONS
    public Sprite[] gunIcons;

    public float UITimer;
    float currentTime;
    public bool hidden;

    public float TimeTillShoot;

    // Start is called before the first frame update
    void Start()
    {
        isMelee = false;
        //shootEnable = true;
        swapEnable = true;
        player = GameObject.Find("Player").transform;

        audioSource = GetComponent<AudioSource>();

        SwapGun(1,0);

    }

    public void ForceFire(int index)
    {
        SwapGun(index, 0);
        Fire();
    }

    // Update is called once per frame
    void Update()
    {


        if (shootEnable)
        {
            CheckShoot();
            
        }

        if (swapEnable)
        {

            
        }


    }



    public void Fire()
    {
        //anim.SetBool("shoot", true);



        if (currentGunIndex != -1)
        {


            if (currentGunIndex == 2)
            {
                StartCoroutine(LateAim());

            }
            else
            {
                anim.Play("gunfire", 0, 0);

            }

            //player.GetComponent<Animator>().Play("shoot");

            GameObject.Find("Cam").GetComponent<CameraShake>().ShakeIt(currentGunShakeMag, currentGunShakeDuration);



            for (int i = 0; i < bulletQuantity; i++)
            {
                Invoke("Spawn", gundelay);
            }


            if (currentSound != null)
            {
                var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
                sound.GetComponent<SoundSample>().SpawnSound(currentSound, 0f, currentGunVolume);
            }

            //audioSource.Play();


        }

        // 0.01, 0.05


    }

    

    public void Spawn()
    {
        if (currentGunIndex != 2)
        {
            Instantiate(currentBullet, barrel.transform.position, Quaternion.identity);
        }
        else
        {

            var swipe = Instantiate(currentBullet, barrel.transform.position, Quaternion.identity);
            swipe.GetComponent<Bullet>().UpOrDown(!currentGun.GetComponent<SwordSwing>().swingUp);


        }

    }

    

    public IEnumerator LateAim()
    {


        currentGun.GetComponent<Aim>().aiming = false;
        currentGun.GetComponent<SwordSwing>().Swing();
        yield return new WaitForSeconds(0.25f);

        currentGun.GetComponent<Aim>().aiming = true;
    }


    void CheckShoot()
    {
        if (currentTime == 0 && hidden == false)
        {
            hidden = true;
            
        }

        if (currentBullet != null)
        {
            if (auto)
            {
                if (Input.GetKey(KeyCode.Mouse0) && timeLeft <= 0)
                {
                    
                    //ammoStock[currentGunIndex] -= ammoCostPerShot;
                    timeLeft = cooldownBase;
                    if (hidden != false && currentGunIndex != -1 && currentGunIndex != 2)
                    {
                        hidden = false;
                        
                    }

                    
                    currentTime = UITimer;
                    Fire();

                }
            }
            if (!auto)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && timeLeft <= 0)
                {
                    
                    //ammoStock[currentGunIndex] -= ammoCostPerShot;
                    timeLeft = cooldownBase;
                    if (hidden != false && currentGunIndex != -1 && currentGunIndex != 2)
                    {
                        hidden = false;
                        
                    }
                    currentTime = UITimer;
                    Fire();

                }
            }
        }



        if (timeLeft > 0)
        {
            //e.gameObject.SetActive(false);
            canShoot = false;
            timeLeft -= Time.deltaTime;
        }
        else
        {
            //e.gameObject.SetActive(true);
            canShoot = true;
            timeLeft = 0;
            

        }


        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }

    }

    public void SwapGun(int type, float ammo)
    {
        
        //ammoStock[currentGunIndex] = currentAmmo;
        if (type != 7)
        {
            currentAmmo = ammo;
        }


        if (type == -1)
        {
            isMelee = false;
            currentGunIndex = -1;
            ammoCostPerShot = 0;
            currentWeaponIndex = -1;

            

            auto = true;
            currentGun.SetActive(false);
            weaponArray[0].SetActive(true);
            currentGun = weaponArray[0];
            currentBullet = null;

            currentSound = null;
            audioSource.clip = currentSound;
            audioSource.volume = 1f;

            currentFreezeFrame = 0f;
            anim = null;
            cooldownBase = 0.25f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }


        if (type == 0)
        {
            isMelee = false;
            currentGunIndex = 0;
            ammoCostPerShot = 1;
            currentWeaponIndex = 0;
            audioSource.Stop();
            

            auto = true;
            currentGun.SetActive(false);
            weaponArray[1].SetActive(true);
            currentGun = weaponArray[1];
            currentBullet = (GameObject)Resources.Load("bullet6", typeof(GameObject));

            currentSound = null;
            audioSource.clip = currentSound;
            audioSource.volume = 1f;

            currentFreezeFrame = 0f;

            anim = currentGun.GetComponent<Animator>();
            cooldownBase = 0.25f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 1)
        {
            isMelee = false;
            currentGunIndex = 1;
            ammoCostPerShot = 1;
            currentWeaponIndex = 1;
            audioSource.Stop();
            

            auto = false;
            currentGunShakeMag = 0.005f;
            currentGunShakeDuration = 0.05f;
            currentGun.SetActive(false);
            weaponArray[2].SetActive(true);
            currentGun = weaponArray[2];
            currentBullet = (GameObject)Resources.Load("bullet1", typeof(GameObject));

            
            anim = currentGun.GetComponent<Animator>();
            currentSound = (AudioClip)Resources.Load("ResourceAudio/Laser_Shot", typeof(AudioClip));
            audioSource.clip = currentSound;
            currentGunVolume = 0.1f;

            anim = currentGun.GetComponent<Animator>();
            currentFreezeFrame = 0f;
            cooldownBase = 0.13f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 2)
        {
            currentGunIndex = 2;
            ammoCostPerShot = 0;
            //currentAmmo = ammoStock[6];

            isMelee = true;
            audioSource.Stop();


            auto = true;
            currentGunShakeMag = 0.03f;
            currentGunShakeDuration = 0.03f;
            currentGun.SetActive(false);
            weaponArray[3].SetActive(true);
            currentGun = weaponArray[3];
            currentBullet = (GameObject)Resources.Load("swordSlash", typeof(GameObject));
            anim = currentGun.GetComponent<Animator>();
            currentSound = (AudioClip)Resources.Load("GunSounds/swordSwingGlitch", typeof(AudioClip));
            audioSource.clip = currentSound;
            currentGunVolume = 0.5f;
            cooldownBase = 0.6f;

            currentFreezeFrame = 0.05f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 3)
        {
            isMelee = false;
            currentGunIndex = 3;
            ammoCostPerShot = 3;
            //currentAmmo = ammoStock[3];
            currentWeaponIndex = 3;
            audioSource.Stop();
            

            auto = false;
            currentGunShakeMag = 0.01f;
            currentGunShakeDuration = 0.05f;
            currentGun.SetActive(false);
            weaponArray[4].SetActive(true);
            currentGun = weaponArray[4];
            currentBullet = (GameObject)Resources.Load("laserBullet", typeof(GameObject));

            currentSound = null;
            audioSource.clip = currentSound;
            currentGunVolume = 1f;

            anim = currentGun.GetComponent<Animator>();
            cooldownBase = 0.5f;
            gundelay = 0f;
            currentFreezeFrame = 0.08f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 4)
        {
            isMelee = false;
            currentGunIndex = 4;
            ammoCostPerShot = 1;
            currentWeaponIndex = 4;

            audioSource.Stop();
            

            auto = true;
            currentGunShakeMag = 0.01f;
            currentGunShakeDuration = 0.05f;
            currentGun.SetActive(false);
            weaponArray[5].SetActive(true);
            currentGun = weaponArray[5];
            currentBullet = (GameObject)Resources.Load("rocketBullet", typeof(GameObject));

            currentSound = null;
            audioSource.clip = currentSound;
            currentGunVolume = 1f;

            anim = currentGun.GetComponent<Animator>();
            cooldownBase = .8f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 5)
        {
            isMelee = false;
            currentGunIndex = 5;
            ammoCostPerShot = 1;
            //currentAmmo = ammoStock[5];
            currentWeaponIndex = 5;
            audioSource.Stop();
            

            auto = false;
            currentGunShakeMag = 0.01f;
            currentGunShakeDuration = 0.05f;
            currentGun.SetActive(false);
            weaponArray[6].SetActive(true);
            currentGun = weaponArray[6];
            currentBullet = (GameObject)Resources.Load("bullet6", typeof(GameObject));
            currentSound = (AudioClip)Resources.Load("GunSounds/sound5", typeof(AudioClip));
            audioSource.clip = currentSound;
            currentGunVolume = 1f;
            anim = currentGun.GetComponent<Animator>();
            currentFreezeFrame = 0.12f;
            cooldownBase = 1.2f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 6)
        {
            isMelee = false;
            currentGunIndex = 6;
            ammoCostPerShot = 1;
            //currentAmmo = ammoStock[6];
            currentWeaponIndex = 6;

            audioSource.Stop();
            

            auto = false;

            currentGunShakeMag = 0.025f;
            currentGunShakeDuration = 0.02f;
            currentGun.SetActive(false);
            weaponArray[7].SetActive(true);
            currentGun = weaponArray[7];
            currentBullet = (GameObject)Resources.Load("bullet6", typeof(GameObject));
            anim = currentGun.GetComponent<Animator>();
            currentSound = (AudioClip)Resources.Load("GunSounds/revolver", typeof(AudioClip));
            audioSource.clip = currentSound;
            currentGunVolume = 0.25f;
            cooldownBase = 0.4f;

            currentFreezeFrame = 0.02f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

        if (type == 7)
        {
            currentGunIndex = 7;
            ammoCostPerShot = 0;
            //currentAmmo = ammoStock[6];

            isMelee = true;
            audioSource.Stop();
            

            auto = true;
            currentGunShakeMag = 0.03f;
            currentGunShakeDuration = 0.03f;
            currentGun.SetActive(false);
            weaponArray[8].SetActive(true);
            currentGun = weaponArray[8];
            currentBullet = (GameObject)Resources.Load("bullet7", typeof(GameObject));
            anim = currentGun.GetComponent<Animator>();
            currentSound = (AudioClip)Resources.Load("GunSounds/swordSwingGlitch", typeof(AudioClip));
            audioSource.clip = currentSound;
            currentGunVolume = 0.25f;
            cooldownBase = 0.6f;

            currentFreezeFrame = 0.05f;
            gundelay = 0f;
            bulletQuantity = 1f;
            barrel = currentGun.transform.GetChild(0).gameObject;

        }

    }
}
