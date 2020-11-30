using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class laser : MonoBehaviour
{
    public float maxRange = 100f;
    private LineRenderer line;
    public RaycastHit2D ray;

    public RaycastHit2D[] rayArray;

    public GameObject rootShoot;
    public float distance;
    public LayerMask[] hitableLayers;
    public LayerMask whatToHit;
    public float shootAngle;

    public int damage;

    public GameObject barrel;

    public GameObject bulletDeath;
    public GameObject bulletDeathEnemy;

    public bool hit;
    public int end;

    void Start()
    {




        //barrel = GameObject.Find("RootShoot").GetComponent<Shoot>().currentGun.transform.GetChild(0).gameObject;



        shootAngle = rootShoot.GetComponent<Shoot>().currentGun.GetComponent<Aim>().rotation;
        transform.LookAt(GameObject.Find("CrosshairProxy").transform);
        rayArray = Physics2D.RaycastAll(transform.position, transform.forward, distance, whatToHit);



        for (int i = 0; i < rayArray.Length; i++)
        {
            if (rayArray[i].collider != null)
            {
                hit = true;
            }
        }

        if (hit)
        {

            for (int i = 0; i < rayArray.Length; i++)
            {
                if (rayArray[i].collider != null)
                {
                    if (rayArray[i].collider.tag == "Environment")
                    {
                        end = i;
                        i = rayArray.Length;
                    }

                }
            }

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, rayArray[end].point);
            InvokeRepeating("ThinLine", 0f, 0.01f);
            Invoke("KillLine", 0.2f);

            for (int i = 0; i <= end; i++)
            {
                if (rayArray[i].collider != null)
                {
                    if (rayArray[i].collider.tag == "Enemy")
                    {
                        Invoke("LineToWhite", 0.1f);
                        if (rayArray[i].collider.GetComponent<EnemyController>() != null)
                        {
                            rayArray[i].collider.GetComponent<EnemyController>().TakeDamage(damage);
                        }
                        

                        Instantiate(bulletDeathEnemy, ray.point, Quaternion.identity);



                    }
                    else if (rayArray[i].collider != null)
                    {
                        Instantiate(bulletDeath, ray.point, Quaternion.identity);
                    }
                }
            }




        }
        if (!hit)
        {

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward.normalized * distance);
            InvokeRepeating("ThinLine", 0.1f, 0.001f);
            Invoke("KillLine", 0.25f);

        }



    }

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        rootShoot = GameObject.Find("RootShoot");
        for (int i = 0; i < hitableLayers.Length; i++)
        {
            whatToHit = whatToHit + hitableLayers[i];
        }

        line.enabled = false;

        line.positionCount = 2;

        line.startWidth = 0.3f;
        line.endWidth = 0.275f;
    }

    void LineToWhite()
    {
        line.startColor = new Color(1, 0.6f, 0.6f, 1);
        line.endColor = new Color(1, 0.6f, 0.6f, 0.75f);
    }

    void Update()
    {



    }

    void ThinLine()
    {
        line.startWidth = Mathf.Lerp(line.startWidth, 0, 0.2f);
        line.endWidth = Mathf.Lerp(line.endWidth, 0, 0.175f);
    }

    void KillLine()
    {
        Destroy(gameObject);
    }
}