using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.States;
using RLFramework.RLEnvirontments.Actions;

namespace RLFramework.RLAgents.ActorCritics
{

    //π(S,A)
    public class Actor<S,A>
        where S: IRLState
        where A: IRLAction , new()
    {
        Preferences<S,A> preferences;

        double alpha = 0.1;

        public Actor()
        {
            preferences = new Preferences<S,A>();
        }

        public Preferences<S, A> Preferences
        {
            get
            {
                return preferences;
            }
        }


        double T = 5;

        //Policy
        /* */
        public double GetPolicy(S state, A action){ //[0,1] :probability
            //PI(s,a) = Pr{a_t = a | s_t = s} = (exp(p(s,a))/(Σexp(p(s,a)))
            //分子を求める
            double si = Math.Exp(this.preferences[state,action])/T;

            var candidate = new A().CandidateActions();
            //分母を求める
            double bo = 0;
            foreach (A candidateAction in candidate)
            {
                bo += Math.Exp(this.Preferences[state,candidateAction])/T;
            }

            return si/bo;
        }
        //*/


        public A ActionSelect(S state, IEnumerable<A> candidateActions){ //[0,1] :probability
            //PI(s,a) = Pr{a_t = a | s_t = s} = (exp(p(s,a))/(Σexp(p(s,a)))

            //合計を求める
            double sum = 0;

            foreach (var action in candidateActions)
            {
                sum += Math.Exp(this.Preferences[state, action])/T;
            }
            //ここが無限になってしまうとこまる．



            //ランダムの針
            double r = MyRandom.RandomPool.Get("action").NextDouble() * sum;

            double cum = 0;

            foreach (var action in candidateActions)
            {
                cum += Math.Exp(this.Preferences[state, action])/T;

                if (r < cum)
                {
                    return action;
                }
            }
            throw new Exception("何かがおかしい");
        }


        internal void update(S currentState, A action, double TDError)
        {
            this.Preferences[currentState, action] += 
                alpha * TDError * (1 - this.GetPolicy(currentState, action));
        }

        internal void Show()
        {
            preferences.Show();
        }
    }
}
