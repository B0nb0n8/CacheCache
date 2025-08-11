using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float turnSpeed = 50f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Vector3 move = transform.forward * moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
        rb.MoveRotation(rb.rotation * turn);
    }
}
