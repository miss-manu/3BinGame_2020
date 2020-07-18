/*
            SCRIPT TO MAKE A LEVEL EDITOR
            Tutorial:  https://www.youtube.com/watch?v=B_Xp9pt8nRY&feature=youtu.be by Brackeys 

            When we start the game, Unity is going to create a level by looping through all the pixels
            in the map we have given. 

            For each pixel, it's going to call the GenerateTile function, which will get the colour data from that
            particular pixel (store it inside of pixelColour) and if it's transparent, ignore that pixel, but if it matches a 
            certain colour - then replace it with a prefab. 

            When loading the map into Unity, ensure the following settings in the inspector: 
                Filter Mode = Pixel
                Compression = None
                Advanced/Read-Write-Enabled = True (allows us to read colour data from map)
 */

using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public Texture2D map;                               // Allows an IMAGE to be placed into the inspector window slot
    public ColourToPrefab[] colourMappings;             // Reference an array of the pixel colours to search for.  Ensure Alpha = 255. 
    
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();                            
    }

    void GenerateLevel()                                // Method to load data from the map
    {
        for (int x = 0;  x < map.width; x++)            // Loop through the width of our texture
        {
            for(int y = 0; y < map.height; y++)         // Loop through the height of our texture
            {
                GenerateTile(x, y);                    
            }
        }
    }

    void GenerateTile(int x, int y)                     // Method to get colour data from pixel
    {
        Color pixelColour = map.GetPixel(x, y);         // Find the colour of the pixel we are looking at

        if (pixelColour.a == 0)                         // If the pixel is transparent, ignore it
        {
            return;  
        }

        // Otherwise, tell Unity that when we get a coloured pixel, we want to spawn a prefab
        foreach (ColourToPrefab colourMapping in colourMappings)
        {
            if (colourMapping.colour.Equals(pixelColour))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate (colourMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
