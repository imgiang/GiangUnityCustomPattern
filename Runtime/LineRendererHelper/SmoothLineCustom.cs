using System.Collections.Generic;
using UnityEngine;

namespace GiangCustom.Runtime.LineRendererHelper
{
    public class SmoothLineCustom
    {
        public static Vector3[] SmoothPoints(Vector3[] points, float smoothness)
        {
            if (points.Length < 2)
                return points;

            List<Vector3> smoothPoints = new List<Vector3>();
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 start = points[i];
                Vector3 end = points[i + 1];
                smoothPoints.Add(start);

                float distance = Vector3.Distance(start, end);
                int segments = Mathf.FloorToInt(distance / smoothness);

                for (int j = 1; j < segments; j++)
                {
                    float t = j / (float)segments;
                    Vector3 interpolatedPoint = Vector3.Lerp(start, end, t);
                    smoothPoints.Add(interpolatedPoint);
                }
            }
            smoothPoints.Add(points[points.Length - 1]); // Add the last point

            return smoothPoints.ToArray();
        }
    }
}