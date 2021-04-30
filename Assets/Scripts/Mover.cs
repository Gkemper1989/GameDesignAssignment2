using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //parameters
    [SerializeField] float moveSpeed = 10f;

    //cache
    private Rigidbody playerRB;

    // Update is called once per frame
    void Update()
    {
        playerRB = GetComponent<Rigidbody>();
        MovePlayer();
    }

    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xValue, 0, zValue);
    }
}
