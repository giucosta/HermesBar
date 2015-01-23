using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesManagementAssistant.View.ViewModel
{
    public class FuncionarioViewModel
    {
        public string Nome
        {
            get { return Nome; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ApplicationException("Customer name is mandatory");
            }
        }
    }
}
