using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{

    public int maxMinDistance;
    public int maxMaxDistance;

    private int spriteColour;
    private int maxDistance;
    private int minDistance;

    void Awake()
    {
        byte[] colourBytes = {255, (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255) }; //{alpha; blue; green; red}
        spriteColour = System.BitConverter.ToInt32(colourBytes, 0);
        minDistance = (int)Random.Range(0, maxMinDistance);
        maxDistance = (int)Random.Range(minDistance, maxMaxDistance); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetColour() {
        return this.spriteColour;
    }

    public int GetMinDistance() {
        return this.minDistance;
    }

    public int GetMaxDistance() {
        return this.maxDistance;
    }
}
