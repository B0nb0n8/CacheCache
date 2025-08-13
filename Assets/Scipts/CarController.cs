using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] wayPoints;
    [SerializeField] private float speed = 3f;
    private float chekpointDistance = 0.5f;
    private int currentChekpoint = 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveCar();

        if (Vector3.Distance(gameObject.transform.position, wayPoints[currentChekpoint].position) <= chekpointDistance)
        {
            TurnCar();
        }
    }

    private void MoveCar()
    {
        Vector3 Target = wayPoints[currentChekpoint].position;
        Vector3 newPos = Vector3.MoveTowards(gameObject.transform.position, Target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    private void TurnCar()
    {
        currentChekpoint++;
        if (currentChekpoint >= wayPoints.Length)
        {
            currentChekpoint = 0;
        }

        Transform target = wayPoints[currentChekpoint].transform;
        transform.LookAt(target);
    }

}
