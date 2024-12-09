using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numOfPigs = 0;
    [Tooltip("choose the number of bird you want to start with:")]
    [SerializeField]
    private int numOfBirdsToPlay = 3;
    [SerializeField]
    public GameObject birdPrefab;
    [SerializeField] GameObject hookPrefab;

    [Tooltip("The maximum time that the bird live until she destroy, in second.")] [SerializeField] float limitTimeUntilDie = 10f;
    [SerializeField] TextMeshProUGUI birdCountText;
    [SerializeField] GameObject startPointForCamera;
    [SerializeField] GameObject endPointForCamera;
    [SerializeField] private GameObject winningPanel;
    [SerializeField] private GameObject LosingPanel;
    bool alreadyChackTheBirdInSceane = false;
    bool startToCountToDie = false;

    [SerializeField] Camera mainCamera;
    [SerializeField] private string nextSceneName;

    private void Start()
    {
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
    private void ChackPigs()
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
        startToCountToDie = false;
        numOfBirdsToPlay--;
        CreateNewBird();
    }
    public void StartTimerUntilDie()
    {
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
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
