using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockTradingAnalysis.Data.RavenDb.Extensions
{
    public static class DocumentSessionExtensions
    {
        public static List<T> GetAll<T>(this IDocumentSession session, string indexName)
        {
            const int size = 1024;
            var page = 0;

            RavenQueryStatistics stats;
            var objects = session.Query<T>(indexName)
                .Customize(q => q.NoTracking())
                .Customize(q => q.WaitForNonStaleResultsAsOfLastWrite(TimeSpan.FromSeconds(5)))
                .Statistics(out stats)
                .Skip(page * size)
                .Take(size)
                .ToList();

            page++;

            while ((page * size) <= stats.TotalResults)
            {
                objects.AddRange(session.Query<T>(indexName)
                    .Customize(q => q.NoTracking())
                    .Customize(q => q.WaitForNonStaleResultsAsOfLastWrite(TimeSpan.FromSeconds(5)))
                    .Skip(page * size)
                    .Take(size)
                    .ToList());
                page++;
            }

            return objects;
        }

        public static List<T> GetAll<T>(this IDocumentSession session, string indexName,
            Expression<Func<T, bool>> expression)
        {
            // We cant use RavenQueryStatistics here since it always returns total number of documents in database
            // not just the ones that fit our expression
            var size = 1024;
            var page = 0;

            var objects = session.Query<T>(indexName)
                .Customize(q => q.NoTracking())
                .Customize(q => q.WaitForNonStaleResultsAsOfLastWrite(TimeSpan.FromSeconds(5)))
                .Where(expression)
                .Skip(page * size)
                .Take(size)
                .ToList();

            size = objects.Count;
            page++;

            while (size >= 1024)
            {
                var result = session.Query<T>(indexName)
                    .Customize(q => q.NoTracking())
                    .Customize(q => q.WaitForNonStaleResultsAsOfLastWrite(TimeSpan.FromSeconds(5)))
                    .Where(expression)
                    .Skip(page * size)
                    .Take(size)
                    .ToList();

                objects.AddRange(result);
                size = result.Count;
                page++;
            }

            return objects;
        }
    }
}