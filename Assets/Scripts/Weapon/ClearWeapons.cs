using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChildDisabler
{
    public static void DisableChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}