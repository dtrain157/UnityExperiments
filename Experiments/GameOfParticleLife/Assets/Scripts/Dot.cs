using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {

	private Colour colour;

	void Start () {
		colour = Camera.main.GetComponent<ColourFactory>().GetColour();
		var bytestring = System.BitConverter.GetBytes( colour.GetComponent<Colour>().GetColour() );
        this.GetComponent<SpriteRenderer>().color = new Color32(bytestring[3], bytestring[2], bytestring[1], bytestring[0]);
		this.name = "Dot-" + colour.GetComponent<Colour>().GetColour().ToString("X6") + "-" + Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Colour GetColour() {
		return colour;
	}

}
