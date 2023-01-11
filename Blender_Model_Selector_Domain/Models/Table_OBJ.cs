using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Model_Selector_Domain.Models
{
    public class Table_OBJ : IEnumerable<Table_OBJ>
    {
        public string uiTableName { get; set; }
        public string sqlTableName { get; set; }

        //Create data table to hold each table's sql table data.
        public DataTable dataTable = new DataTable();

        //These are members of the IEnumerable interface that are required to be overwriten upon implementation. They do nothing as they do not contain any code within their method scope.
        public IEnumerator<Table_OBJ> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
