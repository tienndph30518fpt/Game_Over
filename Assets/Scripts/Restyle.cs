using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restyle : MonoBehaviour
{
    //các GameObject để thay đổi giao diện
    public GameObject Player;
    public GameObject Player1;
    public GameObject Player2;

    
    // Phương thức để thay đổi giao diện
    public void ReStyle()
    {
        if (Player.activeInHierarchy)
        {
            Player.SetActive(false);
            Player1.SetActive(true);
            Player2.SetActive(false);
        }else if (Player2.activeInHierarchy)
        {
            
            Player.SetActive(true);
            Player1.SetActive(false);
            Player2.SetActive(false);
        }else if (Player1.activeInHierarchy)
        {
            Player.SetActive(false);
            Player1.SetActive(false);
            Player2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Lưu trạng thái giao diện hiện tại vào PlayerPrefs (cơ chế lưu trữ dữ liệu tạm thời)
        if (Player.activeInHierarchy)
        {
            PlayerPrefs.SetInt("style",0);
        }else if (Player2.activeInHierarchy)
        {
            PlayerPrefs.SetInt("style",2);
        }else if (Player1.activeInHierarchy)
        {
            PlayerPrefs.SetInt("style",1);
        }
    }

    
    // Awake được gọi khi GameObject được khởi tạo
    private void Awake()
    {
        // Lấy trạng thái giao diện cuối cùng từ PlayerPrefs (nếu có)
        int style = PlayerPrefs.GetInt("style", -1);
        // Dựa vào trạng thái, kích hoạt giao diện tương ứng
        if (style == 0)
        {
            Player.SetActive(true);
            Player1.SetActive(false);
            Player2.SetActive(false);  
        }else if (style == 1)
        {
            Player.SetActive(false);
            Player1.SetActive(true);
            Player2.SetActive(false);
        }else if (style == 2)
        {
            Player.SetActive(false);
            Player1.SetActive(false);
            Player2.SetActive(true);
        }
    }
}