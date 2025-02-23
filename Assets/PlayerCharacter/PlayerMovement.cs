using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
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

        Vector2 direction = new Vector2((xInput * 2), (yInput * 2)).normalized;
        body.linearVelocity = new Vector2((xInput*2), (yInput*2)).normalized;
        //body.linearVelocity = direction * speed;
    }
}
