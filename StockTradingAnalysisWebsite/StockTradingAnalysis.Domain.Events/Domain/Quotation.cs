using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Quotation defines a single quotation for some point in time for a share
    /// </summary>
    public class Quotation : IQuotation
    {
        /// <summary>
        /// Gets/sets the Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets/sets the Changed
        /// </summary>
        public DateTime Changed { get; set; }

        /// <summary>
        /// Gets/sets the Open
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Gets/sets the Close
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Gets/sets the High
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Gets/sets the Low
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Default contructor
        /// </summary>
        public Quotation()
        {

        }

        /// <summary>
        /// Initializs this object with the given values
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="changed">Date changed</param>
        /// <param name="open">Open</param>
        /// <param name="close">Close</param>
        /// <param name="high">High</param>
        /// <param name="low">Low</param>
        public Quotation(DateTime date, DateTime changed, decimal open, decimal close, decimal high, decimal low)
        {
            Date = date;
            Changed = changed;
            Open = open;
            Close = close;
            High = high;
            Low = low;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals(obj as Quotation);
        }

        /// <summary>
        /// Defines weather this Quotations is equal to annother quotation.
        /// </summary>
        /// <param name="other"></param>
        /// <returns><c>True</c> if equal</returns>
        protected bool Equals(Quotation other)
        {
            if (other == null)
                return false;

            return Date.Equals(other.Date);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return Date.GetHashCode();
            }
        }
    }
}