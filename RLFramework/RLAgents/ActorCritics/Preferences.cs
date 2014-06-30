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
    //パラメータ化された確率のパラメータ
    public class Preferences <S,A> :IEnumerable<KeyValuePair<StateAction<S,A>,double>>

        where S: IRLState
        where A: IRLAction
    {
        Dictionary<StateAction<S, A>, double> preferences;

        public Preferences()
        {
            preferences = new Dictionary<StateAction<S, A>, double>(new StateActioppnComparer());
            
        }

        //Add Rule を作りたい
        public void AddRule(StateAction<S, A> stateAction, double pre)
        {
            if (preferences.ContainsKey(stateAction))
            {
                preferences[stateAction] = pre;
            }
            else
            {
                preferences.Add(stateAction, pre);
            }
        }
         

        //Preferences 
        public double this[S state, A action]
        {
            get
            {
                var key = new StateAction<S,A>(state, action);

                //同じキーがあるか
                if(preferences.ContainsKey(key)){
                    return preferences[key];
                }

                //同キーがなかった
                else
                { 
                    //作ろう
                    AddRule(key, 0);
                    return preferences[key];
                }
            }

            set
            {
                var key = new StateAction<S, A>(state, action);
                if (preferences.ContainsKey(key))
                {
                    preferences[new StateAction<S, A>(state, action)] = value;
                }
                {
                    preferences[new StateAction<S, A>(state, action)] = value;
                }
            }
        }

        public IEnumerator<KeyValuePair<StateAction<S, A>, double>> GetEnumerator()
        {
            foreach (var pre in preferences)
            {
                yield return pre;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var pre in preferences)
            {
                yield return pre;
            }
        }


        private class StateActioppnComparer : IEqualityComparer<StateAction<S,A>>
        {
            public bool Equals(StateAction<S, A> sa1, StateAction<S, A> sa2)
            {
                return sa1.State.Equals(sa2.State) && sa1.Action.Equals(sa2.Action);
            }

            public int GetHashCode(StateAction<S,A> sa)
            {
                return sa.State.GetHashCode() + sa.Action.GetHashCode();
            }

        }

        internal void Show()
        {
            Console.WriteLine("preferences");

            int i = 0;
            foreach (var pref in preferences)
            {
                if (i++ < 10)
                {
                    Console.WriteLine(pref);
                }
                else
                {
                    return;
                }


            }
        }
    }

}
