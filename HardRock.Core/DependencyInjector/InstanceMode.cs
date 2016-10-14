namespace HardRock.Core.DependencyInjector
{
    public enum InstanceMode
    {
        Singleton,
        Transient,
        PerThread,
        PerWebRequest
    }
}