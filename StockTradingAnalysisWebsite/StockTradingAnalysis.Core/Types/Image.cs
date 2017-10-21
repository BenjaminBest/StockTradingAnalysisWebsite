using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Core.Types
{
    /// <summary>
    /// Defines an image
    /// </summary>
    public class Image : IImage
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">The id of an image</param>
        public Image(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets/sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the contenttype
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets/sets the original name
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the image itself
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            IImage other = obj as Image;

            return
                other != null &&
                Id.Equals(other.Id) &&
                string.Equals(ContentType, other.ContentType) &&
                string.Equals(OriginalName, other.OriginalName) &&
                string.Equals(Description, other.Description) &&
                Equals(Data, other.Data);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (ContentType != null ? ContentType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (OriginalName != null ? OriginalName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Data != null ? Data.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}