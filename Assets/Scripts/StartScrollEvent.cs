using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class StartScrollEvent : MonoBehaviour {
    [SerializeField] private GameObject spotLight;
    [SerializeField] private GameObject scroll;
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject floorTile;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 _targetPosition;
    private Vector3 _scrollTransform;
    private bool _isMovingToScroll;
    private bool _isAligningScroll;
    private bool _isFallingDown;
    
    private void Start() {
        _scrollTransform = scroll.transform.position;
    }

    void OnTriggerEnter(Collider other) {
        inputAction.Disable();

        _targetPosition = scroll.transform.position - scroll.transform.forward * -2f;

        _isMovingToScroll = true;
        _isAligningScroll = true;

    }

    void Update() {
        if (_isMovingToScroll) {
            MovePlayerTowardsScroll();
        }
        if (_isAligningScroll) {
            AlignScrollToAPoint();
        }

        if (_isFallingDown) {
            Falling();
        }
    }

    void MovePlayerTowardsScroll() {
        player.transform.position =
            Vector3.MoveTowards(player.transform.position, _targetPosition, moveSpeed * Time.deltaTime);

        Vector3 directionToScroll = (scroll.transform.position - playerCamera.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToScroll);
        playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(player.transform.position, _targetPosition);
        float rotationDifference = Quaternion.Angle(playerCamera.rotation, targetRotation);
        
        if (distanceToTarget < 1.6f) {
            _targetPosition = new Vector3(-25f, player.transform.position.y, player.transform.position.z);
            moveSpeed = 2;
        }
        
        if (distanceToTarget < 1.6f && rotationDifference < 1f) {
            _isMovingToScroll = false;
            StartCoroutine(StartFalling());
        }
    }
    void AlignScrollToAPoint() {
        Vector3 targetPosition = new Vector3(-25f, 3f, _scrollTransform.z);
        scroll.transform.position = Vector3.Lerp(scroll.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        Quaternion targetRotation = Quaternion.Euler(50f, 0, 0);

        scroll.transform.rotation = Quaternion.Slerp(scroll.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (Vector3.Distance(scroll.transform.position, targetPosition) < 0.1f &&
            Quaternion.Angle(scroll.transform.rotation, targetRotation) < 0.5f) {
            _isAligningScroll = false; // Stop aligning once complete
            //spotLight.GetComponent<Light>().enabled = true;
        }
    }

    void Falling() {
        Quaternion targetRotation = Quaternion.Euler(-70f, 180f, 0);
        playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        if (Quaternion.Angle(playerCamera.transform.rotation, targetRotation) < 0.5f) {
            _isFallingDown = false;
        }
    }

    private IEnumerator StartFalling() {
        yield return new WaitForSeconds(7f);
        plane.SetActive(false);
        floorTile.SetActive(false);
        _isFallingDown = true;
        spotLight.SetActive(false);
        Destroy(scroll);
        //playerCamera.rotation = Quaternion.Slerp(playerCamera.transform.rotation, quaternion.Euler(-70f, 0, 0), 1.5f);
        StartCoroutine(StartMoving());
    }

    private IEnumerator StartMoving() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        inputAction.Enable();
    }
}