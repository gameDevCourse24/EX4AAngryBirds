using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This component lets the player pull the ball and release it.
 */
public class AngryBird: MonoBehaviour {
    [SerializeField] Rigidbody2D hook = null;
    [SerializeField] float releaseTime = .15f;
    [SerializeField] float maxDragDistance = 5f;
    [SerializeField] float CameraViewMultyply = 1.5f;

    private GameManager gameManager;



    private bool isMousePressed = false;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update() {
        if (isMousePressed) {
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

    private void OnMouseDown() {
        isMousePressed = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distanceFromStart = Vector3.Distance(mousePos, transform.position);
        Camera.main.GetComponent<CameraFollower>().resizeCamera(CameraViewMultyply);
    }

    private void OnMouseUp() {
        isMousePressed = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Camera.main.GetComponent<CameraFollower>().resizeCamera(1);
        StartCoroutine(ReleaseBall());
    }

    IEnumerator ReleaseBall() {
        // Wait a short time, to let the physics engine operate the spring and give some initial speed to the ball.
        yield return new WaitForSeconds(releaseTime); 
        GetComponent<SpringJoint2D>().enabled = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bird collide wuth " + collision.gameObject.name);
        gameManager.StartTimerUntilDie();
    }
    
    // private void SetScore(int newScore) {
    //     score = newScore;
    //     PlayerPrefs.SetInt("BasketballScore", score);
    //     print("Score: " + score);
    // }

    // void OnGUI() {
    //     GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
    //     fontSize.fontSize = 16;
    //     fontSize.normal.textColor = Color.black;
    //     GUI.Label(new Rect(70, 0, 150, 50), "Score: " + score, fontSize);
    // }
}
