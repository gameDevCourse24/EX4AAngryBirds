using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numOfPigs = 0;
    [Tooltip("choose the number of bird you want to start with:")]
    [SerializeField]
    private int numOfBirdsToPlay = 3;
    [SerializeField]
    public GameObject birdPrefab;
    [SerializeField] GameObject hookPrefab;

    [SerializeField] float limitCollideUntilDie = 3f;
    [Tooltip("The maximum time that the bird live until she destroy, in second.")][SerializeField] float limitTimeUntilDie = 10f;
    [SerializeField] TextMeshProUGUI birdCountText;
    bool alreadyChackTheBirdInSceane = false;
    bool startToCountToDie = false;

    private void Start()
    {
        ChackPigs();
        UpdateBirdCountText();
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

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            }
             else
            {
                if (!HasMoreBirds())
                {
                    Debug.Log("lose");
                }
                else
                {
                    ReloadBird();
                }
            }
            alreadyChackTheBirdInSceane = true;
        }
        if (GameObject.FindGameObjectsWithTag("Bird").Length >=1)
        {
            alreadyChackTheBirdInSceane = false;
        }

    }
    private void ReloadBird()
    {
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
        yield return new WaitForSeconds(limitTimeUntilDie);
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        foreach (GameObject bird in birds)
        {
            Destroy(bird);
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
    }
    private void UpdateBirdCountText()
    {
        birdCountText.text = numOfBirdsToPlay.ToString();
    }
}
