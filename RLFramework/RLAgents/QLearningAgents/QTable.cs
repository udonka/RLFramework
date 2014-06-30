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
    public class QTable<S,A>
        where S: IRLState
        where A: IRLAction
    {
        HashSet<QRule<S, A>> hashset; //QRule は具象．ここから抽象依存を緩和

        public QTable()
        {
            hashset = new HashSet<QRule<S, A>>(new RuleEqualityComparer());

            


        }

        private class RuleEqualityComparer
             : IEqualityComparer<QRule<S,A>>
        {

            public bool Equals(QRule<S, A> rule1, QRule<S, A> rule2)
            {
                return rule1.State.Equals(rule2.State) && rule1.Action.Equals(rule2.Action);
            }

            public int GetHashCode(QRule<S, A> rule1)
            {
                return rule1.GetHashCode();
            }

        }

        public bool Contains(QRule<S,A> rule){
            return hashset.Contains(rule);
        }

        public bool Add(QRule<S, A> rule)
        {
            return hashset.Add(rule);
        }

        public IEnumerable<QRule<S, A>> GetCandidateRules(S state, IEnumerable<A> candActs)
        {
            if (candActs == null || candActs.Count() == 0)
            {
                throw new Exception("候補の行動がありません");
            }


            //ない場合は状態行動ルールの組み合わせを登録する。addの実装依存。あまりよくない
            foreach (var act in candActs)
            {
                QRule<S,A> rule = new QRule<S,A>(state, act);

                if (!this.Contains(rule))
                {
                    this.Add(rule);
                }
            }

            //candidate
            var rules = from rule in hashset
                        where rule.State.Equals(state)
                        where candActs.Contains(rule.Action)
                        select rule;


            //ルールが見つからないのはおかしい
            if (rules.Count() == 0)
            {
                throw new Exception("該当するルールがありません。");
            }

            return rules;
        }
    }
}
