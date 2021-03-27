using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    float getFuel = 4f;
    float getfuel = PlayerController.jetFuel;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Fuel")
        {
            //How much a fuel collectable collects
            getFuel += 2f;
            PlayerController.jetFuel = getFuel;
            //Audio Pick up for collectables
            musicSource.Play();
            musicSource.loop = false;
            Destroy(this.gameObject);
        }
        
    }
}