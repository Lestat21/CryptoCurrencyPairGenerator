using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp5
{
    public class Pair
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string First { get; set; }
        public string Second { get; set; }



        public List<Pair> Pairs()
        {
            ApplicationContext appContext = new ApplicationContext();

            var result = from p in appContext.Pairs
                         select new Pair
                         {
                             First = p.First,
                             Second = p.Second,
                             Name = p.Name
                         };

            return result.ToList();


        }
    }
}
