using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    private void Update()
        {
            // 이동 로직을 여기에 추가합니다.
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
    
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            transform.Translate(movement * Time.deltaTime * 5f);
        }
}
