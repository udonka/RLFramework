using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFramework.RLEnvirontments.Actions
{
    public interface IRLAction : IEquatable<IRLAction>
    {
        ISet<IRLAction> CandidateActions();//自分と同族の行動のリストを返す

    }
}
