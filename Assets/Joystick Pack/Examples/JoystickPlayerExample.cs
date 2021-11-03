using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    private float speed;
    public FixedJoystick variableJoystick;
    public Rigidbody rb;
    public Animator animator;

    public float Speed { get => speed; set => speed = value; }
    private void Update()
    {
        if (rb.velocity == Vector3.zero)
        {
            animator.SetBool("idlying", true);
            animator.SetBool("running", false);
        }
        else
        {
            animator.SetBool("running", true);
            animator.SetBool("idlying", false);
        }
        if (Input.GetMouseButton(0))
        {
            transform.LookAt(rb.velocity * 12);
        }
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.velocity = direction * Speed * Time.fixedDeltaTime;
        
    }
}