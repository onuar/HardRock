using System;

namespace HardRock.Sample
{
    internal class SampleBusinessLevelException : Exception
    {
        public SampleBusinessLevelException(string message)
            : base(message)
        {
        }
    }
}