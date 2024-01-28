using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    private Vector3 dir;
    SpriteRenderer sr;
    [SerializeField] float movementSpeed = 5.0f;
    bool slashing;
    void Awake()
    {
        animator = GetComponent<Animator>();
        dir = Vector3.zero;
        sr = GetComponent<SpriteRenderer>();
        slashing = false;
    }

    void Update()
    {
        // set direction based on player input
        // if no input, no direction. if no direction, no movement
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir.z = 0;
        if (dir.x > 0)
        {
            sr.flipX = true;

        }
        else
        {
            sr.flipX = false;

        }
        // move player based on direction
        transform.position += dir * Time.deltaTime * movementSpeed;

        animator.SetInteger("dirX", (int)dir.x);
        animator.SetInteger("dirY", (int)dir.y);
        // change animation accordingly
        // i hate unity animator so i did it in code
        //UpdateAnimation();

        //damage prosp when pressing the appropriate key
        if (Input.GetKeyDown(KeyCode.C) && !slashing)
            DamageProps();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                slashing = false;
            }
        }
    }
    void DamageProps()
    {
        // ensure other animiations dont replay
        slashing = true;
        // set animation appropriately
        animator.CrossFade("Slash", 0.1f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Furniture"))
            {
                collider.gameObject.GetComponent<DamageableProp>().DamageProp(gameObject);
            }
        }
    }
    void UpdateAnimation()
    {
        if (dir.y != 0)
        {
            if (dir.y > 0)
            {
                SwapAnimation("MoveUp");
               
            }
            else
            {
                SwapAnimation("MoveDown");
               
            }
        }
        else if (dir.x != 0)
        {
            SwapAnimation("MoveSide");
            if (dir.x > 0)
            {
                sr.flipX = true;
               
            }
            else
            {
                sr.flipX = false;
               
            }
        }
        else
        {
            if (!slashing)
                SwapAnimation("Idle");
        }
    }

    void SwapAnimation(string animName)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            animator.CrossFade(animName, 0.5f);
            Debug.Log(animName);
        }
    }
}
