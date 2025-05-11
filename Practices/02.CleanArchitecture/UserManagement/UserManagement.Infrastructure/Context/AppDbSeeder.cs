using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Context;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        if (context.Users.Any())
        {
            return; // Ya hay datos
        }

        context.Users.AddRange(
            new User { Email = "kplet0@joomla.org", FullName = "Kari Plet", Password = "tZ0I%tm?**\\\"Y", UserName = "kplet0" },
            new User { Email = "sfookes1@unesco.org", FullName = "Saxe Fookes", Password = "oI2#wlf''", UserName = "sfookes1" },
            new User { Email = "pstuffins2@wiley.com", FullName = "Pam Stuffins", Password = "rL0qW|q@~<Ya,", UserName = "pstuffins2" },
            new User { Email = "acristol3@dailymotion.com", FullName = "Averil Cristol", Password = "kE60oQ~)", UserName = "acristol3" },
            new User { Email = "aphette4@dropbox.com", FullName = "Adina Phette", Password = "cK7t!W%Z|khx*(W1", UserName = "aphette4" },
            new User { Email = "lhappert5@sfgate.com", FullName = "Lexy Happert", Password = "xG2OJtFSw,W}12wl", UserName = "lhappert5" },
            new User { Email = "dsoppit6@about.me", FullName = "Dulcea Soppit", Password = "gH6a&?@k(u''{XNZ2", UserName = "dsoppit6" },
            new User { Email = "bdavydkov7@artisteer.com", FullName = "Brander Davydkov", Password = "oP7q.3\"20e,9Yv", UserName = "bdavydkov7" },
            new User { Email = "cpallent8@un.org", FullName = "Christina Pallent", Password = "hR9W<qY9l+,", UserName = "cpallent8" },
            new User { Email = "shuman9@edublogs.org", FullName = "Shirl Human", Password = "xU2t%2d0*", UserName = "shuman9" },
            new User { Email = "cluqueta@icq.com", FullName = "Charity Luquet", Password = "cD1a2P\"=4fmM", UserName = "cluqueta" },
            new User { Email = "hfortb@noaa.gov", FullName = "Haskel Fort", Password = "zY0z{@j3`c>\bSIV", UserName = "hfortb" },
            new User { Email = "bcostanc@privacy.gov.au", FullName = "Bette-ann Costan", Password = "eB6Vj>5De}JDh", UserName = "bcostanc" },
            new User { Email = "reisigd@webnode.com", FullName = "Royal Eisig", Password = "vF5nEHFFU!isi>", UserName = "reisigd" },
            new User { Email = "scottue@123-reg.co.uk", FullName = "Serge Cottu", Password = "jW4(Qa+%f7oTDA", UserName = "scottue" },
            new User { Email = "aantognozziif@kickstarter.com", FullName = "Ayn Antognozzii", Password = "aZ8(#KEZ\"z?dlz4E", UserName = "aantognozziif" },
            new User { Email = "mtilberryg@slashdot.org", FullName = "Marlow Tilberry", Password = "yC2*1X9f", UserName = "mtilberryg" },
            new User { Email = "cingremh@dyndns.org", FullName = "Corilla Ingrem", Password = "eE2==3y47`j|", UserName = "cingremh" },
            new User { Email = "cbuncomi@histats.com", FullName = "Cybil Buncom", Password = "gK97UCrs2,uz8", UserName = "cbuncomi" },
            new User { Email = "wvahlj@reverbnation.com", FullName = "Wylma Vahl", Password = "zX06{q9iY9$6%", UserName = "wvahlj" },
            new User { Email = "despositok@unc.edu", FullName = "Duncan Esposito", Password = "lE2)H4Fy{sTh\"7)", UserName = "despositok" },
            new User { Email = "wspreulll@printfriendly.com", FullName = "Willabella Spreull", Password = "gS4r__5Ce(U}x~`", UserName = "wspreulll" },
            new User { Email = "qmillsapm@shareasale.com", FullName = "Quintina Millsap", Password = "hL9B(ZlW+Yz", UserName = "qmillsapm" },
            new User { Email = "bhinkensn@addthis.com", FullName = "Bobbee Hinkens", Password = "vI2I}E42&nwKj", UserName = "bhinkensn" },
            new User { Email = "spollastrinoo@360.cn", FullName = "Stanly Pollastrino", Password = "jH1X!L@R@<pVPr", UserName = "spollastrinoo" }
        );

        context.SaveChanges();
    }
}
