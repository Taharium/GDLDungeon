using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTeleportationEvent : MonoBehaviour {
    [SerializeField] private GameObject pointLight;
    [SerializeField] private GameObject pointLight2;
    [SerializeField] private GameObject teleportCircle;
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 _targetPosition;
    private int _isEnabled1 = 0;
    private int _isEnabled2 = 0;
    private CharacterController _characterController;
    private Light _pointLight;
    private Light _pointLight2;

    private void Start() {
        _pointLight = pointLight.GetComponent<Light>();
        _pointLight2 = pointLight2.GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other) {
        inputAction.Disable();

        _characterController = player.GetComponent<CharacterController>();
        if (_characterController != null) {
            _characterController.enabled = false;
        }

        StartCoroutine(Teleport());
        _pointLight.enabled = true;
        _isEnabled1 = 1;

    }

    private void Update() {
        _pointLight.intensity += _isEnabled1 * 1000 * Time.deltaTime;
        _pointLight2.intensity -= _isEnabled2 * 1000 * Time.deltaTime;
    }

    private IEnumerator Teleport() {
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(-25f, -0f, -430);

        playerCamera.rotation = Quaternion.Euler(0f, 180f, 0f);
        _pointLight.enabled = false;
        _pointLight2.intensity = _pointLight.intensity;
        _isEnabled1 = 0;
        _isEnabled2 = 1;
        _pointLight2.enabled = true;

        StartCoroutine(FinishTeleport());
    }

    private IEnumerator FinishTeleport() {
        yield return new WaitForSeconds(1f);
        _isEnabled2 = 0;
        _pointLight2.enabled = false;
        
        if (_characterController) {
            _characterController.enabled = true;
        }

        inputAction.Enable();
    }
}