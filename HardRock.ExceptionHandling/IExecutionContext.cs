using System.Reflection;

namespace HardRock.ExceptionHandling
{
    public interface IExecutionContext
    {
        object[] Arguments { get; }
        MethodInfo Method { get; }
    }
}