using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waypoint6 : MonoBehaviour
{
    public RectTransform prefab;
    public Dialogue dialogue;

    private RectTransform waypoint;

    private Transform player;
    private TextMeshProUGUI distanceText;

    private bool once;

    private Vector3 offset = new Vector3(0, 1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.DialogueDone() == true && once == false)
        {
            once = true;

            var canvas = GameObject.Find("Canvas").transform;
            waypoint = Instantiate(prefab, canvas);

            player = GameObject.Find("Player").transform;

            distanceText = waypoint.GetComponentInChildren<TextMeshProUGUI>();
        }

        var screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);
        waypoint.position = screenPos;

        waypoint.gameObject.SetActive(screenPos.z > 0);

        distanceText.text = Vector3.Distance(player.position, transform.position).ToString("0") + " m";
    }
}
