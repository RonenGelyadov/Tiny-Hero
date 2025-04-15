using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] CinemachineCamera cinemaCam;

    private void Start() {
        cinemaCam.Follow = PlayerController.Instance.transform;
    }
}
