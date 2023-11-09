using System;

namespace Ean13Calc
{
   public class Program
   {
      static void Main(string[] args)
      {
         try
         {
            if (args.Length == 2 && args[0] == "-v")
            {
               string eanNumber = args[1];
               if (Ean13Calculator.IsValid(eanNumber))
                  Console.WriteLine("{0} Is valid", eanNumber);
               else
                  Console.WriteLine("{0} Is NOT valid", eanNumber);
               return;
            }

            if (args.Length == 2 && args[0] == "-g")
            {
               int nNumbers = int.Parse(args[1]);
               GenerateEan13Numbers(nNumbers);
               return;
            }

            if (args.Length == 4 && args[0] == "-g")
            {
               int nNumbers = int.Parse(args[1]);
               GenerateEan13Numbers(nNumbers, args[2], args[3]);
               return;
            }


            Usage();

         }
         catch (Exception ex)
         {
            Console.WriteLine($"Error: {ex.Message}");

            Usage();
         }

      }

      private static void GenerateEan13Numbers(int nNumbers)
      {
         for (int i = 0; i < nNumbers; i++)
         {
            Console.WriteLine(Ean13Calculator.GenerateEan13Number());
         }
      }

      private static void GenerateEan13Numbers(int nNumbers, string countryCode, string manufaterCode)
      {
         for (int i = 0; i < nNumbers; i++)
         {
            Console.WriteLine(Ean13Calculator.GenerateEan13Number(countryCode, manufaterCode));
         }
      }


      private static void Usage()
      {
         Console.WriteLine();
         Console.WriteLine("Usage:");
         Console.WriteLine();
         Console.WriteLine("EanCalc option [EANNumber | nNumbers [countryCode manufacturerCode]] ");
         Console.WriteLine();
         Console.WriteLine("Options:");
         Console.WriteLine(" -v : Validate Ean Number");
         Console.WriteLine(" -g : Generate n EAN 13 Numbers");
         Console.WriteLine(" -h : Show this Help");
         Console.WriteLine();
         Console.WriteLine("Examples:");
         Console.WriteLine();
         Console.WriteLine("> EanCalc -v 7707248810380");
         Console.WriteLine();
         Console.WriteLine("  - Validte the EAN number");
         Console.WriteLine();
         Console.WriteLine("> EanCalc -g 10");
         Console.WriteLine();
         Console.WriteLine("  - Generate 10 EAN13 Numbers randomly");
         Console.WriteLine();
         Console.WriteLine("> EanCalc -g 10 770 7248");
         Console.WriteLine();
         Console.WriteLine("  - Generate 10 EAN13 Numbers for Country Code = 770 and Manufacturer Code = 7248");
         Console.WriteLine();
         Console.WriteLine("Press any key to continue...");
         Console.ReadKey();
         Console.WriteLine();
      }
   }
}
