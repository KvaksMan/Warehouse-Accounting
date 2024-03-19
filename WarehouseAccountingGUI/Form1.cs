using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WarehouseAccounting;
using System.Runtime.InteropServices;
using static WarehouseAccounting.WarehouseRecord;

namespace WarehouseAccountingGUI
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private Database database;
        private WarehouseRecord.TypeRecord recordType;

        private WarehouseRecordGeneral selectedWarehouseRecord;
        private DataGridViewRow selectedWarehouseRecordRow;

        public Form1()
        {
            //AllocConsole();
            InitializeComponent();

            Task.Run(() => { Init(); InitElements_PanelFilters(); });

            InitElements_PanelEditing();

            Console.WriteLine("Form1()");
        }



        private void Init()
        {
            this.database = new Database();

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        }

        private void InitElements_PanelFilters()
        {
            foreach (string typeRecord in Enum.GetNames(typeof(WarehouseRecord.TypeRecord)))
            {
                //sectionCB.Items.Add(typeRecord);
                sectionCB.Invoke(new Action(() => { sectionCB.Items.Add(typeRecord); }));
            }
            //sectionCB.SelectedIndex = 0;
            sectionCB.Invoke(new Action(() => { sectionCB.SelectedIndex = 0; }));
        }

        private void InitElements_PanelEditing()
        {
            //editingGroupBoxes = new GroupBox[2];
            //GroupBox[] gp = new GroupBox[5];
            //editingGroupBoxes = &gp;

            //editingGroupBoxes[0] = &editingGB_none;
            //editingGroupBoxes[1] = &editingGB_general;

            ToggleEditingGroupBox(editingGB_none);
        }



        private GroupBox? prevEditingGroupBox = null;
        //private GroupBox*[] editingGroupBoxes;

        private void ToggleEditingGroupBox(GroupBox gb)
        {
            if (prevEditingGroupBox == null)
            {
                //for (int i = 0; i < editingGroupBoxes.Length; i++)
                //{
                //    SetGroupBoxStatus(editingGroupBoxes[0], false);
                //}
                SetGroupBoxStatus(editingGB_none, false);
                SetGroupBoxStatus(editingGB_general, false);
            }
            else
            {
                SetGroupBoxStatus(prevEditingGroupBox, false);
            }

            SetGroupBoxStatus(gb, true);

            prevEditingGroupBox = gb;
        }

        private void SetGroupBoxStatus(GroupBox gb, bool status)
        {
            gb.Enabled = status;
            gb.Visible = status;
        }



        private void sectionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            recordType = (TypeRecord)Enum.ToObject(typeof(TypeRecord), sectionCB.SelectedIndex);
            ToggleEditingGroupBox(false);

            DisplayColumnsGENERAL();

            switch (recordType)
            {
                case TypeRecord.GENERAL:
                    DisplayRows_GENERAL(database.GetRecords());
                    break;

                case TypeRecord.MCU:
                    DisplayColumnsAdditional(columnNames_MCU);
                    DisplayRows_MCU(database.GetRecordsMCU());
                    ToggleEditingGroupBox(editingGB_general);
                    break;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    selectedWarehouseRecordRow = dataGridView1.SelectedRows[0];

                    short id = short.Parse(selectedWarehouseRecordRow.Cells[0].FormattedValue.ToString());
                    selectedWarehouseRecord = database.GetRecordByID(id);
                    if (selectedWarehouseRecord == null) return;

                    ToggleEditingGroupBox(true);
                }
                catch (Exception ex)
                {

                }
            }
        }



        //private WarehouseRecordGeneral recordGeneralAdded = null;
        //private int? recordGeneralAddedRow = null;

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //recordGeneralAddedRow = e.RowIndex - 1;
            //recordGeneralAdded = new WarehouseRecordGeneral(0, "", 0, 0, null);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ////if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //if (e.RowIndex == recordGeneralAddedRow)
            //{
            //    object changedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //    MessageBox.Show($"Cell at row {e.RowIndex}, column {e.ColumnIndex} changed to: {changedValue}");

            //    // TODO: complete
            //    switch (recordType)
            //    {
            //        case TypeRecord.MCU:

            //            switch (e.ColumnIndex)
            //            {
            //                case 2:
            //                    recordGeneralAdded = new WarehouseRecordGeneral(
            //                        (short)dataGridView1.Rows[e.RowIndex].Cells[0].Value,
            //                        (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value,
            //                        (short)dataGridView1.Rows[e.RowIndex].Cells[2].Value,
            //                        (int)dataGridView1.Rows[e.RowIndex].Cells[3].Value,
            //                        null
            //                        );

            //                    break;
            //            }

            //            break;
            //    }
            //}
        }

        private void ToggleEditingGroupBox(bool status)
        {
            if (!status)
            {
                selectedWarehouseRecordRow = null;
                selectedWarehouseRecord = null;
                selectedTB.Text = "";
            }
            else
            {
                selectedTB.Text = selectedWarehouseRecord.name;
            }

            takeBTN.Enabled = status;
            putInBTN.Enabled = status;
        }



        private String[] columnNames_GENERAL =
        {
            "ID", "NAME", "COUNT", "LOCATION", "DATA"
        };
        private String[] columnNames_MCU =
        {
            "ID", "NAME", "COUNT", "LOCATION",
            "FLASH", "SRAM", "EEPROM", "GPIO", "PWM Channels", "CPU Frequency",
            "Voltage MAX", "Voltage MIN", "Temperature MAX", "Temperature MIN"
        };



        private void DisplayColumnsGENERAL()
        {
            int columnWidth = TextRenderer.MeasureText("0000", dataGridView1.Font).Width;

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            DisplayColumnsAdditional(columnNames_GENERAL);
        }

        private void DisplayColumnsAdditional(String[] columnNames)
        {
            dataGridView1.ColumnCount = columnNames.Length;
            for (int i = 0; i < columnNames.Length; i++)
            {
                dataGridView1.Columns[i].Name = columnNames[i];
                dataGridView1.Columns[i].Width = GetMinColumnWidthByText(columnNames[i]);
            }
        }

        private void DisplayRows_GENERAL(WarehouseRecordGeneral[] warehouseRecords)
        {
            foreach (WarehouseRecordGeneral record in warehouseRecords)
            {
                // "ID", "NAME", "COUNT", "LOCATION", "DATA"
                Object[] objects = {
                    record.id, record.name, record.count, record.location.GetCode(), record.GetDataJson()
                };
                dataGridView1.Rows.Add(objects);
                SetColumnWidthsBasedOnLastRow();
            }
        }

        private void DisplayRows_MCU(WarehouseRecordMCU[] warehouseRecords)
        {
            foreach (WarehouseRecordMCU record in warehouseRecords)
            {
                // "ID", "NAME", "COUNT", "LOCATION",
                // "FLASH", "SRAM", "EEPROM", "GPIO", "PWM Channels", "CPU Frequency",
                // "Voltage MAX", "Voltage MIN", "Temperature MAX", "Temperature MIN"
                Object[] objects = {
                    record.id, record.name, record.count, record.location.GetCode(),
                    record.FLASH, record.SRAM, record.EEPROM, record.GPIO, record.PWM_CHANNELS, record.CPU_FREQUENCY,
                    record.workingConditions.voltage.max, record.workingConditions.voltage.min, record.workingConditions.temperature.max, record.workingConditions.temperature.min
                };
                dataGridView1.Rows.Add(objects);
                SetColumnWidthsBasedOnLastRow();
            }
        }



        private int GetMinColumnWidthByText(string text)
        {
            int minWidth = 30;

            int cellWidth = TextRenderer.MeasureText(text, dataGridView1.Font).Width + 10;
            minWidth = Math.Max(minWidth, cellWidth);

            return minWidth;
        }

        private void SetColumnWidthsBasedOnLastRow()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];

                for (int columnIndex = 0; columnIndex < dataGridView1.Columns.Count; columnIndex++)
                {
                    int preferredWidth = TextRenderer.MeasureText(
                        lastRow.Cells[columnIndex].FormattedValue.ToString(),
                        dataGridView1.DefaultCellStyle.Font).Width + 10;

                    int minWidth = dataGridView1.Columns[columnIndex].Width;

                    dataGridView1.Columns[columnIndex].Width = Math.Max(preferredWidth, minWidth);
                }
            }
        }


        private void takeBTN_Click(object sender, EventArgs e)
        {
            if (selectedWarehouseRecord == null) return;
            selectedWarehouseRecord--;
            selectedWarehouseRecordRow.Cells["COUNT"].Value = selectedWarehouseRecord.count;
            database.UpdateRecord_Count(selectedWarehouseRecord);
        }

        private void putInBTN_Click(object sender, EventArgs e)
        {
            if (selectedWarehouseRecord == null) return;
            selectedWarehouseRecord++;
            selectedWarehouseRecordRow.Cells["COUNT"].Value = selectedWarehouseRecord.count;
            database.UpdateRecord_Count(selectedWarehouseRecord);
        }
    }
}