namespace TechSmith.Hyde.Table.Fluent
{
   public class QueryDescriptor
   {
      public string PartitionKeyEqualityFilter
      {
         get;
         set;
      }

      public string PartitionKeyLowerBoundExclusive
      {
         get;
         set;
      }

      public string PartitionKeyUpperBoundExclusive
      {
         get;
         set;
      }

      public string PartitionKeyLowerBoundInclusive
      {
         get;
         set;
      }

      public string PartitionKeyUpperBoundInclusive
      {
         get;
         set;
      }

      public string RowKeyEqualityFilter
      {
         get;
         set;
      }

      public string RowKeyLowerBoundExclusive
      {
         get;
         set;
      }

      public string RowKeyUpperBoundExclusive
      {
         get;
         set;
      }

      public string RowKeyLowerBoundInclusive
      {
         get;
         set;
      }

      public string RowKeyUpperBoundInclusive
      {
         get;
         set;
      }

      public int? Top
      {
         get;
         set;
      }
   }
}