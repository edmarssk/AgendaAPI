﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.AppConfig
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidaEm { get; set; }

    }
}
