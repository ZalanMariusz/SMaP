using System;
using System.Collections.Generic;
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
