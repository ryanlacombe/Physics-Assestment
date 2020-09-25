using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementBehavior : MonoBehaviour
{
    private CharacterController _controller = null;
    private Animator _animator = null;

    public float speed = 1.0f;
    public float turnRate = 1.0f;
    public float pushPower = 2.0f;

    public bool tankControls = true;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if (tankControls)
        {
            //Tank movement
            _controller.SimpleMove(transform.forward * desiredMovement.z * speed);
            transform.Rotate(transform.up, desiredMovement.x * turnRate);
        }
        else
        {
            //World-based movement
            _controller.SimpleMove(desiredMovement);
            if (desiredMovement.magnitude > 0)
                transform.forward = desiredMovement.normalized;
        }
        //Animate based on movement
        _animator.SetFloat("Speed", desiredMovement.z * speed);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody otherRB = hit.rigidbody;
        //Stop if there is no rigidbody or if that rigidboy is kinematic
        if (otherRB == null || otherRB.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
        //otherRB.velocity = pushDirection * pushPower;
        otherRB.AddForceAtPosition(pushDirection * pushPower, hit.point);
    }
}