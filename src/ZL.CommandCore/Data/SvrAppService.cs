﻿using System;
using System.Collections.Generic;

namespace ZL.CommandCore.Data
{
    public partial class SvrAppService
    {
        public string ServiceId { get; set; }
        public string AppId { get; set; }
        public string Parameter { get; set; }
        public int? Status { get; set; }
    }
}
