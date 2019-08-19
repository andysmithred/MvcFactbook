﻿using MvcFactbook.Code.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Builder: IComplete
    {
        #region Constructor

        public Builder()
        {
            Ships = new HashSet<Ship>();
            PoliticalEntityBuilders = new HashSet<PoliticalEntityBuilder>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Founded { get; set; }

        public int? Defunct { get; set; }

        [Required]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<Ship> Ships { get; set; }

        public ICollection<PoliticalEntityBuilder> PoliticalEntityBuilders { get; set; }

        #endregion Foreign Properties
    }
}