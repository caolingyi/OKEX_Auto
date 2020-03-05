using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Utilities
{
    public static class IdHelper
    {
        public static long BuildId()
        {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
