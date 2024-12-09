
using UnityEngine;
public class Pig : MonoBehaviour
{
    [SerializeField]
    public int pigStrength = 2; // מספר הפעמים שציפור יכולה לפגוע בחזיר לפני שהוא נהרס

    private int hitCount = 0; // מונה הפגיעות בחזיר

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bird")
        {
            hitCount++;
            if (hitCount >= pigStrength)
            {
                Destroy(gameObject);
            }
        }
    }

}



