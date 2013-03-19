namespace TechSmith.Hyde.Table.Fluent
{
   public interface IPartitionKeyQuery<T> : IBoundedPartitionQuery<T> where T : new()
   {
      IBoundedPartitionQuery<T> PartitionKeyFrom( string from, bool exclusive = false );
      IRowKeyQuery<T> PartitionKeyEquals( string val );
   }
}