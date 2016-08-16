using Microsoft.Win32;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.BusinessLayer.Interface;

namespace ClaimReserveApp.BusinessLayer
{
    public class FileDialogService : IOService
    {
        private ILoggerFacade logger;
        public FileDialogService(ILoggerFacade logger)
        {
            this.logger = logger;
        }
        public Stream OpenFile(string path)
        {
            throw new NotImplementedException();
        }

        public string OpenFileDialog()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == true)
                    return openFileDialog.FileName;
                else return "";
            }
            catch (Exception ex)
            {

                //logger
                logger.Log(ex.ToString(), Category.Exception, Priority.High);
                return "";
            }
           

        }
    }
}
