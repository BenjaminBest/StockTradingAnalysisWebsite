using System;
using System.Collections.Generic;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Services.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// The MonthTimeSlice class is one single time slice which covers a specific time range, see <see cref="P:StockTradingAnalysis.Services.Domain.MonthTimeSlice.Start" /> and <see cref="P:StockTradingAnalysis.Services.Domain.MonthTimeSlice.End" />.
    /// </summary>
    /// <seealso cref="T:StockTradingAnalysis.Interfaces.Domain.ITimeSlice" />
    public class MonthTimeSlice : ITimeSlice
    {
        /// <summary>
        /// The slices
        /// </summary>
        private readonly IList<ITimeSlice> _slices;

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TimeSliceType Type => TimeSliceType.Month;

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime Start { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime End { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets all slices.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITimeSlice> GetAllSlices()
        {
            return _slices.Flatten(s => s.GetAllSlices());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthTimeSlice"/> class.
        /// </summary>
        public MonthTimeSlice(DateTime start, DateTime end, int value)
        {
            Start = start;
            End = end;
            Value = value;

            _slices = new List<ITimeSlice>();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Month {Value:00}/{Start.Year} [{Start.Date}-{End.Date}]";
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(ITimeSlice other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((ITimeSlice)obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode() * 397) ^ End.GetHashCode();
            }
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ITimeSliceKey other)
        {
            if (other == null)
                return false;

            return Start == other.Start && End == other.End;
        }
    }
}
