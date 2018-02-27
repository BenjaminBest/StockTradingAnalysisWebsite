using System;
using System.Collections.Generic;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The QuarterTimeSlice class is one single time slice which covers a specific time range, see <see cref="Start"/> and <see cref="End"/>.
    /// </summary>
    /// <seealso cref="ITimeSlice" />
    public class QuarterTimeSlice : ITimeSlice
    {
        /// <summary>
        /// The slices
        /// </summary>
        private readonly IList<ITimeSlice> _slices;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TimeSliceType Type => TimeSliceType.Quarter;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime End { get; set; }

        /// <summary>
        /// Gets all slices.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITimeSlice> GetAllSlices()
        {
            return _slices.Flatten(s => s.GetAllSlices());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuarterTimeSlice"/> class.
        /// </summary>
        public QuarterTimeSlice(DateTime start, DateTime end, int value)
        {
            Start = start;
            End = end;
            Value = value;

            _slices = new List<ITimeSlice>();
        }

        /// <summary>
        /// Adds the slice.
        /// </summary>
        /// <param name="slice">The slice.</param>
        public void AddSlice(MonthTimeSlice slice)
        {
            _slices.Add(slice);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Quarter {Value}/{Start.Year} [{Start.Date}-{End.Date}]";
        }
    }
}
