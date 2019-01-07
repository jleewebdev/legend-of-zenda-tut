using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField]
    float duration = 5f;
    [SerializeField]
    float radius = 3f;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    float explosionDuration = 0.25f;


    bool exploded;

    float timer;


	// Use this for initialization
	void Start () {
        timer = duration;
        explosion.transform.localScale = Vector3.one * radius;
        explosion.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0f && exploded == false) {
            exploded = true;
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);


            StartCoroutine(Explode());

        }
	}

    IEnumerator Explode() {
        explosion.SetActive(true);
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);

    }
}
