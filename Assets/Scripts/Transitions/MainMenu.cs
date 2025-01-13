using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause Menüsü UI referansý
    private bool isPaused = false; // Oyunun pause durumunu kontrol eder

    void Update()
    {
        // Escape tuþuna basýldýðýnda menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void AppPlay()
    {
        SceneManager.LoadScene(1);
    }
    // Oyuna devam etme (Pause kapatma)
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Pause Menüyü gizle
        Time.timeScale = 1f;          // Zamaný normale döndür
    }

    // Oyunu durdurma (Pause açma)
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Pause Menüyü görünür yap
        Time.timeScale = 0f;         // Zamaný durdur
        isPaused = true;             // Pause durumunu aç
    }

    // Oyunu baþtan baþlatma
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi yeniden yükle
        Time.timeScale = 1f; // Zamaný tekrar normale döndür

    }

    // Ana menüye dönme
    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Zamaný tekrar normale döndür
        SceneManager.LoadScene(0); // Ana menü sahnesine dön
    }

    // Oyunu kapatma
    public void QuitGame()
    {
        Application.Quit(); // Uygulamayý kapat
    }
}

