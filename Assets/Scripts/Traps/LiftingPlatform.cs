using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatform : Trap
{
    [SerializeField] private float speedMove = 1f;
    private float startSpeed;
    [SerializeField] private float timeBtwMove;

    private float topBorder;
    private float downBorder;
    [SerializeField] private Transform topTrigger;
    [SerializeField] private Transform downTrigger;

    private bool isStop;

    //Направление
    private Vector3 dir;
    void Start()
    {   
        isStop = false;
        dir = transform.up * -1;
        startSpeed = speedMove;

        //topBorder = topTrigger.position.y;
        downBorder = downTrigger.position.y;
    }

    void FixedUpdate()
    {
        if (!isStop)
        {
            if (speedMove <= 0f) return;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speedMove * Time.fixedDeltaTime);
            if (topTrigger.position.y <= downBorder) StartCoroutine(changeDirection());
            if (downTrigger.position.y >= downBorder) dir *= -1f;
        }
    }

    public override void activate()
    {
        isStop = !isStop;
    }
    protected override void interactionPlayer(Collision collision)
    {

    }

    private IEnumerator changeDirection()
    {
        speedMove = 0f;
        yield return new WaitForSeconds(timeBtwMove);

        speedMove = startSpeed;
        dir *= -1f;
    }
}
