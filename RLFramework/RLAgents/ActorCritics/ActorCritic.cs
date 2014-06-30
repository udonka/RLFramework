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
    public class ActorCritic<E, S, A>:IRLAgent<E,S,A>
        where S : IRLState
        where A : IRLAction, new()
        where E : IRLEnvironment<S, A>
    {
        double recentreward;

        public E Environment { get; set; }
        public S CurrentState { get; set; }

        public int Id { get; set; }
        public double RecentReward
        {
            get
            {
                List<int> a;
                
                return recentreward;
            }

            set
            {
                if (value != 0)
                {
                    Console.WriteLine("reward" + value);

                }

                recentreward = value;
            }
        }

        public ActorCritic()
        {

        }

        private Actor<S,A> actor = new Actor<S,A>();

        private Critic<S> critic = new Critic<S>();

        public void Act()
        {
            //環境から行動の候補をもらう
            var candidateActions =
                Environment.GetCandidateActions(this,this.CurrentState);

            //行動を方策にしたがって選択
            var action = actor.ActionSelect(this.CurrentState, candidateActions);

            //環境の情報
            double reward = 0; //報酬
            var nextState = Environment.ReceiveAction(this, this.CurrentState, action, out reward);
            RecentReward = reward;

            //TD誤差を計算
            double TDError = critic.CalcurateTDError(CurrentState, nextState, reward);

            //TD誤差によりアクターを更新
            actor.update(this.CurrentState,action,TDError);//状態と行動ぐらい覚えててもいいかもね

            //TD誤差によりクリティックを更新
            //double updated value = 
            critic.updateCritic(CurrentState, TDError);


            this.CurrentState = nextState;
        }


        internal void Show()
        {
            Console.WriteLine("actor"+Id);

            var possiblePreferences = from pre in actor.Preferences
                                      where pre.Key.State.Equals(this.CurrentState)
                                      select pre;

            foreach (var pre in possiblePreferences)
            {
                Console.WriteLine("({0},{1}) -> {2}", pre.Key.State, pre.Key.Action,pre.Value);
            }


            Console.WriteLine();
            Console.WriteLine("critic ");

            var values = from a in this.critic.StateValues
                        where a.Key.Equals(CurrentState)
                        select a.Value;

            foreach (var value in values)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine();
        }

        public void Init()
        {
            actor = new Actor<S, A>();
            critic = new Critic<S>();

        }
    }
}
