using System.Collections.Generic;

namespace Shared
{
    public class Responce<T>
    {
        public string Msg { get; set; }

        public T Item { get; set; }
        public List<T> ItemsList { get; set; }

    }
}
