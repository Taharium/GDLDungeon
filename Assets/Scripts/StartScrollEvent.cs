using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartScrollEvent : MonoBehaviour {
    [SerializeField] private Transform scrollTarget;
    [SerializeField] private InputActionAsset playerMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 _targetPosition;
    private bool _isMovingToScroll = false;

    void OnTriggerEnter(Collider other) {
        playerMovement.Disable();

        _targetPosition = scrollTarget.position + new Vector3(0, 0, +10f);

        _isMovingToScroll = true;
    }

    /*void Update() {
        if (_isMovingToScroll) {
            MovePlayerTowardsScroll();
        }
    }*/
    
    /*void MovePlayerTowardsScroll() {
        // Calculate direction vector to the scroll target
        var directionToScroll = (scrollTarget.position - player.transform.position);

        // Zero out the y component to prevent the player from looking up or down
        directionToScroll.y = -5;
    
        // Normalize the direction vector
        directionToScroll = directionToScroll.normalized;

        // Get the target rotation (only considering the x and z axes)
        Quaternion targetRotation = Quaternion.LookRotation(directionToScroll);
    
        // Smoothly rotate the player toward the target rotation
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move the player toward the target position
        player.transform.position = Vector3.MoveTowards(player.transform.position, _targetPosition, moveSpeed * Time.deltaTime);

        // Check if the player is close enough to the target position to stop moving
        float distanceToTarget = Vector3.Distance(player.transform.position, _targetPosition);
        if (distanceToTarget < 0.1f) {
            _isMovingToScroll = false;
            Debug.Log("HELLO");
            playerMovement.Enable();
        }
    }
    */
    
    /*void MovePlayerTowardsScroll() {
        // Rotation logic: rotate the player to face the target
        Vector3 directionToScroll = (scrollTarget.position - player.transform.position).normalized;
        directionToScroll.y = -2f;
        Quaternion targetRotation = Quaternion.LookRotation(directionToScroll);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move the player towards the target position
        player.transform.position = Vector3.MoveTowards(player.transform.position, _targetPosition, moveSpeed * Time.deltaTime);

        // Check if the player is close enough to the target position to stop moving
        float distanceToTarget = Vector3.Distance(player.transform.position, _targetPosition);
        if (distanceToTarget < 0.1f) {
            _isMovingToScroll = false;
            Debug.Log("HELLO");
            playerMovement.Enable();
        }

        // Optional: If rotation isn't exactly right, you can add a small tolerance for completion
        if (Quaternion.Angle(player.transform.rotation, targetRotation) < 1f && distanceToTarget < 0.1f) {
            _isMovingToScroll = false;
            Debug.Log("HELLO1");
            playerMovement.Enable();
        }
    }*/


    // Update is called once per frame
}