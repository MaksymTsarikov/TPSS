using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiff
{

    static class Extensions
    {
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
    class City : ICloneable
    {
        public int x, y;

        public int id;

        public bool IsComplete;

        public string country;

        public Dictionary<string, int> coins;

        public List<KeyValuePair<string, int>> neighboorCities;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public City(City newcity)
        {
            this.coins = newcity.coins;
            this.country = newcity.country;
            this.id = newcity.id;
            this.x = newcity.x;
            this.y = newcity.y;
            this.IsComplete = newcity.IsComplete;
            this.neighboorCities = newcity.neighboorCities;
        }

        public City(string countryName, int x, int y, int id)
        {
            this.coins = new Dictionary<string, int>();
            this.neighboorCities = new List<KeyValuePair<string, int>>();
            this.id = id;
            this.x = x;
            this.y = y;
            this.country = countryName;
            this.coins.Add(countryName, 1000000);
            this.IsComplete = false;
        }

        public void MoveCoins(List<Country> countryList, List<City> cityList, string country, int cityId)
        {
            List<City> tmp = Extensions.Clone<City>(cityList);
            
            foreach(KeyValuePair<string, int> neighboor in neighboorCities)
            {
                foreach(KeyValuePair<string, int> tmpCoins in this.coins.ToArray())
                {
                    int cnt = cityList[cityId].coins[tmpCoins.Key] / 1000;
                    tmp[neighboor.Value].coins[tmpCoins.Key] += cnt;
                    tmp[cityId].coins[tmpCoins.Key] -= cnt;
                }
                
            }
            cityList = tmp;
        }

        public bool CheckCoins()
        {
            if (!coins.ContainsValue(0))
            {
                this.IsComplete = true;
                return true;
            }
            return false;
        }
    }
}
