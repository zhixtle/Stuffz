using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class CountryManager
    {
        private static CountryBL countryBL = new CountryBL();

        public List<Models.CountryModel> GetCountriesList()
        {
            List<Models.CountryModel> countriesList = new List<Models.CountryModel>();
            var countriesResult = countryBL.GetAllCountries();
            foreach (var item in countriesResult)
            {
                countriesList.Add(new Models.CountryModel()
                {
                    CountryID = item.ID,
                    CountryName = item.COUNTRY
                });
            }
            return countriesList;
        }
    }
}