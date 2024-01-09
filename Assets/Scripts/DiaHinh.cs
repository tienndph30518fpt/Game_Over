using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiaHinh : MonoBehaviour
{
    public Transform player;// lưu thông tin vị trí nhân vật
    public float currentDis = 0f;//vị trí của đối tượng hiện tại và vị trí của người chơi
    public float limitDis = 50f;// ngưỡng đạt quá số này tự tạo địa hình mới
    public float respawDis = 66f;// tái sinh địa hình tai điểm

    
    public GameObject type1EnemyPrefab; // Prefab của loại quái vật Type 1
    public GameObject type2EnemyPrefab; // Prefab của loại quái vật Type 2
    public GameObject type3EnemyPrefab; // Prefab của loại quái vật Type 3
    protected void FixedUpdate()
    {
        this.GetDistance();
        this.Spawing();
    }
    
    
    protected void Spawing()
    {
        if (this.currentDis < this.limitDis) return;

        int numberOfEnemies = 3; // Số lượng quái vật mỗi lần sinh (ví dụ: 5)

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Chọn một loại quái vật ngẫu nhiên từ ba loại bạn đã khai báo
            GameObject enemyPrefab = GetRandomEnemyPrefab();

            if (enemyPrefab != null)
            {
                // Lấy vị trí của camera
                Camera mainCamera = Camera.main;
                float distanceFromCamera = 20f; // Khoảng cách từ camera

                // float distanceFromCamera = mainCamera.transform.position.y - mainCamera.nearClipPlane;

                // Tạo vị trí x ngẫu nhiên trước camera
                float spawnPosX = UnityEngine.Random.Range(mainCamera.transform.position.x+14f, mainCamera.transform.position.x + distanceFromCamera);
             
                // vị trí ngẫu nhiên của trục y
                float spawnPosY = UnityEngine.Random.Range(-0.5f, -2.0f);

                // Tạo vị trí spawn mới dựa trên spawnPosX và spawnPosY
                Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0f);

                // Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            
                // Hủy đối tượng quái vật sau 5 giây
                Destroy(enemy, 6f);
            }
        }

        // Di chuyển vị trí của DiaHinh
        Vector3 pos = transform.position;
        pos.x += this.respawDis;
        transform.position = pos;
    }

    
    
    // Phương thức này tính toán và cập nhật khoảng cách giữa
    // vị trí của người chơi và vị trí của đối tượng hiện tại
    protected virtual void GetDistance()
    {
        this.currentDis = this.player.position.x - transform.position.x;
    }
    
    // Phương thức để lấy Prefab của quái vật ngẫu nhiên từ ba loại đã khai báo
    private GameObject GetRandomEnemyPrefab()
    {
        // Tạo một danh sách các Prefab
        List<GameObject> enemyPrefabs = new List<GameObject>
        {
            type1EnemyPrefab,
            type2EnemyPrefab,
            type3EnemyPrefab
        };

        // Chọn ngẫu nhiên một Prefab từ danh sách
        int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[randomIndex];
    }
   
}
