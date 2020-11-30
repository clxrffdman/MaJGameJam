using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatHitIndicator : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;

    public BeatController beatController;
    public Sprite normalSprite;
    public Sprite offBeatSprite;
    public Sprite onBeatSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.tag == "Note")
        {
            beatController.onBeat = true;
            beatController.currentNote = collision.GetComponent<Note>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            beatController.onBeat = false;
            beatController.currentNote = null;
        }
    }

    public void ChangeToNormalState()
    {
        spriteRenderer.sprite = normalSprite;
    }

    public void ChangeToOnBeatState()
    {
        spriteRenderer.sprite = onBeatSprite;
    }

    public void ChangeToOffBeatState()
    {
        spriteRenderer.sprite = offBeatSprite;
    }
}
