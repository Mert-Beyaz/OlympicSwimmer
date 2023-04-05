using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Animator Animator;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        Animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Animator.SetBool("greatJump", true);
        }
        else
        {
            Animator.SetBool("greatJump", false);
            Animator.SetBool("isTreading", true);
        } 
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Animator.SetBool("badJump", true);
        }
        else
        {
            Animator.SetBool("badJump", false);
            Animator.SetBool("isTreading", true);
        }
    
        if (Input.GetKey(KeyCode.W))
        {
            Animator.SetBool("isSwimming",true);
            Animator.SetBool("isTreading", false);
        }
        else
        {
            Animator.SetBool("isSwimming", false);
            Animator.SetBool("isTreading", true);
        }
    }


}
