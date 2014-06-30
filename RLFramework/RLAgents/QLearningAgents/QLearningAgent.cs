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

    public class QLearningAgent<E, S, A>:IRLAgent<E,S,A>
        where S : IRLState
        where A : IRLAction, new()
        where E : IRLEnvironment<S, A>
    {
        QTable<S, A> table = new QTable<S,A>();

        public E Environment { get; set; }
        public S CurrentState { get; set; }
        public double RecentReward { get; protected set; }

        public int Id { get; set; }


        public QLearningAgent()
        {

        }


        public void Act()
        {
            //環境から行動の候補をもらう
            var candidateActions =
                Environment.GetCandidateActions(this, this.CurrentState);


            //行動の候補をもとにルールを取得
            var rules = table.GetCandidateRules(this.CurrentState, candidateActions);


            double T = 5;

            //価値によってルールを選ぶ
            var selectedRule = BoltzmanSelect(rules, T);


            //行動する。現状態と
            double reward;
            S nextState = Environment.ReceiveAction(this, this.CurrentState, selectedRule.Action, out reward);

            RecentReward = reward;

            //Q学習 
            //選ばれたルールのValue.Valを更新。
            double Q_sa = selectedRule.QValue;
            double alpha = 0.3;
            double gamma = 0.95;

            //次状態のルールを取得 :次状態と，次状態から得られる行動候補により選ぶ   
            //そのなかでもっともよいものを選択
            var nextRules = table.GetCandidateRules(nextState, Environment.GetCandidateActions(this, nextState));
            double maxActionValue = nextRules.Max((r)=>r.QValue);

            //Q学習で更新
            selectedRule.QValue = Q_sa + alpha * (reward + gamma * maxActionValue - Q_sa);

            this.CurrentState = nextState;

        }

        public static QRule<S, A> BoltzmanSelect(IEnumerable<QRule<S, A>> rules, double T)
        {
            var rand = MyRandom.RandomPool.Get("action");

            //合計値を計算
            double sum = 0;

            foreach (var rule in rules)
            {
                sum += expt(rule.QValue, T);

                if (Double.IsInfinity(sum))
                {
                    throw new Exception("ボルツマン分布：分母の和が無限になりました。Tを大きくしてください");
                }
            }


            //ルーレットの針
            double r = rand.NextDouble() * sum;


            double cum = 0; //累積値


            foreach (var rule in rules)
            {
                cum += expt(rule.QValue, T);//正規 累積値
                if (r < cum)
                {
                    return rule;
                }
            }

            throw new Exception("ルール選択がうまくいきませんでした。温度Tを調整するといいかもしれません。");
        }


        static double expt(double p, double T)
        {

            return Math.Exp(p / T);
        }


        public override string ToString()
        {
            return string.Format("state({0}), latest reward({1})", this.CurrentState.ToString(), RecentReward);
        }


        public void Show()
        {
            Console.WriteLine("agent" + Id);
            //current State

            Console.WriteLine(this.CurrentState);

            //actions
            var actions = this.Environment.GetCandidateActions(this, this.CurrentState);

            var rules = table.GetCandidateRules(CurrentState, actions);

            foreach (var rule in rules)
            {
                Console.WriteLine(rule);
            }

            Console.WriteLine("latest reward " + RecentReward);
        }

        public  void Init()
        {
            table = new QTable<S, A>();

            

            throw new NotImplementedException();
        }

    }
}
