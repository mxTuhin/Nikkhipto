using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float gravity = -40f;

    private CharacterController _characterController;

    private Vector3 velocity;

    [SerializeField] private LayerMask groundLayers;

    public bool isGrounded;

    private float horizontalMovement;
    private float verticalMovement;

    public float movementSpeed;
    public float walkSpeed;
    public float runSpeed;

    public float jumpHeight= 2.0f;
    private int jumpCounter = 0;
    public Transform[] groundChecks;
    public Transform[] blockChecks;
    
    private bool jumpPressed;
    private float jumpTimer;
    private float jumpGracePeriod = 0.2f;
    
    [HideInInspector]
    public Animator _animator;
    public bool animMove;


    public bool playerMoving = true;

    public float turnSmoothingTime = 0.1f;
    float turnSmoothVelocity;
    private bool runTrigger;

    public GameObject rifleInHand;

    public static PlayerController instance;

    public float health = 100;

    public bool isMoving;

    public bool inAction=false;

    public bool isShooting;

    public GameObject crosshair;
    // public Image lifeFront;
    public bool canBeAttacked = true;

    public Rig rig = null;
    private float rigPointSpeed = 1f;
    public GameObject rigLooker;
    private int targetValue;

    public GameObject largeMap;
    
    private AudioSource _audioSource;
    public AudioClip[] walkStepShot;

    private bool canWalkSound=true;
    // public GameObject gameOver;
    
    

    [SerializeField] private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        movementSpeed = walkSpeed;
        _characterController = GetComponent<CharacterController>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
        rifleInHand.SetActive(false);
        
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameOver) return;
        if(GameManager.instance.pauseMenuState) return;
        

        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            largeMap.SetActive(!largeMap.activeSelf);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = runSpeed;
            runTrigger=true;
            if (canWalkSound)
            {
                StartCoroutine(DoWalkSound(0.2f));
            }

            
        }
        else
        {
            movementSpeed = walkSpeed;
            runTrigger = false;
        }
        
        

        
        isGrounded = false;
        foreach (var groundCheck in groundChecks)
        {
            if (Physics.CheckSphere(groundCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore))
            {
                
                isGrounded = true;
                break;
            }
        }

        if (isGrounded && velocity.y < 0)
        {
            
            
            velocity.y = 0;
            jumpCounter = 0;
            playerMoving = true;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // var blocked = false;
        // foreach (var blockCheck in blockChecks)
        // {
        //     if (Physics.CheckSphere(blockCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore))
        //     {
        //         blocked = true;
        //         playerMoving = false;
        //         break;
        //     }
        // }

        jumpPressed = Input.GetButtonDown("Jump");
        if (jumpPressed)
        {
            walkSpeed = runSpeed = 0;
            jumpTimer = Time.time;
            _animator.SetTrigger("isJumping");
        }
        
        if ( jumpCounter<1 && (jumpPressed || (jumpTimer > 0 && Time.time<jumpTimer+jumpGracePeriod)))
        {
            
            StartCoroutine(jumpWaitToAlignAnimation());
            jumpCounter++;
            jumpTimer = -1;
        }
        
        
        Vector3 direction = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothingTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        if (direction.magnitude >= 0.1f)
        {
            // float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothingTime);
            // transform.rotation = Quaternion.Euler(0f, angle, 0f);

            

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            _characterController.Move(moveDir * movementSpeed * Time.deltaTime);
        }
        
        if (isGrounded && direction.magnitude >= 0.1f && _audioSource.isPlaying == false && !runTrigger)
        {
            _audioSource.volume = Random.Range(0.8f, 1);
            _audioSource.pitch = Random.Range(0.8f, 1.1f);
           
            _audioSource.Play();
        }
        


        _characterController.Move(velocity * Time.deltaTime);
        
        if ((horizontalMovement > 0.05f || horizontalMovement<-0.05) || (verticalMovement > 0.05 || verticalMovement < -0.05))
        {
            isMoving = true;
            _animator.SetBool("isMoving", true);
            if (runTrigger)
            {
                animateCharacter(1.0f, 1.0f);
            }
            else
            {
                animateCharacter(0f, 1.0f);
            }
            
            
        }

        // else if (horizontalMovement > 0.05f || horizontalMovement<-0.05)
        // {
        //     _animator.SetBool("isMoving", true);
        //     walkAnimate();;
        //     
        // }
        //
        // else if (verticalMovement > 0.05 || verticalMovement < -0.05)
        // {
        //     _animator.SetBool("isMoving", true);
        //     walkAnimate();
        // }
        else
        {
            isMoving = false;
            _animator.SetBool("isMoving", false);
        }

        if (animMove)
        {
            _animator.SetBool("isMoving", true);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetValue = targetValue == 0 ? 1 : 0;
            if (rifleInHand.activeSelf)
            {
                rifleInHand.SetActive(false);
                rifleAnimateCharacter(0.0f, -1.0f);
                inAction = false;
                crosshair.SetActive(false);
                

            }
            else
            {
                rifleInHand.SetActive(true);
                inAction = true;
                _animator.SetTrigger("inAction");
                rifleAnimateCharacter(0.0f, 1.0f);
                crosshair.SetActive(true);
                

            }
            
        }
        rig.weight = Mathf.MoveTowards(rig.weight, targetValue, rigPointSpeed * Time.deltaTime);

        if (inAction)
        {
            
            if (isMoving && movementSpeed == runSpeed)
            {
                rifleAnimateCharacter(5.0f, 5.0f);
            }
            else if (isMoving && movementSpeed == walkSpeed)
            {
                rifleAnimateCharacter(0.0f, 2.0f);
            }
            else
            {
                rifleAnimateCharacter(0.0f, 1.0f);
            }

            
            if(isShooting)
            {
                rifleAnimateCharacter(1.0f, 1.0f);
            }
            if (isShooting && isMoving)
            {
                rifleAnimateCharacter(5.0f, 5.0f);

            }
        }


    }

    
    
    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        
    }

    

    public void animateCharacter(float offset1, float offset2)
    {
        _animator.SetFloat("offset1", offset1, 0.05f, Time.deltaTime);
        _animator.SetFloat("offset2", offset2, 0.05f, Time.deltaTime);
    }

    public void rifleAnimateCharacter(float rifleOffset1, float rifleOffset2)
    {
        _animator.SetFloat("rifleOffset1", rifleOffset1);
        _animator.SetFloat("rifleOffset2", rifleOffset2);
    }
    
    public void animateCharacterAttack(float attack)
    {
        _animator.SetFloat("attack", attack);
    }

    private IEnumerator jumpWaitToAlignAnimation()
    {
        
        yield return new WaitForSeconds(0.5f);
        walkSpeed = runSpeed = 2;
        velocity.y += Mathf.Sqrt((jumpHeight * -1 * gravity));
        Invoke("setSpeedToIdeal", 1.0f);
    }

    private void setSpeedToIdeal()
    {
        if (inAction)
        {
            _animator.SetTrigger("inAction");
        }
        walkSpeed = 4;
        runSpeed = 8;
    }

    IEnumerator DoWalkSound(float speed)
    {
        _audioSource.volume = Random.Range(0.3f, 0.5f);
        _audioSource.pitch = Random.Range(0.5f, 0.8f);
        _audioSource.PlayOneShot(walkStepShot[UnityEngine.Random.Range(0, walkStepShot.Length)]);
        canWalkSound = false;
        yield return new WaitForSeconds(speed);
        canWalkSound = true;
    }

    // public void TakeDamage()
    // {
    //     if (canBeAttacked)
    //     {
    //         _animator.SetTrigger("isStunned");
    //         canBeAttacked = false;
    //         StartCoroutine(controlAttackAbosrb());
    //         health -= 10;
    //         lifeFront.fillAmount = health / 100;
    //         print(health);
    //     
    //         if (health <= 0)
    //         {
    //             health = 100;
    //             lifeFront.fillAmount = 100;
    //             gameOver.SetActive(true);
    //             StartCoroutine(deactivateGameOver());
    //         }
    //     }
    //     
    // }
    //
    //
    //
    // IEnumerator controlAttackAbosrb()
    // {
    //     yield return new WaitForSeconds(1.0f);
    //     canBeAttacked = true;
    // }
    //
    // IEnumerator deactivateGameOver()
    // {
    //     yield return new WaitForSeconds(2.0f);
    //     gameOver.SetActive(false);
    // }
}
