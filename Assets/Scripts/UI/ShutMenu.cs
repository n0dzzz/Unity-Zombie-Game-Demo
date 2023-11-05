using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShutMenu : MonoBehaviour
{
    public GameObject menu;

    public void shutMenu()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
