namespace Planex.Web
{
    using Glimpse.AspNet.Extensions;
    using Glimpse.Core.Extensibility;

    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimeEvent ExecuteOn => RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource;

        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            var httpContext = policyContext.GetHttpContext();
            if (!httpContext.User.IsInRole("Manager"))
            {
                return RuntimePolicy.Off;
            }

            return RuntimePolicy.On;
        }
    }
}