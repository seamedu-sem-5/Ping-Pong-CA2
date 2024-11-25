using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector2 startPosition;

    private float movement;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical2");
        }

        // Explicitly set the y-velocity, without changing x.
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, movement * speed);
    }

    public void Reset()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
    }
}
