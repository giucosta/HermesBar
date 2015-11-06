using MODEL.Backup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IBackup
    {
        List<BackupModel> Get();
        void Restore(string backupAddress);
    }
}