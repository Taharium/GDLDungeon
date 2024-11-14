using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTorches : MonoBehaviour {
    [SerializeField] private GameObject[] corridors;
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private float delayBetweenTorches = 0.5f;
    private Animator _animDoor1;
    private Animator _animDoor2;

    private readonly Dictionary<int, (Light[], GameObject[])> _torchData = new Dictionary<int, (Light[], GameObject[])>();

    void Start() {
        _animDoor1 = door1.GetComponent<Animator>();
        _animDoor2 = door2.GetComponent<Animator>();
        _animDoor1.speed = 0.5f;
        _animDoor2.speed = 0.5f;
        CacheTorchLightsAndFlames();
    }

    // Cache all the lights and flames ahead of time
    private void CacheTorchLightsAndFlames() {
        for (int i = 0; i < corridors.Length; i++) {
            Transform torch1 = corridors[i].transform.GetChild(0); 
            Transform torch2 = corridors[i].transform.GetChild(1);

            // Get the light components
            Light pointLight1 = torch1.Find("Point Light").GetComponent<Light>();
            Light pointLight2 = torch2.Find("Point Light").GetComponent<Light>();

            // Get the flame GameObjects
            GameObject flame1 = torch1.Find("FX_Fire_Small_01").gameObject;
            GameObject flame2 = torch2.Find("FX_Fire_Small_01").gameObject;

            _torchData[i] = (new Light[] { pointLight1, pointLight2 }, new GameObject[] { flame1, flame2 });
        }
    }

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(LightUpTorchesSequentially());
    }

    IEnumerator LightUpTorchesSequentially() {
        for (int i = 0; i < corridors.Length; i++) {
            var (lights, flames) = _torchData[i];

            lights[0].enabled = true;
            lights[1].enabled = true;

            flames[0].GetComponent<ParticleSystem>().Play();
            flames[1].GetComponent<ParticleSystem>().Play();
            flames[0].SetActive(true);
            flames[1].SetActive(true);

            yield return new WaitForSeconds(delayBetweenTorches);
        }
        _animDoor1.SetTrigger("open");
        _animDoor2.SetTrigger("open");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() { }
}