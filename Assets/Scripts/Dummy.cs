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

            dialogue2.LoadNextLevel();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee" && hitDisabled == false)
        {
            hitDisabled = true;
            hits++;
        }
    }

    public void Enable()
    {
        hitDisabled = false;
    }
}