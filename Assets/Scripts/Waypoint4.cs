using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waypoint4 : MonoBehaviour
{
    public RectTransform prefab;

    private RectTransform waypoint;

    private Transform player;
    private TextMeshProUGUI distanceText;

    private Vector3 offset = new Vector3(0.5f, 0.5f, 1);

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("Canvas").transform;
        waypoint = Instantiate(prefab, canvas);

        player = GameObject.Find("Player").transform;

        distanceText = waypoint.GetComponentInChildren<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);
        waypoint.position = screenPos;

        waypoint.gameObject.SetActive(screenPos.z > 0);

        distanceText.text = Vector3.Distance(player.position, transform.position).ToString("0") + " m";
    }
}
