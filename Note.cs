using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    protected Animator animator;

    public bool isBlueNote;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void DestroyNote()
    {
        animator.SetBool("hitOnBeat", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
