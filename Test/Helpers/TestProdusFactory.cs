using ProduseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public class TestProdusFactory
    {

            public static Produs CreateProdus(int id)
            {
                return new Produs
                {
                    Id = id,
                    Expirare = DateTime.Parse("04-10-2024"),
                    Name = "test" + id,
                    Pret = 2 * id
                };
            }

            public static List<Produs> CreateProduse(int count)
            {
                List<Produs> produs = new List<Produs>();
                for (int i = 1; i <= count; i++)
                {
                    produs.Add(CreateProdus(i));
                }

                return produs;
            }

        
    }
}
