using System.Collections;
using System.Collections.Generic;

namespace TechSmith.Hyde.Table.Fluent
{
   public class BoundedPartitionQuery<T> : IBoundedPartitionQuery<T> where T : new()
   {
      private readonly ITableContext _context;
      private readonly string _tableName;
      private readonly QueryDescriptor _query;

      public BoundedPartitionQuery( ITableContext context, string tableName, QueryDescriptor query )
      {
         _context = context;
         _tableName = tableName;
         _query = query;
      }

      public IRowKeyQuery<T> PartitionKeyTo( string to, bool exclusive = false )
      {
         if ( exclusive )
         {
            _query.PartitionKeyUpperBoundExclusive = to;
         }
         else
         {
            _query.PartitionKeyUpperBoundInclusive = to;
         }
         return new RowKeyQuery<T>( _context, _tableName, _query );
      }

      public IBoundedRowKeyQuery<T> RowKeyFrom( string @from, bool exclusive = false )
      {
         return new RowKeyQuery<T>( _context, _tableName, _query ).RowKeyFrom( @from, exclusive );
      }

      public IQuery<T> RowKeyTo( string to, bool exclusive = true )
      {
         return new BoundedRowKeyQuery<T>( _context, _tableName, _query ).RowKeyTo( to, exclusive );
      }

      public IQuery<T> RowKeyEquals( string val )
      {
         return new RowKeyQuery<T>( _context, _tableName, _query ).RowKeyEquals( val );
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