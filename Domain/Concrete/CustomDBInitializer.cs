using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Domain.Entities;

namespace Domain.Concrete
{
    public class CustomDBInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {

            #region User roles

            UserRole adminRole = (new UserRole() { Name = "admin" });

            UserRole userRole = (new UserRole() { Name = "user" });

            UserRole bannedRole = (new UserRole() { Name = "banned" });


            IList<UserRole> roles = new List<UserRole>(){ adminRole, userRole, bannedRole };

            foreach (UserRole rl in roles)
                context.UserRoles.Add(rl);

            #endregion


            #region User profiles

            UserProfile yarProfile = new UserProfile()
            {
                RegistrationDate = DateTime.Now,
                PhoneNumber = "+79063347212",
                IsEmailConfirmed = false
            };

            UserProfile axenProfile = new UserProfile()
            {
                RegistrationDate = DateTime.Now,
                PhoneNumber = "+79063469312",
                IsEmailConfirmed = false
            };

            UserProfile adminProfile = new UserProfile()
            {
                RegistrationDate = DateTime.Now,
                PhoneNumber = "+375292844589",
                IsEmailConfirmed = false
            };

            IList<UserProfile> ups = new List<UserProfile>();

            foreach (UserProfile up in ups)
                context.UserProfiles.Add(up);

            #endregion


            #region Users

            User yarushkin = new User()
            {
                Name = "Max Yarushkin",
                Role = userRole,
                Email = "gofingerstyle@gmail.com",
                Password = Crypto.HashPassword("gofingerstyle"),
                Profile = yarProfile
            };

            User axenov = new User()
            {
                Name = "Andrey Axenov",
                Role = userRole,
                Email = "axenov@gmail.com",
                Password = Crypto.HashPassword("axenov"),
                Profile = axenProfile
            };

            User admin = new User()
            {
                Name = "Admin",
                Role = adminRole,
                Email = "admin",
                Password = Crypto.HashPassword("admin"),
                Profile = adminProfile
            };


            IList<User> users = new List<User>() { admin, yarushkin, axenov };

            foreach (User us in users)
                context.Users.Add(us);

            #endregion


            #region Categories


            #region Guitars

            Category guitars = new Category()
            {
                Name = "Guitars",
                Description = "The guitar is a fretted musical instrument that usually has six strings."
            };

            Category electricGuitars = new Category()
            {
                Name = "Electric guitars",
                Description = "Electric guitars are solid-bodied guitars that are designed to be plugged into an amplifier.",
                SubCategory = guitars
            };

            Category acousticGuitars = new Category()
            {
                Name = "Acoustic guitars",
                Description = "An acoustic guitar is a guitar that produces sound acoustically—by transmitting the vibration of the strings to the air—as opposed to relying on electronic amplification (see electric guitar). The sound waves from the strings of an acoustic guitar resonate through the guitar's body, creating sound." +
                              " This typically involves the use of a sound board and a sound box to strengthen the vibrations of the strings.",
                SubCategory = guitars
            };

            Category classicalGuitars = new Category()
            {
                Name = "Classic guitars",
                Description = "The classical guitar (also known as concert guitar, classical acoustic, nylon-string guitar, or Spanish guitar) is the member of the guitar family used in classical music." +
                              " It is an acoustical wooden guitar with strings made of nylon, rather than the metal strings used in acoustic and electric guitars.",
                SubCategory = guitars
            };

            Category acousticElectricGuitars = new Category()
            {
                Name = "Electro-acoustic guitars",
                Description = "An acoustic-electric guitar is an acoustic guitar fitted with a magnetic pickup, a piezoelectric pickup or a microphone.",
                SubCategory = guitars
            };

            Category twelveStringGuitars = new Category()
            {
                Name = "Twelve-string guitars",
                Description = "The twelve-string guitar is a steel-string guitar with twelve strings in six courses," +
                              " which produces a richer, more ringing tone than a standard six-string guitar.",
                SubCategory = guitars
            };

            Category steelGuitars = new Category()
            {
                Name = "Electric guitars",
                Description = "The steel guitar is unusual in that it is played horizontally across the player's lap. " +
                              "The steel guitar originates from Hawaii where local musicians, newly introduced to the European guitar," +
                              " developed a style of playing involving alternative tunings and the use of a slide." +
                              " The Hawaiian guitarists found that by laying the guitar flat across the lap they could better control the slide. " +
                              "In response to this new playing style some Hawaiian steel guitars were constructed with a small rectangular body" +
                              " which made them more suitable for laying across the lap.",
                SubCategory = guitars
            };

            Category archTopGuitars = new Category()
            {
                Name = "Archtop guitars",
                Description = "The archtop is a semi-hollow steel-string acoustic or electric guitar." +
                              " The arched table combined with violin-style f-holes and internal sound-block creates a timbre that is acoustic and mellow. " +
                              "These two factors have made archtops a firm favourite with jazz guitarists.",
                SubCategory = guitars
            };

            Category resonatorGuitars = new Category()
            {
                Name = "Resonator guitars",
                Description = "Resonator guitars are distinctive for not having a regular sound hole instead they have a large circular perforated cover plate which conceals a resonator cone." +
                              "The cone is made from spun aluminum and resembles a loudspeaker." +
                              "The bridge is connected to either the center or edge of the cone by an aluminum spring called the spider." +
                              "The vibrations from the spider are projected by the cone through the perforated cover plate." +
                              "The most common resonator guitars have a single cone although the original model patented in 1927 by John Dopyera had three and was called a tricone resophonic guitar.Resonator guitars are loud and bright." +
                              "They are popular with blues and country guitarists and can be played with a slide or conventionally.",
                SubCategory = guitars
            };

            Category doubleNeckGuitars = new Category()
            {
                Name = "Double-neck guitars",
                Description = "The archtop is a semi-hollow steel-string acoustic or electric guitar." +
                              " The arched table combined with violin-style f-holes and internal sound-block creates a timbre that is acoustic and mellow. " +
                              "These two factors have made archtops a firm favourite with jazz guitarists.",
                SubCategory = guitars
            };

            Category bassGuitars = new Category()
            {
                Name = "Bass guitars",
                Description = "The bass guitar has a long neck (scale-length) and thick strings. The open strings of the bass guitar corresponds to the four lowest strings of the guitar " +
                              "and are pitched an octave lower. The standard bass has four strings though five and six string basses are available which extends the range of the instrument." +
                              " Though the bass guitar is the bass instrument of the guitar family and the double-bass is the bass instrument of the orchestral string family their similar roles have drawn bass players to both instruments.",
                SubCategory = guitars
            };


            IList<Category> cgs = new List<Category>()
            {
                guitars,
                electricGuitars,
                acousticGuitars,
                classicalGuitars,
                acousticElectricGuitars,
                twelveStringGuitars,
                steelGuitars,
                archTopGuitars,
                resonatorGuitars,
                doubleNeckGuitars,
                bassGuitars
            };

            #endregion


            #region Other

            cgs.Add(new Category()
            {
                Name = "Keys",
                Description =
                    "A keyboard instrument is a musical instrument played using a keyboard, a row of levers which are pressed by the fingers. " +
                    "The most common of these are the piano, organ, and various electronic keyboards, including synthesizers and digital pianos. Other keyboard instruments include celestas, which are struck idiophones operated by a keyboard, and carillons," +
                    " which are usually housed in bell towers or belfries of churches or municipal buildings."
            });

            cgs.Add(new Category()
            {
                Name = "Drums",
                Description =
                    "The drum is a member of the percussion group of musical instruments. In the Hornbostel-Sachs classification system, it is a membranophone." +
                    " Drums consist of at least one membrane, called a drumhead or drum skin, that is stretched over a shell and struck, either directly with the player's hands, or with a drum stick, to produce sound. " +
                    "There is usually a resonance head on the underside of the drum, typically tuned to a slightly lower pitch than the top drumhead. Other techniques have been used to cause drums to make sound, such as the thumb roll. " +
                    "Drums are the world's oldest and most ubiquitous musical instruments, and the basic design has remained virtually unchanged for thousands of years."
            });

            cgs.Add(new Category()
            {
                Name = "Orchestral instruments",
                Description =
                    "An orchestra is a large instrumental ensemble typical of classical music, which mixes instruments from different families, " +
                    "including bowed string instruments such as violin, viola, cello and double bass, as well as brass, woodwinds," +
                    " and percussion instruments, each grouped in sections. Other instruments such as the piano and celesta may sometimes appear in a fifth keyboard section or may stand alone,"
                    + " as may the concert harp and, for performances of some modern compositions, electronic instruments."
            });

            cgs.Add(new Category()
            {
                Name = "Accessories",
                Description =
                    "Accessories for Music Instruments Strings, bows, guitar equipment, music stands, rosin, mouthpiece, violin bridge etc."
            });

            #endregion


            foreach (Category cg in cgs)
                context.Categories.Add(cg);

            #endregion


            #region Lots

            IList<Lot> lots = new List<Lot>();

            lots.Add(new Lot()
            {
                Title = "Sigma omm-st из массива ели",
                Description = "Отличная акустическая гитара! Форма корпуса Grand auditorium. Верхняя дека из массива ели," +
                              " что придаёт инструменту очень яркий звук!",
                ViewCount = 0,
                CurrentPrice = 350,
                GoldPrice = 550,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 12, 18),
                IsEnded = false,
                Owner = yarushkin,
                Categories = { guitars, acousticGuitars }
            });

            lots.Add(new Lot()
            {
                Title = "Fender cd-60-ce",
                Description = "Верхняя дека шпон, цвет Natural. Стоит пьезодатчик от Fishman с трёхполосным эквалайзером.",
                ViewCount = 0,
                CurrentPrice = 200,
                GoldPrice = 350,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 11, 18),
                IsEnded = false,
                Owner = axenov,
                Categories = { guitars, acousticElectricGuitars }
            });

            lots.Add(new Lot()
            {
                Title = "Maton TE series",
                Description = "Австралийский инструмент серии Tommy Emmanuel!",
                ViewCount = 0,
                CurrentPrice = 1500,
                GoldPrice = 2000,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 11, 25),
                IsEnded = false,
                Owner = yarushkin,
                Categories = { guitars, acousticElectricGuitars }
            });

            lots.Add(new Lot()
            {
                Title = "Washburn wd 7s",
                Description = "Великолепная гитара с топом из массива ели!",
                ViewCount = 0,
                CurrentPrice = 150,
                GoldPrice = 400,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 11, 12),
                IsEnded = false,
                Owner = axenov,
                Categories = { guitars, acousticGuitars }
            });

            lots.Add(new Lot()
            {
                Title = "Baton Rouge AR11C / ACE",
                Description = "Прекрасная середина, яркие низы и сочный бас! Всё что нужно для счастья в одном инструменте!",
                ViewCount = 0,
                CurrentPrice = 300,
                GoldPrice = 400,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 10, 12),
                IsEnded = false,
                Owner = yarushkin,
                Categories = { guitars, acousticGuitars }
            });

            lots.Add(new Lot()
            {
                Title = "Sigma DM-ST",
                Description = "Корпус - дредноут ,верхняя дека - массив ели, накладка на гриф - палисандр.",
                ViewCount = 0,
                CurrentPrice = 500,
                GoldPrice = 550,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 12, 12),
                IsEnded = false,
                Owner = axenov,
                Categories = { guitars, acousticGuitars }
            });

            lots.Add(new Lot()
            {
                Title = "Kibin A-style",
                Description = "Мастеровой инструмент от Кибиня Андрея из г.Гродно.",
                ViewCount = 0,
                CurrentPrice = 1500,
                GoldPrice = 2550,
                UploadDate = DateTime.Now,
                EndOfTranding = new DateTime(2017, 12, 10),
                IsEnded = false,
                Owner = yarushkin,
                Categories = { guitars, acousticGuitars }
            });

            foreach (Lot lot in lots)
                context.Lots.Add(lot);

            #endregion


            base.Seed(context);
        }
    }
}
