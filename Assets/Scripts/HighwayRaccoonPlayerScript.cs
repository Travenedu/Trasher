using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighwayRaccoonPlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform playerTarget;
    public float frogSpeed;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTarget.position = transform.position; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.currentState == GameState.playing)
        {
            // Are we close enough
            if (Vector3.Distance(transform.position, playerTarget.position) < 0.05f)
            {
                // Set up offset vector
                Vector3 offsetVector = Vector3.zero;

                if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    offsetVector = Vector3.up;
                    animator.SetBool("Forward", true);
                    
                } else {
                    animator.SetBool("Forward", false);
                }

                if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    offsetVector = Vector3.down;
                    animator.SetBool("Backward", true);
                } else {
                    animator.SetBool("Backward", false);
                }

                if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    offsetVector = Vector3.left;
                    animator.SetBool("Left", true);
                } else {
                    animator.SetBool("Left", false);
                }

                if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    offsetVector = Vector3.right;
                    animator.SetBool("Right", true);
                } else {

                    animator.SetBool("Right", false);
                }

                if(offsetVector.magnitude > 0.05f)
                {
                    // Something was pressed
                    playerTarget.position += offsetVector;
                }
            }
            // animator.SetBool("Stopped", true);
            }


    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, playerTarget.position, (frogSpeed * Time.fixedDeltaTime)));
    }

}
