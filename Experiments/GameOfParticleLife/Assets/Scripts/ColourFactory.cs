using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFactory : MonoBehaviour
{
    public int numberOfColours;
    public int maxForce;
    public Transform colour;
    
    private List<Colour> colours;
    private Dictionary<int, Dictionary<int, float>> forces;
    
    void Awake()
    {
        //set up colours
        colours = new List<Colour>();
        for(int i = 0; i < numberOfColours; i++) {
            var newColour = Instantiate(colour, new Vector2(0,0), Quaternion.identity);
            newColour.name = "Colour-" + newColour.GetComponent<Colour>().GetColour().ToString("X6");
            var bytestring = System.BitConverter.GetBytes( newColour.GetComponent<Colour>().GetColour() );
            newColour.GetComponent<SpriteRenderer>().color = new Color32(bytestring[3], bytestring[2], bytestring[1], bytestring[0]);
            colours.Add(newColour.GetComponent<Colour>());            
        }  


        //set up forces
        forces = new Dictionary<int, Dictionary<int, float>>(); 
        foreach (var rc in colours) {
            forces[rc.GetColour()] = new Dictionary<int, float>();
        }

        foreach (var rc in colours) {
            foreach (var cc in colours) {
                if (forces[cc.GetColour()].ContainsKey(rc.GetColour())) {
                    forces[rc.GetColour()][cc.GetColour()] = forces[cc.GetColour()][rc.GetColour()];
                } else {
                    forces[rc.GetColour()][cc.GetColour()] = Random.Range(-maxForce, maxForce);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Colour GetColour() {
        var index = Random.Range(0, numberOfColours);
        return this.colours[index];
    }

    public float GetForce(Colour colour, Colour other) {
        return forces[colour.GetColour()][other.GetColour()];
    }
}
