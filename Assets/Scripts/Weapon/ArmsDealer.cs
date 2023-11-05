using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsDealer : MonoBehaviour
{
    public float interactionDistance = 3.0f;
    public GameObject armsDealerObject;
    public GameObject armsDealerMenu;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject == armsDealerObject)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    armsDealerMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            armsDealerMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
