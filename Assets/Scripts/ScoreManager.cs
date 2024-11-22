using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton �rne�i

    public TextMeshProUGUI scoreText; // Skor UI Text referans�
    private int score = 0; // Ba�lang�� skoru

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Tek �rnekten fazlas� olmas�n
        }
    }

    // Skoru art�r ve UI'� g�ncelle
    public void AddScore(int amount)
    {
        score += amount; // Skoru art�r
        UpdateScoreUI(); // UI g�ncelle
    }

    // Skor metnini g�ncelle
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }
}
