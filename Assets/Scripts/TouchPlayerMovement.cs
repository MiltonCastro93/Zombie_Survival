using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public float speed = 12f;

    private Vector3 velocity;
    public float gravity = -9.81f;

    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 2f;

    public Transform playerCamera;

    private int leftFingerID, rightFingerID;
    private float halfScreen;
    private Vector2 moveInput;
    private Vector2 moveTouchStartPosition;

    private Vector2 lookInput;
    [SerializeField]private float cameraSensibility;
    private float cameraPitch;

    private void Start()
    {
        leftFingerID = -1;
        rightFingerID = -1;
        halfScreen = Screen.width / 2f;
    }


    void Update()
    {
        GetTouchInput();

        if (leftFingerID != -1)
        {
            Move();
        }

        if (rightFingerID != -1)
        {
            LookAround();
        }
    }

    private void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x < halfScreen && leftFingerID == -1)
                {
                    leftFingerID = t.fingerId;
                    moveTouchStartPosition = t.position;
                }
                else if (t.position.x > halfScreen && rightFingerID == -1)
                {
                    rightFingerID = t.fingerId;
                }
            }
            if (t.phase == TouchPhase.Canceled)
            {

            }
            if (t.phase == TouchPhase.Moved)
            {
                if (leftFingerID == t.fingerId)
                {
                    moveInput = t.position - moveTouchStartPosition;
                }
                else if (rightFingerID == t.fingerId)
                {
                    lookInput = t.deltaPosition * cameraSensibility * Time.deltaTime;
                }

            }
            if (t.phase == TouchPhase.Stationary)
            {
                if (rightFingerID == t.fingerId)
                {
                    lookInput = Vector2.zero;
                }
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (leftFingerID == t.fingerId)
                {
                    leftFingerID = -1;
                }
                else if (rightFingerID == t.fingerId)
                {
                    rightFingerID = -1;
                }
            }
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        Vector3 move = transform.right * moveInput.normalized.x + transform.forward * moveInput.normalized.y;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void LookAround()
    {
        cameraPitch -= lookInput.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        transform.Rotate(Vector3.up, lookInput.x);
    }
}


/*
       float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
 */