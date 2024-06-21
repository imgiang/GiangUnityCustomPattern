using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.CommandPattern
{
    public class Fan : MonoBehaviour
    {
        public void TurnOn()
        {
            Debug.Log("Turn On");
        }

        public void TurnOff()
        {
            Debug.Log("Turn Off");
        }
    }
}
