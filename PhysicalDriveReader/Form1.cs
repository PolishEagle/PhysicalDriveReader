using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicalDriveReader
{
    public partial class Form1 : Form
    {
        #region Variables
        // The list of drives
        private Dictionary<string, Drive> _drives;
        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Loads the drives into the combobox on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            List<String> drives = WindowsDevices.GetListOfDrives();

            // Add the drive list to the combobox
            if (drives.Count > 0)
            {
                // Get the dictionary that stores the drives and the drive information
                _drives = WindowsDevices.GetDriveDictionary(drives);

                // Populate the drop combo box
                cmbDrives.DataSource = drives;

                // Select the first index
                cmbDrives.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// This function is used to handle the combo box selected index changing.
        /// It will populate the labels with the selected drives details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            String driveName = (sender as ComboBox).Text;

            if (!String.IsNullOrEmpty(driveName) && _drives != null)
            {
                Drive selectedDrive = _drives[driveName];

                lblMediaType.Text = String.Format("Media Type: {0}", selectedDrive.GetMediaType());
                lblTotalSectors.Text = String.Format("Total Sectors: {0} ({1})", selectedDrive.GetTotalSectors(), DataHighestFactor(selectedDrive.GetTotalSectors()));
                lblTotalSize.Text = String.Format("Total Size: {0} ({1})", selectedDrive.GetTotalBytes(), DataHighestFactor(selectedDrive.GetTotalBytes()));
                lblBytesPerSector.Text = String.Format("Bytes Per Sector: {0}", selectedDrive.GetBytesPerSector());
            }

            hexBox.ByteProvider = null;
            hexBox.ResetText();
            lblStatusMsg.Text = "";
        }

        /// <summary>
        /// This function is the handler for when the read sector button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadSector_Click(object sender, EventArgs e)
        {
            long sector;

            Drive selectedDrive = _drives[(String)cmbDrives.SelectedItem];

            // try to read the sector, if it fails display an error and return
            try
            {
                sector = Convert.ToInt64(txtSector.Text);

                if (sector > selectedDrive.GetTotalSectors() || sector < 0)
                    throw new Exception("out of range");
            }
            catch
            {
                lblStatusMsg.Text = "A number between 0 and max sector number please!";
                return;
            }

            // Read the sector
            ReadSector(sector, selectedDrive);
        }

        /// <summary>
        /// Generate a random number from 0 to the max sectors the current
        /// drive has and display the bytes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRandSector_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            Drive selectedDrive = _drives[(String)cmbDrives.SelectedItem];

            int randNum = rand.Next(0, (int)selectedDrive.GetTotalSectors());
            ReadSector(randNum, selectedDrive);
        }

        /// <summary>
        /// Event handler for when a key is pressed down within the sector text box.
        /// If return is pressed the current sector will be looked up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Return))
                btnReadSector.PerformClick();
        }
        #endregion

        #region Supporting functions
        /// <summary>
        /// Display the sector for specified for the given drive.
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="selDrive"></param>
        public void ReadSector(long sector, Drive selDrive)
        {
            // Clear the status text box
            lblStatusMsg.Text = String.Format("Sector #{0}", sector);

            // Number succesfully parsed, now read the sector.
            byte[] buffer = new byte[selDrive.GetBytesPerSector()];
            selDrive.ReadSector(buffer, sector);

            // Display the sector data into the HexBox.
            hexBox.ByteProvider = new Be.Windows.Forms.DynamicFileByteProvider(new MemoryStream(buffer));
        }

        /// <summary>
        /// This function parses the bytes into the highest whole number
        /// factor of data. Example. bytes to GB, etc..
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static String DataHighestFactor(long bytes)
        {
            int divisions = 0;
            while (bytes > 1024)
            {
                bytes /= 1024;
                divisions++;
            }

            // Determine the size notation
            String dataSize = "";
            switch (divisions)
            {
                case 0:
                    dataSize = "B";
                    break;
                case 1:
                    dataSize = "K";
                    break;
                case 2:
                    dataSize = "M";
                    break;
                case 3:
                    dataSize = "G";
                    break;
                case 4:
                    dataSize = "T";
                    break;
                case 5:
                    dataSize = "P";
                    break;
            }

            return String.Format("{0} {1}", bytes, dataSize);
        }
        #endregion
    }
}
