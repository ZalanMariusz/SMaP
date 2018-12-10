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
        private DictionaryTypeDAL DictionaryTypeDal { get; set; }
        public DictionaryDAL()
        {
            this.DictionaryTypeDal = new DictionaryTypeDAL();
        }

        public List<DictionaryType> DictionaryTypeList()
        {
            return this.DictionaryTypeDal.FindAll().ToList();
        }
        public List<Dictionary> DictionaryListByType(int typeID)
        {
            return FindAll(x => x.DictionaryTypeID == typeID);
        }
    }
}
