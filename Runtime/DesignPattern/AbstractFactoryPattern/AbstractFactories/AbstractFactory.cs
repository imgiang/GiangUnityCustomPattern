using GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts;
using GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Factories;
using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.AbstractFactories
{
    public abstract class AbstractFactory : MonoBehaviour, IAnimalFactory
    {
        public abstract IAnimal CreateAnimal(int type);
    }
}
