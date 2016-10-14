namespace HardRock.Core.Initializer
{
    public interface IBootstrapperStart
    {
        IBootstrapperInitializer InitializeApplication();

        IBootstrapperForPreStart InitializeApplicationWithPreStart();
    }
}
