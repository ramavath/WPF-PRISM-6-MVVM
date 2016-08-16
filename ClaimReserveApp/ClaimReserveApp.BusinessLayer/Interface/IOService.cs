using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserveApp.BusinessLayer.Interface
{
    public interface IOService
    {
        string OpenFileDialog();

        Stream OpenFile(string path);
    }
}
