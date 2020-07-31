using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CollectionHelpers
{
     internal class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        internal static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked((Environment.TickCount * 31) + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
