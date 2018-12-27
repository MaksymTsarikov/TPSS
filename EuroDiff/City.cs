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


        public List<int> outcomes = new List<int>();

        public int id;

        public bool IsComplete;

        public string country;

        public Dictionary<string, int> coins;

        public List<KeyValuePair<string, int>> neighborCities;

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
            this.neighborCities = newcity.neighborCities;
        }

        public City(string countryName, int x, int y, int id)
        {
            this.coins = new Dictionary<string, int>();
            this.neighborCities = new List<KeyValuePair<string, int>>();
            this.id = id;
            this.x = x;
            this.y = y;
            this.country = countryName;
            this.coins.Add(countryName, 1000000);
            this.IsComplete = false;
        }

        public void MoveCoins(List<Country> countryList, List<City> cityList, string country, int cityId)
        {

            foreach (KeyValuePair<string, int> neighbor in neighborCities)
            {
                foreach(KeyValuePair<string, int> tmpCoins in this.coins.ToArray())
                {
                    int cntr = cityList[cityId].coins[tmpCoins.Key] / 1000;
                    int cntrr = cntr;
                    outcomes.Add(cntrr);
                    /*tmp[neighbor.Value].coins[tmpCoins.Key] += cnt;
                    tmp[cityId].coins[tmpCoins.Key] -= cnt;*/
                }
                
            }
        }
        public void Finalise(List<Country> countryList, List<City> cityList, string country, int cityId)
        {
            int cnt = 0;
            foreach (KeyValuePair<string, int> neighbor in neighborCities)
            {
                foreach (KeyValuePair<string, int> tmpCoins in this.coins.ToArray())
                {
                    cityList[neighbor.Value].coins[tmpCoins.Key] += outcomes[cnt];
                    cityList[cityId].coins[tmpCoins.Key] -= outcomes[cnt];
                    cnt++;
                }

            }
            outcomes.Clear();
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
