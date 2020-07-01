/*
    SCRIPT TO ALLOW CAMERA TO FOLLOW THE PLAYER
    Tutorial: https://www.youtube.com/watch?v=OxJ6j4ImXiU by D Wood, uses Lerp API
    Tutorial: https://www.youtube.com/watch?v=7JjzhhC06xw&t=46s by LostRelicGames uses Lerp & SmoothDamp API
*/

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;               // Reference to the target we want camera to follow
    public float offSet;                    // How far the camera is away from the player
    private Vector3 playerPosition;         // Find where the player is, to know what to follow
    public float offSetSmoothing;           // Specifiy how much time it takes for camera to move from one side to the other
    
    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if(player.transform.rotation.y == 0f) // If the player's sprite rotation is facing forward
        {
            // Set the camera to position the player to the left of the screen
            playerPosition = new Vector3(playerPosition.x + offSet, playerPosition.y, playerPosition.z);
        }
        else
        {
            // Set the camera to postion the player to the right of the screen
            playerPosition = new Vector3(playerPosition.x - offSet, playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offSetSmoothing * Time.deltaTime);
        
    }
}
