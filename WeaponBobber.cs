using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponBobber : MonoBehaviour
{

    public float bobTimeIdle;
    public float bobTimeWalk;
    // Start is called before the first frame update
    void Start()
    {
        StartBob(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBob(int type)
    {
        LeanTween.cancel(gameObject);
        
        if(type == 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, .5f, transform.localPosition.z);
            LeanTween.moveLocalY(gameObject, .4f, bobTimeIdle).setLoopPingPong(-1).setEase(LeanTweenType.easeInOutQuad);
        }
        if(type == 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, .4f, transform.localPosition.z);
            LeanTween.moveLocalY(gameObject, .5f, bobTimeWalk).setLoopPingPong(-1).setEase(LeanTweenType.easeInOutQuad);
        }
        
        
    }
}
