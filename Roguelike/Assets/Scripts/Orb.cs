using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{

    [SerializeField] GameObject particleEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            particleEffect.SetActive(true);
        }
    }

}
