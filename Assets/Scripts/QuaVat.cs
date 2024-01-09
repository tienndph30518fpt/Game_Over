using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaVat : MonoBehaviour
{
    public float speed = 2f;
    private bool isMovingLeft = true;
  
  
    void Update()
    {
        if (isMovingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);//tốc độ không phụ thuộc vào FPS
        }
    }
        
    }

