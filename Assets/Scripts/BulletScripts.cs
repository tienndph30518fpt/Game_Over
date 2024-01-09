using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletScripts : MonoBehaviour
{
    // điểm Nhân Vật
    

    private bool isRight;
    
    void Start()
    {
      

        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((isRight ? Vector3.right: Vector3.left) *Time.deltaTime*5f);
    }

    public void setIsRight(bool isRight)
    {
        this.isRight = isRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("quaivat"))
        {
            Destroy(other.gameObject); // Biến mất quái vật
            Destroy(gameObject); // Biến mất đạn
        
        }
    }
   
}
