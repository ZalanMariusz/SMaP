using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SMaP_APP.DAL
{
    class DictionaryDAL : GenericDAL<Dictionary>
    {
        public DictionaryTypeDAL DictionaryTypeDal { get; set; }
        public DictionaryDAL()
        {
            this.DictionaryTypeDal = new DictionaryTypeDAL();
        }

        internal List<DictionaryType> DictionaryTypeList()
        {
            return this.DictionaryTypeDal.FindAll().ToList();
        }
    }
}
