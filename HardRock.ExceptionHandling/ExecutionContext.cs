using System.Reflection;

namespace HardRock.ExceptionHandling
{
    public class ExecutionContext : IExecutionContext
    {
        public ExecutionContext(MethodInfo method, params object[] arguments)
        {
            Method = method;
            Arguments = arguments;
        }

        public MethodInfo Method { get; private set; }

        public object[] Arguments { get; private set; }
    }
}
