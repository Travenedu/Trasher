using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.transform.tag == "Player"){
            GameManager.S. RaccoonScore(100);
            Destroy(collision.gameObject);
            Debug.Log("Raccoon made it");
            GameManager.S.GoalReached();

        }

    }
}
