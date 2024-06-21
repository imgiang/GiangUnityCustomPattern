using GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts;
using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.AbstractFactories
{
    public class FourLegsAnimalFactory : AbstractFactory
    {
        public override IAnimal CreateAnimal(int type)
        {
            if (type == 0)
            {
                Debug.Log("Duck");
                return Instantiate(AnimalCapture.Instance.GetAnimal("Duck")).GetComponent<IAnimal>();
            }
            else
            {
                Debug.Log("Chicken");
                return Instantiate(AnimalCapture.Instance.GetAnimal("Chicken")).GetComponent<IAnimal>();
            }
        }
    }
}
