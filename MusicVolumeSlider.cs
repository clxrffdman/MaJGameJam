using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicVolumeSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public AudioMixer mixer;


    private void Start()
    {
        mixer = Resources.Load("MasterMixer") as AudioMixer;
        if (index == 0)
        {
            float value;
            bool result = mixer.GetFloat("musicVol", out value);
            GetComponent<Slider>().value = value;
        }
        else
        {
            float value;
            bool result = mixer.GetFloat("FXvol", out value);
            GetComponent<Slider>().value = value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(index == 0)
        {
            mixer.SetFloat("musicVol", GetComponent<Slider>().value);
        }
        if (index == 1)
        {
            mixer.SetFloat("FXvol", GetComponent<Slider>().value);
        }

    }
}
