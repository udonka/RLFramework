using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.States;
using RLFramework.RLEnvirontments.Actions;

namespace RLFramework.RLAgents.ActorCritics
{
    public class Critic<S>
        where S: IRLState
    {
        StateValues<S> stateValue;

        double gamma = 0.8;
        double alpha = 0.05;

        public Critic()
        {
            stateValue = new StateValues<S>();
            

        }

        public StateValues<S> StateValues
        {
            get
            {
                return stateValue;
            }
        }


        //pure function
        public double CalcurateTDError(S currentState, S nextState, double reward)
        {
            double currentValue = this.StateValues[currentState];
            double nextValue = this.StateValues[nextState];

            var TDerror = reward + gamma * nextValue - currentValue;

            return TDerror;
        }

        public double updateCritic(S currentState, double td)
        {
            return this.stateValue[currentState] += alpha * td;

        }




        internal void Show()
        {
            Console.WriteLine("state values");

            StateValues.Show();
        }
    }
}
