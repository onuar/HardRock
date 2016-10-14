using System;
using System.Linq;
using HardRock.Core.Intercept.Aspect;

namespace HardRock.Core.Intercept
{
    public class AspectRunnerInterceptor : BaseInterceptor
    {
        public override object Intercept(IMethodInvocation methodInvocation)
        {
            var isDefined = Attribute.IsDefined(methodInvocation.Method, typeof(BaseAspectAttribute));

            if (!isDefined)
            {
                methodInvocation.Proceed();

                return methodInvocation.ReturnValue;
            }

            var attributes = methodInvocation.Method.GetCustomAttributes(typeof(BaseAspectAttribute), true).Cast<BaseAspectAttribute>().ToList();

            bool continueInvocation = true, invokeAfterAspects = true;

            try
            {
                attributes = attributes.OrderBy(attribute => attribute.Order).ToList();

                foreach (var aspectAttributeBaseAttribute in attributes)
                {
                    MethodInvocationContext invocationContext = aspectAttributeBaseAttribute.BeforeMethodInvocation(methodInvocation, aspectAttributeBaseAttribute.MethodInvocationContext);

                    aspectAttributeBaseAttribute.MethodInvocationContext = invocationContext;

                    if (invocationContext.BreakInvocation)
                    {
                        continueInvocation = false;
                    }

                    if (!invocationContext.InvokeAfterAspects)
                    {
                        invokeAfterAspects = false;
                    }
                }
                if (continueInvocation)
                {
                    methodInvocation.Proceed();
                }

                if (invokeAfterAspects)
                {
                    attributes.ForEach(attribute => { attribute.AfterMethodInvocation(methodInvocation, attribute.MethodInvocationContext); });
                }
            }
            catch (Exception ex)
            {
                attributes.ForEach(attribute => { attribute.OnMethodInvocationFailed(methodInvocation, ex); });
                //TODO: sorun çıkarabilir. 
                throw;
            }

            return methodInvocation.ReturnValue;
        }
    }
}
