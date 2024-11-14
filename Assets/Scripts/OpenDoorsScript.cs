using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorsScript : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    private Animator _animDoor1;
    private Animator _animDoor2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider other) {
        _animDoor1.SetTrigger("open");
        _animDoor2.SetTrigger("open");
        Destroy(gameObject);
    }
    void Start() {
        _animDoor1 = door1.GetComponent<Animator>();
        _animDoor2 = door2.GetComponent<Animator>();
        _animDoor1.speed = 0.2f;
        _animDoor2.speed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
