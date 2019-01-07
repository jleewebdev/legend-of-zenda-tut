using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    GameObject model;
    [SerializeField]
    float rotationSpeed = 2;

    [SerializeField]
    float jumpingVelocity;
    [SerializeField]
    float movementVelocity;

    bool canJump;
    Rigidbody rb;
    Quaternion targetModelRotation;

    [Header("Equipment")]
    [SerializeField]
    public Sword sword;
    [SerializeField]
    GameObject bombPrefab;
    [SerializeField]
    float throwingSpeed;
    [SerializeField]
    int bombAmount = 5;
    [SerializeField]
    Bow bow;
    [SerializeField]
    int arrowAmount = 5;
    


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
 
        // Check if touching ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)) {
            canJump = true;
        }

        // Rotate model towards target rotation

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation,
            targetModelRotation,
            Time.deltaTime * rotationSpeed
            );

        ProcessInput();

    }

    void ProcessInput() {

        rb.velocity = new Vector3(
            0,
            rb.velocity.y,
            0
            );

        if (Input.GetKey("right")) {
            rb.velocity = new Vector3(
                movementVelocity,
                rb.velocity.y,
                rb.velocity.z
                );
            targetModelRotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey("left")) {
            rb.velocity = new Vector3(
                -movementVelocity,
                rb.velocity.y,
                rb.velocity.z
                );
            targetModelRotation = Quaternion.Euler(0, 270, 0);
        }
        if (Input.GetKey("up")) {
            rb.velocity = new Vector3(
                rb.velocity.x,
                rb.velocity.y,
                movementVelocity
                );

            targetModelRotation = Quaternion.Euler(0, 0, 0);

        }
        if (Input.GetKey("down")) {
            rb.velocity = new Vector3(
                rb.velocity.x,
                rb.velocity.y,
                -movementVelocity
                );

            targetModelRotation = Quaternion.Euler(0, 180, 0);

        }

        if (Input.GetKeyDown("space") && canJump) {
            canJump = false;
            rb.velocity = new Vector3(
                rb.velocity.x,
                jumpingVelocity,
                rb.velocity.z);
                
        }

        // Check equipment interaction

        if (Input.GetKeyDown("z")) {
            sword.Attack();
        }

        if (Input.GetKeyDown("x")) {
            if (arrowAmount > 0) {
                bow.Attack();
                arrowAmount--;
            }
        }

        if (Input.GetKeyDown("c")) {
            ThrowBomb();
        }
    }

    void ThrowBomb() {
        if (bombAmount <= 0) {
            return;
        }
        GameObject bombObject = Instantiate(bombPrefab);
        bombObject.transform.position = transform.position + model.transform.forward;

        Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;

        bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);

        bombAmount--;
    }
}
