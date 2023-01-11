using Blender_Model_Selector_Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blender_Model_Selector_Domain.Data_Layer
{
    internal class SQL_Manager
    {
        #region Class Members
        //Setup the connection string.
        private string connectionString = "Data Source=localhost; Initial Catalog=Model_Selector; Trusted_Connection=True;";

        //Setup the query string to be used by all SQL call functions.
        private string query = "";

        //Create confirmation boolean
        private int numRows;

        //Create SQL Objects to interact with the SQL Server.
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;

        #endregion

        #region Private Query Managers
        //Overloaded runQuery method that intakes just one query at a time.
        private DataTable runQuerySELECT(string query)
        {
            //Create a new data table for each executed query.
            DataTable dataTable = new DataTable();

            using (sqlConnection = new SqlConnection(connectionString)) {

                using (sqlCommand = sqlConnection.CreateCommand()) {

                    using (sqlDataAdapter = new SqlDataAdapter(sqlCommand)) {

                        try {

                            //Add query to sql command text
                            sqlCommand.CommandText = query;

                            //Open the connection
                            sqlConnection.Open();

                            //Fill the dataset using the data adapter.
                            sqlDataAdapter.Fill(dataTable);

                        } catch (SqlException e) {

                            MessageBox.Show($"Unable to fill data table: {dataTable}. Error at: {e.LineNumber}. Exception found: {e.Message}");
                        }
                    }
                }
            }

            //Return the filled dataset (containing a list of tables).
            return dataTable;
        }

        private int createGeneratedProject(Generated_Project projectObject, string query)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {

                using (sqlCommand = sqlConnection.CreateCommand())
                {
                    //Add associated FK IDs and values from the project Object as parameters for the sql command to be executed.
                    sqlCommand.Parameters.AddWithValue("@acID", projectObject.accentColorID);

                    sqlCommand.Parameters.AddWithValue("@wtID", projectObject.worldThemeID);

                    sqlCommand.Parameters.AddWithValue("@euID", projectObject.emotionalUndertoneID);

                    sqlCommand.Parameters.AddWithValue("@qID", projectObject.qualityID);

                    sqlCommand.Parameters.AddWithValue("@tID", projectObject.typeID);

                    sqlCommand.Parameters.AddWithValue("@asID", projectObject.artStyleID);

                    //Assign the query to the sqlCommand object's command text.
                    sqlCommand.CommandText = query;

                    //Open SQL Connection
                    sqlConnection.Open();

                    //Execute the UPDATE Sql command, return number of rows affected. 
                    //Execute Non Query -> Means we execute a query by which we are not expecting any data in return.
                    numRows = sqlCommand.ExecuteNonQuery();
                }
            }

            //Return the filled dataset (containing a list of tables).
            return numRows;
        }

        //Overloaded runQuery method that takes in a list of sql commands and returns the number of effected rows.
        private int runQueryUPDATE(List<SqlCommand> updateQueries)
        {
            //Reset numRows
            numRows = 0;

            //For each sql command within the list of sql commands
            foreach (SqlCommand sqlCommand in updateQueries) {

                //Create an sqlConnection using the private connection string.
                sqlConnection = new SqlConnection(connectionString);

                //Assign sql connection to sql command's connection property.
                sqlCommand.Connection = sqlConnection;

                //Using this command's connection 
                using (sqlCommand.Connection) {

                    //Using this sql command
                    using (sqlCommand) {

                        //If the sql command connection state is closed
                        if (sqlCommand.Connection.State == ConnectionState.Closed)
                        {

                            //Open sql command's connection.
                            sqlCommand.Connection.Open();
                        }

                        //Execute an update to the database, return number of rows affected, and increase the variable per each iteration where a row was affected.
                        numRows += sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            //Return the filled dataset (containing a list of tables).
            return numRows;
        }

        //Overloaded runQuery method that takes in a single table object and updates the corresponding data table in SQL. 
        private int runQueryUPDATE(Table_OBJ updatedTable)
        {

            //Using sql connection
            using (sqlConnection = new SqlConnection(connectionString)) {

                //Create query string from incoming table with updates.
                query = $"SELECT * FROM {updatedTable.sqlTableName}";

                //Using sql command, provide query as command text and sql connection object.
                using (sqlCommand = new SqlCommand(query, sqlConnection)) {

                    //Using sql data adapter
                    using (sqlDataAdapter = new SqlDataAdapter(sqlCommand.CommandText, sqlConnection))
                    {
                        //Create new dataset to hold table with updates.
                        DataSet dataSet = new DataSet();

                        //Create new sql command builder object to create update command; pass sql data adapter.
                        SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                        //Add updatedTable to the data set.
                        dataSet.Tables.Add(updatedTable.dataTable);

                        //Set data set's table name to the ui table name.
                        dataSet.Tables[0].TableName = updatedTable.uiTableName;

                        //Create mapping between the dataset's user friendly table name and the sql table name within the database. 
                        sqlDataAdapter.TableMappings.Add($"{updatedTable.sqlTableName}", $"{dataSet.Tables[0].TableName}");

                        //Open the sql connection
                        sqlConnection.Open();

                        //Update database table (selectedTable.sqlTableName) using the selectedTable as reference for what changes were made.
                        numRows = sqlDataAdapter.Update(dataSet, $"{updatedTable.sqlTableName}");

                    }
                }                
            } 

            //Return number of rows affected.
            return numRows;
        }

        //Overloaded runQuery method that takes in a single data table and updates the corresponding data table in SQL. 
        private int runQueryDelete(DataTable selectedTable)
        {

            //Using sql connection
            using (sqlConnection = new SqlConnection(connectionString))
            {

                //Create query string from incoming table with updates.
                query = $"SELECT * FROM {selectedTable.TableName}";

                //Using sql command, provide query as command text and sql connection object.
                using (sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    //Using sql data adapter
                    using (sqlDataAdapter = new SqlDataAdapter(sqlCommand.CommandText, sqlConnection))
                    {
                        //Create new dataset to hold table with updates.
                        DataSet dataSet = new DataSet();

                        //Create new sql command builder object to create update command; pass sql data adapter.
                        SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                        //Add updatedTable to the data set.
                        dataSet.Tables.Add(selectedTable);

                        //Set data set's table name to the ui table name.
                        dataSet.Tables[0].TableName = selectedTable.TableName;

                        //Create mapping between the dataset's user friendly table name and the sql table name within the database. 
                        sqlDataAdapter.TableMappings.Add($"{selectedTable.TableName}", $"{dataSet.Tables[0].TableName}");

                        //Open the sql connection
                        sqlConnection.Open();

                        //Update database table (selectedTable.sqlTableName) using the selectedTable as reference for what changes were made.
                        numRows = sqlDataAdapter.Update(dataSet, $"{selectedTable.TableName}");

                    }
                }
            }

            return numRows;
        }

        #endregion

        #region Functional Methods
        //Overloaded method getTableValues (Output: The Generated Projects table)
        public DataTable getGeneratedProjects()
        {
            //Create query string to pull necessary values from Generated Projects
            query = "SELECT " +
                "accentColor.Name AS 'Accent Color', " +
                "artStyle.Name AS 'Art Style', " +
                "emotionalUndertone.Name AS 'Emotional Undertone', " +
                "quality.Name AS 'Quality', " +
                "type.Name AS 'Type', " +
                "worldTheme.Name AS 'World Theme' " +

                "FROM dbo.Generated_Projects as generatedProjects " +

                "LEFT JOIN dbo.Accent_Color as accentColor " +
                "ON accentColor.ID = generatedProjects.FK_Accent_Color_ID " +

                "LEFT JOIN dbo.Art_Style as artStyle " +
                "ON artStyle.ID = generatedProjects.FK_Art_Style_ID " +

                "LEFT JOIN dbo.Emotional_Undertone as emotionalUndertone " +
                "ON emotionalUndertone.ID = generatedProjects.FK_Emotional_Undertone_ID " +

                "LEFT JOIN dbo.Quality as quality " +
                "ON quality.ID = generatedProjects.FK_Quality_ID " +

                "LEFT JOIN dbo.Type as type " +
                "ON type.ID = generatedProjects.FK_Type_ID " +

                "LEFT JOIN dbo.World_Theme as worldTheme " +
                "ON worldTheme.ID = generatedProjects.FK_World_Theme_ID";

            //Call to the sqlManager overload method runQuery which takes in a single string and outputs a dataset of one
            DataTable dataTable = runQuerySELECT(query);

            //Return data table.
            return dataTable;

        }

        //Method to store generated sample into SQL Table.
        public int storeProject(List<Table_OBJ> tableObjects, Generated_Project projectObject)
        {
            //Create INSERT query
            query = "INSERT INTO dbo.Generated_Projects (" +
                "FK_Accent_Color_ID, " +
                "FK_Art_Style_ID, " +
                "FK_Emotional_Undertone_ID, " +
                "FK_Quality_ID, " +
                "FK_Type_ID, " +
                "FK_World_Theme_ID)" +

                "VALUES (" +

                "   (SELECT ID FROM dbo.Accent_Color WHERE ID = @acID)," +
                "   (SELECT ID FROM dbo.Art_Style WHERE ID = @asID)," +
                "   (SELECT ID FROM dbo.Emotional_Undertone WHERE ID = @euID)," +
                "   (SELECT ID FROM dbo.Quality WHERE ID = @qID)," +
                "   (SELECT ID FROM dbo.Type WHERE ID = @tID)," +
                "   (SELECT ID FROM dbo.World_Theme WHERE ID = @wtID)" +

                ");";

            //Execute update query, return number of rows.
            numRows = createGeneratedProject(projectObject, query);

            //If numRows equals 1, i.e., if there was one row affected.
            if (numRows == 1)
            {

                //Update data grid table values TimesGenerated, return number of rows affected.
                numRows = updateTimesGenerated(tableObjects, projectObject);

            }

            //Return bool value back for confirmation.
            return numRows;
        }

        //Private method called within Store Project that finds each table and increments the TimesGenerated Value for each int value found in the project object properties.
        private int updateTimesGenerated(List<Table_OBJ> tableObjects, Generated_Project projectObject)
        {
            //Create list of strings to hold the names of each project object property
            List<int> projectObjectValues = new List<int>();

            //For each property found on the project object
            foreach (PropertyInfo propertyInfo in projectObject.GetType().GetProperties())
            {
                //Grab each property value, and place them into a list. This will allow them to be added iteratively to the UPDATE statement below.
                projectObjectValues.Add((int) propertyInfo.GetValue(projectObject, null));

            }

            //Create a list of sql commands to be executed.
            List<SqlCommand> updateQueries = new List<SqlCommand>();

            //Create string builder object.
            StringBuilder stringBuilder = null;

            //Increment variable //Start at -1 to grab each index in project object value list.
            int i = -1;

            //For each table in the list of tables, construct the update query to increase the times generated column value per associated value within the sample.
            foreach (Table_OBJ table in tableObjects)
            {
                //Increment to grab every table.
                i++;

                //Instantiate new string builder object.
                stringBuilder = new StringBuilder();

                //Create sql command to hold query attributes, e.g., text, etc.
                SqlCommand cmd = new SqlCommand();

                //Construct query string using each table's name, and each iteration of the project object int value.
                stringBuilder.Append($"UPDATE {table.sqlTableName} SET TimesGenerated = TimesGenerated + 1 FROM {table.sqlTableName} WHERE ID = {projectObjectValues[i]}");

                //Add string builder query to command text.
                cmd.CommandText = stringBuilder.ToString();

                //Add sql command to list of commands
                updateQueries.Add(cmd);
            }

            //Call to UPDATE SQL query method
            numRows = runQueryUPDATE(updateQueries);

            return numRows;
        }

        //Method to populate all table objects required for the data grid view, combo boxes, and other functionality requiring the table and its associated data.
        public List<Table_OBJ> getCoreTableObjects()
        {
            //Use query string to grab all table names except generated projects.
            query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'Model_Selector' AND TABLE_NAME != 'Generated_Projects' ORDER BY TABLE_NAME;";

            //Call to private method to retrieve all database sql table names, return them all into a single data table.
            DataTable dataTable = runQuerySELECT(query);

           //Create list to store each table as a table object.
            List<Table_OBJ> tableObjects = new List<Table_OBJ>();

            //For each row in the data table
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //For each row, Create a new table display object
                Table_OBJ tableDisplay = new Table_OBJ();

                //For each object, assign the curret index's row, but as a string.
                tableDisplay.sqlTableName = dataRow[0].ToString();

                //Add the table name to each data table's table name property //For readability.
                tableDisplay.dataTable.TableName = dataRow[0].ToString();

                //Create query string, pass sql table name from table display object
                query = $"SELECT * FROM {tableDisplay.sqlTableName}";

                //Assign each returning table to a table display object's data table.
                tableDisplay.dataTable = runQuerySELECT(query);

                //Add each populated table display into the list of table displays.
                tableObjects.Add(tableDisplay);
            }

            //Return the list of table objects.
            return tableObjects;
        }

        //Method to take the currently selected table, its updated values, and update the corresponding table in SQL.
        public int updateTable(Object selectedTable)
        {

            if (selectedTable.GetType() == typeof(Table_OBJ))
            {
                //Execute update query, return number of rows.
                numRows = runQueryUPDATE((Table_OBJ)selectedTable);
            }
            else {

                //Execute update query, return number of rows.
                numRows = runQueryDelete((DataTable)selectedTable);
            }
            MessageBox.Show(selectedTable.GetType().ToString());


            //Return number of rows affected.
            return numRows;
        }

        #endregion
    }
}
