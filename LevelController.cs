using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killsText;
    public GameObject pauseMenu;
    public GameObject endCard;
    public GameObject leftEndCard;
    public GameObject rightEndCard;
    public GameObject waveCard;
    public BeatController beatController;
    public AudioSource aud;
    public GameObject tutorial;
    public GameObject soundSpawn;

    [Header("Audio Files")]
    public AudioClip loseTheme;
    public AudioClip winTheme;
    public AudioClip pauseSound;
    public AudioClip buttonSound;

    [Header("Level-Specific Data: Phase One")]
    public string levelName;
    public int levelIndex;
    public bool hasTutorial;
    public int initialSeconds;
    public int initialKillRequirement;
    

    [Header("Level-Specific Data: Phase Two")]
    public int secondarySeconds;
    public int secondaryKillRequirement;


    [Header("Game States")]
    public bool win;
    public bool paused;
    public bool canPause;
    public int currentPhase;
    public bool inProgress;

    [Header("Objective States")]
    public int objectiveIndex; // starts from 0
    public int totalKills;
    public int currentKills;
    public int currentRequiredKills;
    public float timer;
    public int timerDisplay;
    public int highestCombo;
    public bool inTutorial;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        canPause = true;
        inProgress = true;
        currentPhase = 0;
        timer = initialSeconds;
        currentRequiredKills = initialKillRequirement;
        aud = GetComponent<AudioSource>();
        if(timerText == null)
        {
            timerText = GameObject.Find("UICanvas").transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        if (killsText == null)
        {
            killsText = GameObject.Find("UICanvas").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        if(pauseMenu == null)
        {
            pauseMenu = GameObject.Find("UICanvas").transform.GetChild(GameObject.Find("UICanvas").transform.childCount - 1).gameObject;
        }
        if (endCard == null)
        {
            endCard = GameObject.Find("UICanvas").transform.GetChild(GameObject.Find("UICanvas").transform.childCount - 2).gameObject;
        }
        if (beatController == null)
        {
            beatController = GameObject.Find("BeatController").transform.GetComponent<BeatController>();
        }

        if (hasTutorial)
        {
            Invoke("Tutorial", 0.1f);
            
        }

        Invoke("StartBeat", 1f);

    }

    public void StartBeat()
    {
        beatController.StartBeatController();
    }


    // Update is called once per frame
    void Update()
    {

        

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        
        timerDisplay = Mathf.RoundToInt(timer);
        timerText.text = UpdateTimerDisplay(timerDisplay);
        killsText.text = UpdateKillsDisplay();

        if (inProgress && canPause && !inTutorial && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }

        if (currentKills >= currentRequiredKills && timer > 0)
        {
            killsText.text = "SURVIVE!";
        }

        if (inProgress && currentPhase == 0 && timer <= 0)
        {
            if(currentKills >= currentRequiredKills)
            {
                NextPhase();
            }
            else
            {
                EndLevel();
            }
            
        }

        if (inProgress && currentPhase == 1 && timer <= 0)
        {
            if (currentKills >= currentRequiredKills)
            {
                win = true;
            }

            EndLevel();
            
        }


    }

    public void Tutorial()
    {
        inTutorial = true;
        paused = true;
        Time.timeScale = 0;
        var foundAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in foundAudioSources)
        {
            aud.Pause();
        }
        tutorial.SetActive(true);

        
    }

    public void CloseTutorial()
    {
        inTutorial = false;
        paused = false;
        Time.timeScale = 1;
        var foundAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in foundAudioSources)
        {
            aud.UnPause();
        }
        tutorial.SetActive(false);
    }

    public void EndLevel()
    {
        inProgress = false;
        canPause = false;
        endCard.SetActive(true);

        paused = true;
        Time.timeScale = 0;
        var foundAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in foundAudioSources)
        {
            aud.Pause();
        }

        leftEndCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600, 0, 0);
        rightEndCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(600, 0, 0);

        leftEndCard.SetActive(true);
        rightEndCard.SetActive(true);

        LeanTween.moveX(leftEndCard.GetComponent<RectTransform>(), 0, 0.8f).setIgnoreTimeScale(true);
        LeanTween.moveX(rightEndCard.GetComponent<RectTransform>(), 0, 0.8f).setIgnoreTimeScale(true);

        rightEndCard.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = totalKills + "";
        rightEndCard.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + beatController.highestCombo;
        float totalShotSub = beatController.totalShots;
        float accuracy = Mathf.RoundToInt((totalShotSub / beatController.totalClicks) * 100);
        if(accuracy < 1)
        {
            accuracy = 0;
        }
        rightEndCard.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + accuracy + "%";
        
        if (win != true)
        {
            aud.clip = loseTheme;
            aud.Play();
            endCard.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Failure";
            if(levelIndex != 3)
            {
                endCard.transform.GetChild(endCard.transform.childCount - 2).gameObject.SetActive(false);
            }
            
        }
        else
        {
            aud.clip = winTheme;
            aud.Play();
            endCard.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Victory";
            endCard.transform.GetChild(endCard.transform.childCount - 3).gameObject.SetActive(false);
            if(levelIndex == 2)
            {
                endCard.transform.GetChild(endCard.transform.childCount - 2).gameObject.SetActive(false);
            }
        }
    }

    IEnumerator PlayButtonSound(int index)
    {


        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(buttonSound, 0f, 0.2f);

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1;
        if (index == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

        if (index == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        if (index == 2)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
    public void ToNextLevel()
    {
        
        StartCoroutine(PlayButtonSound(0));
    }

    

    public void RetryLevel()
    {
        
        StartCoroutine(PlayButtonSound(1));
    }

    public void NextPhase()
    {
        StartCoroutine(WaveCard());

        currentPhase++;
        currentKills = 0;
        timer = secondarySeconds;
        currentRequiredKills = secondaryKillRequirement;
        
    }

    public IEnumerator WaveCard()
    {
        
        waveCard.SetActive(true);
        waveCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(190, 330, 0);
        waveCard.transform.GetChild(0).GetComponent<Animator>().Play("wavecardopen");
        LeanTween.moveY(waveCard.GetComponent<RectTransform>(), 270, 0.8f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.scaleY(waveCard.transform.GetChild(1).gameObject, 1, 0.6f);


        yield return new WaitForSeconds(2);
        waveCard.transform.GetChild(0).GetComponent<Animator>().Play("wavecardclose");
        LeanTween.moveY(waveCard.GetComponent<RectTransform>(), 330, 0.8f);
        LeanTween.scaleY(waveCard.transform.GetChild(1).gameObject, 0, 0.2f);
        yield return new WaitForSeconds(1);
        waveCard.SetActive(false);
    }

    public void ResetKillCount()
    {
        currentKills = 0;
    }

    public void Pause()
    {


        paused = true;
        Time.timeScale = 0;
        var foundAudioSources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource aud in foundAudioSources)
        {
            aud.Pause();
        }

        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(pauseSound, 0f, 0.2f);

        pauseMenu.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level: " + levelName + "\nTotal Kills: " + totalKills + "\nHighest Combo: " + beatController.highestCombo;

        pauseMenu.SetActive(true);
    }

    public void LoadMainMenu()
    {
        
        StartCoroutine(PlayButtonSound(2));
        /*
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        */

    }

    public void UnPause()
    {
        var sound = Instantiate(soundSpawn, transform.position, Quaternion.identity);
        sound.GetComponent<SoundSample>().SpawnSound(pauseSound, 0f, 0.2f);

        paused = false;
        Time.timeScale = 1;
        var foundAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in foundAudioSources)
        {
            aud.UnPause();
        }
        pauseMenu.SetActive(false);

    }

    string UpdateTimerDisplay(int timeLeft)
    {
        string rv = "";
        int minutes = 0;
        while (timeLeft >= 60)
        {
            timeLeft -= 60;
            minutes++;
        }

        if(timeLeft >= 10)
        {
            rv = "0" + minutes + ":" + timeLeft;
        }
        else
        {
            rv = "0" + minutes + ":0" + timeLeft;
        }
        

        return rv;
    }

    string UpdateKillsDisplay()
    {
        string rv = "";
        rv += currentKills + "/" + currentRequiredKills;
        rv += "\nKills";
        return rv;
    }
}
