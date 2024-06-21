using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts
{
    public class Cat : FourLegsAnimal
    {
        public override void GetName()
        {
            Debug.Log("this is: " + name);
        }
    }
}
