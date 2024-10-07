using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public Rigidbody[] rb;
    float timer = 0;
    private bool isTime;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>5)
        {
            gameObject.SetActive(false);
        }
        else if(timer>2 && !isTime)
        {
            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].excludeLayers = ~0;
               
            }
            isTime = true;
        }
        
    }
}
