namespace HardRock.Core.Module
{
    public class CustomOrderedModuleAttribute : ModuleOrderAttribute
    {
        public CustomOrderedModuleAttribute(int order) : base(order)
        {
        }
    }
}
