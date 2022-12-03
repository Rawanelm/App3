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

    public AudioClip shieldPickupSound;
    public AudioClip gunPickupSound;
    public AudioClip throwPickupSound;

    // health stuff
    [SerializeField] public int health = 100;

    // camera stuff
    [SerializeField] private Camera cameraComponent;

    //score stuff
    public int score = 0;

    // hats
    private string hatName;
    private GameObject hat;
    public GameObject hatPoint;

    // pickups
    private UnityEngine.KeyCode USE_PICKUP;

    // gun
    private string gunGameObjectName = "PickupItems/gun";
    private GameObject gunObject;
    private GunController gunScript;
    public GameObject gunPoint; 

    // throw (bomb)
    private string throwModelGameObjectName = "PickupItems/bombModel";
    private GameObject throwModelObject;
    private BombModelController throwModelScript;
    public GameObject throwPoint; 

    //booleans to check if any pickups are active
    private bool shieldActive = false;
    private bool gunActive = false;
    private bool throwActive = false;

    //shield stuff
    private string shieldGameObjectName = "Shield";
    private GameObject shield;
    private ShieldController shieldScript;
    public GameObject shieldPoint;


    // rigid body
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down;

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
        GetUsePickUpInput();
        GetDrivingInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }


    private void OnCollisionEnter(Collision other) {

        if (!shieldActive)
        {
            if (other.gameObject.tag == "bullet")
            {
                health = health - 1;
            }
        }
    }



    private void GetUsePickUpInput(){
        if (Input.GetKey(USE_PICKUP) && gunActive){
            gunScript.ShootGun();
        }
        if (Input.GetKey(USE_PICKUP) && throwActive){
            throwModelScript.Throw();
            RemoveThrow();
        }
    }

    private void OnTriggerEnter(Collider other) {
        print("from " + $"{playerNumber}" + $"{other.gameObject.name}");

        private void DetectBump(){
            if (other.gameObject.name == "FrontCollider")
            {
                health = health - 100;
            }
        }

        //checks for shield
        if (shieldActive) {
            //do not decrease health index
            //do not pick up anything else
        }
        else if(gunActive || throwActive) //check if any weapons are already active
        {
            //do not pick anything
            DetectBump()
        }
        else if (!gunActive && !throwActive && !shieldActive)
        {
            DetectBump();

            //activates pickups based on tag 
            if (other.gameObject.CompareTag("ShieldPickUp"))
            {
                AudioSource.PlayClipAtPoint(shieldPickupSound, transform.position, 1f);

                other.gameObject.SetActive(false); //deavtivates the cube

                //activate the shield
                EquipShield();

                //deactivate after 3 seconds
                StartCoroutine(DisablePickupAfterSeconds(1000));
            }

            if (other.gameObject.CompareTag("gunPickUp"))
            {
                AudioSource.PlayClipAtPoint(gunPickupSound, transform.position, 1f);

                other.gameObject.SetActive(false); //deactivate the pickup icon

                //activate gun
                EquipGun(); //equip gun
                
                //deactivate after 3 seconds
                StartCoroutine(DisablePickupAfterSeconds(1000));
            }

            if (other.gameObject.CompareTag("ThrowPickUp"))
            {
                AudioSource.PlayClipAtPoint(throwPickupSound, transform.position, 1f);

                other.gameObject.SetActive(false); //deactivate cube

                //activate throw object
                EquipThrow();
            }
        }
    }

    //resets booleans and disables pickup after three seconds
    IEnumerator DisablePickupAfterSeconds(int seconds)
    {
        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(seconds);

        if(gunActive){
            RemoveGun();
        }
        if(shieldActive){
            RemoveShield();
        }
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
        gunActive = true;
    }

    private void RemoveGun()
    {
        Destroy(gunObject);
        gunScript = null;
        gunActive = false;

    }

    //shield
    private void EquipShield()
    {
        shield = Instantiate(Resources.Load(shieldGameObjectName, typeof(GameObject))) as GameObject;
        shield.transform.position = shieldPoint.transform.position;
        shield.transform.rotation = shieldPoint.transform.rotation;
        shieldScript = shield.GetComponent<ShieldController>();
        shield.transform.parent = transform;
        shieldActive = true;
    }

    private void RemoveShield()
    {
        Destroy(shield);
        shieldActive = false;
    }

    private void EquipThrow(){
        throwModelObject = Instantiate(Resources.Load(throwModelGameObjectName, typeof(GameObject))) as GameObject;
        throwModelObject.transform.position = throwPoint.transform.position;
        throwModelObject.transform.rotation = throwPoint.transform.rotation;
        throwModelScript = throwModelObject.GetComponent<BombModelController>();
        throwModelObject.transform.parent = transform;
        throwActive = true;
    }

    // called after thrown
    private void RemoveThrow(){
        Destroy(throwModelObject);
        throwModelScript = null;
        throwActive = false;
    }

    public void AddPoint(){
        score = score + 1;
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