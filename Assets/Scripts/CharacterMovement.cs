using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator characterAnimator;
    
    void Start()
    {
        characterAnimator = GetComponent<Animator>();

        characterAnimator.SetBool("isRunning",true);
    }
    
    void Update()
    {
        
    }
}
