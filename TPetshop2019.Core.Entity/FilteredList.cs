using System.Collections.Generic;

namespace TPetshop2019.Core.Entity
{
    public class FilteredList <T>
    {
        public IEnumerable<T> List { get; set; }
        public int Count { get; set; }
    }
}