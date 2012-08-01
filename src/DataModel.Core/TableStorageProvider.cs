using System.Collections.Generic;
using System.Linq;

namespace TechSmith.CloudServices.DataModel.Core
{
   public abstract class TableStorageProvider
   {
      private readonly Dictionary<string, ITableContext> _tableNameToContext = new Dictionary<string, ITableContext>();
      private readonly object _syncObject = new object();
      private readonly List<ITableContext> _contextsToSave = new List<ITableContext>();

      protected abstract ITableContext GetContext();

      public void Add<T>( string tableName, T instance, string partitionKey, string rowKey ) where T : new()
      {
         var context = GetContext( tableName );

         context.AddNewItem( tableName, instance, partitionKey, rowKey );
      }

      public T Get<T>( string tableName, string partitionKey, string rowKey ) where T : new()
      {
         var context = GetContext( tableName );

         return context.GetItem<T>( tableName, partitionKey, rowKey );
      }

      public IEnumerable<T> GetCollection<T>( string tableName, string partitionKey ) where T : new()
      {
         var context = GetContext( tableName );

         return context.GetCollection<T>( tableName, partitionKey );
      }

      public IEnumerable<T> GetRange<T>( string tableName, string partitionKeyLow, string partitionKeyHigh ) where T : new()
      {
         var context = GetContext( tableName );

         return context.GetRange<T>( tableName, partitionKeyLow, partitionKeyHigh );
      }

      private ITableContext GetContext( string tableName )
      {
         lock ( _syncObject )
         {
            if ( !_tableNameToContext.Keys.Contains( tableName ) )
            {
               _tableNameToContext[tableName] = GetContext();
            }
         }

         return _tableNameToContext[tableName];
      }

      public void Save()
      {
         foreach ( var tableContext in _tableNameToContext )
         {
            tableContext.Value.Save();
         }

         foreach ( var tableContext in _contextsToSave )
         {
            tableContext.Save();
         }
      }

      public void Upsert<T>( string tableName, T instance, string partitionKey, string rowKey ) where T : new()
      {
         var context = GetContext();
         context.Upsert( tableName, instance, partitionKey, rowKey );
         _contextsToSave.Add( context );
      }

      public void Delete( string tableName, string partitionKey, string rowKey )
      {
         var context = GetContext( tableName );
         context.DeleteItem( tableName, partitionKey, rowKey );
      }

      public void DeleteCollection( string tableName, string partitionKey )
      {
         var context = GetContext( tableName );

         context.DeleteCollection( tableName, partitionKey );
      }

      public void Update<T>( string tableName, T item, string partitionKey, string rowKey ) where T : new()
      {
         var context = GetContext();
         context.Update( tableName, item, partitionKey, rowKey );
         _contextsToSave.Add( context );
      }
   }
}