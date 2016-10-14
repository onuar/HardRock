namespace HardRock.Core.Initializer
{
    public interface IBootstrapperInitializer
    {
        IBootstrapperForPreStart ExecutePreStart();
    }
}
