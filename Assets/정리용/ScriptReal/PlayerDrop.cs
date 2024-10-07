using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : MonoBehaviour
{
    public SceneManagerScript sceneManagerScript;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sceneManagerScript.GoRetryScene();
        }
    }
}
