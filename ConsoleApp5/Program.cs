using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                #region Читаем в БД из файла 1 раз для загрузки пар в БД
                //StreamReader f = new StreamReader("C:\\test.txt");
                //while (!f.EndOfStream)
                //{
                //    string s = f.ReadLine();
                //    Pair pair = new Pair();
                //    pair.Name = s;
                //    string[] words = s.Split(new char[] { '_' });
                //    pair.First = words[0];
                //    pair.Second = words[1];

                //    db.Pairs.Add(pair);
                //    db.SaveChanges();
                //    Console.WriteLine("=============");
                //    Console.WriteLine("Записана пара");
                //    Console.WriteLine(pair.Name);
                //    Console.WriteLine(pair.First);
                //    Console.WriteLine(pair.Second);

                //}
                //f.Close();
                #endregion

                int count = 0;
                string writePath = @"C:\string.txt"; //пишем результаты
                Pair pair = new Pair();
                
                var res1 = from test in pair.Pairs() select test;

                var MyPair = new Pair();
               
                string inpit; // = Console.ReadLine();
                foreach (var it in res1)
                {
                    inpit = it.Name;
                    MyPair = res1.FirstOrDefault(p => p.Name == inpit);

                    using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine($"\nВыбрана стартовая валюта  {MyPair.Name }  Депозит должен быть в  {MyPair.First} ");
                    }

                    var serch1 = res1.Where(p => (p.First == MyPair.Second && p.Second != MyPair.First) || (p.Second == MyPair.Second && p.First != MyPair.First));
                    
                    foreach (Pair item in serch1)
                    {
                        var users2 = res1.Where(p => (p.First == item.First && p.Second == MyPair.First) ||
                                                       (p.First == item.Second && p.Second == MyPair.First) ||
                                                       (p.Second == item.First && p.First == MyPair.First) ||
                                                       (p.Second == item.Second && p.First == MyPair.First));
                        
                        var user3 = users2.Where(p => p.Name != MyPair.Name);
                  
                        foreach (Pair item2 in user3)
                        {
                            ++count;
                            
                            Console.WriteLine($"{count}| {MyPair.Name}---{item.Name}---{item2.Name}");
                            
                            string mainResult = String.Concat(count,"\t|| " , MyPair.Name, "---", item.Name, "---", item2.Name);

                            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                            {
                                sw.WriteLine(mainResult);
                            }
                        }
                    }
                }
                Console.ReadKey();
            }
        }
    }
}
