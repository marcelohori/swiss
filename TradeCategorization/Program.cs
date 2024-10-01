using System;
using System.Collections.Generic;
using System.Globalization;

namespace TradeCategorization
{
    class Program
    {
        static void Main(string[] args)
        {
            // Leitura da data de referência no formato mm/dd/yyyy
            DateTime referenceDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            // Leitura do número de negociações
            int n = int.Parse(Console.ReadLine());

            // Lista para armazenar as negociações
            List<ITrade> trades = new List<ITrade>();

            // Leitura das negociações
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(' ');
                double value = double.Parse(input[0]);
                string sector = input[1];
                DateTime nextPaymentDate = DateTime.ParseExact(input[2], "MM/dd/yyyy", CultureInfo.InvariantCulture); // Formato mm/dd/yyyy

                trades.Add(new Trade(value, sector, nextPaymentDate));
            }

            // Instancia o categorizador
            TradeCategorizer categorizer = new TradeCategorizer();

            // Classifica e imprime o resultado para cada negociação
            foreach (var trade in trades)
            {
                Console.WriteLine(categorizer.Categorize(trade, referenceDate));
            }
        }
    }
}