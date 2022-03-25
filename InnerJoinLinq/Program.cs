using System;
using System.Collections.Generic;
using System.Linq;

namespace InnerJoinLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Buyer> buyers = new List<Buyer>()
            {
                new Buyer(){Name = "Jonhy", District = "Fantasy District", Age = 22},
                new Buyer(){Name = "Peter", District = "Scientists District", Age = 40},
                new Buyer(){Name = "Paul", District = "Fantasy District", Age = 30},
                new Buyer(){Name = "Maria", District = "Scientists District", Age = 35},
                new Buyer(){Name = "Joshua", District = "EarthIsFlat District", Age = 40},
                new Buyer(){Name = "Sylvia", District = "Developers District", Age = 22},
                new Buyer(){Name = "Rebecca", District = "Scientists District", Age = 30},
                new Buyer(){Name = "Jaime", District = "Developers District", Age = 35},
                new Buyer(){Name = "Pierce", District = "Fantasy District", Age = 40},
            };
            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier(){Name = "Harrison", District = "Fantasy District", Age = 22},
                new Supplier(){Name = "Charles", District = "Developers District", Age = 40},
                new Supplier(){Name = "Hailee", District = "Scientists District", Age = 35},
                new Supplier(){Name = "Taylor", District = "EarthIsFlat District", Age = 30},
            };

            Console.WriteLine("1) Agrupar por Distrito las personas y mostrar si es Comprador o Proveedor");

            var innerJoin = suppliers.Join(buyers, s => s.District, b => b.District,
                (s, b) => new
                {
                    SupplierName = s.Name,
                    BuyerName = b.Name,
                    District = s.District // podría tomarlo del s también

                });

            foreach (var item in innerJoin)
            {
                Console.WriteLine($"District: {item.District}, Suppliers: {item.SupplierName}, Buyers: {item.BuyerName}");
            }
            Console.WriteLine("-------------");

            Console.WriteLine("2) Agrupar por Distrito y Edad las personas y mostrar sus datos");

            var compositeJoin = suppliers.Join(buyers,
                s => new { s.District, s.Age },
                b => new { b.District, b.Age },
                (s, b) => new
                {
                    SupplierName = s.Name,
                    BuyerName = b.Name,
                    District = b.District,
                    Age = s.Age // podría tomarlo del b también
                });

            foreach (var item in compositeJoin)
            {
                Console.WriteLine($"{item.District}, Age: {item.Age}");
                Console.WriteLine($"Supplier: {item.SupplierName}");
                Console.WriteLine($"Buyer: {item.BuyerName}");
            }
            Console.WriteLine("-------------");

            Console.WriteLine("3) Agrupar por Distrito las personas y mostrar si es Comprador o Proveedor con Syntax Method");

            var innerJoinSyntax = from s in suppliers
                                  orderby s.District
                                  join b in buyers on s.District equals b.District
                                  select new
                                  {
                                      suppliersName = s.Name,
                                      buyersName = b.Name,
                                      b.District
                                  };

            foreach (var item in innerJoinSyntax)
            {
                Console.WriteLine($"District: {item.District}, Supplier: {item.suppliersName}, Buyer: {item.buyersName}");
            }
            Console.WriteLine("-------------");
        }
    }
}
