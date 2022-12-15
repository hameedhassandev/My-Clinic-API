using my_clinic_api.Interfaces;

namespace my_clinic_api.Services
{
    public class Comparer2Lists : IComparer2Lists
    {
        public bool Comparer2IntLists(List<int> OrgList, List<int> SecList)
        {
            int counter = 0;
            foreach (var item in SecList)
            {
                foreach (var item2 in OrgList)
                {
                    if (item == item2)
                    {
                        counter++;
                    }
                }
            }
            if (counter != SecList.Count())
                return false;
            return true;

        }
    }
}
