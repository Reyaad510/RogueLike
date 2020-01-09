using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public Text coinText;
    public GameObject deathScreen;

    public string newGameScene, mainMenuScene;
    public GameObject pauseMenu, mapDisplay, bigMapText;

    [Header("Fade")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool _fadeToBlack, _fadeOutBlack;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _fadeOutBlack = true;
        _fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        Fade();

    }



    private void Fade()
    {
     
        if (_fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                _fadeOutBlack = false;
            }
        }

        if (_fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 255f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 255f)
            {
                _fadeToBlack = false;
            }
        }
    }


    public void StartFadeToBlack()
    {
        _fadeToBlack = true;
        _fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }


    public void Resume()
    {
        LevelManager.instance.PauseOrUnpause();
    }



    // Note
    // TO fix healthSlider from not completely filling or depleting. You need to set Value all the way up to "1" and then go to Fill
    // Zoom all the way in on Fill and move bar to fit anchor point. Then go to Fill area and move all of it to fill.
    // Same for depeleting fully, Set value to 1 and do same for end.

    // Double Outline technique
    // Use double outline 2/2 instead of doing 4/4 bcuz Unity will make it look weird
}
