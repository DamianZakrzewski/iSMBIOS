﻿
namespace iTin.Core.Hardware.Specification.Smbios
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    using Device.DeviceProperty;

    /// <summary>
    /// The <b>SmbiosBaseType</b> class provides functions to analyze the properties associated with a structure <see cref="SMBIOS" />.
    /// </summary>
    public abstract class SmbiosBaseType
    {
        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] _strings;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Hashtable _content;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Hashtable _properties;
        #endregion

        #region private readonly members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int _smbiosVersion;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SmbiosStructureHeaderInfo _smbiosStructureHeaderInfo;
        #endregion

        #region constructor/s

        #region [protected] SmbiosBaseType(SmbiosStructureHeaderInfo, int): Initializes a new instance of the class by specifying the Header of the structure and current SMBIOS
        /// <summary>
        /// Initializes a new instance of the class <see cref="SmbiosBaseType"/> by specifying the Header of the structure and current SMBIOS.
        /// </summary>
        /// <param name="smbiosStructureHeaderInfo">Header of the current structure.</param>
        /// <param name="smbiosVersion">Current SMBIOS version.</param>
        protected SmbiosBaseType(SmbiosStructureHeaderInfo smbiosStructureHeaderInfo, int smbiosVersion)
        {
            _smbiosVersion = smbiosVersion;
            _smbiosStructureHeaderInfo = smbiosStructureHeaderInfo;
        }
        #endregion

        #endregion

        #region public properties

        #region [public] (Hashtable) Content: Obtiene las propiedades disponibles para este dispositivo.
        /// <summary>
        /// Obtiene las propiedades disponibles para este dispositivo.
        /// </summary>
        /// <value>
        /// 	<para>Tipo: <see cref="T:System.Collections.Hashtable"/></para>
        /// 	<para>Propiedades disponibles.</para>
        /// </value>
        public Hashtable Content
        {
            get
            {
                if (_content == null)
                {
                    _content = new Hashtable();
                    _strings = SmbiosHelper.ParseStrings(_smbiosStructureHeaderInfo.RawData);
                }

                return _content;
            }
        }
        #endregion

        #region [public] (SmbiosStructureHeaderInfo) HeaderInfo: Gets the raw information from this structure
        /// <summary>
        /// Gets the raw information from this structure.
        /// </summary>
        /// <value>
        /// A <see cref="SmbiosStructureHeaderInfo"/> object that contains the information.
        /// </value>
        public SmbiosStructureHeaderInfo HeaderInfo => _smbiosStructureHeaderInfo;
        #endregion

        #region [public] (Hashtable) Properties: Gets the properties available for this structure
        /// <summary>
        /// Gets the properties available for this structure.
        /// </summary>
        /// <value>
        /// Availables properties.
        /// </value>
        public Hashtable Properties
        {
            get
            {
                if (_properties != null)
                {
                    return _properties;
                }

                _properties = new Hashtable();
                _strings = SmbiosHelper.ParseStrings(_smbiosStructureHeaderInfo.RawData);
                Parse(_properties);

                return _properties;
            }
        }
        #endregion

        #endregion

        #region protected readonly properties

        #region [protected] (int) SmbiosVersion: Gets the current version of SMBIOS
        /// <summary>
        /// Gets the current version of <see cref="SMBIOS" />.
        /// </summary>
        /// <value>
        /// Value representing the current version of <see cref="SMBIOS" />.
        /// </value>
        protected int SmbiosVersion => _smbiosVersion;
        #endregion

        #region [protected] (ReadOnlyCollection<string>) Strings: Gets the strings associated with this structure
        /// <summary>
        /// Gets the strings associated with this structure.
        /// </summary>
        /// <value>
        /// An read-only collection that contains the strings of this structure.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected ReadOnlyCollection<string> Strings => new ReadOnlyCollection<string>(_strings);

        #endregion

        #endregion

        #region public methods

        #region [public] (IDeviceProperty) GetProperty(PropertyKey): Gets a reference to an object that implements the IDeviceProperty interface, represents the strongly typed value of the property
        /// <summary>
        /// Gets a reference to an object that implements the IDeviceProperty interface, represents the strongly typed value of the property.
        /// </summary>
        /// <param name="propertyKey">Key to the property to obtain</param>
        /// <returns>
        /// Reference to the object that represents the strongly typed value of the property
        /// </returns>
        public IDeviceProperty GetProperty(PropertyKey propertyKey)
        {
            if (!Content.Contains(propertyKey))
            {
                Content.Add(propertyKey, GetTypedProperty(propertyKey));
            }

            return (IDeviceProperty)Content[propertyKey];
        }
        #endregion

        #endregion

        #region public override methods

        #region [protected] {override} (string) ToString(): Gets the property collection for this structure
        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// This method returns a string that includes the property <see cref="SmbiosStructureHeaderInfo.StructureType" />.
        /// </remarks> 
        public override string ToString()
        {
            return $"Type = {HeaderInfo.StructureType}";
        }
        #endregion

        #endregion

        #region protected methods

        #region [protected] (byte) GetByte(byte): Returns the stored value from the specified byte
        /// <summary>
        /// Returns the stored value from the specified byte.
        /// </summary>
        /// <param name="target">target byte</param>
        /// <returns>
        /// The value stored in the indicated byte.
        /// </returns>
        protected byte GetByte(byte target)
        {
            return HeaderInfo.RawData[target];
        }
        #endregion

        #region [protected] (int) GetWord(byte): Returns the stored value from the specified byte
        /// <summary>
        /// Returns the stored value from the specified byte.
        /// </summary>
        /// <param name="start">start byte</param>
        /// <returns>
        /// The value stored in the indicated byte.
        /// </returns>
        protected int GetWord(byte start)
        {
            return HeaderInfo.RawData.GetWord(start);
        }
        #endregion

        #region [protected] (int) GetDoubleWord(byte): Returns the stored value from the specified byte
        /// <summary>
        /// Returns the stored value from the specified byte.
        /// </summary>
        /// <param name="start">start byte</param>
        /// <returns>
        /// The value stored in the indicated byte.
        /// </returns>
        protected int GetDoubleWord(byte start)
        {
            return HeaderInfo.RawData.GetDoubleWord(start);
        }
        #endregion

        #region [protected] (long) GetQuadrupleWord(byte): Returns the stored value from the specified byte
        /// <summary>
        /// Returns the stored value from the specified byte.
        /// </summary>
        /// <param name="start">start byte</param>
        /// <returns>
        /// The value stored in the indicated byte.
        /// </returns>
        protected long GetQuadrupleWord(byte start)
        {
            return HeaderInfo.RawData.GetQuadrupleWord(start);
        }
        #endregion

        #region [protected] (string) GetString(byte): Returns the stored string from the specified byte
        /// <summary>
        /// Returns the stored string from the specified byte.
        /// </summary>
        /// <param name="target">target byte</param>
        /// <returns>
        /// The value stored in the indicated byte.
        /// </returns>
        protected string GetString(byte target)
        {
            return Strings[GetByte(target)];
        }
        #endregion

        #endregion

        #region protected virtual methods

        #region [protected] {virtual} (object) GetValueTypedProperty(PropertyKey): Gets a value that represents the value of the specified property.
        /// <summary>
        /// Gets a value that represents the value of the specified property.
        /// </summary>
        /// <param name="propertyKey">Key to the property to obtain</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that contains property.
        /// </returns>
        protected virtual object GetValueTypedProperty(PropertyKey propertyKey)
        {
            return null;
        }
        #endregion

        #region [protected] {virtual} (void) Parse(Hashtable): Gets the property collection for this structure
        /// <summary>
        /// Gets the property collection for this structure.
        /// </summary>
        /// <param name="properties">Collection of properties of this structure.</param>
        protected virtual void Parse(Hashtable properties)
        {
        }
        #endregion

        #endregion

        #region private members

        #region [private] (IDeviceProperty) GetTypedProperty(PropertyKey): Gets a reference to an object that implements the interface IDeviceProperty, represents the value of the property specified by its key by the parameter propertyKey.
        /// <summary>
        /// Gets a reference to an object that implements the interface <see cref="IDeviceProperty" />, represents the value of the property specified by its key by the parameter <paramref name="propertyKey"/>.
        /// </summary>
        /// <param name="propertyKey">Key to the property to obtain</param>
        /// <returns>
        /// Interface that represents the value of the property.
        /// </returns>
        private IDeviceProperty GetTypedProperty(PropertyKey propertyKey)
        {
            object propertyValue = GetValueTypedProperty(propertyKey);
            IDeviceProperty newTypedProperty = DevicePropertyFactory.CreateTypedDeviceProperty(propertyKey, propertyValue);

            return newTypedProperty;
        }
        #endregion

        #endregion
    }
}
