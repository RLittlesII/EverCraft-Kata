namespace Evercraft.Dice
{
    public interface IDieRoller
    {
        int Roll<T>() where T : IDie;
    }
}