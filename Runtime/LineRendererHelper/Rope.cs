using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform player;

    public LineRenderer rope;
    public LayerMask collMask;

    public List<Vector3> ropePositions { get; set; } = new List<Vector3>();

    private void Awake() => AddPosToRope(transform.position);

    private void Update()
    {
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();

        DetectCollisionEnter();
        if (ropePositions.Count > 2) DetectCollisionExits();        
    }

    private void DetectCollisionEnter()
    {
        // var hit2 = Physics2D.Linecast((Vector2)player.position, rope.GetPosition(ropePositions.Count - 2), collMask);
        // Debug.DrawLine((Vector2)player.position, rope.GetPosition(ropePositions.Count - 2));
        // if (hit2)
        // {
        //     Debug.Log(hit2.transform.name);
        //     ropePositions.RemoveAt(ropePositions.Count - 1);
        //     AddPosToRope(hit2.point);
        // }
        RaycastHit hit;
        if (Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 2), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 1);
            AddPosToRope(hit.point);
        }
    }

    private void DetectCollisionExits()
    {
        RaycastHit hit;
        if (!Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 3), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 2);
        }
        // var hit2 = Physics2D.Linecast((Vector2)player.position, rope.GetPosition(ropePositions.Count - 3), collMask);
        // Debug.DrawLine((Vector2)player.position, rope.GetPosition(ropePositions.Count - 3));
        // if (hit2)
        // {
        //     Debug.Log(hit2.transform.name);
        //     ropePositions.RemoveAt(ropePositions.Count - 2);
        // }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
        ropePositions.Add(player.position); //Always the last pos must be the player
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos() => rope.SetPosition(rope.positionCount - 1, player.position);
}
