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
    public interface IRule<S,A>
        where S:IRLState
        where A:IRLAction
    {
        S State { get; set; }
        A Action { get; set; }

        double QValue { get; set; }

        //update ... each implementation
    }
}
