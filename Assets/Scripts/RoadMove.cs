using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    float currentSpeed=25f;

    GameController controller;

    private void Start()
    {
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
        //speed value from GameController.
        currentSpeed = controller.roadSpeed;
    }

    void Update()
    {
        //infinite move for the background.
        transform.transform.Translate(-currentSpeed*Time.deltaTime,0,0);

        //speed can change at GameController.
        currentSpeed = controller.roadSpeed;
    }
}
