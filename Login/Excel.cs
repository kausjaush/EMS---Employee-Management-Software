using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    class Excel
    {
            public static void ExtractDataToCSV(DataGridView dgv,string period)
            {

                // Don't save if no data is returned
                if (dgv.Rows.Count == 0)
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                // Column headers
                string columnsHeader = "";
                sb.Append(period + Environment.NewLine);
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    columnsHeader += dgv.Columns[i].HeaderText + ",";
                }
                sb.Append(columnsHeader + Environment.NewLine);
                // Go through each cell in the datagridview
                foreach (DataGridViewRow dgvRow in dgv.Rows)
                {
                    // Make sure it's not an empty row.
                    if (!dgvRow.IsNewRow)
                    {
                        for (int c = 0; c < dgvRow.Cells.Count; c++)
                        {
                            // Append the cells data followed by a comma to delimit.
                            sb.Append(dgvRow.Cells[c].Value.ToString().Trim() + ",");
                        }
                        // Add a new line in the text file.
                       
                    }
                    sb.Append(Environment.NewLine);
                }
                // Load up the save file dialog with the default option as saving as a .csv file.
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV files (*.csv)|*.csv";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // If they've selected a save location...
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                    {
                        // Write the stringbuilder text to the the file.
                        sw.WriteLine(sb.ToString());
                    }
                    MessageBox.Show("CSV file saved.");
                }
                else
                {
                    MessageBox.Show("File Saving Aborted", "Error");
                }

            }
        
    }
}
