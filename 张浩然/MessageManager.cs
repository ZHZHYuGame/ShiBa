using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 月考一服务器
{
    internal class MessageManager<T> : Singleton<MessageManager<T>>
    {
        Dictionary<int, Action<T>> MesDic = new Dictionary<int, Action<T>>();
        public void AddListen(int id, Action<T> action)
        {
            if (MesDic.ContainsKey(id))
            {
                MesDic[id] += action;
            }
            else
            {

                MesDic.Add(id, action);
            }
        }
        public void OnBoardCast(int id, T t)
        {
            if (MesDic.ContainsKey(id))
            {
                MesDic[id](t);
            }
        }
    }
}
