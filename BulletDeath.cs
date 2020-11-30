using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeath : MonoBehaviour
{

    public Animator anim;
    public float index;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        if (index == 0)
        {
            anim.Play("bulletDeath");
        }
        if (index == 1)
        {
            anim.Play("bulletDeathEnemyHit");
        }
        if (index == 2)
        {
            anim.Play("bulletDeath");

            Vector3 targ = transform.position;
            targ.z = 0f;

            Vector3 objectPos = GameObject.Find("Player").transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }

        Invoke("Death", 0.2f);
    }



    void Death()
    {
        Destroy(gameObject);
    }
}
