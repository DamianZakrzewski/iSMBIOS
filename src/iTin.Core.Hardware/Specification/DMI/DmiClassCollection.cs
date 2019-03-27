﻿
namespace iTin.Core.Hardware.Specification.Dmi
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Smbios;

    /// <inheritdoc />
    /// <summary>
    /// Representa una colección de objetos <see cref="T:iTin.Core.Hardware.Specification.Dmi.DmiClass" />.
    /// </summary>
    public sealed class DmiClassCollection : ReadOnlyCollection<DmiClass> 
    {
        /// <inheritdoc />
        /// <summary>
        /// Initialize a new instance of the <see cref = "T:iTin.Core.Hardware.Specification.Dmi.DmiClassCollection" /> class.
        /// </summary>
        /// <param name="parent">Estructura.</param>
        internal DmiClassCollection(DmiStructure parent) : base(new List<DmiClass>())
        {
            SmbiosStructureCollection structures = DmiHelper.Smbios.Get(parent.Class);
            foreach (var structure in structures)
            {
                Items.Add(new DmiClass(structure));
            }
        }
    }
}
