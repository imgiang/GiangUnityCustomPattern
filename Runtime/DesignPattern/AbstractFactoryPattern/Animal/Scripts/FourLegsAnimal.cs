using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts
{
    public abstract class FourLegsAnimal : MonoBehaviour, IAnimal
    {
        public abstract void GetName();
    }
}
