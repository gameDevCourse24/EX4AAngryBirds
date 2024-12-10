using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This component lets the player pull the ball and release it.
 */
public class AngryBird : MonoBehaviour
{
    [SerializeField, Tooltip("The Rigidbody that serves as the hook")]
    Rigidbody2D hook = null;
    [SerializeField, Tooltip("The time it takes to release the ball from the hook (in seconds)")]
    float releaseTime = .15f;
    [SerializeField, Tooltip("The maximum distance the ball can be dragged")]
    float maxDragDistance = 5f;
    [SerializeField, Tooltip("How match to make the camera bigger when you drag the bird")]
    float CameraViewMultyply = 1.5f;

    private GameManager gameManager;



    private bool isMousePressed = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Updates the position of the ball based on mouse input if the mouse is pressed.        
        if (isMousePressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        isMousePressed = true;
        // changes the Rigidbody to Kinematic for dragging.
        rb.bodyType = RigidbodyType2D.Kinematic;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distanceFromStart = Vector3.Distance(mousePos, transform.position);
        // Tell the GameManager Script that he need to scale up the camera view.
        Camera.main.GetComponent<CameraFollower>().resizeCamera(CameraViewMultyply);
    }

    private void OnMouseUp()
    {
        isMousePressed = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Tell the GameManager Script that he need to normalize the camera view.
        Camera.main.GetComponent<CameraFollower>().resizeCamera(1);
        // starts the release coroutine.
        StartCoroutine(ReleaseBall());
    }

    IEnumerator ReleaseBall()
    {
        // Waits for the specified release time before disabling the SpringJoint2D to release the ball.
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Logs the collision with another object and starts the timer in the GameManager.
        //  When the timer is up, the bird will be destoied.
        Debug.Log("Bird collide wuth " + collision.gameObject.name);
        gameManager.StartTimerUntilDie();
    }
}
