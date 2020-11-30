using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public bool swingUp;
    public Animator anim;
    public float sheathTime;
    public float currentSheathTime;
    public bool sheathed;
    public bool checking;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CheckSheathTime());
    }

    void OnEnable()
    {

        //StartCoroutine(CheckSheathTime());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(currentSheathTime > 0)
        {
            currentSheathTime -= Time.deltaTime;
        }
        */
    }

    public IEnumerator CheckSheathTime()
    {
        while (checking)
        {
            if (currentSheathTime <= 0)
            {
                if (swingUp)
                {
                    print("DOWN");
                    anim.Play("downToSheath");
                    swingUp = true;
                    checking = false;
                    GetComponent<Aim>().addAngle = -140;
                    sheathed = true;

                }
                else
                {
                    print("up");
                    anim.Play("upToSheath");
                    swingUp = true;
                    checking = false;
                    GetComponent<Aim>().addAngle = -140;
                    sheathed = true;

                }
            }

            yield return new WaitForSeconds(0.05f);
        }

    }

    public void Swing()
    {

        StopAllCoroutines();
        checking = true;
        currentSheathTime = sheathTime;
        //StartCoroutine(CheckSheathTime());

        if (sheathed)
        {
            anim.Play("sheathToUp");
            GetComponent<Aim>().addAngle = 90;
            swingUp = false;
            sheathed = false;
        }
        else
        {
            if (swingUp)
            {
                anim.Play("swingUp");
                GetComponent<Aim>().addAngle = 90;
                swingUp = false;
            }
            else
            {
                anim.Play("swingDown");
                GetComponent<Aim>().addAngle = -90;
                swingUp = true;
            }
        }


    }
}
