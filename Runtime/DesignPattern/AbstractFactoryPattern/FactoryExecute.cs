using System;
using System.Collections;
using System.Collections.Generic;
using GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.AbstractFactories;
using UnityEngine;

public class FactoryExecute : MonoBehaviour
{
    [SerializeField] private TwoLegsAnimalFactory twoLegsAnimalFactory;
    [SerializeField] private FourLegsAnimalFactory fourLegsAnimalFactory;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            twoLegsAnimalFactory.CreateAnimal(0);
        }
        if (Input.GetMouseButtonUp(0))
        {
            twoLegsAnimalFactory.CreateAnimal(1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            fourLegsAnimalFactory.CreateAnimal(0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            fourLegsAnimalFactory.CreateAnimal(1);
        }
    }
}
