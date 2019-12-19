using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4f;

    private void Awake()
    {
        instance = this;
    }


   public IEnumerator NextLevelLoadTime(string levelToLoad)
    {
        
        AudioManager.instance.PlayWinMusic();
        PlayerController.instance.canMove = false;
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(levelToLoad);
    }
}
