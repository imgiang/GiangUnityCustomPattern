using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.CommandPattern
{
    public class Remote : MonoBehaviour
    {
        [SerializeField] private TurnOnCommand turnOnCommand;
        [SerializeField] private TurnOffCommand turnOffCommand;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TurnOn();
            }
            if (Input.GetMouseButtonDown(1))
            {
                TurnOff();
            }
        }

        private void TurnOn()
        {
            turnOnCommand.Execute();
        }

        private void TurnOff()
        {
            turnOffCommand.Execute();
        }
    }
}
