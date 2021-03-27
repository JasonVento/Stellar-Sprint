using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*Though this is called a "timer", this script will also be the fuel tank collectable, due
 * to them both affecting each other so much.*/

public class Timer : MonoBehaviour
{
    /*The "Timer". We decided to make the timer the oxygen tank, so that's what these
     * two set up */
    public float timeLeft = 60;
    public GameObject timeLeftUI;
    //Audio source for a collectable to play a jingle.
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        timeLeftUI.gameObject.GetComponent<Text>().text = ("Oxygen Left: " + (int)timeLeft);

        if (timeLeft < 0.1f)
        {
            //This just reloads the scene again, we could have a screen pop up instead saying game over?
            //Use whatever the scene is named. Change for different levels.
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("Loss");
        }
    }
    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.name == "EndLevel")
        {
            //Set to name of "win scene". If we aren't switching scenes upon finishing the level, will rework script.
            SceneManager.LoadScene("Win Screen");
        }
        //Tag Oxygen tanks with Ox
        if (trig.gameObject.tag == "Ox")
        {
            //Set how much Oxygen tanks fill
            timeLeft += 20;
            //Sound Jingle for collectables
            musicSource.Play();
            musicSource.loop = false;
            //Destroy collectable
            Destroy(trig.gameObject);
        }
    }
}