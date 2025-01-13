using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Match;
using UnityEngine;

public class PortalSkill : MonoBehaviour
{
    [SerializeField] private Transform _leftObjectPlacement; // Sol nesnenin yerleþtirileceði nokta
    [SerializeField] private Transform _rightObjectPlacement; // Sað nesnenin yerleþtirileceði nokta
    [SerializeField] private Animator _animator; // Kapak animasyonu için
    [SerializeField] private GameObject portalObject; // Portal particle'ý ve sesini barýndýran GameObject
    [SerializeField] private float moveDuration = 1f; // Nesnelerin hareket süresi
    [SerializeField] private float dropDuration = 1f; // Nesnelerin aþaðý düþme süresi
    private readonly int _openLidHash = Animator.StringToHash("OpenLid");
    private readonly int _closeLidHash = Animator.StringToHash("CloseLid");

    private bool _isSkillUsed = false; // Skill'in daha önce kullanýlýp kullanýlmadýðýný kontrol eder

    public void ActivateMatchSkill()
    {
        // Eðer skill zaten kullanýldýysa, bir þey yapma
        if (_isSkillUsed) return;

        // Tüm nesneleri oyun alanýndan bul
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Moveable");
        if (allObjects.Length < 2) return; // Yeterli nesne yoksa çýk

        // Ayný türden iki nesneyi bul
        Item firstItem = null;
        Item secondItem = null;

        for (int i = 0; i < allObjects.Length; i++)
        {
            var item1 = allObjects[i].GetComponent<Item>();
            if (item1 == null) continue;

            for (int j = i + 1; j < allObjects.Length; j++)
            {
                var item2 = allObjects[j].GetComponent<Item>();
                if (item2 == null) continue;

                if (item1.IsMatching(item2))
                {
                    firstItem = item1;
                    secondItem = item2;
                    break;
                }
            }

            if (firstItem != null && secondItem != null) break;
        }

        // Eþleþme bulunamadýysa çýk
        if (firstItem == null || secondItem == null) return;

        // Portal GameObject'ini aktif et ve oynat
        portalObject.SetActive(true); // Particle ve ses için GameObject'i aktif et
        portalObject.GetComponent<ParticleSystem>().Play(); // Particle'ý baþlat
        portalObject.GetComponent<AudioSource>().Play(); // Ses parçasýný baþlat

        // Nesneleri platforma taþý
        StartCoroutine(MatchObjects(firstItem, secondItem));

        // Skill'in kullanýldýðýný iþaretle
        _isSkillUsed = true;
    }

    private IEnumerator MatchObjects(Item firstItem, Item secondItem)
    {
        // Nesneleri kinematik yap ve colliderlarýný kapat
        firstItem.GetComponent<Rigidbody>().isKinematic = true;
        secondItem.GetComponent<Rigidbody>().isKinematic = true;
        firstItem.SetCollidersActive(false);
        secondItem.SetCollidersActive(false);

        // DOTween ile hareket ettir
        firstItem.transform.DOMove(_leftObjectPlacement.position, moveDuration);
        firstItem.transform.DORotate(_leftObjectPlacement.rotation.eulerAngles, moveDuration);
        secondItem.transform.DOMove(_rightObjectPlacement.position, moveDuration);
        secondItem.transform.DORotate(_rightObjectPlacement.rotation.eulerAngles, moveDuration);

        yield return new WaitForSeconds(moveDuration);

        // Kapak açýlma animasyonu
        _animator.SetTrigger(_openLidHash);
        yield return new WaitForSeconds(0.5f);

        // Nesneleri merkeze al
        Vector3 targetPosition = (_leftObjectPlacement.position + _rightObjectPlacement.position) / 2f;
        firstItem.transform.DOMove(targetPosition, dropDuration);
        secondItem.transform.DOMove(targetPosition, dropDuration);

        yield return new WaitForSeconds(dropDuration);

        // Nesneleri aþaðý düþür
        targetPosition += Vector3.down * 2f;
        firstItem.transform.DOMove(targetPosition, dropDuration);
        secondItem.transform.DOMove(targetPosition, dropDuration);

        yield return new WaitForSeconds(dropDuration);

        // Kapak kapama animasyonu
        _animator.SetTrigger(_closeLidHash);

        // Nesneleri yok et
        yield return new WaitForSeconds(0.5f);
        firstItem.gameObject.SetActive(false);
        secondItem.gameObject.SetActive(false);

        // Portal GameObject'ini kapat
        portalObject.SetActive(false); // Particle ve sesin durmasý için GameObject'i kapat

        GameEvents.OnItemMatched?.Invoke(firstItem.itemData);
    }

    // Bu metodu oyun baþladýðýnda veya restart atýldýðýnda çaðýrarak skill'i sýfýrlayabilirsiniz
    public void ResetSkill()
    {
        _isSkillUsed = false; // Skill tekrar kullanýlabilir
    }
}
