using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    private bool shieldActive = false;
    private bool shootActive = false;
    private bool throwActive = false;

    [SerializeField] private float playerNumber = 1;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] private int HEALTH = 100;

    private TextMeshPro healthBar;
    [SerializeField] private GameObject text3d;



    private UnityEngine.KeyCode FORWARD;
    private UnityEngine.KeyCode RIGHT;
    private UnityEngine.KeyCode LEFT;
    private UnityEngine.KeyCode BACK;

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        healthBar = text3d.GetComponent<TextMeshPro>();
        
 
      if (playerNumber == 1){
        FORWARD = KeyCode.W;
        RIGHT = KeyCode.D;
        LEFT = KeyCode.A;
        BACK = KeyCode.S;
      }
      else if (playerNumber == 2){
        FORWARD = KeyCode.UpArrow;
        RIGHT = KeyCode.RightArrow;
        LEFT = KeyCode.LeftArrow;
        BACK = KeyCode.DownArrow;
      }
    }

    private void OnCollisionEnter(Collision other) {
        // RigidBody tag = other.gameObject.GetComponent<Rigidbody>();
        // print("COLLISION");
        // print(other.gameObject.name);
        // print(otherRB.velocity.magnitude);
        // HEALTH = HEALTH - 1;
        
    }

    private void OnTriggerEnter(Collider other) {
        // RigidBody tag = other.gameObject.GetComponent<Rigidbody>();
        print("from "+ $"{playerNumber}" + $"{other.gameObject.name}");
        if (other.gameObject.name == "FrontCollider"){
            HEALTH = HEALTH - 10;
        }
        // print(otherRB.velocity.magnitude);
        // HEALTH = HEALTH - 1;

        //activates pickups based on tag 
        if (other.gameObject.CompareTag("ShieldPickUp"))
        {
            other.gameObject.SetActive(false); //deavtivates the cube

            //activate the shield
            shieldActive = true;

            //activate shield animation

        }

        if (other.gameObject.CompareTag("shootPickUp"))
        {
            other.gameObject.SetActive(false); //deactivate cube

            //activate gun
            shootActive = true;
        }

        if (other.gameObject.CompareTag("ThrowPickUp"))
        {
            other.gameObject.SetActive(false); //deactivate cube

            //activate throw object
            throwActive = true;
        }

    }


    private void FixedUpdate()
    {
        healthBar.text = $"{HEALTH}";
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }


    private void GetInput()
    {
        
        float forwardComponent = 0;
        float backwardComponent = 0;
        float rightComponent = 0;
        float leftComponent = 0;

        if (Input.GetKey(RIGHT)){
            rightComponent = 1;
        }
        else {
            rightComponent = 0;
        }
        if (Input.GetKey(LEFT)){
            leftComponent = 1;
        }
        else {
            leftComponent = 0;
        }

        horizontalInput = rightComponent - leftComponent;


        if (Input.GetKey(FORWARD)){
            forwardComponent = 1;
            // print(rb.velocity.magnitude);
        }
        else {
            forwardComponent = 0;
        }
        if (Input.GetKey(BACK)){
            backwardComponent = 1;
        }
        else {
            backwardComponent = 0;
        }

        verticalInput = forwardComponent - backwardComponent;

        if (rb.velocity.magnitude > 5 && backwardComponent == 1){
            isBreaking = true;
        }
        else{
            isBreaking = false;
        }


        // isBreaking = Input.GetKey(BREAK);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}