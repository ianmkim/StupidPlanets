using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public Renderer textureRender;
    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture = MapGenerator.GenerateColourMap(100,100,25,5,0.5f,2,0);
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
