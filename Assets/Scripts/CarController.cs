using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{

    // driving controls

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

    private UnityEngine.KeyCode FORWARD;
    private UnityEngine.KeyCode RIGHT;
    private UnityEngine.KeyCode LEFT;
    private UnityEngine.KeyCode BACK;

    // health stuff
    [SerializeField] public int HEALTH = 100;
    private TextMeshPro healthBar;
    [SerializeField] private GameObject healthBarGameObject;

    // camera stuff
    [SerializeField] private Camera cameraComponent;

    // hats
    private string hatName;
    private GameObject hat;
    public GameObject hatPoint;

    // pickups
    private UnityEngine.KeyCode USE_PICKUP;

    // gun
    private bool gunIsEquipped;
    private string gunGameObjectName = "PickupItems/gun";
    private GameObject gunObject;
    private GunController gunScript;
    public GameObject gunPoint;


    // rigid body
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        healthBar = healthBarGameObject.GetComponent<TextMeshPro>();

        PlayerSettings();
        WearHat();
    }

    private void PlayerSettings(){
      if (playerNumber == 1){
        FORWARD = KeyCode.W;
        RIGHT = KeyCode.D;
        LEFT = KeyCode.A;
        BACK = KeyCode.S;
        USE_PICKUP = KeyCode.Q;

        cameraComponent.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);

        hatName = "Hats/MagicianHat";
        // TODO: uncomment
        //hatName = PlayerPrefs.GetString("player1hat");
      }
      else if (playerNumber == 2){
        FORWARD = KeyCode.UpArrow;
        RIGHT = KeyCode.RightArrow;
        LEFT = KeyCode.LeftArrow;
        BACK = KeyCode.DownArrow;
        USE_PICKUP = KeyCode.RightShift;

        cameraComponent.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        cameraComponent.GetComponent<AudioListener>().enabled = false ;
        hatName = "Hats/CowboyHat";
        // TODO: uncomment
        //hatName = PlayerPrefs.GetString("player2hat");
      }
    }

    private void FixedUpdate()
    {
        healthBar.text = $"{HEALTH}";
        GetUsePickUpInput();
        GetDrivingInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }


    private void OnCollisionEnter(Collision other) {
    
    }



    private void GetUsePickUpInput(){
        if (Input.GetKey(USE_PICKUP) && gunIsEquipped){
            gunScript.ShootGun();
        }
    }

    private void OnTriggerEnter(Collider other) {


        //checks for shield
        if (shieldActive) {
            //do not decrease health index
            //do not pick up anything else
        }
        if (other.gameObject.tag == "gunPickUp"){
            other.gameObject.SetActive(false);
            EquipGun();
        }  
        else if(shootActive || throwActive) //check if any weapons are already active
        {
            //do not pick anything
            //RigidBody tag = other.gameObject.GetComponent<Rigidbody>();
            print("from " + $"{playerNumber}" + $"{other.gameObject.name}");
            if (other.gameObject.name == "FrontCollider")
            {
                HEALTH = HEALTH - 10;
            }
            // print(otherRB.velocity.magnitude);
            // HEALTH = HEALTH - 1;
        }
        else if (!shootActive && !throwActive && !shieldActive)
        {

            // RigidBody tag = other.gameObject.GetComponent<Rigidbody>();
            print("from " + $"{playerNumber}" + $"{other.gameObject.name}");
            if (other.gameObject.name == "FrontCollider")
            {
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

                //deactivate after 3 seconds
                StartCoroutine(PickupActive());
            }

            if (other.gameObject.CompareTag("shootPickUp"))
            {
                other.gameObject.SetActive(false); //deactivate cube

                //activate gun
                shootActive = true;

                //deactivate after 3 seconds
                StartCoroutine(PickupActive());
            }

            if (other.gameObject.CompareTag("ThrowPickUp"))
            {
                other.gameObject.SetActive(false); //deactivate cube

                //activate throw object
                throwActive = true;

                //deactivate after 3 seconds
                StartCoroutine(PickupActive());
            }
        }
    }

    IEnumerator PickupActive()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        shootActive = false;
        throwActive = false;
        shieldActive = false;

    }


    private void FixedUpdate()
    {
        healthBar.text = $"{HEALTH}";
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }


    private void GetDrivingInput()
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
    }


    // pickups
    private void EquipGun(){
        gunObject = Instantiate(Resources.Load(gunGameObjectName, typeof(GameObject))) as GameObject;
        gunObject.transform.position = gunPoint.transform.position;
        gunObject.transform.rotation = gunPoint.transform.rotation;
        gunObject.transform.parent = transform;
        gunScript = gunObject.GetComponent<GunController>();
        gunIsEquipped = true;
    }



    // customization
    private void WearHat(){
        if(hatName != ""){
            hat = Instantiate(Resources.Load(hatName, typeof(GameObject))) as GameObject;
            hat.transform.position = hatPoint.transform.position;
            hat.transform.rotation = hatPoint.transform.rotation;
            hat.transform.parent = transform;
        }
    }


    // driving helper functions
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
        Quaternion rot;       
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}