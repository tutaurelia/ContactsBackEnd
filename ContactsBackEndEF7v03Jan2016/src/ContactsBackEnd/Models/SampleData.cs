using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ContactsBackEnd.Models
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ContactsContext>();

            if (!context.Contacts.Any())
            {

                context.Contacts.AddRange(new List<Contact>()
                    {
                        new Contact("wim", "abbas", "Zwijnaardsesteenweg 744", 2540, "HOVE", "03 455 19 62",
                            "wim.abbas@tutaurelia.net", new DateTime(1985, 10, 15)),
                        new Contact("angelov", "abbasi", "Zwijnaardsesteenweg 557", 1330, "RIXENSART", "02 652 22 86",
                            "angelov.abbasi@tutaurelia.net", new DateTime(1979, 3, 2)),
                        new Contact("hans", "abdi sharmarke", "Zwijnaardse Steenweg 5 / 7", 1200, "BRUXELLES",
                            "02 770 09 34", "hans.abdi.sharmarke@tutaurelia.net", new DateTime(1966, 6, 22)),
                        new Contact("fatou", "abdulhameed jam", "Zwarte Vijverstraat 71", 8400, "OOSTENDE",
                            "059 70 52 60", "fatou.abdulhameed.jam@tutaurelia.net", new DateTime(1967, 5, 15)),
                        new Contact("bernard", "abdullahi", "Zur Domäne 7", 1970, "WEZEMBEEK,OPPEM", "02 731 23 57",
                            "bernard.abdullahi@tutaurelia.net", new DateTime(1964, 11, 15)),
                        new Contact("bergmans", "abi char", "Zuidlaan 64", 1000, "BRUSSEL", "02 644 00 00",
                            "bergmans.abi.char@tutaurelia.net", new DateTime(1979, 11, 24)),
                        new Contact("gerry", "abied", "Zuiderlaan 1,3", 1180, "BRUXELLES", "02 376 56 50",
                            "gerry.abied@tutaurelia.net", new DateTime(1997, 12, 6)),
                        new Contact("gerrit", "abon escalona", "Zuiderakker 4", 9090, "MELLE", "09 225 59 49",
                            "gerrit.abon.escalona@tutaurelia.net", new DateTime(1992, 4, 20)),
                        new Contact("meiying", "aboulakil", "Zuiderakker 4", 1070, "BRUXELLES", "02 521 63 15",
                            "meiying.aboulakil@tutaurelia.net", new DateTime(1974, 6, 15)),
                        new Contact("yi", "abraham", "Zoutelaan 81", 1180, "BRUXELLES", "02 375 84 08",
                            "yi.abraham@tutaurelia.net", new DateTime(1988, 4, 28))
                    });
                context.SaveChanges();
            }
        }
    }
}