using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float obstacleSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 movePosition = transform.position + (Vector3.right * obstacleSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePosition);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.transform.tag == "Player"){
            Destroy(collision.gameObject);
            Debug.Log("Dead Raccoon");

            // Tell Game Manager our raccoon got hit
            GameManager.S.RaccoonOutOfPlay();
        }



        
    }
}
