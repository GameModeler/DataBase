// <copyright file="IProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataBase.Database.Utils;

    /// <summary>
    /// Provider interface
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Gets or sets provider
        /// </summary>
        ProviderType Provider { get; set; }
    }
}
