using UnityEngine;
using TMPro; // Make sure to include this namespace

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI Lives_Num_Text;
    public TextMeshProUGUI Lives_Num_Text_Shadow;
    public TextMeshProUGUI scoreText; // Assign this in the inspector
    public TextMeshProUGUI scoreText_Shadow;
    public Lives_System Score;
    void Start()
    {
        Score = GameObject.FindWithTag("Stage n Life Sys").GetComponent<Lives_System>();
        Lives_Num_Text.text = Lives_System.Lives.ToString();
        Lives_Num_Text_Shadow.text = Lives_Num_Text.text;
        UpdateScoreText(); // Initial update to display the starting score
    }

    // Call this method to add points and update the score display
    public void AddPoints(int pointsToAdd)
    {
        Score.The_Score += pointsToAdd;
        UpdateScoreText();
    }

    // Updates the TextMeshPro text to show the current score
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = Score.The_Score.ToString();
            scoreText_Shadow.text = scoreText.text;
        }
    }
}
