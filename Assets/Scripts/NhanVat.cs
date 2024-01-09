using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NhanVat : MonoBehaviour
{

    public float initialSpeed = 4.0f;
    public float maxSpeed = 88.0f;
    public float speedIncreaseRate = 2.0f; // Tốc độ tăng mỗi giây
    public float speedDecreaseAmount = 1f; // Tốc độ giảm khi va chạm với quai vat

    private float currentSpeed;
    
    public Animator anim;

    private Rigidbody2D rb;

    private bool isFacingRight = true;
    private bool canJump = false; // Biến này xác định xem nhân vật có thể nhảy không

    private bool isRight = true;

    // máu nhân vật
    public Slider sliderMau;
    private float tongMau =100f;
    
    // điểm Nhân Vật
    public TextMeshProUGUI diemText;
    private int tongDiem = 0;
    
    // hiển thị lên màn thua
    public GameObject OverGame;
    
    // thanh máu nhiều màu
    public Gradient Gradient;// hiển thị màu theo lượng máu
    public Image fillcoler;// backgruond hiển thị máu
    
    
    public float speed = 4.0f;// tốc độ chạy
    private Vector2 direction = Vector2.right; // Mặc định di chuyển sang phải

    // thay đổi trang phục
    public GameObject hat;
    private int style;
    public GameObject circle;
    private bool checkedJump = false;
    private void Awake()
    {
        // lấy style được lưu trong prefabs ra để kích hoạt style
        style = PlayerPrefs.GetInt("style", -1);
        
        if (style == 0)
        {
                
        }else if (style == 1)
        {
            hat.SetActive(true);
        }else if (style == 2)
        {
            circle.SetActive(true);
        }
         
    }
    // tính tông điểm
    void tinhTong(int score)
    {
        tongDiem += score;
        diemText.text = "Điểm: " + tongDiem;
        PlayerPrefs.SetInt("diem", tongDiem); // Lưu điểm số vào PlayerPrefs
    }
    void Start()
    {
        
        
        currentSpeed = initialSpeed;
        StartCoroutine(IncreaseSpeedCoroutine());
     
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;

        tongDiem = PlayerPrefs.GetInt("diem", 0);

        // Cập nhật TextMeshProUGUI với điểm số đã lưu
        diemText.text = "Điểm: " + tongDiem;
        
        
        if (PlayerPrefs.HasKey("mauHienTai"))
        {
            tongMau = PlayerPrefs.GetFloat("mauHienTai");
            sliderMau.value = tongMau;
        }
      
        
        
        // kiểm tra lưu tên nhân vật

        sliderMau.interactable = false;
    }

    
    private IEnumerator IncreaseSpeedCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Đợi 1 giây

            // Tăng tốc độ hiện tại
            currentSpeed += speedIncreaseRate;

            // Giới hạn tốc độ tối đa
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
  
        
        
       // direction = Vector2.right;

        // Run();
        // CheckJump();
      bullet();
     
     // Thiết lập việc di chuyển tự động về phải
     float move = 1.0f; // Điều chỉnh tốc độ di chuyển 
     rb.velocity = new Vector2(move * currentSpeed, rb.velocity.y);
     anim.SetFloat("run", 1);
     // Kiểm tra và nhảy khi nhấn phím Space
     if (canJump && Input.GetKeyDown(KeyCode.Space))
     {
         Debug.Log("Đã Nhấn Nhảy");
         anim.SetFloat("jump", 1);
         rb.velocity = new Vector2(rb.velocity.x, 10f); // Điều chỉnh độ cao của nhảy ở đây
         canJump = false; // Ngăn việc nhảy liên tục khi giữ phím
     }
     else
     {
         anim.SetFloat("jump", 0);
     }
     fillcoler.color = Gradient.Evaluate(sliderMau.normalizedValue);// hiển thị máu theo màu
   
    }

    // tăng tốc độ chạy
    void Move(float speed)
    {
        // Xử lý di chuyển ở đây sử dụng tốc độ được truyền vào
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * move * speed * Time.deltaTime);

        // Cập nhật giá trị Speed trong Animator (nếu cần)
        anim.SetFloat("run", Mathf.Abs(move * speed));
    }
    
    
    
    // nhảy bằng phím để buil lên android

    public void JumpUp()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            canJump = false;
        }
       
    }
    
    
    
    
// chạy trong game
    void Run()
    {
        fillcoler.color = Gradient.Evaluate(sliderMau.normalizedValue);// cập nhật hiển thị máu theo màu
        float move = Input.GetAxis("Horizontal");
    
        if (move<0 && isFacingRight)
        {
            Flip();
        }else if (move>0 && !isFacingRight )
        {
            Flip();
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            isRight = false;
            anim.SetFloat("run", 1);
        }else if (Input.GetKey(KeyCode.D))
        {
            isRight = true;
            anim.SetFloat("run", 1);
        }
        else
        {
            anim.SetFloat("run", 0);
        }
    
        rb.velocity = new Vector2(move * 4, rb.velocity.y);
    
    }

    
// bắn đạn 

    void bullet()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Tính toán vị trí mới của viên đạn dựa trên vị trí hiện tại và hướng của nhân vật
            var x = transform.position.x + (isRight ? 0.5f : -0.5f);// Tính toán vị trí X của viên đạn dựa trên hướng nhân vật
            var y = transform.position.y;// Giữ nguyên vị trí Y của nhân vật
            var z = transform.position.z;
            // Tạo viên đạn mới tại vị trí được tính toán và không quay đối tượng viên đạn
            GameObject newBullet = Instantiate(Resources.Load("Prefabs/bullet"), new Vector3(x, y, z), Quaternion.identity) as GameObject;
            // Lấy script của viên đạn để cài đặt hướng ban đầu cho viên đạn
            BulletScripts bulletScript = newBullet.GetComponent<BulletScripts>();
            // Nếu viên đạn không có script, thêm script mới và lấy ra
            if (bulletScript == null)
            {
                bulletScript = newBullet.AddComponent<BulletScripts>();
            }
            // Thiết lập hướng cho viên đạn thông qua script
            bulletScript.setIsRight(isRight);
        }
    }

    public void bulletUp()
    {
        var x = transform.position.x + (isRight ? 0.5f : -0.5f);
        var y = transform.position.y;
        var z = transform.position.z;
    
        GameObject newBullet = Instantiate(Resources.Load("Prefabs/bullet"), new Vector3(x, y, z), Quaternion.identity) as GameObject;
    
        BulletScripts bulletScript = newBullet.GetComponent<BulletScripts>();
        if (bulletScript == null)
        {
            bulletScript = newBullet.AddComponent<BulletScripts>();
        }
            
        bulletScript.setIsRight(isRight);
    }
    
// nhảy trong game
    void CheckJump()
    {
        //
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Đã Nhấn Nhảy");
            anim.SetFloat("jump", 1);
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            canJump = false; // Ngăn việc nhảy liên tục khi giữ phím
        }
        else
        {
          
            anim.SetFloat("jump", 0);
        }
    }
    // hướng di chuyển
    void Flip()
    {
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
       
        if (other.gameObject.CompareTag("matdat"))
        {
            canJump = true; // Cho phép nhảy khi va chạm với "matdat"
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("matdat"))
        {
            canJump = false; // Ngăn không cho nhảy khi không còn va chạm với "matdat"
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("quaivat"))
        {   
            
            // Giảm tốc độ khi va chạm với tag "quaivat"
            currentSpeed -= speedDecreaseAmount;

            // Giới hạn tốc độ tối thiểu
            currentSpeed = Mathf.Max(currentSpeed, 4f);
            
            
            
            tongMau -= 20;
            sliderMau.value = tongMau;
            PlayerPrefs.SetFloat("mauHienTai", tongMau);
            
            if (tongMau<=0)
            {
                tongDiem = 0;
                // Lưu điểm số vào PlayerPrefs
                PlayerPrefs.SetInt("diem", tongDiem);
                // Cập nhật TextMeshProUGUI với điểm số đã đặt lại
                diemText.text = "Điểm: " + tongDiem;
            
                PlayerPrefs.DeleteKey("mauHienTai"); // Lưu trạng thái máu hiện tại vào PlayerPrefs
                // playerHeartSlider.value = mauToiDa;
                OverGame.SetActive(true);
                // chạm vào trái thì sẽ chết
                Time.timeScale = 0; // dừng lại sence;
               
            }

        }

        else if (other.gameObject.tag=="coin")
        {
           
          tinhTong(5);
          Destroy(other.gameObject,0.3f);

        } else if (other.gameObject.tag == "mau")
        {
            tongMau += 20;
            if (tongMau > 100)
            {
                tongMau = 100; // Đặt lại giá trị tongMau về 100 nếu nó vượt quá 100
            }
        
            sliderMau.value = tongMau;
            Destroy(other.gameObject, 0.5f);
        }
    }
}
