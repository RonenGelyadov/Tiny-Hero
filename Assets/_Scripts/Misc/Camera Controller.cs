using UnityEngine;
using Unity.Cinemachine;

public class CameraController : Singleton<CameraController>
{
	CinemachineCamera cinemaCam;

    private void Start() {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow() {
		cinemaCam = FindAnyObjectByType<CinemachineCamera>();
		cinemaCam.Follow = PlayerController.Instance.transform;
	}
}
