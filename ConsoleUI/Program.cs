using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;
using Domain.Concrete.Repositories;
using Domain.Entities;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new EFDbContext())
            {
                var rep = new EFLotRepository();

                IEnumerable<Lot> lots = rep.Lots;

                foreach (var l in lots)
                {
                    Console.WriteLine(l.Title);
                }

                Console.ReadKey();

                //  Locality ostr = new Locality() { Name = "Астровля",District = "Лидский район",Region = "Гроднеская область",Type="д."};

                //UserRole mod = new UserRole { Name = "moderator" };

                //UserRole cl = new UserRole { Name = "client" };

                //UserRole ban = new UserRole { Name = "banned" };


                // ctx.Localities.Add(ostr);
                // ctx.UserRoles.Add(adm);
                //ctx.UserRoles.Add(mod);
                //ctx.UserRoles.Add(cl);
                //ctx.SaveChanges();


                //User max = new User { Name = "Max Yarushkin",Role=}


                //Lot lt = new Lot
                //{
                //    Title = "Sigma omm-st из массива ели",
                //    Description =
                //        "Отличная акустическая гитара! Форма корпуса Grand auditorium. Верхняя дека из массива ели, что придаёт инструменту яркий звук!",
                //    ViewCount =0, CurrentPrice = 350,GoldPrice=550,UploadDate = DateTime.Now,
                //    EndOfTranding= new DateTime(2017, 12, 18),IsEnded=false,Owner=


            }
        }
    }
}
