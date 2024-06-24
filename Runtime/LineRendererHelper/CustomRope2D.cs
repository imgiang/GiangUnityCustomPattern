using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class CustomRope2D : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform navigator;
    [SerializeField] private Transform startLineTrs;

    [SerializeField] private GameObject pref;

    private Camera camera;
    private Rigidbody2D r2b;
    private bool isDragging;
    private float forceMultiplier = 10f;
    private Vector3 lastMousePosition;
    private int countControlUpdate = 0;
    private RaycastHit2D rayToClosestPivotPoint;
    private const string WRAPLAYERNAME = "RopeWrapLayer";
    private List<bool> pivotSwingList = new List<bool>();// if bool is true swing is clockwise
    private int pivotsAdded = 0;
    private float pushOutIncrement;
    private float oldAngle;
    private float currentAngle;
    private List<Vector2> lstEdgePoints = new List<Vector2>();
    private EdgeCollider2D edgeCollider2D;


    private bool endLevel = false;

    private void Start()
    {
        camera = Camera.main;
        
        r2b = navigator.GetComponent<Rigidbody2D>();
        r2b.gravityScale = 0;

        edgeCollider2D = GetComponent<EdgeCollider2D>();
        
        line.positionCount = 2;
        SetRopeEndPoints();
        UpdateEdgeCollider2D();
    }

    private void Update()
    {
        if (endLevel) return;
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = MousePos();
            countControlUpdate = 15;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            r2b.velocity = Vector2.zero;
            countControlUpdate = 0;
        }
        
        if (IsLineOfSiteToClosestPivotClear() == false)            //The rope should wrap if there is an obstacle between player and the closest pivot/bending point
            WrapTheRope();
        else if (IsLineOfSightTo2ndClosestPivotClear()
                 || IsShootLineCastCheckUnWrap())            //Since rope does not wrap, we should check if it should Unwrap
            ClearClosestPivotAndSwingFromList();
        
        if (isDragging)
        {
            Drag();
        }
        SetRopeEndPoints();

        if (Input.GetMouseButtonUp(0))
        {
            CheckEndLevel();
        }
    }

    private void CheckEndLevel()
    {
        var hit = Physics2D.Raycast(navigator.transform.position, 
            Vector2.zero, 1f, LayerMask.GetMask("Target"));
        if (hit && hit.transform.gameObject.CompareTag(Tag.endPoint))
        {
            endLevel = true;
            line.SetPosition(0, hit.transform.GetComponent<Transform>().position);
        }
    }
    bool IsShootLineCastCheckUnWrap()
    {
        if (line.positionCount <= 2) return false;
        RaycastHit2D hit = Physics2D.Linecast((Vector2)line.GetPosition(0),
            (Vector2)line.GetPosition(2),
            1 << LayerMask.NameToLayer(WRAPLAYERNAME));
        if (hit)
        {
            return false;
        }

        return true;
    }

    private void Drag()
    {
        if (countControlUpdate == 20)
        {
            var pos = MousePos();
        
            r2b.velocity = (pos - lastMousePosition) * 1700 * Time.deltaTime;
            lastMousePosition = pos;
            countControlUpdate = 0;
        }
        countControlUpdate++;
        
        SetRopeEndPoints();
    }

    private void SetRopeEndPoints()
    {
        line.SetPosition(0, navigator.transform.position); //Line starts with player
        line.SetPosition(line.positionCount - 1,
            startLineTrs.position); //Line ends at anchor point 
    }

    private void UpdateEdgeCollider2D()
    {
        var tmpLstPoints = new Vector3[line.positionCount];
        line.GetPositions(tmpLstPoints);
        lstEdgePoints.Clear();
        foreach (var t in tmpLstPoints)
        {
            lstEdgePoints.Add(t - transform.position);
        }
            
        if (edgeCollider2D)
        {
            edgeCollider2D.points = lstEdgePoints.ToArray();
        }
    }

    private Vector3 MousePos()
    {
        var p = camera.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        return p;
    }
    
#region Wrap Related Region
    bool IsLineOfSiteToClosestPivotClear()
    {
        rayToClosestPivotPoint = SendRayToClosestPivotPoint();

        if (rayToClosestPivotPoint.collider != null)        //If the ray to closest/latest pivot point hits an obstacle, line of site is not clear
            return false;
        else
            return true;
    }
    RaycastHit2D SendRayToClosestPivotPoint()
    {
        Vector2 playerPosition = (Vector2)navigator.transform.position;

        float ropeDistance = 0;
        Vector2 rayDirection;
        Vector2 closestPivotToPlayer = (Vector2)line.GetPosition(1);
        ropeDistance = Vector2.Distance(closestPivotToPlayer, playerPosition);
        rayDirection = closestPivotToPlayer - playerPosition;

        LayerMask layerMask = LayerMask.GetMask(WRAPLAYERNAME);        //We want the rope to wrap only around specific objects on RopeWrapLayer

        // Debug.DrawLine(playerPosition, playerPosition+rayDirection * ropeDistance, Color.grey);
        RaycastHit2D rayResult = Physics2D.Raycast(playerPosition, rayDirection, ropeDistance, layerMask);        //Send a ray from players position to closest/lates pivot point.
        return rayResult;
    }

    void WrapTheRope()
    {
        Vector2 polygonVertexPoint = GetClosestColliderPointFromRaycastHit(rayToClosestPivotPoint,
            rayToClosestPivotPoint.collider.gameObject.GetComponent<PolygonCollider2D>());

        AddSwingDirectionForNewPivot(polygonVertexPoint);
        AddLineRenderPivotPoint(polygonVertexPoint);
        PushPivotPointOutwards(rayToClosestPivotPoint.collider.gameObject.GetComponent<Rigidbody2D>());
    }
    
    void AddSwingDirectionForNewPivot(Vector2 polygonHitPoint)
    {
        bool isSwingClockWise = CheckSwingDirectionByPlayerPositon(polygonHitPoint);

        pivotSwingList.Add(isSwingClockWise);
    }
    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));

        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        
        var polygonVertexPoint = orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
        
        polygonVertexPoint =
            VectorTranslations(polygonVertexPoint, 
                (polygonVertexPoint - (Vector2)hit.transform.gameObject.transform.position).normalized, 0.02f);
        return polygonVertexPoint;
    }

    private Vector2 VectorTranslations(Vector2 polygonVertexPoint, Vector2 direction, float magnitude)
    {
        return polygonVertexPoint + direction * magnitude;
    }
    void AddLineRenderPivotPoint(Vector2 polygonHitPoint)
    {
        pivotsAdded++;

        Vector2 playerNextFramePosition = (Vector2)navigator.transform.position;

        Vector2[] tempPoints = new Vector2[line.positionCount + 1];
        tempPoints[0] = line.GetPosition(0);
        tempPoints[1] = polygonHitPoint;

        for (int i = 2; i < line.positionCount + 1; i++)
            tempPoints[i] = line.GetPosition(i - 1);

        line.positionCount++;

        for (int i = 0; i < tempPoints.Length; i++)
        {
            line.SetPosition(i, (Vector3)tempPoints[i]);
        }
    }
    void PushPivotPointOutwards(Rigidbody2D rgbdWrapped)
    {
        Vector3 pointToPush = line.GetPosition(1);
        Vector2 pushVector = pointToPush - (Vector3)rgbdWrapped.worldCenterOfMass;
        pushVector = Vector2.ClampMagnitude(pushVector, pushOutIncrement * 5f);     //Pushed by half of the line width so that rope is not buried in obstacle when drawn on screen
        pointToPush += (Vector3)pushVector;
        line.SetPosition(1, pointToPush);
    }
    bool CheckSwingDirectionByPlayerPositon(Vector2 pivotPosition)
    {
        bool isSwingClockWise = false;
        float playerX = navigator.transform.position.x;
        float playerY = navigator.transform.position.y;

        Vector3 pivotPointNew = (Vector3)pivotPosition;
        Vector3 pivotPointOld = line.GetPosition(1);
        Vector3 playerPoint = new Vector3(playerX, playerY, 0); ;

        Vector3 firstVector = pivotPointOld - pivotPointNew;
        Vector3 secondVector = playerPoint - pivotPointNew;

        Vector3 leftHandRuleVector = Vector3.Cross(firstVector, secondVector);

        if (leftHandRuleVector.z > 0)
            isSwingClockWise = true;

        return isSwingClockWise;
    }
    bool IsLineOfSightTo2ndClosestPivotClear()
    {
        bool isClear;
        //First of all, we make sure there are more than 2 pivots (other than start&end points).
        //Which means rope has wrapped before and additional pivots are present. Otherwise there is nothing to "unwrap"
        if (pivotsAdded > 0)
        {
            if (SendRayTo2ndClosestPivotHit() && line.positionCount > 2
                                              && IsAngleGettingLarger() && IsPivotAngleOnCounterSwingDirection())
                isClear = true;
            else
                isClear = false;
        }
        else
            isClear = false;        //If there is nothing to unwrap, there is no 2nd closest pivot. So we return false anyway.

        return isClear;
    }
    bool SendRayTo2ndClosestPivotHit()
    {
        bool isLineToSecondPivotClear = true;

        float ropeDistance = Vector2.Distance((Vector2)line.GetPosition(2), (Vector2)navigator.transform.position);
        Vector2 rayDirection = (Vector2)line.GetPosition(2) - (Vector2)navigator.transform.position;
        LayerMask layerMask = LayerMask.GetMask(WRAPLAYERNAME);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)navigator.transform.position, rayDirection, ropeDistance * 0.95f, layerMask);

        if (hit.collider != null)
            isLineToSecondPivotClear = false;

        return isLineToSecondPivotClear;
    }
    bool IsAngleGettingLarger()
    {
        GetAngleBetweenPoints(navigator.transform.position, line.GetPosition(1), line.GetPosition(2));

        if (currentAngle > oldAngle)
            return true;
        else
            return false;
    }
    float GetAngleBetweenPoints(Vector3 a, Vector3 b, Vector3 c)
    {
        oldAngle = currentAngle;

        float result = 0;

        float ab = Vector2.Distance(a, b);
        float bc = Vector2.Distance(b, c);
        float ac = Vector2.Distance(a, c);

        float cosB = Mathf.Pow(ac, 2) - Mathf.Pow(ab, 2) - Mathf.Pow(bc, 2);
        cosB /= (2 * ab * bc);

        result = Mathf.Acos(cosB) * Mathf.Rad2Deg;
        currentAngle = result;
        return result;
    }
    
    bool IsPivotAngleOnCounterSwingDirection()
    {
        bool isPivotVsPlayerClockWise = false;
        int closestPivotIndex = 1;

        Vector2 pivotPoint = (Vector2)line.GetPosition(closestPivotIndex);
        Vector2 playerOldPoint = (Vector2)line.GetPosition(closestPivotIndex + 1);
        Vector2 playerNewPoint = navigator.transform.position;

        Vector2 firstVector = playerOldPoint - pivotPoint;
        Vector2 secondVector = playerNewPoint - pivotPoint;

        Vector3 leftHandRuleVector = Vector3.Cross(firstVector, secondVector);

        if (leftHandRuleVector.z < 0)
            isPivotVsPlayerClockWise = true;

        if (isPivotVsPlayerClockWise == pivotSwingList[pivotSwingList.Count - 1])
            return true;
        else
            return false;
    }
    void ClearClosestPivotAndSwingFromList()
    {
        pivotsAdded--;
        DeleteLastLineRenderBendPoint();
    }
    void DeleteLastLineRenderBendPoint()
    {
        pivotSwingList.RemoveAt(pivotSwingList.Count - 1);

        Vector2[] tempPoints = new Vector2[line.positionCount - 1];
        tempPoints[0] = line.GetPosition(0);

        for (int i = 1; i < line.positionCount - 1; i++)
            tempPoints[i] = line.GetPosition(i + 1);

        line.positionCount--;

        for (int i = 0; i < tempPoints.Length; i++)
            line.SetPosition(i, (Vector3)tempPoints[i]);
    }

#endregion
}
