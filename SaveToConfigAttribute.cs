namespace My_BoomSosed_NET
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SaveToConfigAttribute : Attribute
    { 
        public string Name { get; }
        public SaveToConfigAttribute()
        {
        }
    }
}
