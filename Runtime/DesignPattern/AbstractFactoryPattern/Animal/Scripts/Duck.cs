using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts
{
    public class Duck : TwoLegsAnimal
    {
        public override void GetName()
        {
            Debug.Log("this is: " + name);
        }
    }
}
