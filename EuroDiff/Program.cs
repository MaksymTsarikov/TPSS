using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            int command = Convert.ToInt32(Console.ReadLine());
            List<Country> countryList = new List<Country>();
            List<City> cityList = new List<City>();
            for (int i = 0; i < command; i++)
            {
                string[] countryInput = Console.ReadLine().Split(' ');
                string countryName = countryInput[0];
                int xl = int.Parse(countryInput[1]);
                int yl = int.Parse(countryInput[2]);
                int xh = int.Parse(countryInput[3]);
                int yh = int.Parse(countryInput[4]);
                countryList.Add(new Country(countryName, xl, yl, xh, yh));
                for(int x = xl; x <= xh; x++)
                {
                    for(int y = yl; y <= yh; y++)
                    {
                        cityList.Add(new City(countryName, x, y, cityList.Count));
                        countryList[i].CitiesIds.Add(cityList.Count - 1);
                    }
                }
            }
            foreach(City city in cityList)
            {
                City tmpCity = cityList.Find(c => c.x == city.x + 1 && c.y == city.y);
                if (tmpCity != null)
                {
                    if(!city.neighboorCities.Contains(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id)))
                    {
                        city.neighboorCities.Add(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id));
                    }
                }
                tmpCity = cityList.Find(c => c.x == city.x - 1 && c.y == city.y);
                if (tmpCity != null)
                {
                    if (!city.neighboorCities.Contains(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id)))
                    {
                        city.neighboorCities.Add(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id));
                    }
                }
                tmpCity = cityList.Find(c => c.y == city.y - 1 && c.x == city.x);
                if (tmpCity != null)
                {
                    if (!city.neighboorCities.Contains(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id)))
                    {
                        city.neighboorCities.Add(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id));
                    }
                }
                tmpCity = cityList.Find(c => c.y == city.y + 1 && c.x == city.x);
                if (tmpCity != null)
                {
                    if (!city.neighboorCities.Contains(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id)))
                    {
                        city.neighboorCities.Add(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id));
                    }
                }
                foreach(Country country in countryList)
                {
                    if (!city.coins.ContainsKey(country.name))
                    {
                        city.coins.Add(country.name, 0);
                    }
                }
            }
            int days = 1;
            while (true)
            {
                foreach (City city in cityList)
                {
                    city.MoveCoins(countryList, cityList, city.country, city.id);
                }
                int tmpResult = 0;
                foreach(Country country in countryList)
                {
                    if (country.IsComplete)
                    {
                        tmpResult += 1;
                    }
                    else if(country.CheckCities(cityList, countryList, days))
                    {
                        tmpResult += 1;
                    }
                }
                if(tmpResult == countryList.Count)
                {
                    foreach(Country country in countryList)
                    {
                        Console.WriteLine(country.name + " " + country.days);
                    }
                    Console.ReadLine();
                    return;
                }
                days++;
            }
        }
    }
}
