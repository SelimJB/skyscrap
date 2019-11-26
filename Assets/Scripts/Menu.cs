using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int score;

    void OnEnable()
    {
        GameManager.NextFloor += IncrScore;
    }
    void OnDisable()
    {
        GameManager.NextFloor -= IncrScore;
    }

    void IncrScore(){
        score++;
        scoreText.text = score.ToString();
    }
}
