using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Animator platformAnimator; // Animator referansý

    public void OpenPlatformLid()
    {
        if (platformAnimator != null)
        {
            platformAnimator.SetTrigger("OpenLid"); // OpenLid trigger'ýný tetikle
        }
    }
}
