namespace TechSmith.Hyde.Table.Fluent
{
   public interface IBoundedPartitionQuery<T> : IRowKeyQuery<T> where T : new()
   {
      IRowKeyQuery<T> PartitionKeyTo( string to, bool exclusive = true );
   }
}