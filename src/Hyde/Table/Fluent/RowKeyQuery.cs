using System.Collections;
using System.Collections.Generic;

namespace TechSmith.Hyde.Table.Fluent
{
   public class RowKeyQuery<T> : IRowKeyQuery<T> where T : new()
   {
      private readonly ITableContext _context;
      private readonly string _tableName;
      private readonly QueryDescriptor _query;

      public RowKeyQuery( ITableContext context, string tableName, QueryDescriptor query )
      {
         _context = context;
         _tableName = tableName;
         _query = query;
      }

      public IBoundedRowKeyQuery<T> RowKeyFrom( string @from, bool exclusive = false )
      {
         if ( exclusive )
         {
            _query.RowKeyLowerBoundExclusive = @from;
         }
         else
         {
            _query.RowKeyLowerBoundInclusive = @from;
         }
         return new BoundedRowKeyQuery<T>( _context, _tableName, _query );
      }

      public IQuery<T> RowKeyEquals( string val )
      {
         _query.RowKeyEqualityFilter = val;
         return new DeferredQuery<T>( _context, _tableName, _query );
      }

      public IQuery<T> RowKeyTo( string to, bool exclusive = true )
      {
         return new BoundedRowKeyQuery<T>( _context, _tableName, _query ).RowKeyTo( to, exclusive );
      }

      public IQuery<T> Top( int count )
      {
         return new DeferredQuery<T>( _context, _tableName, _query ).Top( count );
      }

      public IEnumerator<T> GetEnumerator()
      {
         return new DeferredQuery<T>( _context, _tableName, _query ).GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}