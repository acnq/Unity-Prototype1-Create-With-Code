using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float horsePower = 20.0f;
    [SerializeField] float turnSpeed = 45.0f;
    private float horizontallInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;

    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] float speed;

    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // playerRb.centerOfMass = centerOfMass.transform.position; // 添加重心反而容易弹飞
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Read the keyboard Input
        horizontallInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            // Move the vehicle forward (based on vertical Input)
            // transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);


            // Rotates the car (based on horizontal input, Make the vehicle to turn left and right)
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontallInput);

            // Output Speed
            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f); // 3.6 for kph
            speedometerText.SetText("Speed: " + speed + "mph");

            // Output RPM
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }

        
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;   
            }
        }
        if (wheelsOnGround == 4)
        {
            Debug.Log("Wheel on Ground");
            return true;    
        }
        else
        {
            Debug.Log("Wheel not on Ground");
            return false;
        }
    }
}
