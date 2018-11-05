using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Model.Base
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public bool IsValid { get; set; } = true;
    }
}
