using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class CountryBL
    {
        private static DataAccess<REF_COUNTRY> countryDataAccess = new DataAccess<REF_COUNTRY>();

        public List<REF_COUNTRY> GetAllCountries()
        {
            return countryDataAccess.GetAll();
        }

        public string GetCountryName(int? countryID)
        {
            string country = countryDataAccess.GetSingle(c => c.ID == countryID).COUNTRY;
            return country;
        }
    }
}
