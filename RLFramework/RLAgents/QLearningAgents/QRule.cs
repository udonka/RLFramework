using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.States;
using RLFramework.RLEnvirontments.Actions;

namespace RLFramework.RLAgents.QLearningAgents
{
    //IRuleをクラス化しただけ．コンストラクタがあるよ．
    public class QRule<S,A>:IRule<S,A>, IEquatable<QRule<S,A>>
        where S:IRLState
        where A:IRLAction
    {
        public S State { get; set; }
        public A Action { get; set; }

        public double QValue { get; set; }

        public QRule(S state, A action)
        {
            this.State = state;
            this.Action = action;
            this.QValue = 0;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj is QRule<S, A>)
            {
                return this.Equals(obj as QRule<S, A>);
            }
            else
            {
                throw new Exception("QRule はQRuleとか比べられません");
            }
        }

        public bool Equals(QRule<S,A> rule)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (rule == null || GetType() != rule.GetType())
            {
                return false;
            }

            return this.State.Equals(State) && this.Action.Equals(rule.Action); 
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return State.GetHashCode() + Action.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[s:{0} a:{1} q:{2}]",State,Action,QValue);
        }
    }
}
