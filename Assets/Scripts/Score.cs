using UnityEngine;
using TMPro;

/// <summary>
/// Score manager.
/// </summary>
public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private int score;
    private int bestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "" + bestScore;
    }

    public void AddScore(int amount = 1)
    {
        score += amount;
        scoreText.text = "" + score;

        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = "" + bestScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "" + score;
    }
}