using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts
{
    public abstract class TwoLegsAnimal : MonoBehaviour, IAnimal
    {
        public abstract void GetName();
    }
}
