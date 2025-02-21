using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float gravity = 10f;
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Transform rayCastOrigin;
    private RaycastHit2D Hit2D;
    [SerializeField] private Collider2D m_CrouchDisableCollider;

    public Vector2 inputDirection,lookDirection;
    Animator anim;

    Vector3 touchStart, touchEnd;
    public Image dpad;
    public float dpadRadius;

    private Rigidbody2D m_Rigidbody2D;

    Touch theTouch;

    private RespawnController respawn;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        //getting input from keyboard controls
        calculateDesktopInputs();
        //calculateMobileInput();
        //calculateTouchInputs();

        //sets up the animator
        animationSetup();

        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }
    void calculateDesktopInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float inputDirectionY = inputDirection.y;

        inputDirection = new Vector2(x, 0).normalized;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            attack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }
    void calculateMobileInput()
    {
        if (Input.touchCount > 0)
        {
            dpad.gameObject.SetActive(true);
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStart = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)

            touchEnd = theTouch.position;

            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            inputDirection = new Vector2(x, y).normalized;

            if((touchEnd - touchStart).magnitude > dpadRadius)
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
            }

            else
            {
                dpad.transform.position = touchEnd;
            }
        }

        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
        }
    }

    void calculateTouchInputs()
    {
        if (Input.GetMouseButton(0))
        {
            dpad.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
            }

            touchEnd = Input.mousePosition;

            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            inputDirection = new Vector2(x, y).normalized;

            if ((touchEnd - touchStart).magnitude > dpadRadius)
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
            }

            else
            {
                dpad.transform.position = touchEnd;
            }
        }

        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
        }
    }
    void animationSetup()
    {
        //checking if the player wants to move the character or not
        if (inputDirection.magnitude > 0.1f)
        {
            //changes look direction only when the player is moving, so that we remember the last direction the player was moving in
            lookDirection = inputDirection;

            //sets "isWalking" true. this triggers the walking blend tree
            anim.SetBool("isWalking", true);
        }
        else
        {
            // sets "isWalking" false. this triggers the idle blend tree
            anim.SetBool("isWalking", false);
        }

        //sets the values for input and lookdirection. this determines what animation to play in a blend tree
        anim.SetFloat("inputX", lookDirection.x);
        anim.SetFloat("inputY", lookDirection.y);
        anim.SetFloat("lookX", lookDirection.x);
        anim.SetFloat("lookY", lookDirection.y);
    }

    public void attack()
    {
        anim.SetTrigger("Attack");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision is true.");
    }

    private void GroundCheckMethod()
    {

    }
}
