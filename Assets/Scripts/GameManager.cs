using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numOfPigs = 0;
    [Tooltip("choose the number of bird you want to start with in this Level:")]
    [SerializeField]
    private int numOfBirdsToPlay = 3;
    [SerializeField]
    public GameObject birdPrefab;
    [SerializeField] GameObject hookPrefab;

    [SerializeField, Tooltip("The maximum time that the bird live until she destroy, in second.")] float limitTimeUntilDie = 10f;
    [SerializeField] TextMeshProUGUI birdCountText;
    [SerializeField, Tooltip("The starting point for the camera to show the pigs in this level (Put here an empty object that is just after the pig that is farthest from the bird.)")] GameObject startPointForCamera;
    [SerializeField, Tooltip("The end point for the camera to show the pigs in this level (Put here an empty object that is on the main camera.)")] GameObject endPointForCamera;
    [SerializeField, Tooltip("The panel that will appear when all the pigs are destroyed")] private GameObject winningPanel;
    [SerializeField, Tooltip("The panel that will appear if you lose")] private GameObject LosingPanel;
    [SerializeField, Tooltip("The scene you will be transported to when you win and press the /NextLevel/ button")] private string nextSceneName;
    [SerializeField, Tooltip("The main camera")] Camera mainCamera;

    /*
    alreadyChackTheBirdInSceane
    I keep this variable so that we don't go into checking whether a bird exists all the time but only once unless the bird is destroyed.
    */
    bool alreadyChackTheBirdInSceane = false;

    /*
    startToCountToDie:
    This variable holds a boolean value that checks if we have already started counting down.
    I hold this variable so that birds are not killed for no reason.
    Explanation: When a bird collides with something (which is not a border),a countdown begins.
    At the end of the countdown the bird on the screen is destroyed.
    As soon as there are several collisions with the same bird, then a count starts each time,
    and then a situation can arise where the first count is over and the bird is destroyed and another bird takes its place
    but then the next count ends and the second bird is also destroyed even though it has not yet collided with anything.
    */
    bool startToCountToDie = false;


    private void Start()
    {
        /*
        At the beginning of the stage:
        1) I chack how many pigs there are.
        2) Updates the canvas how many birds are left.
        3) Makes sure the win/loss panels are inactive.
        4) Makes sure the game doesn't stop.
        5) Starts the camera movement to show the whole stage with the pigs and the walls and the walls etc
        */
        ChackPigs();
        UpdateBirdCountText();
        winningPanel.SetActive(false);
        LosingPanel.SetActive(false);
        Time.timeScale = 1f;
        StartCameraMovement(startPointForCamera.transform.position, endPointForCamera.transform.position);
    }

    public void StartCameraMovement(Vector3 startPoint, Vector3 endPoint)
    {
        mainCamera.GetComponent<CameraFollower>().MoveCamera(new Vector3(startPoint.x, 0, -10), new Vector3(endPoint.x, 0, -10));
    }
    private void ChackPigs()//Checks the amount of pigs and updates the console.
    {
        GameObject[] pigs = GameObject.FindGameObjectsWithTag("Pig");
        Debug.Log("There is " + pigs.Length + " Pigs.");
        foreach (GameObject pig in pigs)
        {
            Debug.Log("There is a pig in location: " + pig.transform.position);
        }
        numOfPigs = pigs.Length;
    }
    public bool HasMoreBirds()
    {
        return numOfBirdsToPlay > 0;
    }

    public void RestartLevel()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Update()
    {
        /*
        Here I check if there is a bird on the board:
        If there is no bird and I haven't checked since the last extermination, then I check if I won, according to the amount of pigs on the screen.
            If there are no pigs - I activate the victory panel.
            If there are pigs - I check if I have more birds to use.
                If not - activates the loss panel.
                If there is - loads another bird.
        */
        if (GameObject.FindGameObjectsWithTag("Bird").Length == 0 && !alreadyChackTheBirdInSceane)
        {
            Debug.Log("There are no birds left.");
            ChackPigs();
            if (numOfPigs == 0)
            {
                Debug.Log("Win");
                mainCamera.GetComponent<CameraFollower>().PlayerToFollowXAxis = hookPrefab;
                winningPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                if (!HasMoreBirds())
                {
                    Debug.Log("lose");
                    mainCamera.GetComponent<CameraFollower>().PlayerToFollowXAxis = hookPrefab;
                    LosingPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    ReloadBird();
                }
            }
            alreadyChackTheBirdInSceane = true;
        }
        if (GameObject.FindGameObjectsWithTag("Bird").Length >= 1)
        {
            alreadyChackTheBirdInSceane = false;
        }

    }
    private void ReloadBird()
    {
        startToCountToDie = false;//So that the next bird can start the countdown.
        numOfBirdsToPlay--;
        CreateNewBird();
    }
    public void StartTimerUntilDie()
    {
        /*
        A function that starts the countdown (only if it hasn't started already).
        The function is public because it is called from another script.
        */
        if (!startToCountToDie)
        {
            startToCountToDie = true;
            StartCoroutine(TimeToDie());
        }
    }
    IEnumerator TimeToDie()
    {
        Debug.Log("StartTimerUntilDie Function ");
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        yield return new WaitForSeconds(limitTimeUntilDie);
        foreach (GameObject bird in birds)
        {
            if (startToCountToDie)
            {
                Destroy(bird);
            }
        }
    }
    private void CreateNewBird()
    {
        /*
        This function creates a new bird, connects it to the hook, and updates the camera to follow the new bird.
        */
        GameObject hook = GameObject.FindGameObjectWithTag("Hook");
        GameObject newBird = Instantiate(birdPrefab, Vector3.zero, Quaternion.identity);
        SpringJoint2D springJoint = newBird.GetComponent<SpringJoint2D>();
        springJoint.connectedAnchor = Vector2.zero;
        springJoint.connectedBody = hook.GetComponent<Rigidbody2D>();
        Debug.Log("New Bird Created and connected to hook");
        startToCountToDie = false;
        UpdateBirdCountText();
        CameraFollower cameraFollower = mainCamera.GetComponent<CameraFollower>();
        cameraFollower.PlayerToFollowXAxis = newBird;
    }
    private void UpdateBirdCountText()
    {
        birdCountText.text = numOfBirdsToPlay.ToString();
    }
    public void GoToNextLevel()//This function is public so I can call it from the victory panel button.
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
