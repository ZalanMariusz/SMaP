using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.ViewModel
{
    class StudentImportViewModel : BaseViewModel<Student>
    {
        public DataTable Table { get; set; }
        
        public StudentImportViewModel(DataTable dt,StudentImportWindow source)
        {
            this.Table = dt;
            this.SourceWindow = source;
        }
    }
}
