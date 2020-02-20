using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalDriveReader
{
    class WindowsDevices
    {
        #region DLL Imports & Kernel32 Related items
        // This function will be used for getting the list of all MS DOS devices.
        // The devices will be added to the provided memory and each device is
        // separated by a null terminating string
        [DllImport("kernel32.dll")]
        static extern int QueryDosDevice(string lpDeviceName, IntPtr lpTargetPath, int ucchMax);
        #endregion

        #region Functions
        /// <summary>
        /// This function queries the system for all the physical drive
        /// and then returns a list of the drive names
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListOfDrives()
        {
            List<string> drives = null;

            // Allocate some memory to get a list of all system devices.
            // Start with a small size and dynamically give more space until we have enough room.
            int numTChars = 0;
            int bufferSize = 100;
            string dosDevices = null;
            IntPtr tempMemory;

            while (numTChars == 0)
            {
                tempMemory = Marshal.AllocHGlobal(bufferSize);
                if (tempMemory != IntPtr.Zero)
                {
                    try
                    {
                        // Get the list of devices
                        numTChars = QueryDosDevice(null, tempMemory, bufferSize);

                        // check if the function returned successfully
                        if (numTChars != 0)
                        {
                            // get all the dos devices and parse out the physical drives
                            dosDevices = Marshal.PtrToStringAnsi(tempMemory, numTChars);
                            drives = dosDevices.Split('\0').Where(val => val.StartsWith("PhysicalDrive")).ToList<string>();
                        }
                        else if (Marshal.GetLastWin32Error() == 122)
                        {
                            // The buffer was too small, increase the size and try again
                            bufferSize *= 2;
                        }
                        else
                        {
                            // throw any other exceptions that we weren't expecting
                            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(tempMemory);
                    }
                }
                else
                {
                    // error occured trying to get unmanaged memory, we can't continue
                    throw new OutOfMemoryException();
                }
            }

            // Return the list of physical drives
            return drives;
        }

        /// <summary>
        /// This function takes in the string list of drives and attempts to
        /// add get the information for all the drives to store in a dictionary.
        /// </summary>
        /// <param name="drives"></param>
        /// <returns></returns>
        public static Dictionary<String, Drive> GetDriveDictionary(List<string> drives)
        {
            Dictionary<String, Drive> drivesDictionary = new Dictionary<string,Drive>();

            foreach (String drive in drives)
                drivesDictionary.Add(drive, new Drive(drive));

            return drivesDictionary;
        }
        #endregion
    }
}
