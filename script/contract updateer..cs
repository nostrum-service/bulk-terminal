using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Xpo;
using DevExpress.Mvvm;
using bbt.asup.DataModel;
using Nostrum.Framework;
using Nostrum.Domain;

NLog.LogManager.GetCurrentClassLogger().Info(string.Format("{0:dd.MM.yy}", DateTime.Now));

Session session = new Session();
var src = session.Query<Contract>().ToArray();

foreach (var li in src)
{
    NLog.LogManager.GetCurrentClassLogger().Info(string.Format("{0} {1:dd.MM.yy} {2}", li.ObjectCode, li.DocumentDate, li.ContractType.Name));
    
    
    if (li.FirstParty == null && li.OppositeParty == null)
    {
        li.FirstParty = li.Participants.Where(c => c.AbstractRole.RefName == "apr:p01").Select(c => c.Participant).FirstOrDefault();
        li.OppositeParty = li.Participants.Where(c => c.AbstractRole.RefName == "apr:p02").Select(c => c.Participant).FirstOrDefault();

        if (li.FirstParty != null && li.OppositeParty != null)
            li.Save();
    }

}
