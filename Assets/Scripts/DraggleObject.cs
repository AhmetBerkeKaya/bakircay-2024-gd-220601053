using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    private PlacementArea currentArea = null;
    private Rigidbody rb;

    public float throwForce = 10f; // F�rlatma kuvveti

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>(); // Rigidbody'yi al
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // B�rak�ld���nda bir alan i�indeyse
        if (currentArea != null)
        {
            // E�er ba�ka bir alana ta��nd�ysa, �nceki alandaki objeyi null yap
            if (currentArea.placedObject == null)
            {
                currentArea.placedObject = this;
            }

            CheckForMatch(currentArea);
        }
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlacementArea"))
        {
            currentArea = other.GetComponent<PlacementArea>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlacementArea"))
        {
            currentArea = null;
        }
    }

    private void CheckForMatch(PlacementArea area)
    {
        PlacementArea otherArea = FindOtherArea(area); // Di�er alan� bul

        if (otherArea != null && area.HasObject && otherArea.HasObject)
        {
            bool objectsAreDifferent = area.placedObject.CompareTag(otherArea.placedObject.tag) == false;

            if (objectsAreDifferent)
            {
                // Farkl� objeler yerle�tirilmi�se her iki objeyi de f�rlat
                ThrowObject(area.placedObject);
                ThrowObject(otherArea.placedObject);

                // placedObject'lar� null yap
                area.placedObject = null;
                otherArea.placedObject = null;
            }
            else
            {
                // Ayn� objeler yerle�tirildiyse, onlar� yok et
                area.ClearBoth(otherArea);

                // placedObject'lar� null yap
                area.placedObject = null;
                otherArea.placedObject = null;
            }
        }
    }

    private PlacementArea FindOtherArea(PlacementArea currentArea)
    {
        foreach (var area in FindObjectsOfType<PlacementArea>())
        {
            if (area != currentArea && area.HasObject)
                return area;
        }
        return null;
    }

    private void ThrowObject(DraggableObject obj)
    {
        if (obj != null && obj.rb != null)
        {
            // F�rlatma y�n�n�, objenin bulundu�u alan ile olan mesafeye g�re ayarl�yoruz
            Vector3 throwDirection = (obj.transform.position - currentArea.transform.position).normalized;
            obj.rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}
