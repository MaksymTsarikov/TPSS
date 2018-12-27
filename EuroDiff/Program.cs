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
            int command = 0;
            while (true)
            {
                try
                {
                    command = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invlid input, try again");
                }
            }

            List<Country> countryList = new List<Country>();
            List<City> cityList = new List<City>();
            for (int i = 0; i < command; i++)
            {
                string[] countryInput = Console.ReadLine().Split(' ');
                string countryName = countryInput[0];

                int xl = 0;
                try
                {
                    xl = int.Parse(countryInput[1]);
                    if (xl <= 0)
                    {
                        Console.WriteLine("Invalid input");
                        i--;
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    i--;
                    continue;
                }
                int yl = 0;
                try
                {
                    yl = int.Parse(countryInput[2]);
                    if (yl <= 0)
                    {
                        Console.WriteLine("Invalid input");
                        i--;
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    i--;
                    continue;
                }
                int xh = 0;
                try
                {
                    xh = int.Parse(countryInput[3]);
                    if (xh <= 0)
                    {
                        Console.WriteLine("Invalid input");
                        i--;
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    i--;
                    continue;
                }
                int yh = 0;
                try
                {
                    yh = int.Parse(countryInput[4]);
                    if (yh <= 0)
                    {
                        Console.WriteLine("Invalid input");
                        i--;
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    i--;
                    continue;
                }
                countryList.Add(new Country(countryName, xl, yl, xh, yh));
                for (int x = xl; x <= xh; x++)
                {
                    for (int y = yl; y <= yh; y++)
                    {
                        cityList.Add(new City(countryName, x, y, cityList.Count));
                        countryList[i].CitiesIds.Add(cityList.Count - 1);
                    }
                }
            }
            foreach (City city in cityList)
            {
                AddNeighborCity(cityList, 1, 0, city);
                AddNeighborCity(cityList, -1, 0, city);
                AddNeighborCity(cityList, 0, -1, city);
                AddNeighborCity(cityList, 0, 1, city);
                foreach (Country country in countryList)
                {
                    if (!city.coins.ContainsKey(country.name))
                    {
                        city.coins.Add(country.name, 0);
                    }
                }
            }
            int borderCnt = 0;
            foreach(City city in cityList)
            {
                foreach(KeyValuePair<string, int> neighbor in city.neighborCities)
                {
                    if(city.country != cityList[neighbor.Value].country)
                    {
                        borderCnt++;
                    }
                }
            }
            if(borderCnt == 0)
            {
                Console.WriteLine("Not all countries have neighbors. Invalid input.");
                Console.ReadLine();
                return;
            }
            int days = 1;
            while (true)
            {
                foreach (City city in cityList)
                {
                    city.MoveCoins(countryList, cityList, city.country, city.id);
                }
                foreach (City city in cityList)
                {
                    city.Finalise(countryList, cityList, city.country, city.id);
                }
                int tmpResult = 0;
                foreach (Country country in countryList)
                {
                    if (country.IsComplete)
                    {
                        tmpResult += 1;
                    }
                    else if (country.CheckCities(cityList, countryList, days))
                    {
                        tmpResult += 1;
                    }
                }
                if (tmpResult == countryList.Count)
                {
                    foreach (Country country in countryList)
                    {
                        Console.WriteLine(country.name + " " + country.days);
                    }
                    Console.ReadLine();
                    return;
                }

                days++;
            }
        }

        private static void AddNeighborCity(List<City> cityList, int deltaX, int deltaY, City currentCity)
        {
            City tmpCity = cityList.Find(c => c.x == currentCity.x + deltaX && c.y == currentCity.y + deltaY);
            if (tmpCity != null)
            {
                if (!currentCity.neighborCities.Contains(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id)))
                {
                    currentCity.neighborCities.Add(new KeyValuePair<string, int>(tmpCity.country, tmpCity.id));
                }
            }
        }
    }
}
