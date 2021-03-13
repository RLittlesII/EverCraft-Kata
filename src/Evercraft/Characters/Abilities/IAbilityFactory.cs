namespace Evercraft.Characters.Abilities
{
    public interface IAbilityFactory
    {
        T Create<T>(int score)
            where T : Ability;
    }
}