using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject credits;
    public GameObject soundSpawn;

    public AudioClip startSound;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void DisableCredits()
    {
        credits.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    void LoadLevel0()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    void LoadLevel2()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
    public void StartLevel0()
    {
        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(startSound, 0f, 0.3f);

        Invoke("LoadLevel0", 1.3f);
    }
    public void StartLevel1()
    {
        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(startSound, 0f, 0.4f);

        Invoke("LoadLevel1", 1.3f);
    }
    public void StartLevel2()
    {
        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(startSound, 0f, 0.4f);

        Invoke("LoadLevel2", 1.3f);
    }
}
