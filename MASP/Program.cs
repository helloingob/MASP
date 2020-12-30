using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace MASP
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var generatedContent = "Datum;Wert;Buchungswährung;Typ;Notiz\n";
            foreach (var line in File.ReadLines(args[0]))
            {
                var typ = string.Empty;

                if (Regex.IsMatch(line,
                    "(Deposits)|(^Incoming client.*)|(^Incoming currency exchange.*)"))
                    typ = "Einlage";

                if (Regex.IsMatch(line,
                    "(^Withdraw application.*)|(Outgoing currency.*)|(Withdrawal)"))
                    typ = "Entnahme";

                if (Regex.IsMatch(line,
                    "(^Delayed interest.*)|(^Late payment.*)|(^Interest income.*)|(^Cashback.*)|(^.*interest received$)|(^.*late fees received$)|(interest received)|(late fees received)")
                )
                    typ = "Zinsen";

                if (Regex.IsMatch(line, "(^FX commission.*)")) typ = "Gebühren";

                if (string.IsNullOrEmpty(typ)) continue;
                var rowParts = line.Split(",");
                var dateRaw = rowParts[0].Replace("\"", string.Empty);
                var dateFormatted = DateTime.ParseExact(dateRaw, "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture);
                var notiz = (rowParts[2] + " (TID:" + rowParts[1] + ")").Replace("\"", string.Empty);

                var newLine = dateFormatted.ToString("dd.MM.yyyy") + ";" + rowParts[3].Replace(".", ",") + ";EUR;" +
                              typ + ";" + notiz;
                generatedContent += newLine + "\n";
            }

            File.WriteAllText("masp_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".csv", generatedContent);
            Console.WriteLine("Finished.");
        }
    }
}