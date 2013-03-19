namespace TechSmith.Hyde.Table.Fluent
{
   public interface IBoundedRowKeyQuery<T> : IQuery<T> where T : new()
   {
      IQuery<T> RowKeyTo( string to, bool exclusive = true );
   }
}