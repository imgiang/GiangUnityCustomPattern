using System;
using UnityEngine;

namespace GiangCustom.Runtime.LineRendererHelper
{
    public class DrawWithMouse : MonoBehaviour
    {
        private LineRenderer line;
        private Vector3 previousPosition;

        [SerializeField] private float minDistance = 0.1f;
        // [SerializeField, Range(0.1f, 2f)] private float width;
        private void Start()
        {
            line = GetComponent<LineRenderer>();
            line.positionCount = 1;
            previousPosition = transform.position;
            // line.startWidth = line.endWidth = width;
        }

        public void SetLine(LineRenderer lineRenderer)
        {
            line = lineRenderer;
        }

        public void StartLine(Vector2 position)
        {
            line.positionCount = 1;
            line.SetPosition(0, position);
        }

        private void UpdateLine()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPosition.z = 0;

                if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
                {
                    if (previousPosition == transform.position)
                    {
                        line.SetPosition(0, currentPosition);
                    }
                    else
                    {
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, currentPosition);
                        previousPosition = currentPosition;
                    }
                }
            }
        }
    }
}

