using Blender_Model_Selector_Domain.Data_Layer;
using Blender_Model_Selector_Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Blender_Model_Selector_Domain.Managers
{
    public class TableManager
    {
        //Create SQL Manager class to use throughout.
        private SQL_Manager sqlManager = new SQL_Manager();

        //Method to get all values of a single table. 
        public DataTable getGeneratedProjects()
        {
            //Call to sqlManager instantiated object.
            sqlManager = new SQL_Manager();

            //Get the generated projects table, have it return a filled data table.
            DataTable dataTable = sqlManager.getGeneratedProjects();

            //Return datatable
            return dataTable;
        }

        //Method to get all available table names.
        public List<Table_OBJ> getCoreTableObjects()
        {
            //Call to the DataLayer Namespace, then to the SQL_Manager class, then to the GetTableNames method and return a List of strings.
            List<Table_OBJ> tables = sqlManager.getCoreTableObjects();

            //For each list item
            for (int i = 0; i < tables.Count; i++)
            {
                //For each list item, assign its uiTableName to the sqlTableName without the underscore.
                tables[i].uiTableName = tables[i].sqlTableName.Replace("_", " ");

                //Set Table name of table object's data table
                tables[i].dataTable.TableName = tables[i].sqlTableName;
            }

            //Return the list of tables to be populated into list box.
            return tables;
        }

        //Method to store data table's values into SQL Table
        public int storeProject(List<Table_OBJ> tableList, Generated_Project projectObject)
        {
            //Call to SQL Manager class' method, store project, pass the sample.
            int numRows = sqlManager.storeProject(tableList, projectObject);

            //Return number of rows affected
            return numRows;
        }

        //Method to save changes within the selected table to the corresponding table in SQL.
        public int saveChanges(Object selectedTable)
        {

            //Call to the SQL Manager class method, save changes, pass the table display, and return number of rows affected.
            int numRows = sqlManager.updateTable(selectedTable);

            //Return number of rows affected
            return numRows;
        }
    }
}