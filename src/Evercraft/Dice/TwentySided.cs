namespace Evercraft
{
    public class TwentySided : DieBase
    {
        protected override int Roll() => NumberGenerator.Next(20);
    }
}