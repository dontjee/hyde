using System.Collections;
using System.Collections.Generic;

namespace TechSmith.Hyde.Table.Fluent
{
   public class PartitionKeyQuery<T> : IPartitionKeyQuery<T> where T : new()
   {
      private readonly string _tableName;
      private readonly ITableContext _context;
      private readonly QueryDescriptor _query;

      public PartitionKeyQuery( string tableName, ITableContext context )
      {
         _tableName = tableName;
         _context = context;
         _query = new QueryDescriptor();
      }

      public IBoundedPartitionQuery<T> PartitionKeyFrom( string @from, bool exclusive = false )
      {
         if ( exclusive )
         {
            _query.PartitionKeyLowerBoundExclusive = @from;
         }
         else
         {
            _query.PartitionKeyLowerBoundInclusive = @from;
         }
         return new BoundedPartitionQuery<T>( _context, _tableName, _query );
      }

      public IRowKeyQuery<T> PartitionKeyEquals( string partitionKey )
      {
         _query.PartitionKeyEqualityFilter = partitionKey;
         return new RowKeyQuery<T>( _context, _tableName, _query );
      }

      public IRowKeyQuery<T> PartitionKeyTo( string to, bool exclusive = false )
      {
         return new BoundedPartitionQuery<T>( _context, _tableName, _query ).PartitionKeyTo( to, exclusive );
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