using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLife : MonoBehaviour
{
    public int numberofDots;
    public Transform dotTransform;

    private List<Dot> dots;

    // Start is called before the first frame update
    void Start()
    {
        dots = new List<Dot>();
        for (int i = 0; i < numberofDots; i++) {
            var newDot = Instantiate(dotTransform, new Vector2(Random.Range(-6.35f, 6.35f), Random.Range(-3.75f, 3.75f)), Quaternion.identity);
            newDot.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value, Random.value);
            dots.Add(newDot.GetComponent<Dot>());
        }    
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var dot in dots) {
            var dotRigidBody2D = dot.GetComponent<Rigidbody2D>();
            if (((dot.transform.position.x >= 6.35f) && (dotRigidBody2D.velocity.x > 0)) 
                    || ((dot.transform.position.x <= -6.35f) && (dotRigidBody2D.velocity.x < 0))) {
                dotRigidBody2D.velocity = new Vector2(-dotRigidBody2D.velocity.x, dotRigidBody2D.velocity.y);
            }
            if (((dot.transform.position.y >= 3.75f) && (dotRigidBody2D.velocity.y > 0)) 
                    || ((dot.transform.position.y <= -3.75f) && (dotRigidBody2D.velocity.y < 0))) {
                dotRigidBody2D.velocity = new Vector2(dotRigidBody2D.velocity.x, -dotRigidBody2D.velocity.y);
            }

            var colliders = Physics2D.OverlapCircleAll(dot.transform.position, dot.GetColour().GetMaxDistance());
            foreach(var collider in colliders) {
                if (!dot.name.Equals(collider.gameObject.name)) {
                    var forceToAdd = GetForceToAdd(dot, collider.GetComponent<Dot>());
                    collider.gameObject.GetComponent<Rigidbody2D>().AddForce(forceToAdd);
                }
            }
        }
    }

    private Vector2 GetForceToAdd(Dot dot, Dot other) {
        var x = dot.transform.position.x - other.transform.position.x;
        var y = dot.transform.position.y - other.transform.position.y;
        var dist = Mathf.Sqrt((x * x) + (y * y));
        var factor = 1f;
        if (dist <= dot.GetColour().GetMinDistance()) {
            factor = dist / dot.GetColour().GetMinDistance();
        } 
        else
        {
            factor = 1 - dist / (dot.GetColour().GetMaxDistance());
        }

        return factor * new Vector2(x, y) * Camera.main.GetComponent<ColourFactory>().GetForce(dot.GetColour(), other.GetColour());
    }
}
