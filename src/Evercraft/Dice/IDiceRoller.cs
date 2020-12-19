namespace Evercraft
{
    public interface IDiceRoller
    {
        public int Roll<T>() where T : IDie;
    }
}