using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.States;
using RLFramework.RLEnvirontments.Actions;

using MyRandom;

namespace RLFramework.RLAgents
{
    //クラス版
    public class RandomActionAgent<E,S,A> : IRLAgent<E,S,A>
        where S: IRLState
        where A: IRLAction,new()
        where E:IRLEnvironment<S,A>
    {
        public E Environment { get; set; }

        public S CurrentState { get; set; }


        public int Id { get; set; }
        public double RecentReward { get; protected set; }

        public void Act()
        {
            var e = this.Environment;
            ISet<A> candAct = e.GetCandidateActions(this,this.CurrentState);

            //generate candidates of actions
            //ISet<IRLAction> candAct = new A().CandidateActions(); //もっといい方法ないかなあ

            //Action Select
            int r = MyRandom.RandomPool.Get("action").Next(candAct.Count);
            var a = candAct.ElementAt(r);


            //Send Action to environtment
            double reward;//ignore
            S nextState = e.ReceiveAction(this, this.CurrentState, a ,out reward);
            RecentReward = reward;

            double x = (double)reward;
            double y = reward ;
            double z = x + y;
            //move State
            this.CurrentState = nextState;

        }

        public override string ToString()
        {
            return string.Format("state({0}), latest reward({1})",this.CurrentState.ToString(), RecentReward);
        }
    }
}
