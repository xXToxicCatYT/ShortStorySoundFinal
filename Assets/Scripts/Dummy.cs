using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Dialogue2 dialogue2;

    public TextMeshProUGUI hitsText;

    private int hits;
    private bool hitDisabled = false;



    // Update is called once per frame
    void Update()
    {
        hitsText.text = "(Training) Number of Hits: " + hits + "/20";

        if (hits == 20)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.scream, this.transform.position);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ambience, this.transform.position);
            dialogue2.LoadNextLevel();
            hits = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee" && hitDisabled == false)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.woodSlicing, this.transform.position);
            hitDisabled = true;
            hits++;
        }
    }

    public void Enable()
    {
        hitDisabled = false;
    }
}