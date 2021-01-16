namespace Evercraft.Dice
{
    public sealed class TwentySided : DieBase
    {
        protected override int Roll() => NumberGenerator.Next(1, 20);
    }
}