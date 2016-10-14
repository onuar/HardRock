namespace HardRock.Core.Initializer
{
    public interface IBootstrapperForPreStart
    {
        IBootstrapperForStart ExecuteStart();
    }
}
