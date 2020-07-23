/*
    SCRIPT TO CREATE A DYNAMIC LEVEL BACKGROUND (Parallax Scrolling)
    Tutorial: https://www.youtube.com/watch?v=zit45k6CUMk by Dani
    
*/

using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;      // give the length of the sprite
    }

    // Update is called once per frame
    void Update()
    {
        float repeat = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (repeat > startPos + length)
        {
            startPos += length;
        }
        else if (repeat < startPos - length)
        {
            startPos -= length;
        }
    }
}
