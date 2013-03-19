using System.Collections;
using System.Collections.Generic;

namespace TechSmith.Hyde.Table.Fluent
{
   public class BoundedRowKeyQuery<T> : IBoundedRowKeyQuery<T> where T : new()
   {
      private readonly ITableContext _context;
      private readonly string _tableName;
      private readonly QueryDescriptor _query;

      public BoundedRowKeyQuery( ITableContext context, string tableName, QueryDescriptor query )
      {
         _context = context;
         _tableName = tableName;
         _query = query;
      }

      public IQuery<T> RowKeyTo( string to, bool exclusive = true )
      {
         if ( exclusive )
         {
            _query.RowKeyUpperBoundExclusive = to;
         }
         else
         {
            _query.RowKeyUpperBoundInclusive = to;
         }
         return new DeferredQuery<T>( _context, _tableName, _query );
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