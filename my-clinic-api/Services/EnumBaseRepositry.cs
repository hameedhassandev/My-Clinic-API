using my_clinic_api.Interfaces;

namespace my_clinic_api.Services
{
    public class EnumBaseRepositry : IEnumBaseRepositry
    {
        public IDictionary<int, string> GetValuesOfEnums(Type enumType)
        {
            IDictionary<int, string> data = new Dictionary<int, string>();
            for (int i = 0; i < Enum.GetNames(enumType).Length; i++)
            {
                data.Add(i, Enum.GetName(enumType, i));
            }
            return data;
        }
    }
}
