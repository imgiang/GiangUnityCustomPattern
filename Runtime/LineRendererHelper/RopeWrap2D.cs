using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class RopeWrap2D : MonoBehaviour
{
    [SerializeField] private Transform navigator;
    [SerializeField] private LayerMask layermaskObstacle;
    private LineRenderer lineRenderer;
    private Vector3 startPoint;
    private bool isDragging = false;
    private Vector3 previousMousePosition;
    private Vector3 accumulatedDrag;
    private EdgeCollider2D m_EdgeCollider2D;
    private List<Vector2> lstEdgePoints = new List<Vector2>();
    private Camera camera;

    private bool stopLine = false;

    private void Start()
    {
        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        
        lineRenderer.positionCount = 2;
        startPoint = new Vector3(transform.position.x, transform.position.y, 0);
        lineRenderer.SetPosition(0, startPoint);
        
        Vector2 endPos = new Vector3(startPoint.x + 1, startPoint.y, 0);
        lineRenderer.SetPosition(1, endPos);
        
        navigator.position = endPos;
        accumulatedDrag = new Vector3(1, 0, 0);
        m_EdgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            previousMousePosition = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        
        if (isDragging)
        {
            var currentMousePosition = Input.mousePosition;
        
            var worldDragDelta = Camera.main.ScreenToWorldPoint(new Vector3(currentMousePosition.x,
                                         currentMousePosition.y, Camera.main.nearClipPlane))
                                     - Camera.main.ScreenToWorldPoint(new Vector3(previousMousePosition.x,
                                         previousMousePosition.y, Camera.main.nearClipPlane));
        
            accumulatedDrag += new Vector3(worldDragDelta.x, worldDragDelta.y, 0);
        
            var newEndPoint = startPoint + accumulatedDrag;
            newEndPoint.z = 0;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newEndPoint);
            navigator.position = newEndPoint;
            previousMousePosition = currentMousePosition;
        
            var tmpLstPoints = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(tmpLstPoints);
            lstEdgePoints.Clear();
            foreach (var t in tmpLstPoints)
            {
                lstEdgePoints.Add(t - transform.position);
            }
            
            if (m_EdgeCollider2D)
            {
                m_EdgeCollider2D.points = lstEdgePoints.ToArray();
            }
        }
    }

    private void CheckCollideBlock()
    {
        var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        var hit = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.zero);
        if (hit &&
            (GameManager.Instance.layerBlockLine.value & 
             (1 << hit.transform.gameObject.layer)) != 0)
        {
            
        }
    }
}
