using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    // Public Variables
    enum LaneDirection { Left, Right};
    [SerializeField]
    private LaneDirection laneDirection = LaneDirection.Right;

    [HeaderAttribute("Spawn Positions")]

    [SerializeField]
    private Transform leftStartPosition;
    [SerializeField]
    private Transform rightStartPosition;

    [HeaderAttribute("Obstacle Info")]
    public GameObject obstaclePrefab;
    public float obstacleSpeed = 1.0f;
    public float minimumTimeBetweenObstacles = 1.0f;
    public int OneInXChance = 2;

    // Private Variables
    private Transform laneSpawn;

    private bool _isPlaying = false; 

    public bool playerArrived = false;


    // Start is called before the first frame update
    void Start()
    {
        if (laneDirection == LaneDirection.Left){
            
            laneSpawn = rightStartPosition;
            obstacleSpeed *= -1.0f;
        } else {

            laneSpawn = leftStartPosition;
        }

        // Start spawn process
        _isPlaying = true;
        StartCoroutine(ObstacleSpawner());

    }

    private IEnumerator ObstacleSpawner(){

        while(_isPlaying){

            // Roll the dice.
            int spawnChance = Random.Range(0, OneInXChance);

            if(spawnChance == 0){

                GameObject thisObstacle = Instantiate(obstaclePrefab, laneSpawn.position, Quaternion.identity);

                ObstacleScript thisObstacleScript = thisObstacle.GetComponent<ObstacleScript>();

                if(thisObstacleScript){

                    thisObstacleScript.obstacleSpeed = obstacleSpeed;
                }
            }

            yield return new WaitForSeconds(minimumTimeBetweenObstacles);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.transform.tag == "Player"){
            if(playerArrived == false){
                Debug.Log("Raccoon arrived at this lane");
                GameManager.S. RaccoonScore(10);
                playerArrived = true;
            } else{
                Debug.Log("Raccoon has already been here");
            }
            
        }


    }
}
