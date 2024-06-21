namespace GiangCustom.Runtime.DesignPattern.CommandPattern
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
