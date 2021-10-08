using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace SeaCodeLib.Common
{
    /// <summary>
    /// 多任务事务单元执行
    /// </summary>
    public class UnitOfWork
    {
        public static void Invoke(Action action)
        {
            TransactionScope transaction = null;
            try
            {
                transaction = new TransactionScope();
                action.Invoke();
                transaction.Complete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
