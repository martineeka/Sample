﻿using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class BusinessStatusOwnership
    {
        public BusinessStatusOwnership()
        {
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
    }
}
