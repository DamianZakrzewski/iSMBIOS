﻿
namespace iTin.Core.Hardware.Specification
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    using Interop.Windows.Development.SystemServices.SystemInformation.Firmware;

    /// <summary>
    /// Methods for handle system firmware tables.
    /// Based on <c>open-hardware-monitor</c> project, for more info, please see: https://searchcode.com/codesearch/view/3147305/ [Michael Möller].
    /// </summary>
    public static class FirmwareTable
    {
        /// <summary>
        /// Returns an array that contains the tables associated with the specified provider.
        /// </summary>
        /// <param name="provider">Table provider</param>
        /// <returns>
        /// Tables associated with the specified provider
        /// </returns>
        public static string[] EnumerateTables(KnownProvider provider)
        {
            int size;
            try
            {
                size = NativeMethods.EnumSystemFirmwareTables(provider, IntPtr.Zero, 0);
            }
            catch (DllNotFoundException)
            {
                return null;
            }
            catch (EntryPointNotFoundException)
            {
                return null;
            }

            IntPtr nativeBuffer = Marshal.AllocHGlobal(size);
            NativeMethods.EnumSystemFirmwareTables(provider, nativeBuffer, size);
            byte[] buffer = new byte[size];
            Marshal.Copy(nativeBuffer, buffer, 0, size);
            Marshal.FreeHGlobal(nativeBuffer);

            string[] result = new string[size / 4];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Encoding.ASCII.GetString(buffer, 4 * i, 4);
            }

            return result;
        }

        /// <summary>
        /// Returns raw data from the specified provider table.
        /// </summary>
        /// <param name="provider">Table provider</param>
        /// <param name="table">Table name</param>
        /// <returns>
        /// Raw data for specified table.
        /// </returns>
        public static byte[] GetTable(KnownProvider provider, string table)
        {
            int id = table[3] << 24 | table[2] << 16 | table[1] << 8 | table[0];
            return GetTable(provider, id);
        }


        /// <summary>
        /// Returns raw data from the specified provider table.
        /// </summary>
        /// <param name="provider">Table provider</param>
        /// <param name="table">Table name</param>
        /// <returns>
        /// Raw data for specified table.
        /// </returns>
        private static byte[] GetTable(KnownProvider provider, int table)
        {
            int size;

            try
            {
                size = NativeMethods.GetSystemFirmwareTable(provider, table, IntPtr.Zero, 0);
            }
            catch (DllNotFoundException)
            {
                return null;
            }
            catch (EntryPointNotFoundException)
            {
                return null;
            }

            if (size <= 0)
            {
                return null;
            }

            IntPtr nativeBuffer = Marshal.AllocHGlobal(size);
            NativeMethods.GetSystemFirmwareTable(provider, table, nativeBuffer, size);

            if (Marshal.GetLastWin32Error() != 0)
            {
                return null;
            }

            byte[] buffer = new byte[size];
            Marshal.Copy(nativeBuffer, buffer, 0, size);
            Marshal.FreeHGlobal(nativeBuffer);

            return buffer;
        }
    }
}