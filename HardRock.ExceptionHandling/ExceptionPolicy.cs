using System;
using System.Collections.Generic;
using System.Linq;
using HardRock.ExceptionHandling.Interfaces;

namespace HardRock.ExceptionHandling
{
    public class ExceptionPolicy<TException> : ITypedExceptionPolicy<TException>
        where TException : Exception
    {
        private readonly List<BaseExceptionHandler> _handlers;
        public string Name { get; set; }

        public ExceptionPolicy()
        {
            _handlers = new List<BaseExceptionHandler>();
        }

        public List<BaseExceptionHandler> Handlers => _handlers;

        public void ExecuteHandlers(Exception exception, IExecutionContext executionContext)
        {
            if (_handlers.Count != 0)
            {
                var orderedHandlers = _handlers.OrderBy(_ => _.Order);
                foreach (var handler in orderedHandlers)
                {
                    handler.HandledException(exception, executionContext);
                }
            }
        }
    }
}