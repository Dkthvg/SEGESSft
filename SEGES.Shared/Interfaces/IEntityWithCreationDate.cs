﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGES.Shared.Interfaces
{
    public interface IEntityWithCreationDate
    {
        DateTime CreationDate { get; set; }
    }
}
