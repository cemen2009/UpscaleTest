using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;

    float verticalInput, horizontalInput;

    Vector3 movementDirection;
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] AudioClip walkingSFX, runningSFX;

    [Header("Movement")]
    [SerializeField] float movingSpeed;
    [SerializeField] float sprintSpeedMultiplier;
    bool isSprinting;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameFlow) return;

        if (Input.GetKey(KeyCode.LeftShift)) isSprinting = true;
        else isSprinting = false;

        GetInput();
        SpeedLimit();
    }

    private void FixedUpdate()
    {
        ApplyInput();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void ApplyInput()
    {
        // calculating movement direction
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (movementDirection == Vector3.zero)
            audioSource.Stop();

        if (GameManager.Instance.gameState != GameState.GameFlow)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            return;
        }

        if (isSprinting)
        {
            if (!audioSource.isPlaying || audioSource.clip != runningSFX)
            {
                audioSource.clip = runningSFX;
                audioSource.volume = 1f;
                audioSource.Play();
            }

            rb.AddForce(movementDirection.normalized * movingSpeed * sprintSpeedMultiplier, ForceMode.Force);
        }
        else
        {
            if (!audioSource.isPlaying || audioSource.clip != walkingSFX)
            {
                audioSource.clip = walkingSFX;
                audioSource.volume = .1f;
                audioSource.Play();
            }

            rb.AddForce(movementDirection.normalized * movingSpeed, ForceMode.Force);
        }
    }

    private void SpeedLimit()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // if velocity is greater than should be - limit it
        if (flatVelocity.magnitude > movingSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movingSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
