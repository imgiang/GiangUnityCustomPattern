using UnityEngine;

namespace GiangCustom.Runtime.LayerMaskHelper
{
    public class LayerMaskHelper
    {
        public static bool IsGameObjectInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }
    }
}
