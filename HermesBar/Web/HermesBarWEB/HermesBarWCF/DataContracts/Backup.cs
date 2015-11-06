using BLL.Backup;
using HELPER;
using MODEL.Backup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Backup
    {
        private BackupBLL BackupBLL;
        public Backup()
        {
            this.BackupBLL = Singleton<BackupBLL>.Instance();
        }
        public List<BackupModel> Get()
        {
            return BackupBLL.Get();
        }
        public void Restore(string backupAddress)
        {
            BackupBLL.Restore(backupAddress);
        }
    }
}