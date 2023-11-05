using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    
    public GameManager gameManager;
    public static PlayerController Instance { get; private set; }
    public float playerHp = 150.0f;
    public float playerMoney = 500.0f;
    
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;

    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;
    public CharacterController characterController;
    
    public GameObject playerObject;

    private void Awake()
    {
        // Ensure that only one instance of PlayerController exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerObject = GameObject.Find("Player");    

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HealthCheck();
        changeWeapons();

        // Rotation
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        characterController.Move(speed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = 5f;
            }
        }
    }

    void changeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            secondaryWeapon.SetActive(true);
            primaryWeapon.SetActive(false);
        }
    }

    public void Die()
    {
        //playerObject.SetActive(false);

        playerHp = 100;
        playerMoney = 500;
        // death logic

        Invoke("RestartGame", 2.0f);
    }
    
    private void RestartGame()
    {     
        gameManager.RestartGame();
    }
    
    void HealthCheck()
    {
        if (playerHp <= 0) 
        {
            Die();
        }
    }
}