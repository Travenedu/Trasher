using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S; // singleton
    private AudioSource audio;
    public AudioClip gameSound;

    private void Awake(){
        S = this; // singleton declaration
    }
 
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayGameSound(){
        audio.PlayOneShot(gameSound);
    }

    public void StopSound(){
        audio.Stop();
    }

    // public void PlayCollisionSound(string soundTag){
        
    //     switch(soundTag){
    //         case "Brick":
    //             audio.PlayOneShot(brickSound);
    //             break;
            
    //         case "Wall":
    //             audio.PlayOneShot(wallSound);
    //             break;

    //         case "Paddle":
    //             audio.PlayOneShot(paddleSound);
    //             break;

    //         case "DeadWall":
    //             audio.PlayOneShot(deadSound);
    //             break;

    //         default:
    //             Debug.Log("Collision Sound - Unresolved String: " + soundTag);
    //             break;

    //     }
    // }
}
