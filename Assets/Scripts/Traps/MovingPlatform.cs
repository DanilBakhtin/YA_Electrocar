using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Trap
{
    [SerializeField] private float speedMove = 1f;
    [SerializeField] private Axis axis;

    private float leftBorder;
    private float rightBorder;
    [SerializeField] private Transform leftTrigger;
    [SerializeField] private Transform rightTrigger;
    [SerializeField] private Transform leftBorderObj;
    [SerializeField] private Transform rightBorderObj;

    //Направление
    private Vector3 dir;
    void Start()
    {
        dir = transform.right;

        if (axis == Axis.x)
        {
            leftBorder = leftBorderObj.position.x;
            rightBorder = rightBorderObj.position.x;
        }
        if (axis == Axis.z)
        {
            leftBorder = leftBorderObj.position.z;
            rightBorder = rightBorderObj.position.z;
        }
    }

    void FixedUpdate()
    {
        if (!isActivate)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speedMove * Time.fixedDeltaTime);
            if (leftTrigger.transform.position.x <= leftBorder && axis == Axis.x) dir *= -1f;
            if (rightTrigger.transform.position.x >= rightBorder && axis == Axis.x) dir *= -1f;

            if (leftTrigger.transform.position.z <= leftBorder && axis == Axis.z) dir *= -1f;
            if (rightTrigger.transform.position.z >= rightBorder && axis == Axis.z) dir *= -1f;
        }
    }

    public override void activate()
    {
        isActivate = true;

    }

    protected override void interactionPlayer(Collision collision)
    {

    }

    private enum Axis{
        x,
        z
    }

}
