using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    private currentMoveSpeed controls;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public float currentMoveSpeed {get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;

                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0; 
            }
        } }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    [SerializeField]
    private bool _isRunning = false;
    private bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Assert.IsNotNull(rb);
       // Assert.IsNotNull();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x*currentMoveSpeed,rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;

        } else if (context.canceled)
        {
            IsRunning = false;
        }
    } 
}
