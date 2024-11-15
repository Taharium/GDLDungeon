using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathToLava : MonoBehaviour {
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private GameObject playerCapsule;
    [SerializeField] private Transform playerCamera;


    private void OnTriggerEnter(Collider other) {
        inputAction.Disable();

        CharacterController characterController = playerCapsule.GetComponent<CharacterController>();
        if (characterController != null) {
            characterController.enabled = false;
        }

        Debug.Log(playerCapsule.transform.position);
        playerCapsule.transform.position = new Vector3(-25f, -131f, -336f);
        Debug.Log(playerCapsule.transform.position);

        playerCamera.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (characterController != null) {
            characterController.enabled = true;
        }

        inputAction.Enable();
    }

    /*private void OnTriggerEnter(Collider other) {
        inputAction.Disable();
        Debug.Log(playerCapsule.transform.position);
        playerCapsule.transform.position = new Vector3(-25f, -131f, -336f);
        Debug.Log(playerCapsule.transform.position);
        playerCamera.rotation = quaternion.Euler(0f, 180f, 0f);
        //StartCoroutine(Restart());
    }*/

    // Update is called once per frame
    void Update() { }
}