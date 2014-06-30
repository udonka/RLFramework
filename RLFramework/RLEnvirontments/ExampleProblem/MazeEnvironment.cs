using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLFramework.RLAgents;

namespace RLFramework.RLEnvirontments.ExampleProblem
{

    using A = IRLAgent<MazeEnvironment, PositionState, MoveAction>;

    public class MazeEnvironment : IRLEnvironment<PositionState, MoveAction>
    {
        MazeMap map = new MazeMap();


        //所属するエージェント一覧
        //注意！！相互参照

        List<A> agents
             = new List<A>();

        public MazeMap Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }

        public IEnumerable<A> Agents
        {
            get
            {
                return agents;
            }
        }

        public ISet<MoveAction> GetCandidateActions(object sender, PositionState CurrentStae)
        {

            A agent = sender as A;

            HashSet<MoveAction> rets = new HashSet<MoveAction>();

            var cands = (new FourDirectionAction().CandidateActions());

            foreach (var candAct in cands)
            {

                MoveAction candFourAct = candAct as MoveAction;
                PositionState p = new PositionState(agent.CurrentState);
                p.X += candFourAct.Dx;
                p.Y += candFourAct.Dy;

                if (map.CanGo(p))
                {
                    rets.Add(candFourAct);
                }
            }
            return rets;
        }


        public void AddAgent(A agent)
        {
            agents.Add(agent);
        }

        public PositionState ReceiveAction(object sender, PositionState curretState, MoveAction action, out double reward)
        {
            var agent = sender as A;

            if (!agents.Contains(agent))
            {
                throw new Exception(string.Format("{0}は登録されていません", agent));
            }


            if (agent.CurrentState.Equals(this.GoalState))
            {
                reward = 0;
                return StartState;
            }

            PositionState state = new PositionState(agent.CurrentState);

            state.X += action.Dx;
            state.Y += action.Dy;

            if (!map.CanGo(state)) //行けない場所
            {
                reward = -30;
                return agent.CurrentState;
            }
            else
            {
                if (map[state] == 3) //崖
                {
                    reward = -10;
                    return state;
                }
                else if (map[state] == 2) //Goal
                {
                    reward = 10;
                    return state;
                }
                else //ふつうのところ
                {
                    //他のエージェントがいないかチェック
                    foreach (var a in Agents)
                        if (a != sender)
                        {
                            if (a.CurrentState.Equals(state))//!!ぶつかった
                            {
                                reward = -10;
                                return agent.CurrentState;//移動できない
                            }
                        }
                    reward = 0;
                    return state;
                }
            }
        }

        private PositionState startState = null;

        public PositionState StartState
        {
            get
            {
                if (startState == null)
                {
                    for (int y = 0; y < map.Height; y++)
                    {
                        for (int x = 0; x < map.Width; x++)
                        {
                            var state = new PositionState(x, y);

                            if (map.isStart(state))
                            {
                                startState = state;
                                return state;
                            }
                        }
                    }
                    throw new Exception("Start地点がありません");
                }
                else
                {
                    return startState;
                }
            }
        }

        private PositionState goalState = null;

        public PositionState GoalState
        {
            get
            {
                if (goalState == null)
                {
                    for (int y = 0; y < map.Height; y++)
                    {
                        for (int x = 0; x < map.Width; x++)
                        {
                            var state = new PositionState(x, y);

                            if (map.isGoal(state))
                            {
                                goalState = state;
                                return state;
                            }
                        }
                    }
                    throw new Exception("goal地点がありません");
                }
                else
                {
                    return goalState;
                }
            }
        }
    }
}
