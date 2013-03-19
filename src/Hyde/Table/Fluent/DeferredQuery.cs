using System.Collections;
using System.Collections.Generic;

namespace TechSmith.Hyde.Table.Fluent
{
   public class DeferredQuery<T> : IQuery<T> where T : new()
   {
      private readonly ITableContext _context;
      private readonly string _tableName;
      private readonly QueryDescriptor _query;

      public DeferredQuery( ITableContext context, string tableName, QueryDescriptor query )
      {
         _context = context;
         _tableName = tableName;
         _query = query;
      }

      public IQuery<T> Top( int count )
      {
         _query.Top = count;
         return new DeferredQuery<T>( _context, _tableName, _query );
      }

      public IEnumerator<T> GetEnumerator()
      {
         return _context.PerformQuery<T>( _tableName, _query ).GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}