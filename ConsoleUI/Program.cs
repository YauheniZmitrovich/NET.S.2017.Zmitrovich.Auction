using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;
using Domain.Entities;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new EFDbContext())
            {

              /  UserRole adm = new UserRole { Name = "administrator" };

                //UserRole mod = new UserRole { Name = "moderator" };

                //UserRole cl = new UserRole { Name = "client" };

                //UserRole ban = new UserRole { Name = "banned" };


               // ctx.UserRoles.Add(adm);
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
