using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public bool activateBlueNote;
    public GameObject blueNote;
    public GameObject redNote;

    // Start is called before the first frame update
    void Start()
    {
        if (activateBlueNote)
        {
            blueNote.SetActive(true);
            redNote.SetActive(false);
        } else
        {
            redNote.SetActive(true);
            blueNote.SetActive(false);
        }
    }
}
