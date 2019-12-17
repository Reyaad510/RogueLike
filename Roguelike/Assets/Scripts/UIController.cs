using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    // Note
    // TO fix healthSlider from not completely filling or depleting. You need to set Value all the way up to "1" and then go to Fill
    // Zoom all the way in on Fill and move bar to fit anchor point. Then go to Fill area and move all of it to fill.
    // Same for depeleting fully, Set value to 1 and do same for end.

    // Double Outline technique
    // Use double outline 2/2 instead of doing 4/4 bcuz Unity will make it look weird
}
