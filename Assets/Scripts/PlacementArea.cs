using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public DraggableObject placedObject; // �zerine b�rak�lan nesne
    public Animator lidAnimator; // Kapak i�in Animator referans�

    public bool HasObject => placedObject != null;

    public bool IsMatch(PlacementArea otherArea)
    {
        return HasObject && otherArea.HasObject &&
               placedObject.CompareTag(otherArea.placedObject.tag);
    }

    public void ClearBoth(PlacementArea otherArea)
    {
        // Animasyonu tetikle
        if (lidAnimator != null)
        {
            lidAnimator.SetTrigger("OpenLid"); // "OpenLid" animasyon trigger'� tetikleniyor
        }

        // E�er her iki alanda da nesne varsa, yok et
        if (placedObject != null && otherArea.placedObject != null)
        {
            Destroy(placedObject.gameObject);
            Destroy(otherArea.placedObject.gameObject);
        }

        // Alanlar� temizle
        placedObject = null;
        otherArea.placedObject = null;

        // Puan ekle
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(10); // 10 puan ekle
        }
    }
}
