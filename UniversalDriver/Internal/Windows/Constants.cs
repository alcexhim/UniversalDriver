using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalDriver.Internal.Windows
{
    internal static class Constants
    {
        [Flags()]
        public enum SetupDiGetClassDevsFlags : uint
        {
            None = 0x0,
            /// <summary>
            /// Return a list of installed devices for all device setup classes or all device interface classes.
            /// </summary>
            AllClasses = 0x4,
            /// <summary>
            /// Return devices that support device interfaces for the specified device interface classes. This flag must be set in the Flags parameter if the Enumerator parameter specifies a device instance ID.
            /// </summary>
            DeviceInterfaces = 0x10,
            /// <summary>
            /// Return only the device that is associated with the system default device interface, if one is set, for the specified device interface classes.
            /// </summary>
            Default = 0x1,
            /// <summary>
            /// Return only devices that are currently present in a system.
            /// </summary>
            Present = 0x2,
            /// <summary>
            /// Return only devices that are a part of the current hardware profile.
            /// </summary>
            HardwareProfile = 0x8
        }
        [Flags()]
        public enum SPDeviceInterfaceDataFlags : uint
        {
            None = 0,
            /// <summary>
            /// The interface is active (enabled).
            /// </summary>
            Active = 0x1,
            /// <summary>
            /// The interface is the default interface for the device class.
            /// </summary>
            Default = 0x2,
            /// <summary>
            /// The interface is removed.
            /// </summary>
            Removed = 0x4
        }
        public static class DeviceClassGUID
        {
            public static Guid UsbDevice = new Guid("(A5DCBF10-6530-11D2-901F-00C04FB951ED)");
        }

        [Flags()]
        public enum CreateFileDesiredAccess : uint
        {
            GenericAll = 0x10000000,
            GenericExecute = 0x20000000,
            GenericWrite = 0x40000000,
            GenericRead = 0x80000000,
        }

        /// <summary>
        /// The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none.
        /// </summary>
        [Flags()]
        public enum CreateFileShareMode : uint
        {
            None = 0x00000000,
            /// <summary>
            /// Enables subsequent open operations on a file or device to request read access. Otherwise, other processes cannot open the file or device if they request
            /// read access. If this flag is not specified, but the file or device has been opened for read access, the function fails.
            /// </summary>
            Read = 0x00000001,
            /// <summary>
            /// Enables subsequent open operations on a file or device to request write access. Otherwise, other processes cannot open the file or device if they request
            /// write access. If this flag is not specified, but the file or device has been opened for write access or has a file mapping with write access, the function
            /// fails.
            /// </summary>
            Write = 0x00000002,
            /// <summary>
            /// Enables subsequent open operations on a file or device to request delete access. Otherwise, other processes cannot open the file or device if they request
            /// delete access. If this flag is not specified, but the file or device has been opened for delete access, the function fails. Delete access allows both
            /// delete and rename operations.
            /// </summary>
            Delete = 0x00000004
        }

        /// <summary>
        /// An action to take on a file or device that exists or does not exist.
        /// </summary>
        [Flags()]
        public enum CreateFileCreationDisposition
        {
            /// <summary>
            /// Creates a new file, only if it does not already exist. If the specified file exists, the function fails and the last-error code is set to ERROR_FILE_EXISTS
            /// (80). If the specified file does not exist and is a valid path to a writable location, a new file is created.
            /// </summary>
            CreateNew = 1,
            /// <summary>
            /// Creates a new file, always. If the specified file exists and is writable, the function overwrites the file, the function succeeds, and last-error code is
            /// set to ERROR_ALREADY_EXISTS (183). If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and the
            /// last-error code is set to zero.
            /// </summary>
            CreateAlways = 2,
            /// <summary>
            /// Opens a file or device, only if it exists. If the specified file or device does not exist, the function fails and the last-error code is set to
            /// ERROR_FILE_NOT_FOUND (2).
            /// </summary>
            OpenExisting = 3,
            /// <summary>
            /// Opens a file, always. If the specified file exists, the function succeeds and the last-error code is set to ERROR_ALREADY_EXISTS (183). If the specified
            /// file does not exist and is a valid path to a writable location, the function creates a file and the last-error code is set to zero.
            /// </summary>
            OpenAlways = 4,
            /// <summary>
            /// Opens a file and truncates it so that its size is zero bytes, only if it exists. If the specified file does not exist, the function fails and the
            /// last-error code is set to ERROR_FILE_NOT_FOUND (2). The calling process must open the file with the GENERIC_WRITE bit set as part of the dwDesiredAccess
            /// parameter.
            /// </summary>
            TruncateExisting = 5
        }

        /// <summary>
        /// The file or device attributes and flags, <see cref="CreateFileAttributes.Normal"/> being the most common default value for files.
        /// </summary>
        [Flags()]
        public enum CreateFileAttributes
        {
            /// <summary>
            /// The file is read only. Applications can read the file, but cannot write to or delete it.
            /// </summary>
            ReadOnly = 0x1,
            /// <summary>
            /// The file is hidden. Do not include it in an ordinary directory listing.
            /// </summary>
            Hidden = 0x2,
            /// <summary>
            /// The file is part of or used exclusively by an operating system.
            /// </summary>
            System = 0x4,
            /// <summary>
            /// The file should be archived. Applications use this attribute to mark files for backup or removal.
            /// </summary>
            Archive = 0x20,
            /// <summary>
            /// The file does not have other attributes set. This attribute is valid only if used alone.
            /// </summary>
            Normal = 0x80,
            /// <summary>
            /// The file is being used for temporary storage.
            /// </summary>
            Temporary = 0x100,
            /// <summary>
            /// The data of a file is not immediately available. This attribute indicates that file data is physically moved to offline storage. This attribute is used by
            /// Remote Storage, the hierarchical storage management software. Applications should not arbitrarily change this attribute.
            /// </summary>
            Offline = 0x1000,
            /// <summary>
            /// The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the
            /// default for newly created files and subdirectories. This flag has no effect if FILE_ATTRIBUTE_SYSTEM is also specified. This flag is not supported on Home,
            /// Home Premium, Starter, or ARM editions of Windows.
            /// </summary>
            Encrypted = 0x4000
        }
    }
}
