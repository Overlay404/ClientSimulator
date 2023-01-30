using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulator.Model
{
    partial class Realtor
    {
        
        
        public interface IDataErrorInfo
        {
            string Error { get; }
            string this[string columnName] { get; }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "DealShare":
                        if ((DealShare < 0) || (DealShare > 100))
                        {
                            error = "Процентная ставка должена быть больше 0 и меньше 100";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
