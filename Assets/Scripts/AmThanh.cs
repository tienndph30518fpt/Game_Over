using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmThanh : MonoBehaviour
{
    public AudioClip file_am_thanh;
    private AudioSource audio_src;
    // Start is called before the first frame update
    void Start()
    {
        audio_src = GetComponent<AudioSource>();
        audio_src.clip = file_am_thanh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="nhanvat")
        {
            Debug.Log("Đã Va Chạm Vào Nhạc");
            audio_src.Play();
            Invoke("AudioStop", 0.5f);
           
        }
    }


    private void AudioStop()
    {
        audio_src.Stop();
    }
    
    
    
}
