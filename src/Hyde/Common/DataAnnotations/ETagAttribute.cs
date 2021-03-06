﻿using System;

namespace TechSmith.Hyde.Common.DataAnnotations
{
   /// <summary>
   /// Marks a property as a table storage ETag.
   /// </summary>
   [AttributeUsage( AttributeTargets.Property, AllowMultiple = false, Inherited = true )]
   public class ETagAttribute : DontSerializeAttribute
   {
   }
}
