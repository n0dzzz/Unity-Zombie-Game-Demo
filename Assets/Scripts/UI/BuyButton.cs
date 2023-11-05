using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public GameObject targetGameObject; 

    public void OnDisableChildrenButtonClicked()
    {
        ChildDisabler.DisableChildren(targetGameObject);
    }
}
