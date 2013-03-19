namespace TechSmith.Hyde.Table.Fluent
{
   public interface IRowKeyQuery<T> : IBoundedRowKeyQuery<T> where T : new()
   {
      IBoundedRowKeyQuery<T> RowKeyFrom( string from, bool exclusive = false );
      IQuery<T> RowKeyEquals( string val );
   }
}