using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.IO;

namespace Opc.Ua.Core
{
    public static class PooledMemoryStream
    {
        private static readonly RecyclableMemoryStreamManager s_manager = new RecyclableMemoryStreamManager();
        public static MemoryStream GetMemoryStream([CallerMemberName] string memberName = "")
        {
            return s_manager.GetStream(memberName);
        }

        public static MemoryStream GetMemoryStream(int initialSize, [CallerMemberName] string memberName = "")
        {
            return s_manager.GetStream(memberName, initialSize);
        }
    }
}
