using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class MoveBox : MonoBehaviour
{
    Controller2D controller;
    Vector3 velocity;
    float gravity=-31f;

    void Start()
    {
        controller = GetComponent<Controller2D>();
    }
    void Update()
    {
        CalculateGravity();
        controller.Move(velocity * Time.deltaTime, false);
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }
    void CalculateGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }
}
