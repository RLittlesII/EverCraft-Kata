using ReactiveUI;

namespace Evercraft.Tests
{
    public abstract class TestBase
    {
        protected TestBase() => RxApp.DefaultExceptionHandler = new EvercraftExceptionHandler();
    }
}