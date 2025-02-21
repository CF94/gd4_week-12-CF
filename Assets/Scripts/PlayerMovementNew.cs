using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    public CharacterController2D controller;

    public float moveSpeed = 40f;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;

    public Vector2 inputDirection, lookDirection;
    Animator anim;

    private Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        lookDirection = new Vector2(0, -1);
    }
    void Update()
    {
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //verticalMove = Input.GetAxisRaw("Vertical");

        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }
    void calculateDesktopInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float inputDirectionY = inputDirection.y;

        inputDirection = new Vector2(x, 0).normalized;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //attack();
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
        */
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
