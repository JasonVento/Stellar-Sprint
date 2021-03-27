using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //Speed boost
    public float maxFuel = 4f;
    public float RunSpeed = 17f;
    public float NormalSpeed = 10f;
    public static float jetFuel;
    public AudioSource musicSource;
    public GameObject fuelLeftUI;

    //Player object
    public GameObject player;
    public int getOn = -5;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        jetFuel = maxFuel;
        player = GameObject.FindGameObjectWithTag("Player");
        AudioSource[] audios = GetComponents<AudioSource>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && jetFuel > 0f)
        {
            jetFuel -= Time.deltaTime;
            speed = RunSpeed;
        }
        else
        {
            speed = NormalSpeed;
        }

        fuelLeftUI.gameObject.GetComponent<Text>().text = ("Fuel Left: " + (int)jetFuel);
    }

    void OnTriggerEnter()
    {
        if (player.transform.position.y < getOn)
        {
            SceneManager.LoadScene("Loss");
        }
    }

    void OnTriggerEnter(Collision trig)
    {
        if (trig.gameObject.CompareTag("Fuel"))
        {
            //How much a fuel collectable collects
            jetFuel += 2f;
            //Audio Pick up for collectables
            musicSource.Play();
            musicSource.loop = false;
            Destroy(gameObject); // destroy the projectile anyway
        }
    }
}