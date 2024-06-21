using GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Animal.Scripts;

namespace GiangCustom.Runtime.DesignPattern.AbstractFactoryPattern.Factories
{
    public interface IAnimalFactory
    {
        IAnimal CreateAnimal(int type);
    }
}
