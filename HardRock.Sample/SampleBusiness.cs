using System;

namespace HardRock.Sample
{
    public class SampleBusiness : ISampleBusiness
    {
        public void DoSomething()
        {
            Console.WriteLine("Sample business does something.");
        }

        public void ThrowException()
        {
            throw new SampleBusinessLevelException("An error occured!");
        }
    }
}