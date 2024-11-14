using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartScrollEvent : MonoBehaviour {
    [SerializeField] private Transform scrollTarget;
    [SerializeField] private InputActionAsset playerMovement;
    [SerializeField] private GameObject player;
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;
    private Vector3 targetPosition;
    
    
}