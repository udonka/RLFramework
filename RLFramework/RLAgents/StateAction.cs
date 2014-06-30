using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RLFramework.RLAgents
{
    using RLEnvirontments.Actions;
    using RLEnvirontments.States;

    public class StateAction<S,A>:IEquatable<StateAction<S,A>>
        where S: IRLState
        where A: IRLAction
    {

        public S State { get; set; }
        public A Action { get;set; }



        public StateAction(S s, A a)
        {
            State = s;
            Action = a;
        }



        // override object.Equals
        public override bool Equals(object obj)
        {
            return this.Equals(obj as StateAction<S, A>);
        }

        public bool Equals(StateAction<S,A> obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return State.Equals(obj.State) && Action.Equals(obj.State);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return State.GetHashCode() + Action.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("SA({0}, {1})", State, Action);
        }
    }
}
