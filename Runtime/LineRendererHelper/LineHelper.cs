using UnityEngine;

namespace GiangCustom.Runtime.LineRendererHelper
{
    public class LineHelper : MonoBehaviour
    {
        public static void ScrollLine(LineRenderer line, Vector2 direction)
        {
            line.material.SetTextureOffset("_MainTex", direction * Time.time);
        }
    }
}
