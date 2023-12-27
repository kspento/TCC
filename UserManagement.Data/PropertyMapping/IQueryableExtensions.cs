using System;
using System.Linq;
using System.Linq.Expressions;

namespace UserManagement.Data.PropertyMapping
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderBySerializedString<T>(this IQueryable<T> source, string serializedSort)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(serializedSort))
            {
                return source;
            }

            var sortClauses = serializedSort.Split(';');

            IOrderedQueryable<T> orderedQuery = null;

            foreach (var sortClause in sortClauses)
            {
                var parts = sortClause.Trim().Split(' ');
                if (parts.Length > 0)
                {
                    var propertyName = parts[0];
                    var descending = (parts.Length > 1) && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    var type = typeof(T);
                    var parameter = Expression.Parameter(type, "x");
                    var property = Expression.Property(parameter, propertyName);
                    var lambda = Expression.Lambda(property, parameter);

                    if (descending)
                    {
                        orderedQuery = orderedQuery == null
                            ? Queryable.OrderByDescending(source, (dynamic)lambda)
                            : Queryable.ThenByDescending(orderedQuery, (dynamic)lambda);
                    }
                    else
                    {
                        orderedQuery = orderedQuery == null
                            ? Queryable.OrderBy(source, (dynamic)lambda)
                            : Queryable.ThenBy(orderedQuery, (dynamic)lambda);
                    }
                }
            }

            return orderedQuery ?? source;
        }


        //public static IQueryable<object> ShapeData<TSource>(this IQueryable<TSource> source,
        //    string fields,
        //   Dictionary<string, PropertyMappingValue> mappingDictionary)
        //{
        //    if (source == null)
        //    {
        //        throw new ArgumentNullException("source");
        //    }
        //
        //    if (mappingDictionary == null)
        //    {
        //        throw new ArgumentNullException("mappingDictionary");
        //    }
        //
        //    if (string.IsNullOrWhiteSpace(fields))
        //    {
        //        return (IQueryable<object>)source;
        //    }
        //
        //    // ignore casing
        //    fields = fields.ToLower();
        //
        //    // the field are separated by ",", so we split it.
        //    var fieldsAfterSplit = fields.Split(',');
        //
        //    // select clause starts with "new" - will create anonymous objects
        //    var selectClause = "new (";
        //
        //    // run through the fields
        //    foreach (var field in fieldsAfterSplit)
        //    {
        //        // trim each field, as it might contain leading 
        //        // or trailing spaces. Can't trim the var in foreach,
        //        // so use another var.
        //        var propertyName = field.Trim();
        //
        //        // find the matching property
        //        if (!mappingDictionary.ContainsKey(propertyName))
        //        {
        //            throw new ArgumentException($"Key mapping for {propertyName} is missing");
        //        }
        //
        //        // get the PropertyMappingValue
        //        var propertyMappingValue = mappingDictionary[propertyName];
        //
        //        if (propertyMappingValue == null)
        //        {
        //            throw new ArgumentNullException("propertyMappingValue");
        //        }
        //
        //        // Run through the destination property names
        //        foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
        //        {
        //            // add to select clause
        //            selectClause += $" {destinationProperty},";
        //        }
        //    }
        //
        //    // remove last comma, add closing arrow and execute select clause
        //    selectClause = selectClause.Substring(0, selectClause.Length - 1) + ")";
        //    return (IQueryable<object>)source.Select(selectClause);
        //}

    }
}
