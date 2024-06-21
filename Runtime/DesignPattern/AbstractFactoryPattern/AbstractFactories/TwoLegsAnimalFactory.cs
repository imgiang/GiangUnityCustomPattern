using GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts;
using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.AbstractFactories
{
    public class TwoLegsAnimalFactory : AbstractFactory
    {
        public override IAnimal CreateAnimal(int type)
        {
            if (type == 0)
            {
                Debug.Log("Dog");
                return Instantiate(AnimalCapture.Instance.GetAnimal("Dog")).GetComponent<IAnimal>();
            }
            else
            {
                Debug.Log("Cat");
                return Instantiate(AnimalCapture.Instance.GetAnimal("Cat")).GetComponent<IAnimal>();
            }
        }
    }
}
