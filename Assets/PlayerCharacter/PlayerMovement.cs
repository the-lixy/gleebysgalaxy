using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        body.linearVelocity = new Vector2(xInput, yInput);
    }
}
