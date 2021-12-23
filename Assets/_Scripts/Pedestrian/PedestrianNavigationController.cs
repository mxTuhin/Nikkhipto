using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianNavigationController : MonoBehaviour
{
    public Vector3 destination;

    public bool reachedDestination;

    public float stopDistance=2.5f;

    public float rotationSpeed=120;

    public float movementSpeed=4;

    public bool walk=false;

    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination && walk)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward*movementSpeed*Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
            _animator.SetBool("isWalking", true);
            _animator.SetFloat("offset", 0f);
        }
        
    }

    public void SetDestination(Vector3 destination)
    {
        print("Destination Set");
        this.destination = destination;
        reachedDestination = false;
        walk = true;
    }
    
}
