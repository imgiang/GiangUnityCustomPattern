using UnityEngine;

namespace GiangCustom.Runtime.RayCastHelper
{
    public static class ShootingRayCast
    {
        public static RaycastHit2D ShootRayCastTarget(Vector2 targetRayCast)
        {
            if (Camera.main == null) return default;
		
            Vector2 cubeRay = Camera.main.ScreenToWorldPoint(targetRayCast);
            var hit = Physics2D.Raycast(cubeRay, Vector2.zero);

            return !hit ? default : hit;
        }
    }
}
