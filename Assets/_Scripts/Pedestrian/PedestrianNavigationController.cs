using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianNavigationController : MonoBehaviour
{
    public Vector3 destination;

    public bool reachedDestination;

    public float stopDistance=2.5f;

    public float rotationSpeed=120;

    private float movementSpeed;

    public bool walk=false;

    public bool isPeds;
    public bool isVehicle;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        if (isPeds && !isVehicle)
        {
            _animator = GetComponentInChildren<Animator>();
        }
        if(isPeds)
        {
            movementSpeed = Random.Range(1, 2);
        }
        else if (isVehicle)
        {
            movementSpeed = Random.Range(5, 8);
        }
        
        
        

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

            if (isPeds && !isVehicle)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetFloat("offset", 0f);
            }
            
        }
        
    }

    public void SetDestination(Vector3 destination)
    {
        // print("Destination Set");
        this.destination = destination;
        reachedDestination = false;
        walk = true;
    }
    
}
