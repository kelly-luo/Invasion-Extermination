using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    PlayerMotor motor;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("We click " + hit.collider.name + " " + hit.point);

                // Move our player to what we click to
                motor.MoveToPoint(hit.point);

                // Stop focusing on any objects
            }
        }

        // right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("We click " + hit.collider.name + " " + hit.point);

                // Check if we hit an interactable then set as focus

            }
        }
    }
}
