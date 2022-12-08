using Connection.Model;
using System;
using System.Reflection;

namespace BaseLibrary.BaseRepository
{
    public abstract class BaseRepository
    {
        public object Execute(string pFuncName, object[] pClientParam)
        {
            MResult result = new MResult();

            try
            {
                MethodInfo[] methods = GetType().GetMethods();
                MethodInfo method = GetType().GetMethod(pFuncName);
                if (method == null)
                {
                    result.rettype = 1;
                    result.retmsg = GetType().ToString() + ".Execute: " + pFuncName + " аргачлал олдсонгүй!";
                }
                else
                {
                    result = (MResult)method.Invoke(this, new object[] { pClientParam });
                }
            }
            catch (Exception ex)
            {
                result.rettype = 1;
                result.retmsg = this.GetType().ToString() + ".Execute: " + ex.Message;
            }
            return result;
        }
    }
}