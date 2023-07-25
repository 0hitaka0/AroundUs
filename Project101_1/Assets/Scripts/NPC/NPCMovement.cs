using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDestinationInterval = 3f;
    public float changeDirectionRange = 2f;

    private Vector3 targetPosition;
    private Vector3 originalPosition;
    private float timer;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isPlayerCollision;
    private float minX, maxX, minY, maxY;
    public float xRange = 9f;
    public float yRange = 4f;
    private void Start()
    {
        timer = changeDestinationInterval;
        GetNewDestination();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Freeze rotation to prevent spinning
        //originalPosition = transform.position;

        // Calculate camera view boundaries
        float camOrthoSize = Camera.main.orthographicSize;
        float camAspect = Camera.main.aspect;
        float camPosX = Camera.main.transform.position.x;
        float camPosY = Camera.main.transform.position.y;

        minX = camPosX - camOrthoSize * camAspect;
        maxX = camPosX + camOrthoSize * camAspect;
        minY = camPosY - camOrthoSize;
        maxY = camPosY + camOrthoSize;
    }

    private void Update()
    {
        if (!isPlayerCollision)
        {
            // Move towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if reached the target position
            if (transform.position == targetPosition)
            {
                timer -= Time.deltaTime;

                // If the timer reaches zero, get a new destination
                if (timer <= 0f)
                {
                    GetNewDestination();
                    timer = changeDestinationInterval;
                }
            }
        }
        else
        {
            // Change direction within a certain range when collided with the player
            targetPosition = originalPosition + Random.insideUnitSphere * changeDirectionRange;
        }
        

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector2(xRange, transform.position.y);
        }

        if (transform.position.x < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }

        if (transform.position.x > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }

        /*Keep NPC within camera view boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);*/

        // Update the animation based on movement direction
        UpdateAnimation();
    }

    private void GetNewDestination()
    {
        targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void UpdateAnimation()
    {
        Vector3 movementDirection = targetPosition - transform.position;
        movementDirection.Normalize();

        // Set the animation parameters based on the movement direction
        animator.SetFloat("moveX", movementDirection.x);
        animator.SetFloat("moveY", movementDirection.y);

        animator.SetBool("moving", movementDirection != Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerCollision = false;
        }
    }

       

}
