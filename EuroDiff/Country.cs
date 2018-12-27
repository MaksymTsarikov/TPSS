using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiff
{
    class Country
    {
        public string name;

        public bool IsComplete;

        public int days;

        public int xl, yl, xh, yh;

        public List<int> CitiesIds;

        public Country(string name, int xl, int yl, int xh, int yh)
        {
            this.CitiesIds = new List<int>();
            this.name = name;
            this.xl = xl;
            this.yl = yl;
            this.xh = xh;
            this.yh = yh;
            this.IsComplete = false;
        }

        public bool CheckCities(List<City> cityList, List<Country> countryList, int days)
        {
            int completedCities = 0;
            foreach(City city in cityList)
            {
                city.CheckCoins();
                if (CitiesIds.Contains(city.id) && cityList[city.id].IsComplete)
                {
                    completedCities += 1;
                }
            }
            if(completedCities == CitiesIds.Count)
            {
                this.days = days;
                this.IsComplete = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
