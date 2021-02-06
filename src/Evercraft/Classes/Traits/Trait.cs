namespace Evercraft.Classes.Traits
{
    public abstract class Trait : ITrait
    {
        void ITrait.Apply() => Apply();
        
        protected virtual void Apply() {}
    }

    public interface ITrait<T>
    {
        void Apply();
    }
}