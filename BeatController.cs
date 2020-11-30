using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{

    protected AudioSource audioSource;
    protected float noteSpeed;
    protected Camera mainCamera;
   
    public Note currentNote;

    [Header ("Beat Bar Position")]
    public float xCameraOffset;
    public float yCameraOffset;

    [Header("Tempo of Music")]
    public float beatTempo;

    [Header("Time Indicator Inactive After Miss")]
    public float deactivationTime;


    public Color activeColor;
    public Color inactiveColor;

    public AudioClip music;
    public GameObject beatBarHolder;
    public GameObject noteHolder;
    public BeatHitIndicator beatHitIndicator;
    public GameObject rootShoot;

    public bool onBeat;
    public bool hasStarted;
    public bool indicatorActive;

    public LevelController levelControl;
    public int highestCombo;
    public int currentCombo;
    public int totalShots;
    public int totalClicks;

    // Start is called before the first frame update
    void Start()
    {


        audioSource = GetComponent<AudioSource>();
        mainCamera = FindObjectOfType<Camera>();
        if(rootShoot == null)
        {
            rootShoot = GameObject.Find("Player").transform.GetChild(GameObject.Find("Player").transform.childCount-1).gameObject;
        }

        if(levelControl == null)
        {
            levelControl = GameObject.Find("LevelController").transform.GetComponent<LevelController>();
        }

        audioSource.clip = music;
        noteSpeed = beatTempo / 60f;
        indicatorActive = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (hasStarted)
        {
            MoveNotes();
        }

        if (hasStarted && indicatorActive)
        {
            if ((Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && !levelControl.paused)
            {
                totalClicks++;
                if (onBeat)
                {
                    if(currentNote != null)
                    {
                        if (currentNote.isBlueNote)
                        {
                            //shoot player gun
                            rootShoot.GetComponent<Shoot>().ForceFire(1);
                            totalShots++;
                        } else
                        {
                            //swing sword
                            rootShoot.GetComponent<Shoot>().ForceFire(2);
                            totalShots++;
                        }

                        currentCombo++;

                        if(currentCombo > highestCombo)
                        {
                            highestCombo = currentCombo;
                        }

                        currentNote.DestroyNote();
                        beatHitIndicator.ChangeToOnBeatState();
                        Invoke("ChangeBeatBarHitIndicatorToNormal", .25f);
                    }
                } else
                {
                    currentCombo = 0;
                    DeactivateBeatBarHitIndicator();
                    Invoke("ActivateBeatBarHitIndicator", deactivationTime);
                }
                
            }

            
        }
        
    }

    public void StartBeatController()
    {
        StartPlayingMusic();
        hasStarted = true;
    }

    public void FixedUpdate()
    {
        SetBeatBarPosition();
    }

    public void StartPlayingMusic()
    {
        audioSource.Play();
    }

    public void MoveNotes()
    {
        noteHolder.transform.position -= new Vector3(noteSpeed * Time.deltaTime, 0f, noteHolder.transform.position.z);
    }

    public void SetBeatBarPosition()
    {
        beatBarHolder.transform.position = new Vector3(mainCamera.transform.position.x + xCameraOffset, mainCamera.transform.position.y + yCameraOffset, transform.position.z);
    }

    public void DeactivateBeatBarHitIndicator()
    {
        indicatorActive = false;
        beatHitIndicator.ChangeToOffBeatState();
    }

    public void ActivateBeatBarHitIndicator()
    {
        indicatorActive = true;
        beatHitIndicator.ChangeToNormalState();
    }

    public void ChangeBeatBarHitIndicatorToNormal()
    {
        beatHitIndicator.ChangeToNormalState();
    }
}
