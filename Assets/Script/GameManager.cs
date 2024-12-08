using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int LevelPoint = 0, numOfPigs = 0;
    [Tooltip("choose the number of bird you want to start with:")]
    [Serializedfield]
    int numOfBirds = 3;
}
