using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //SpriteRenderer spriteRenderer;
    Animator animator;
    private Vector3 dir;
    [SerializeField] float movementSpeed = 5.0f;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir.z = 0;

        transform.position += dir * Time.deltaTime * movementSpeed;

        animator.SetInteger("dirX", (int)dir.x);
        animator.SetInteger("dirY", (int)dir.y);

    }
}
