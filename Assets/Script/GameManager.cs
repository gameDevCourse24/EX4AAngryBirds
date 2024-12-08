using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numOfPigs = 0;
    [Tooltip("choose the number of bird you want to start with:")]
    [SerializeField]
    private int numOfBirds = 3;
    [SerializeField]
    public GameObject birdPrefab;

    // [SerializeField] float limitCollideUntilDie = 3f;
    [Tooltip("The maximum time that the bird live until she destroy, in second.")][SerializeField] float limitTimeUntilDie = 10f;

    

    private void Start()
    {
        GameObject[] pigs = GameObject.FindGameObjectsWithTag("Pig");
        Debug.Log("There is " + pigs.Length + " Pigs.");
        foreach (GameObject pig in pigs)
        {
            Debug.Log("There is a pig in location: " + pig.transform.position);
        }
        numOfPigs = GameObject.FindGameObjectsWithTag("Pig").Length;
    }
    public bool HasMoreBirds()
    {
        return numOfBirds > 0; // בודק אם יש עוד ציפורים
    }
    public void SpawnBird(Vector3 position)
    {
        if (HasMoreBirds())
        {
            Instantiate(birdPrefab, position, Quaternion.identity); // טוען ציפור חדשה
            numOfBirds--; // מפחית את כמות הציפורים
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
// IEnumerator DBaRemoveBirdAfterDelayll()
//     {
//         yield return new WaitForSeconds(limitTimeUntilDie); 
//         {
//             Destroy(gameObject);
//         }
//     }


//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         Debug.Log("The collision is with: " + other.name);
//         if (other.tag == "Border")
//         {
//             Destroy(this.gameObject);

//         }
//     }
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         StartCoroutine (DBaRemoveBirdAfterDelayll());
//         if (collision.gameObject.tag == "Pig")
//         {
//             Destroy(collision.gameObject);
//         }
//     }

//     private void detroyBird()
//     {
//         Destroy(this.gameObject);
//         if (usedBirds<numOfBirds)
//         {
//             CreateNewBird();
//         }
//         else
//         {
//             Debug.Log("game Over.");
//         }
//     }

//     private void CreateNewBird()
//     {

//     }
