using UnityEngine;
using UnityEngine.SceneManagement;

public class Border : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bird")
        {
            Debug.Log("The bird hit the border.");
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}