using System.Collections.Generic;
using UnityEngine;

namespace GiangCustom.Runtime.Polygon
{
    public class PolygonSplitter : MonoBehaviour
    {
        public static (List<Vector2> leftPoints, List<Vector2> rightPoints) DividePolygonPoints(
            PolygonCollider2D polygonCollider, Vector2 linePoint1, Vector2 linePoint2)
        {
            List<Vector2> leftPoints = new List<Vector2>();
            List<Vector2> rightPoints = new List<Vector2>();

            Vector2[] polygonPoints = polygonCollider.points;

            foreach (Vector2 point in polygonPoints)
            {
                Vector2 worldPoint = polygonCollider.transform.TransformPoint(point);

                if (IsPointLeftOfLine(linePoint1, linePoint2, worldPoint))
                {
                    leftPoints.Add(worldPoint);
                }
                else
                {
                    rightPoints.Add(worldPoint);
                }
            }

            if (leftPoints.Count > 1 && 
                Vector2.Distance(linePoint1, leftPoints[^1]) > Vector2.Distance(linePoint1, leftPoints[0]))
            {
                leftPoints.Reverse();
            }

            if (rightPoints.Count > 1 &&
                Vector2.Distance(linePoint1, rightPoints[^1]) > Vector2.Distance(linePoint1, rightPoints[0]))
            {
                rightPoints.Reverse();
            }

            return (leftPoints, rightPoints);
        }

        private static bool IsPointLeftOfLine(Vector2 linePoint1, Vector2 linePoint2, Vector2 point)
        {
            float crossProduct = (linePoint2.x - linePoint1.x) * (point.y - linePoint1.y) - (linePoint2.y - linePoint1.y) * (point.x - linePoint1.x);
            return crossProduct > 0;
        }
    }
}
