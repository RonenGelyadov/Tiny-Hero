using UnityEngine;

public class StartPosition : MonoBehaviour
{
	private void Start() {
        PlayerController.Instance.transform.position = transform.position;       
    }
}
