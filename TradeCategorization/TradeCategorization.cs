using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeCategorization
{
    public interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
        DateTime NextPaymentDate { get; }
    }

    public class Trade : ITrade
    {
        public double Value { get; private set; }
        public string ClientSector { get; private set; }
        public DateTime NextPaymentDate { get; private set; }

        public Trade(double value, string clientSector, DateTime nextPaymentDate)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
        }
    }

    public interface ICategory
    {
        string Name { get; }
        bool IsMatch(ITrade trade, DateTime referenceDate);
    }

    public class ExpiredCategory : ICategory
    {
        public string Name => "EXPIRED";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return (referenceDate - trade.NextPaymentDate).Days > 30;
        }
    }

    public class HighRiskCategory : ICategory
    {
        public string Name => "HIGHRISK";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1000000 && trade.ClientSector == "Private";
        }
    }

    public class MediumRiskCategory : ICategory
    {
        public string Name => "MEDIUMRISK";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1000000 && trade.ClientSector == "Public";
        }
    }

    public class TradeCategorizer
    {
        private readonly List<ICategory> _categories;

        public TradeCategorizer()
        {
            _categories = new List<ICategory>
            {
                new ExpiredCategory(),
                new HighRiskCategory(),
                new MediumRiskCategory()
            };
        }

        public string Categorize(ITrade trade, DateTime referenceDate)
        {
            foreach (var category in _categories)
            {
                if (category.IsMatch(trade, referenceDate))
                {
                    return category.Name;
                }
            }
            return "UNCATEGORIZED";
        }
    }
}
