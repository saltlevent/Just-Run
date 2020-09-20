using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //
    //BİTMEDİ
    //
    [Tooltip("character constant position")]
    public Vector3 standardPosition = new Vector3(-6,-2,-5);
    [Tooltip("maximum jump position at y axis")]
    public float jumpMaxHeight = 1.5f;
    [Space]
    public float jumpSpeed;
    public float fallSpeed;
    [Space]

    Animator characterAnimator;

    //Karakterin yerde mi olduğunu kontrol eder.
    bool isGrounded = true;

    //Zıplama yapılabileceğinde true olur.
    bool jumpUp = true;
    //Düşüş başladığında true olur.
    bool fallingDown = false;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        //Animatörde bulunan isRunning bool'u true yapıldı yani koşma animasyonu aktif.
        characterAnimator.SetBool("isRunning",true);
    }
    
    void Update()
    {
#if UNITY_EDITOR
        //eğer space tuşuna basılırsa zıplama işlemi başlar
        if (Input.GetKeyDown(KeyCode.Space) || !isGrounded)
        {
            Jump();
        }
#elif UNITY_ANDROID
       
        // burası dokunmatik giriş için doldurulacak.

#endif
    }

    void Jump()
    {
        //karakterin yerde olmadığı belirtildi
        isGrounded = false;
        
        //Zıplama animasyonu etkinleştirildi.
        characterAnimator.SetBool("isJumping", true);

        //maksimum yüksekliğe ulaşana kadar konumu y ekseninde arttırıldı
        if (transform.position.y <= jumpMaxHeight && jumpUp)
        {
            transform.position += Vector3.up * Time.deltaTime * jumpSpeed;

            //eğer maksimum yüksekliğe ulaşılırsa düşüş başlayacak
            //düşüşün başlaması için jumpUp false, fallingDown true olmalı
            if (transform.position.y >= jumpMaxHeight)
            {
                jumpUp = false;
                fallingDown = true;
            }
        }
        else
        {
        //zemine (standardPosition) kadar konum y ekseninde azaltıldı
            transform.position += Vector3.down * Time.deltaTime * fallSpeed;

            
            if (transform.position.y <= standardPosition.y)
            {
                //zemine değdikten sonra zıplayabilmesi için jumpUp true edildi
                jumpUp = true;
                //düşme bittiği için fallingDown false edildi
                fallingDown = false;
                //yere değdiği için isGrounded true edildi
                isGrounded = true;
                //zıplama animasyonu durduruldu
                characterAnimator.SetBool("isJumping", false);
            }
        }
         
        
    }
}
