
using UnityEngine;
/*
This code is attached to the pig, and determines its strength, and counts the number of times it has been hit.
If you hit him enough times he will be destroyed.
*/
public class Pig : MonoBehaviour
{
    [SerializeField, Tooltip("The number of times you have to hit the pig to destroy it.")] public int pigStrength = 1;

    private int hitCount = 0; //Count the number of hits.

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



