﻿
namespace iTin.Core.Hardware.Specification.Smbios
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Represents a collection of <see cref="T:iTin.Core.Hardware.Specification.Smbios.GroupAssociationElement" />.
    /// </summary>
    sealed class GroupAssociationElementCollection : ReadOnlyCollection<GroupAssociationElement>
    {
        #region constructor/s

        #region [internal] GroupAssociationElementCollection(IEnumerable<GroupAssociationElement>): Inicializa una nueva instancia de la clase.
        /// <inheritdoc />
        /// <summary>
        /// Initialize a new instance of the class <see cref="T:iTin.Core.Hardware.Specification.Smbios.GroupAssociationElementCollection" />.
        /// </summary>
        /// <param name="elements">Item list.</param>
        internal GroupAssociationElementCollection(IEnumerable<GroupAssociationElement> elements) : base(elements.ToList())
        {
        }
        #endregion

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a class String that represents the current object
        /// <summary>
        /// Returns a class <see cref="T:System.String" /> that represents the current object.
        /// </summary>
        /// <returns>
        /// Object <see cref="T:System.String" /> that represents the current <see cref="MemoryControllerContainedElementCollection"/> class.
        /// </returns>
        /// <remarks>
        /// This method returns a string that includes the number of available items
        /// </remarks>                                    
        public override string ToString() => $"Elements = { Items.Count}";
        #endregion

        #endregion              
    }
}
