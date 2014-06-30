using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFramework.RLEnvirontments
{
    using RLAgents;
    using States;
    using Actions;

    public interface IRLEnvironment<S, A>
        where S : IRLState   //状態制約
        where A: IRLAction,new() //行動制約
	{
        //なぜsenderがobjectかというと，エージェントにしようとするといろいろややこしいから．
        //特殊化した時にいろいろ入れる役割でもいい．
        S ReceiveAction(object sender,S state, A action, out double reward);

        ISet<A> GetCandidateActions(object sender, S CurrentState);

    }
}
