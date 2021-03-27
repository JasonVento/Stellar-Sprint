using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    //Change max Fuel and Thrust Force as needed, apply character rigid body and an empty object that is a child of the player character for the grounded Transform.
    public float maxFuel = 4f;
    public float thrustForce = 0.5f;
    public Rigidbody rigid;
    public Transform groundedTransform;
    //Audio Source
    public AudioSource musicSource;

    public GameObject fuelLeftUI;

    private float jetFuel;

    void Start()
    {
        jetFuel = maxFuel;
    }

    void Update()
    {
        fuelLeftUI.gameObject.GetComponent<Text>().text = ("Fuel Left: " + (int)jetFuel);

        //Jetpack is Shift
        if (Input.GetKey(KeyCode.LeftShift) && jetFuel > 0f)
        {
            jetFuel -= Time.deltaTime;
            rigid.AddForce(rigid.transform.up * thrustForce, ForceMode.Impulse);
        }
        else if (Physics.Raycast(groundedTransform.position, Vector3.down, 0.05f, LayerMask.GetMask("Grounded")) && jetFuel < maxFuel)
        {
            /*Code below is if there is a recharge when not in use (for testing).
            jetFuel += Time.deltaTime;*/
        }
        else
        {

        }
    }

    void OnTriggerEnter(Collider trig)
    {
        //Fuel collectable
        if (trig.gameObject.tag == "Fuel")
        {
            if (jetFuel < maxFuel)
            {
                //How much a fuel collectable collects
                jetFuel += 2;
                //Audio Pick up for collectables
                musicSource.Play();
                musicSource.loop = false;
                //Destroy collectable
                Destroy(trig.gameObject);
            }
        }
        else
        {

        }
    }
}