﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryMappedFile
{
    public interface ISamplesBuffer : IDisposable
    {
        long Length { get; }

        byte[] GetBytes(long pos, int length);

        short this[long index]
        {
            get;
        }
    }
}
