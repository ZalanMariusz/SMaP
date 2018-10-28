using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.Model
{
    public interface IBaseModel
    {
        int ID { get; set; }
        bool Deleted { get; set; }
    }
}
