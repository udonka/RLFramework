using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RLFramework.RLEnvirontments.ExampleProblem
{

    using RLAgents;

    using A = RLAgents.IRLAgent<LocalViewMazeEnvironment, LocalViewState, MoveAction>;

    /**/
    public class LocalViewMazeEnvironment : IRLEnvironment<LocalViewState, MoveAction>

    {
        MazeMap map = new MazeMap();

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
        
        //所属するエージェント一覧
        //注意！！相互参照

        Dictionary<A, PositionState> agentsPosition = new Dictionary<A, PositionState>();

        public IEnumerable<A> Agents
        {
            get
            {
                foreach (var pair in agentsPosition)
                {
                    yield return pair.Key;
                }
            }
        }

        public Dictionary<A,PositionState> AgentsPosition
        {
            get
            {
                return agentsPosition;
            }
        }

        //コンストラクタ
        public LocalViewMazeEnvironment()
        {

        }

        public ISet<MoveAction> GetCandidateActions(object sender, LocalViewState CurrentState)
        {
            A agent = sender as A;

            var pos = this.agentsPosition[agent];

            HashSet<MoveAction> rets = new HashSet<MoveAction>();

            var cands = (new FiveDirectionAction().CandidateActions());

            foreach(var candAct in cands){

                MoveAction candFourAct = candAct as MoveAction;
                PositionState p = new PositionState(pos);

                p.X += candFourAct.Dx;
                p.Y += candFourAct.Dy;

                if (map.CanGo(p))
                {
                    rets.Add(candFourAct);
                }
            }
            return rets;
        }

        public void AddAgent(A agent){

            agentsPosition.Add(agent,this.StartPosition);

        }

        public LocalViewState ReceiveAction(object sender, LocalViewState currentState, MoveAction action, out double reward)
        {
            //currentState使ってない
            var agent = sender as A;

            if (!Agents.Contains(agent))
            {
                throw new Exception(string.Format("{0}は登録されていません",agent));
            }


            if (agentsPosition[agent].Equals(this.GoalState))
            {
                reward = 0;
                agentsPosition[agent] = this.StartPosition;
                return translateState(this.StartPosition,agent);
            }

            PositionState prevPos = this.agentsPosition[agent];

            PositionState nextPos = new PositionState(prevPos.X + action.Dx, prevPos.Y + action.Dy);

            if (!map.CanGo(nextPos)) //行けない場所
            {
                reward = -300;
                agentsPosition[agent] = nextPos;
                return translateState(nextPos,agent);
            }

            if (map[nextPos] == 3) //崖
            {
                reward = -100;
                agentsPosition[agent] = nextPos;
                return translateState(nextPos,agent);
            }

            if (map[nextPos] == 2) //Goal
            {
                reward = 100;
                agentsPosition[agent] = nextPos;
                return translateState(nextPos,agent);
            }

            //他のエージェントがいないかチェック
            foreach (var a in Agents)
            if(a != sender)
            {
                if (agentsPosition[a].Equals(nextPos))//!!ぶつかった
                {
                    reward = -100;
                    agentsPosition[agent] = prevPos;
                    return translateState(prevPos,agent);//移動できない
                }
            }

            reward = 0;
            agentsPosition[agent] = nextPos;
            return translateState(nextPos,agent);
        }


        private LocalViewState translateState(PositionState posState, A me)
        {
            int[] a = new int[9];

            int k = 0;

            //自分の場所を中心に視野を見渡す
            for (int y = -1; y <= 1; y++)
			{
                for (int x = -1; x <= 1; x++){
                    var pos = new PositionState(posState.X + x ,posState.Y + y );

                    if(!map.CanGo(pos)){ //行けない場所なら壁を見せる
                        a[k] = -1;
                    }
                    else{
                        //基本マップを見せる

                        a[k] = this.map[pos];

                        //エージェントがいる場合上書き
                        foreach (var agent in Agents)
                            if (agent != me)
                        {
                            if (agentsPosition[agent] !=null && agentsPosition[agent].Equals(pos))//エージェントがいたら
                            {
                                a[k] = 4;
                            }
                        }
                    }
                    k++;
                }
			}

            return new LocalViewState(a);
        }


        private PositionState startState = null;

        public LocalViewState GetStartState(A me)
        {
            return this.translateState(this.StartPosition,me);
        }

        public PositionState StartPosition
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
                { return startState; }
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
    //*/
}
