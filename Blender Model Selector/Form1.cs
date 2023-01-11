using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Blender_Model_Selector_Domain.Managers;
using Blender_Model_Selector_Domain.Models;

namespace Blender_Model_Selector
{
    public partial class Form1 : Form
    {

        #region Class members
        //Create property containing table manager class.
        private TableManager tableManager { get; set; }

        //Create a property to store table objects which is a list of Table Displays that contain a UI & SQL Table name plus a data table.
        private List<Table_OBJ> tableObjects { get; set; }

        //Create Generated Project property 
        private Generated_Project projectObject { get; set; }

        //Create a dictionary property to hold all comboBoxes and assign them each an index value.
        private Dictionary<int, ComboBox> formComboBoxes { get; set; }

        //Create a dictionary property to hold all labels from form.
        private Dictionary<int, Label> formLabels { get; set; }

        //Create data table to hold generated project 
        private DataTable sample = new DataTable();

        private BindingSource bindingSource = new BindingSource();

        //Create private int to carry the number of values changed in the data grid view.
        private int numberOfValuesChanged;
        #endregion

        #region On Load
        public Form1()
        {
            InitializeComponent();

        }

        //On load, get the list of tables that are currently within the database.
        private void Form1_Load(object sender, EventArgs e)
        {
            //Instantiate Table Manager class
            tableManager = new TableManager();

            //Grab all core tables (accent color, etc.) and their values.
            tableObjects = tableManager.getCoreTableObjects();

            //Collect combo boxes with data tables from the list of table objects.
            collectComboBoxes(tableObjects);

            //Collect labels for later iteration
            collectComboBoxLabels();

            //Prevents open button from being used when no table is selected.
            btn_GetTableValues.Enabled = false;

            //Prevents calling the store project method before a project has been generated. 
            btn_StoreProject.Enabled = false;

            //Add the list of table displays to the list box.
            listBox_Tables.Items.AddRange(tableObjects.ToArray());

            //Set the list box to only display the uiTableName object attribute, not the sqlTableName.
            listBox_Tables.DisplayMember = "uiTableName";

            //Add Generated Project Table Name
            listBox_Tables.Items.Add("Generated Projects");

            //Disable combo box filters until project has been generated OR table opened.
            disableFilters();

            //Disable save button until a value within the data grid view has changed.
            btn_Save.Enabled = false;

        }

        #endregion

        #region Buttons

        //Store Project Button
        private void btn_StoreProject_Click(object sender, EventArgs e)
        {
            //Call to the check filters method, pass the generated sample.
            checkFiltersAssignProperties(sample);

            //Call to the store project method, send the list of table objects and the project object containing assigned values minus their IDs.
            int numRows = tableManager.storeProject(tableObjects, projectObject);

            //If num rows returns 6, then create success message. 
            if (numRows == tableObjects.Count)
            {
                MessageBox.Show("Project Stored. Update Successful.");
            }
            else {

                MessageBox.Show("Project Failed to Store. Update Un-successful.");
            }

            //Reset filters.
            clearFilters();

            //Disable Store Project button once again.
            btn_StoreProject.Enabled = false;

            //Clear data grid to prevent previous generate call from storing.
            dataGridView.Columns.Clear();
        }

        //Open Button
        private void btn_GetTableValues_Click(object sender, EventArgs e)
        {
            //Clear data grid view of any prior selection.
            dataGridView.ClearSelection();

            //Disable store project button to prevent user from trying to store table as generated sample.
            btn_StoreProject.Enabled = false;
            
            //If there is a value selected within the list box of tables.
            if (listBox_Tables.SelectedIndex != -1) {

                //Get the current selected item from the list box, cast it back into a Table Display object.
                Table_OBJ tableDisplay = listBox_Tables.SelectedItem as Table_OBJ;

                //Create instance of data table 
                DataTable dataTable = new DataTable();

                //If the selected item is Generated Projects
                if (listBox_Tables.SelectedItem.ToString() == "Generated Projects")
                {
                    //Call to the table manager get Generated Project Method, return generated projects' data table.
                    dataTable = tableManager.getGeneratedProjects();

                    //Set data table's name to the selected item.
                    dataTable.TableName = "Generated_Projects";

                    //Call to data binder method, pass data table.
                    bindDataGridView(dataTable);

                    //Fill combo boxes with values pulled from each table object's data table.
                    fillComboBoxes(tableObjects, formComboBoxes);
                }

                //Else, for all other tables, add to data grid view.
                else {

                    //Disable filters for any table other than the Generated Projects Table.
                    disableFilters();

                    //Call to data binder method, pass data table.
                    bindDataGridView(tableDisplay.dataTable);
                }
            }

            //Clear combo box filters.
            clearFilters();

        }
        //Private data binder method that takes in a data table.
        private void bindDataGridView(DataTable dataTable)
        {
            //Create new binding source
            bindingSource = new BindingSource();

            //Attach generated projects data table to the binding source
            bindingSource.DataSource = dataTable;

            //Attach the binding source to the data grid view's data source 
            dataGridView.DataSource = bindingSource;
        }

        //Generate Button
        private void btn_GenerateProject_Click(object sender, EventArgs e)
        {

            //Enable store project button.
            btn_StoreProject.Enabled = true;

            //If data grid already has rows, clear them.
            if (dataGridView.Columns.Count > 0) {

                //Clear data grid to prevent previous generate call from storing.
                dataGridView.Columns.Clear();
            }

            //Create data table to hold generated project sample.
            sample = new DataTable();

            //Create an empty data row to hold each row returned from the LINQ query of each table's data table.
            DataRow tableObjectData = null;

            //Create a new data row to contain the single row of randomly selected values. //Set to null to prevent more than one row being created.
            DataRow dataRow = null;

            //For each table object in the list of table objects
            foreach (Table_OBJ tableObject in tableObjects)
            {
                //If the data row is null, create a new row and sets its schema to be the same as the sample data table.
                if (dataRow == null)
                {

                    //Create a data row, mimic the sample table's schema.
                    dataRow = sample.NewRow();

                }

                //Collect row of data, randomly selected from each table, and return that row as a data row object.
                tableObjectData = (from r in tableObject.dataTable.AsEnumerable()
                                   .OrderBy(r => Guid.NewGuid()) 
                                   select r).FirstOrDefault();

                //Add the table object's name to the sample table's column.
                sample.Columns.Add(tableObject.uiTableName);

                //At this data row's column, assign it the second value of this table object's data row item array. 
                dataRow[tableObject.uiTableName] = tableObjectData.ItemArray[1];

            }

            //Add data row to sample table.
            sample.Rows.Add(dataRow);

            //Set table name for later usage.
            sample.TableName = "Generated Project";

            //Call to data binder method, pass data table.
            bindDataGridView(sample);

            //Fill combo boxes with values pulled from each table object's data table.
            fillComboBoxes(tableObjects, formComboBoxes);
        }

        //Exit Button
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //AI Generate Button
        private void aiGenerateButton_Click(object sender, EventArgs e)
        {

        }

        //Save Button
        private void btn_Save_Click(object sender, EventArgs e)
        {

            //Create return int values for user message responses.
            int numRows = 0;

            //Grab the data table from within the binding source.
            DataTable dataTable = (DataTable)bindingSource.DataSource;

            //Assign binding source the data source (updated data table) 
            bindingSource = dataGridView.DataSource as BindingSource;

            //Get the number of rows deleted.
            var rowsDeleted = from rows in dataTable.AsEnumerable()
                     where rows.RowState == DataRowState.Deleted
                     select dataTable.Rows.Count;

            //Call to the save changes method in the table manager, pass the table display that was selected.
            numRows = tableManager.saveChanges(dataTable);

            //Message some form of confirmation back to the user.
            if (numRows > 1)
            {
                //Re-use numRows int and store count of rows that were deleted.
                numRows = rowsDeleted.Count();

                //If no rows were deleted.
                if (numRows <= 0)
                {
                    //Display message to user upon success.
                    MessageBox.Show($"{dataTable.TableName} successfully updated. ({numberOfValuesChanged}) values changed.");

                    //Reset variable.
                    numberOfValuesChanged = 0;
                }
                else if (numRows > 0 && numberOfValuesChanged < 0) {

                    //Display message to user upon success.
                    MessageBox.Show($"{dataTable.TableName} successfully updated. Deleted ({numRows}) Row(s).");

                } else if (numRows > 0 && numberOfValuesChanged > 0) {

                    //Display message to user upon success.
                    MessageBox.Show($"{dataTable.TableName} successfully updated. Deleted ({numRows}) Row(s). Updated ({numberOfValuesChanged}) values.");
                }
            }
            else
            {
                //Display message to user upon failure
                MessageBox.Show($"Update to {dataTable.TableName} was not successful.");
            }

            //Clear columns from data grid view 
            dataGridView.Columns.Clear();

            //Disable save button again.
            btn_Save.Enabled = false;
        }

        #endregion

        #region Data Grid View & List Box methods
        //Private method to enable save button if a value in the data grid view has been changed.
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //If there is a value selected within the list box of tables.
            if (listBox_Tables.SelectedIndex != -1)
            {
                //Allow user to click the save button.
                btn_Save.Enabled = true;
            }

            //Increase the number of values that have been changed.
            numberOfValuesChanged++;
        }

        //Method to prevent submission of an empty list box value.
        private void listBox_Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Allow user to click the open button.
            btn_GetTableValues.Enabled = true;
        }

        //Private method to fire if a row is being deleted. 
        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            btn_Save.Enabled = true;

            ////Get dataGridView from sender.
            //var senderDataGrid = (DataGridView) sender;

            ////Int value to hold selected data grid view row index.
            //int index = 0;



            ////Message warning to user; display number of rows selected for deletion.
            //DialogResult result = MessageBox.Show($"Delete {selectedRowCount} rows? Note: This will delete any rows associated to a Generated Project once saved.", "Warning", MessageBoxButtons.YesNo);

            ////If YES to delete row(s), grab data source, store selected rows into data table, call to Table Manager and pass data table.
            //if (result == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    //Assign binding source the data source (updated data table) 
            //    bindingSource = senderDataGrid.DataSource as BindingSource;

            //    //Grab the data table from within the binding source.
            //    DataTable dataTable = (DataTable)bindingSource.DataSource;

            //    //Create data table to hold rows selected for deletion; use existing data table schema to allow 
            //    DataTable rowsToBeDeleted = dataTable.Clone();

            //    //Get each selected row
            //    foreach (DataGridViewRow dataGridViewRow in senderDataGrid.SelectedRows)
            //    {
            //        //Convert data grid view row to data row.
            //        DataRow dataRow = ((DataRowView)dataGridViewRow.DataBoundItem).Row;

            //        //Add data rows to data table.
            //        rowsToBeDeleted.ImportRow(dataRow);

            //    }

            //    //Pass data table to table manager, return number of rows affected.
            //    int numRows = tableManager.deleteRow(rowsToBeDeleted);

            //    //Display message to user regarding detele success or failure.
            //    if (numRows > 0)
            //    {
            //        MessageBox.Show($"Deletion of ({numRows}) Row(s) successful.");
            //    }
            //    else {

            //        MessageBox.Show($"Delete failed. ({numRows}) Row(s) un-affected.");
            //    }
            //}
        }

        #endregion

        #region Combo Box Methods

        //Private method to get all comboBoxes in the filter row above the data grid view.
        private void collectComboBoxes(List<Table_OBJ> tableObjects)
        {
            //Create a generic variable (supports a simple iteration over a type of collection), is essentially a list. (var)
            //Grab the combo boxes found on this form, then check to see if any of their names start with the string: combobox.
            var comboBoxes = this.Controls.OfType<ComboBox>().Where(x => x.Name.StartsWith("comboBox"));

            //Sort the combo boxes by name (to set comboBox1 as first value)
            comboBoxes = comboBoxes.OrderBy(item => item.Name);

            //Create a dictionary to assign each combo box in the comboBoxes list a number.
            formComboBoxes = new Dictionary<int, ComboBox>();

            //Iteration variable to assign each combo box an indexable value.
            int i = 0;

            //For each combo box in the var type list of combo boxes
            foreach (var comboBox in comboBoxes)
            {

                //Add the artifical index and combo box, thus creating a Key | Value relationship.
                formComboBoxes.Add(i, comboBox);

                //Increment Iteration Variable 
                i++;
            }

            //For each combo box, add an event handler that fires if the selected index changes.
            foreach (KeyValuePair<int, ComboBox> comboBox in formComboBoxes)
            {
                comboBox.Value.SelectedIndexChanged += new EventHandler((object o, EventArgs e) =>
                {
                    //Check if the data grid view has been populated with data.
                    if (dataGridView.Rows.Count != 0)
                    {
                        //If the combo box firing this event has a value.
                        if (comboBox.Value.SelectedIndex != -1) {

                            //When THIS changes, highlight the corresponding row in the grid below.
                            dataGridView.Rows[0].Cells[comboBox.Key].Style.BackColor = Color.Tomato;
                        }
                    }
                });
            }
        }

        //Method to collect all combo box labels into a dictionary.
        private void collectComboBoxLabels()
        {
            //Create a generic variable (supports a simple iteration over a type of collection), is essentially a list. (var)
            //Grab the combo box labels found on this form, then check to see if any of their names start with the string: lbl_.
            var comboBoxLabels = this.Controls.OfType<Label>().Where(x => x.Name.StartsWith("lbl_"));

            //Sort the combo box labels by name (to set lbl_1 as first value)
            comboBoxLabels = comboBoxLabels.OrderBy(item => item.Name);

            //Create a dictionary to assign each combo box label in the formLabels list a number.
            formLabels = new Dictionary<int, Label>();

            //Iteration variable to assign each label an indexable value.
            int i = 0;

            //For each label in the var type list of combo box labels
            foreach (var label in comboBoxLabels)
            {

                //Add the artifical index and label, thus creating a Key | Value relationship.
                formLabels.Add(i, label);

                //Increment Iteration Variable 
                i++;
            }
        }

        //Method to populate combo boxes
        private void fillComboBoxes(List<Table_OBJ> tableObjects, Dictionary<int, ComboBox> formComboBoxes)
        {

            //If any of the combo boxes are not visible.
            if (formComboBoxes.Values.Any(x => x.Visible == false)) {

                //Enable combo box filters.
                enableFilters();
            }

            //If any of the combo boxes have selected values.
            if (formComboBoxes.Values.Any(x => x.SelectedIndex > -1)) {

                //Clear any selected values from filters.
                clearFilters();
            }

            //If the data grid view has selected cells, then clear the selection.
            if (dataGridView.SelectedCells.Count > 0) {

                //Prevent any values from being selected upon loading the data grid view.
                dataGridView.ClearSelection();
            }

            //Iteration variable to grab every table object's data table.
            int i = 0;

            //For each combo box in the dictionary
            foreach (ComboBox comboBox in formComboBoxes.Values)
            {

                //If the combo box is not empty.
                if (comboBox.Items.Count != 0)
                {
                    //Clear the combo box's items
                    comboBox.Items.Clear();
                }

                //For each data row in the table object's data table rows.
                foreach (DataRow row in tableObjects[i].dataTable.Rows)
                {

                    //Add each row from this table into this combo box.
                    comboBox.Items.Add(row.ItemArray[1]);

                    //Set this combo box's drop down height (use the font height of this value in the combo box * 8).
                    comboBox.DropDownHeight = comboBox.Font.Height * 8;

                }

                //Increase iteration variable to grab each table object's data table.
                i++;
            }
        }

        //Method to check if filters have values and interchange them with the generated sample. 
        private void checkFiltersAssignProperties(DataTable generatedSample)
        {

            //For each ID | combo box pair
            foreach (KeyValuePair<int, ComboBox> comboBoxObject in formComboBoxes)
            {
                //Check if the combo box has a selectedItem
                if (comboBoxObject.Value.SelectedIndex != -1)
                {
                    //Add to first row, at the combo box's index, the value of the combo box's selected item.
                    generatedSample.Rows[0][comboBoxObject.Key] = comboBoxObject.Value.SelectedItem;
                }
            }

            //Create a Generated Project object
            projectObject = new Generated_Project(tableObjects, generatedSample);

        }

        #region Filter Affector Methods

        //Method to reset all combo box filters.
        private void clearFilters()
        {

            //Create iteration variable to set all cell background colors to white.
            int i = 0;

            //If the data grid has rows.
            if (dataGridView.Rows.Count > 0)
            {               
                //To prevent any values from being selected upon loading the data grid view.
                dataGridView.ClearSelection();

                //Get the current cell count of the first row.
                int cellCount = dataGridView.Rows[0].Cells.Count;

                for (i = 0; i < cellCount; i++)
                {

                    //Set each cell's back ground color back to white.
                    dataGridView.Rows[0].Cells[i].Style.BackColor = Color.White;
                }
            }

            //After storing the values, clear the selected items.
            foreach (ComboBox cmbBox in formComboBoxes.Values)
            {
                //Clear combo box text
                cmbBox.ResetText();
            }
        }

        //Private method to enable all combo box filters
        private void enableFilters()
        {
            //Enable all combo box labels
            foreach (Label label in formLabels.Values)
            {
                if (!label.Enabled) {

                    label.Enabled = true;
                    label.Visible = true;
                }                
            }

            //Enable all combo box filters
            foreach (ComboBox comboBox in formComboBoxes.Values)
            {
                if (!comboBox.Visible) {

                    comboBox.Enabled = true;
                    comboBox.Visible = true;
                }
            }
        }

        //Private method to disable all combo box filters
        private void disableFilters()
        {
            foreach (Label label in formLabels.Values) {
                
                if (label.Enabled)
                {
                    label.Enabled = false;
                    label.Visible = false;
                }
            }

            //Enable all filters after data grid view has been populated.
            foreach (ComboBox comboBox in formComboBoxes.Values)
            {
                if (comboBox.Enabled) {

                    comboBox.Enabled = false;
                    comboBox.Visible = false;
                }
            }
        }

        #endregion

        #endregion

        //Private method to fire upon the data grid view's data source changing. 
        //Sets up filters for combo boxes upon execution of Open Button against the Generated Projects table.
        private void dataGridView_DataSourceChanged(object sender, EventArgs a)
        {
            //Ensure binding source is not null
            if (bindingSource.DataSource != null) {

                //Assign binding source the data source (updated data table) 
                bindingSource = dataGridView.DataSource as BindingSource;

                //Grab the data table from within the binding source.
                DataTable dataTable = (DataTable) bindingSource.DataSource;

                //If the data table is Generated Projects
                if (dataTable.TableName.Equals("Generated Projects"))
                {

                    //For each combo box
                    foreach (KeyValuePair<int, ComboBox> comboBox in formComboBoxes)
                    {

                        //Check that the combo box has a selected value
                        if (comboBox.Value.SelectedItem != null)
                        {

                            //Add another event handler on selected index changing that will order the Generated Projects table by the combo box value
                            comboBox.Value.SelectedIndexChanged += new EventHandler((object o, EventArgs e) =>
                            {

                                //NOT FUNCTIONING

                                int comboBoxIndex = comboBox.Key;

                                string comboBoxValue = comboBox.Value.SelectedItem.ToString();

                                var value = from column in dataTable.AsEnumerable()
                                            where column.Table.Columns.IndexOf(dataTable.Columns[comboBoxIndex].ColumnName) == comboBoxIndex
                                            orderby column.ItemArray[comboBoxIndex]
                                            select column;

                                //value.CopyToDataTable(dataTable, LoadOption.OverwriteChanges);

                                //dataTable.AcceptChanges();

                                //dataTable.DefaultView.ToTable();
                            });
                        }
                    }
                }
            } 
        }
    }
}