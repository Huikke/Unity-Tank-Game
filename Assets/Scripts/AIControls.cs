using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControls : MonoBehaviour
{
    public float movementSpeed;
    public float turningSpeed;
    public float turretTurningSpeed;
    public float shootingCooldown;
    public float detectRange;
    public float stoppingRange;
    public float switchTargetDistance;
    public float switchDistance;

    public float AIDelay;

    public Transform turret;
    public Transform muzzle;
    public GameObject projectile;

    public string stringState;

    private Rigidbody rb;
    private float t;
    private float AIt;

    private GameObject targetObject;
    private Vector3 target;

    private int obstacleMask;

    private enum State { forward, left, right, back, stop };
    private State state;
    private State nextState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = 0f;
        AIt = 0f;
        obstacleMask = LayerMask.GetMask("Obstacle");
        state = State.forward;
        nextState = State.forward;
    }

    void FixedUpdate()
    {
        Vector3 currentRotation = rb.rotation.eulerAngles;
        rb.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);

        // Etsit‰‰n pelaaja
        if(targetObject != null)
        {
            target = targetObject.transform.position;
        }
        else
        {
            targetObject = GameObject.FindGameObjectWithTag("Player");
        }

        float angle = Vector3.SignedAngle(transform.forward, target - transform.position, Vector3.up);

        // Tilakone
        if (state == State.forward)
        {
            stringState = "forward";
            if (angle < 0)
            {
                Turning(-1f);
            }
            else if (angle > 0)
            {
                Turning(1f);
            }

            if (Mathf.Abs(angle) < 90)
            {
                Move(1f);
            }
        }
        if (state == State.left)
        {
            stringState = "left";
        }
        if (state == State.right)
        {
            stringState = "right";
        }
        if (state == State.back)
        {
            stringState = "back";
        }
        if (state == State.stop)
        {
            stringState = "stop";
        }
    }

    private void Move(float input)
    {
        Vector3 movement = transform.forward * input * movementSpeed;
        rb.velocity = movement;
    }

    private void Turning(float input)
    {
        Vector3 turning = Vector3.up * input * turningSpeed;
        rb.angularVelocity = turning;
    }
}
