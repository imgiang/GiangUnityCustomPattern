using System;
using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.CommandPattern
{
    public class TurnOnCommand : MonoBehaviour, ICommand
    {
        private Fan fan;
        private void Start()
        {
            fan = GetComponent<Fan>();
        }

        public void Execute()
        {
            fan.TurnOn();
        }

        public void Undo()
        {
            fan.TurnOff();
        }
    }
}
