using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] float waitForAnyKey = 2f;
    [SerializeField] GameObject anyKeyText;
    [SerializeField] string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PressAnyKeyToLoadMainMenu();
    }


    private void PressAnyKeyToLoadMainMenu()
    {
        // waitForAnyKey set to greater than 0. 
        // Once reach 0 then display text and then allow user press any key to transition to main menu
        if (waitForAnyKey > 0)
        {

            waitForAnyKey -= Time.deltaTime;
            if (waitForAnyKey <= 0)
            {
                anyKeyText.SetActive(true);
            }
        }

        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }

}
