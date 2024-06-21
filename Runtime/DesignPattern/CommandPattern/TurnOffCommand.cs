using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.CommandPattern
{
    public class TurnOffCommand : MonoBehaviour, ICommand
    {
        private Fan fan;
        void Start()
        {
            fan = GetComponent<Fan>();
        }

        public void Execute()
        {
            fan.TurnOff();
        }

        public void Undo()
        {
            fan.TurnOn();
        }
    }
}
