using UnityEngine;

namespace GiangCustom.Runtime.RayCastHelper
{
    public static class ShootingRayCast
    {
        public static RaycastHit2D ShootRayCastTarget<T>(Vector2 targetRayCast, LayerMask layer)
        {
            if (Camera.main == null) return default;
		
            Vector2 cubeRay = Camera.main.ScreenToWorldPoint(targetRayCast);
            return Physics2D.Raycast(cubeRay, Vector2.zero, 0f, layer);
        }
    }
}
