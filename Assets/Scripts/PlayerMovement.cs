using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed;

  public float jumpForce;
  public Animator anim;

  private bool canJump; 
      // Start is called before the first frame update
      void Start()
      {
      }
      // Update is called once per frame
      void Update()
      {
    playerMove();
      }
    public void playerMove()
{
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    // Only move in one direction at a time
    if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
    {
        vertical = 0f;
    }
    else
    {
        horizontal = 0f;
    }

    Vector3 move = new Vector3(horizontal, 0f, vertical);
    anim.SetFloat("Vertical", vertical);
    anim.SetFloat("Horizontal", horizontal);
    move = Vector3.ClampMagnitude(move, 1);
    
    anim.SetBool("MovingLeft", horizontal < 0);
    anim.SetBool("MovingDown", vertical < 0);

    // If the player isn't moving, switch to the idle animation
    if (move == Vector3.zero) {
        anim.SetBool("MovingLeft", false);
        anim.SetBool("MovingDown", false);
    }

    // Calculate rotation based on movement direction
    Quaternion targetRotation;
    if (horizontal > 0)
    {
        targetRotation = Quaternion.Euler(0, 90, 0);
    }
    else if (horizontal < 0)
    {
        targetRotation = Quaternion.Euler(0, -90, 0);
    }
    else if (vertical > 0)
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
    }
    else if (vertical < 0)
    {
        targetRotation = Quaternion.Euler(0, 180, 0);
    }
    else
    {
        targetRotation = transform.rotation;
    }

    // Smoothly rotate player towards target rotation
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

    // Move player in direction of input
    transform.Translate(move * Time.deltaTime * moveSpeed, Space.World);

    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (canJump == true)
        {
            Rigidbody2D playerRB = transform.GetComponent<Rigidbody2D>();
            playerRB.AddForce(Vector2.up * jumpForce);
        }
    }
}
}