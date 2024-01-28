using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // DK if we need this

[System.Serializable]
public struct EndingRange
{
    public GameManager.EndingType ending;
    public float min, max;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum EndingType { Undetermined, Ending1, Ending2, Ending3} // Add more and rename these later

    public EndingType chosenEnding = EndingType.Undetermined; // Set the default value to undetermined because the ending
    [SerializeField] EndingRange[] ranges;

    public float totalScore = 0f;
    [SerializeField] float timeLimit = 300f; // Tentatively, the time limit is 5 mins
    float remainingTime;

    public GameObject endingObject;
    void Awake() 
    {
        if (Instance != null && Instance != this) // Make this a singleton
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        remainingTime = timeLimit;
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Decrease timer until it reaches below 0
        }
        else
        {
            //chosenEnding = DetermineEnding(); // After reaching the end of the timer, use score to determine ending
            LoadEnding();
        }
    }

    public void AddScore(float amountToAdd)
    {
        totalScore += amountToAdd;
    }

    EndingType DetermineEnding() // Call this at the end of the game to determine your ending
    {
        foreach (EndingRange range in ranges)
        {
            if (Mathf.Clamp(totalScore, range.min, range.max) == totalScore) // Find which range of values our total score falls under, and use that to determine ending
            {
                return range.ending;
            }
        }

        return EndingType.Undetermined; // If somehow we dont fit into any range of ending, we use Undetermined or the Bad Ending idk
    }

    void LoadEnding()
    {
        endingObject.SetActive(true);
        /*
        switch (chosenEnding)
        {
            case EndingType.Undetermined:
                break;
            case EndingType.Ending1:
                break;
            case EndingType.Ending2:
                break;
            case EndingType.Ending3:
                break;
        }
        */
    }
}


