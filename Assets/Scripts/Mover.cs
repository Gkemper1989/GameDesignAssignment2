using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float turnSpeed = 150.0f;
    private float horizontalInput;
    private float forwardInput;

    //cache
    private Rigidbody playerRB;

    // Update is called once per frame
    void Update()
    {
        //input from the players
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        playerRB = GetComponent<Rigidbody>();
        MovePlayer();
    }

    void MovePlayer()
    {
        //moving and turning the vehicle
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
