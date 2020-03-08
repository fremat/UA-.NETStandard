/* Copyright (c) 1996-2019 The OPC Foundation. All rights reserved.
   The source code in this file is covered under a dual-license scenario:
     - RCL: for OPC Foundation members in good-standing
     - GPL V2: everybody else
   RCL license terms accompanied with this source code. See http://opcfoundation.org/License/RCL/1.00/
   GNU General Public License as published by the Free Software Foundation;
   version 2 of the License are accompanied with this source code. See http://opcfoundation.org/License/GPLv2
   This source code is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/

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
