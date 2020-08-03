/*
    SCRIPT TO ALLOW CAMERA TO FOLLOW THE PLAYER
    Tutorial: https://www.youtube.com/watch?v=OxJ6j4ImXiU by D Wood, uses Lerp API
    Tutorial: https://www.youtube.com/watch?v=7JjzhhC06xw&t=46s by LostRelicGames uses Lerp & SmoothDamp API
    Tutorial: https://www.youtube.com/watch?v=05VX2N9_2_4 by LostRelicGames on Camera Bounds and limits using Gizmos
*/

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;                      // Reference to the target we want camera to follow
    public float timeOffSet;                       // Specify how much time it takes for camera to move from one side to the other
    public Vector2 positionOffset;                 // Find and save where the player is, to know what to follow
    private float offSetSmoothing;                 // How far the camera is away from the player

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float topLimit;
    [SerializeField] float bottomLimit;

    // Update is called once per frame
    void Update()                                   // Consider use of LateUpdate(), to ensure all movement is made before moving camera
    {
        //playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        /*if(player.transform.rotation.y == 0f) // If the player's sprite rotation is facing forward
        {
            // Set the camera to position the player to the left of the screen
            playerPosition = new Vector3(playerPosition.x + offSet, transform.position.y, playerPosition.z); // y axis: transform.position.y
        }
        else
        {
            // Set the camera to postion the player to the right of the screen
            playerPosition = new Vector3(playerPosition.x - offSet, transform.position.y, playerPosition.z);
        }*/

        //transform.position = Vector3.Lerp(transform.position, playerPosition, offSetSmoothing * Time.deltaTime);

        Vector3 startPos = transform.position;          // Camera current position
        Vector3 endPos = player.transform.position;     // Player current position

        //
        endPos.x += positionOffset.x;
        endPos.y += positionOffset.y;
        endPos.z = -10;

        // Interpolate the value between the two points
        transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);

        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),           // Confines transform value to a left limit & right limit
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),           // Confines transform value to a top limit & bottom limit
                transform.position.z
             );
    }

    // InBuilt Unity Function to draw Gizmos, that are only seen in the editor (not in the game)
    private void OnDrawGizmos()
    {
        // Draw box around our camera boundary
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));       // draw top boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));   // draw right boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit)); // draw bottom boundary line
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));     // drawn left boundary line

    }
}
