using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour
{
    public Transform body;

    private void Update()
    {
        transform.position = body.position;
    }
}
