namespace My_BoomSosed_NET
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SaveToConfigFileAttribute : Attribute
    { 
        public string Name { get; }
        public SaveToConfigFileAttribute(string Name)
        {
            this.Name = Name; 
        }
    }
}
