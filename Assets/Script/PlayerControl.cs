using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    Vector2 move;
    
    [SerializeField] float horizontalSpeed = 20f;
    [SerializeField] float verticalSpeed = 20f;
    [SerializeField] float[] xRange = new float[2] { -10f, 10f };
    [SerializeField] List<float> yRange = new List<float> { -2f, 10f };
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlPitchFactor = 2f;
    [SerializeField] float controlRollFactor = 2f;

    [Header("Smooth Input")]
    [SerializeField] float smoothInputTime = 0.125f;
    Vector2 currentInput = Vector2.zero;
    Vector2 smoothInputVelocity = Vector2.zero;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        smoothInput();
        Rotate();
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    void smoothInput()
    {
        currentInput = Vector2.SmoothDamp(currentInput, move, ref smoothInputVelocity, smoothInputTime);
    }

    void Move()
    {
        float xOffset = currentInput.x * horizontalSpeed * Time.deltaTime;
        float yOffset = currentInput.y * verticalSpeed * Time.deltaTime;
        float xPosition = Mathf.Clamp(transform.localPosition.x + xOffset, xRange[0], xRange[1]);
        float yPosition = Mathf.Clamp(transform.localPosition.y + yOffset, yRange[0], yRange[1]);
        transform.localPosition = new Vector3(xPosition, yPosition, transform.localPosition.z);
    }

    void Rotate()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + currentInput.y * -controlPitchFactor;
        float yaw = transform.localPosition.x * -positionYawFactor;
        float roll = currentInput.x * -controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
