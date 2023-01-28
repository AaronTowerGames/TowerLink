using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform _transformCam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + _transformCam.forward);
    }
}
