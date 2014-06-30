using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.States;
using RLFramework.RLEnvirontments.Actions;

namespace RLFramework.RLAgents.ActorCritics
{
    public class StateValues<S> :IEnumerable<KeyValuePair<S,double>>
        where S: IRLState
    {
        Dictionary<S, double> vals;

        public StateValues()
        { 
            vals = new Dictionary<S, double>(new StateComparer<S>());

        }

        public double this[S state]
        {
            get
            {
                if (vals.ContainsKey(state))
                {
                    return vals[state];
                }
                else
                {
                    vals[state] = 0;
                    return vals[state];
                }
            }
            set
            {
                vals[state] = value;
            }
        }

        public IEnumerator<KeyValuePair<S, double>> GetEnumerator()
        {
            foreach(var val in vals){
                yield return val;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach(var val in vals){
                yield return val;
            }
        }


        private class StateComparer<S0> : IEqualityComparer<S0>
            where S0 : IRLState
        {
            public bool Equals(S0 s1, S0 s2)
            {
                //状態はEqualsを入れてるはず
                return s1.Equals(s2);
            }

            public int GetHashCode(S0 s1)
            {
                return s1.GetHashCode();
            }
        }

        internal void Show()
        {

            Console.WriteLine();

            int i = 0;
            foreach(var val in vals){
                if (i++ < 10)
                {
                    Console.WriteLine(val);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
