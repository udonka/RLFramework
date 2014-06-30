using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFramework.RLEnvirontments.ExampleProblem
{
    using RLFramework.RLEnvirontments.Actions;

    public class FiveDirectionAction :MoveAction
    {

        public FiveDirectionAction()
        {
        }

        public FiveDirectionAction(int dx, int dy)
            :base(dx,dy)
        {
        }

        public override ISet<IRLAction> CandidateActions()//自分と同族の行動のリストを返す
        {
            return new HashSet<IRLAction>{
                new FiveDirectionAction(1,0),new FiveDirectionAction(0,1),
                new FiveDirectionAction(-1,0),new FourDirectionAction(0,-1),
                new FiveDirectionAction(0,0)};
        }
    }
}
