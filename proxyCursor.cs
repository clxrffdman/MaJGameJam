using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proxyCursor : MonoBehaviour
{
    public Vector3 mousePosition;
    public Rigidbody rb;
    public float proxyAngleOfShot;
    public Vector3 playerPos;

    public bool checkAngle;

    public float addangle;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkAngle)
        {
            if (GameObject.Find("RootShoot").GetComponent<Shoot>().currentGun.GetComponent<Aim>() != null)
            {
                addangle = GameObject.Find("RootShoot").GetComponent<Shoot>().currentGun.GetComponent<Aim>().addAngle;
            }
            else
            {
                addangle = 0;
            }

            
            proxyAngleOfShot = GameObject.Find("RootShoot").GetComponent<Shoot>().currentGun.transform.rotation.eulerAngles.z - addangle;
            playerPos = GameObject.Find("Player").transform.position;
            //Transform prox. = playerPos - GameObject.Find("Crosshair").transform.position;


            rb.position = new Vector3(GameObject.Find("RootShoot").GetComponent<Shoot>().currentGun.transform.position.x + 3 * Mathf.Cos(Mathf.Deg2Rad * proxyAngleOfShot), GameObject.Find("RootShoot").GetComponent<Shoot>().currentGun.transform.position.y + 3 * Mathf.Sin(Mathf.Deg2Rad * proxyAngleOfShot), 0f);
        }




        /*
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        */
    }
}
