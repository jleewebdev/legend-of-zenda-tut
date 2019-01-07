using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    [SerializeField]
    float swingingSpeed = 2f;
    [SerializeField]
    float attackDuration = 0.35f;

    [SerializeField]
    float cooldownSpeed = 2f;
    [SerializeField]
    float cooldownDuration = 0.5f;

    bool isAttacking;

    float cooldownTimer;

    Quaternion targetRotation;

	// Use this for initialization
	void Start () {
        targetRotation = Quaternion.Euler(0, 0, 0); 
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking) {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, swingingSpeed * Time.deltaTime);
        } else {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, cooldownSpeed * Time.deltaTime);
        }
        cooldownTimer -= Time.deltaTime;
	}

    public void Attack() {
        if (cooldownTimer > 0f) {
            return;
        }
        targetRotation = Quaternion.Euler(90, 0, 0);
        cooldownTimer = cooldownDuration;

        StartCoroutine(CooldownWait());
    }

    IEnumerator CooldownWait() {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        targetRotation = Quaternion.Euler(0, 0, 0); 
    }
}
