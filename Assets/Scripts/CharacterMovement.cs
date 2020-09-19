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
    bool isGrounded;

    //Zıplama başladığında true olur.
    bool jumpUp;
    //Düşüş başladığında true olur.
    bool fallingDown;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        //Animatörde bulunan isRunning bool'u true yapıldı yani koşma animasyonu aktif.
        characterAnimator.SetBool("isRunning",true);
    }
    
    void Update()
    {
      
    }
    
    void Jump()
    {
        isGrounded = true;

        do
        {

        } while (transform.position.y >= jumpMaxHeight );


    }
}
