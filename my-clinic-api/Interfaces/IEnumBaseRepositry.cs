namespace my_clinic_api.Interfaces
{
    public interface IEnumBaseRepositry
    {
        public IDictionary<int, string> GetValuesOfEnums(Type enumType);
    }
}
