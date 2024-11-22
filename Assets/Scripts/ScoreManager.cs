using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton örneði

    public TextMeshProUGUI scoreText; // Skor UI Text referansý
    private int score = 0; // Baþlangýç skoru

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Tek örnekten fazlasý olmasýn
        }
    }

    // Skoru artýr ve UI'ý güncelle
    public void AddScore(int amount)
    {
        score += amount; // Skoru artýr
        UpdateScoreUI(); // UI güncelle
    }

    // Skor metnini güncelle
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }
}
