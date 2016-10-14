namespace HardRock.Sample
{
    public interface ISampleBusiness
    {
        [SampleAspect]
        void DoSomething();

        void ThrowException();
    }
}