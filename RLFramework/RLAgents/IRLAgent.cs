using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.States;
using RLFramework.RLEnvirontments.Actions;

namespace RLFramework.RLAgents
{
    public interface IRLAgent<E,S,A>
        where S: IRLState
        where A: IRLAction,new()
        where E: IRLEnvironment<S,A>
    {
        E Environment { get;  }
        S CurrentState { get; set; }

        int Id{get;set;}
        double RecentReward { get; }

        void Act();
    }
}
