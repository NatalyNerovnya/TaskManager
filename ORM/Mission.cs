namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mission
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }

        public virtual Task Task { get; set; }
    }
}
