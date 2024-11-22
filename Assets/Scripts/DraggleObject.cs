using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    private PlacementArea currentArea = null;
    private Rigidbody rb;

    public float throwForce = 10f; // Fýrlatma kuvveti

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

        // Býrakýldýðýnda bir alan içindeyse
        if (currentArea != null)
        {
            // Eðer baþka bir alana taþýndýysa, önceki alandaki objeyi null yap
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
        PlacementArea otherArea = FindOtherArea(area); // Diðer alaný bul

        if (otherArea != null && area.HasObject && otherArea.HasObject)
        {
            bool objectsAreDifferent = area.placedObject.CompareTag(otherArea.placedObject.tag) == false;

            if (objectsAreDifferent)
            {
                // Farklý objeler yerleþtirilmiþse her iki objeyi de fýrlat
                ThrowObject(area.placedObject);
                ThrowObject(otherArea.placedObject);

                // placedObject'larý null yap
                area.placedObject = null;
                otherArea.placedObject = null;
            }
            else
            {
                // Ayný objeler yerleþtirildiyse, onlarý yok et
                area.ClearBoth(otherArea);

                // placedObject'larý null yap
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
            // Fýrlatma yönünü, objenin bulunduðu alan ile olan mesafeye göre ayarlýyoruz
            Vector3 throwDirection = (obj.transform.position - currentArea.transform.position).normalized;
            obj.rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}
