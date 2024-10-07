using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject fractured;

    public void BreakTheThing()
    {
        Instantiate(fractured, transform.position, transform.rotation);
        
    }
}
