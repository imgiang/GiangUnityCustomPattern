using System.Collections.Generic;
using GiangCustom.DesignPattern;
using UnityEngine;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts
{
    public class AnimalCapture : SingletonMonoBehaviour<AnimalCapture>
    {
        [SerializeField] private List<GameObject> listAnimal;

        public GameObject GetAnimal(string n)
        {
            foreach (var animal in listAnimal)
            {
                if (animal.name == n)
                {
                    return animal;
                }
            }
            return null;
        }
    }
}
