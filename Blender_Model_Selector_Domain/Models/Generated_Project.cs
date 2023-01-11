using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blender_Model_Selector_Domain.Models
{
    public class Generated_Project
    {

        public Generated_Project(List<Table_OBJ> tableObjects, DataTable generatedSample)
        {
            
            //Call to private method to iterate through generated sample and data tables to return a list of each IDs per each column in the data table.
            List<int> idsFound = populateProperties(tableObjects, generatedSample);

            //Assign IDs from IDs found throughout the table objects.
            //NOTE: Must match property order below.
            accentColorID = idsFound[0];

            artStyleID = idsFound[1];

            emotionalUndertoneID = idsFound[2];

            qualityID = idsFound[3];

            typeID = idsFound[4];

            worldThemeID = idsFound[5];
        }

        //Private method to grab all necessary data to populate the generated project object's properties. 
        private List<int> populateProperties(List<Table_OBJ> tableObjects, DataTable generatedSample)
        {
            //Iteration value to increment to grab each table in the data set
            int i = 0;

            //Create list to store all IDs associated with the values in the generated sample
            List<int> idsFound = new List<int>();

            //For each value in the generated sample
            foreach (string item in generatedSample.Rows[0].ItemArray)
            {
                //For each data row in each table's rows
                foreach (DataRow dataRow in tableObjects[i].dataTable.Rows)
                {
                    //If the name equals the item's value
                    if (dataRow.ItemArray[1].Equals(item))
                    {
                        //Add that ID to a list of IDs to populate the Generated Project object's properties.
                        idsFound.Add((int)dataRow[0]);
                    }
                }

                //Increase iteration value.
                i++;
            }

            return idsFound;
        }

        //NOTE: MUST MATCH getCoreTableObjects query return (table order).
        public int accentColorID { get; set; }
        public int artStyleID { get; set; }
        public int emotionalUndertoneID { get; set; }
        public int qualityID { get; set; }
        public int typeID { get; set; }
        public int worldThemeID { get; set; }
    }
}
