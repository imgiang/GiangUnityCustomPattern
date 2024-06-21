using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts
{
    public class Chicken : TwoLegsAnimal
    {
        public override void GetName()
        {
            Debug.Log("this is: " + name);
        }
    }
}
