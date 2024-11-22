using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Animator platformAnimator; // Animator referans�

    public void OpenPlatformLid()
    {
        if (platformAnimator != null)
        {
            platformAnimator.SetTrigger("OpenLid"); // OpenLid trigger'�n� tetikle
        }
    }
}
