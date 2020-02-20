using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalDriveReader
{
    /// <summary>
    /// This class represents a single physical drive. The class
    /// store the required drive geometry and file stream needed
    /// for communicating with the drive.
    /// </summary>
    public class Drive
    {
        #region Class variables
        private String _driveName;
        private FileStream _stream;
        private SafeFileHandle _driveHndl;
        private DISK_GEOMETRY _driveGeometry;
        #endregion

        #region DLL Imports & Kernel32 Related items
        // Create file is needed for getting the drive info. 
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern SafeFileHandle CreateFile(string lpFileName, System.UInt32 dwDesiredAccess,
                                            System.UInt32 dwShareMode, IntPtr pSecurityAttributes, System.UInt32 dwCreationDisposition,
                                            System.UInt32 dwFlagsAndAttributes, IntPtr hTemplateFile);

        // Needed for sending command to device for information retrieval
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize,
                                            IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);


        const uint GENERIC_READ = 0x80000000;
        const uint FILE_SHARE_READ = 0x00000001;
        const uint FILE_SHARE_WRITE = 0x00000002;
        const uint OPEN_EXISTING = 3;
        const uint IOCTL_DISK_GET_DRIVE_GEOMETRY = 0x70000;

        // Struct to store the disk information.
        public struct DISK_GEOMETRY
        {
            public long Cylinders;
            public short MediaType;
            public int TracksPerCylinder;
            public int SectorsPerTrack;
            public int BytesPerSector;
        }
        #endregion

        #region Constructor/Destructor
        public Drive(String driveName)
        {
            if (String.IsNullOrEmpty(driveName))
                throw new Exception("Invalid drive name");

            _driveName = driveName;

            // Create a file to connect with the physical drive
            _driveHndl = CreateFile(@"\\.\" + _driveName, GENERIC_READ, (FILE_SHARE_READ | FILE_SHARE_WRITE),
                                 IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            // Get the drive geometry
            _driveGeometry = GetDriveGeometry();

        }

        /// <summary>
        /// Destructor for cleaning up the file strean
        /// </summary>
        ~Drive()
        {
            if (_stream != null)
                _stream.Close();
            
            if (_driveHndl != null && !_driveHndl.IsClosed)
                _driveHndl.Close();
        }
        #endregion

        #region Class Accessors
        /// <summary>
        /// Returns the total number of sectors for the drive.
        /// </summary>
        /// <returns></returns>
        public long GetTotalSectors()
        {
            return _driveGeometry.Cylinders * _driveGeometry.TracksPerCylinder * _driveGeometry.SectorsPerTrack;
        }

        /// <summary>
        /// Returns the total number of bytes for the drive.
        /// </summary>
        /// <returns></returns>
        public long GetTotalBytes()
        {
            return _driveGeometry.Cylinders * _driveGeometry.TracksPerCylinder * _driveGeometry.SectorsPerTrack * _driveGeometry.BytesPerSector;
        }

        /// <summary>
        /// Returns the drive media type.
        /// </summary>
        /// <returns></returns>
        public short GetMediaType()
        {
            return _driveGeometry.MediaType;
        }

        /// <summary>
        /// Returns how many bytes per sector the drive has.
        /// </summary>
        /// <returns></returns>
        public int GetBytesPerSector()
        {
            return _driveGeometry.BytesPerSector;
        }
        #endregion

        #region Supporting functions
        /// <summary>
        /// Reads the sectors that is specified and returns the data within the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="sector"></param>
        public void ReadSector(byte[] buffer, long sector)
        {
            // Go to the sector that we want to read.
            _stream.Seek(_driveGeometry.BytesPerSector * sector, SeekOrigin.Begin);
            _stream.Read(buffer, 0, _driveGeometry.BytesPerSector);
        }

        private DISK_GEOMETRY GetDriveGeometry()
        {
            DISK_GEOMETRY diskgeo = new DISK_GEOMETRY();

            // check if the handle is valid that we just created
            if (_driveHndl.IsInvalid)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            else
            {
                // open and save a file stream for reading the sectors later
                _stream = new FileStream(_driveHndl, FileAccess.Read);

                // Allocate memory for the struct
                int geoStructSize = Marshal.SizeOf(typeof(DISK_GEOMETRY));
                IntPtr geoStruct = Marshal.AllocHGlobal(geoStructSize);
                int lpBytesReturned = 0;

                // Run the command to populate the buffer with the geometry data
                if (DeviceIoControl(_driveHndl, IOCTL_DISK_GET_DRIVE_GEOMETRY, IntPtr.Zero, 0, geoStruct, geoStructSize, out lpBytesReturned, IntPtr.Zero))
                {
                    diskgeo = (DISK_GEOMETRY)Marshal.PtrToStructure(geoStruct, typeof(DISK_GEOMETRY));
                }
            }

            return diskgeo;
        }
        #endregion
    }
}
