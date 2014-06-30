using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RLFramework.RLEnvirontments.ExampleProblem
{
    using RLFramework.RLAgents;
    using RLFramework.RLAgents.QLearningAgents;

    public class LocalViewMazeView
    {
        public LocalViewMazeEnvironment Maze{get; set;}

        public void show()
        {
            Console.Clear();

            //上の辺を描画
            for (int x = -1; x <= this.Maze.Map.Width; x++)
            {
                Console.Write("壁 ");
            }
            Console.WriteLine();

            //中身を描画
            for (int y = 0; y < this.Maze.Map.Height; y++)
            {
                Console.Write("壁 ");
                for (int x = 0; x < this.Maze.Map.Width; x++)
                {
                    {//エージェントがたら描画

                        IRLAgent<LocalViewMazeEnvironment, LocalViewState, MoveAction> agent = null;
                        foreach (var a in Maze.Agents)
                        {
                            if (Maze.AgentsPosition[a].Equals(new PositionState(x, y)))
                            {
                                agent = a;
                            }
                        }

                        if (agent != null)
                        {

                            if (agent.RecentReward == 0)
                            {
                                Console.Write("人" + agent.Id);
                            }
                            else if (agent.RecentReward > 0)
                            {
                                Console.Write("金" + agent.Id);
                            }
                            else // if (agent.RecentReward < 0)
                            {
                                Console.Write("痛" + agent.Id);
                            }


                            continue;//次のマスを描画に行こう
                        }
                    }

                    switch (this.Maze.Map[x, y])
                    {
                        case 1:
                            Console.Write("[S]");
                            break;
                        case 2:
                            Console.Write("[G]");
                            break;
                        case 3:
                            Console.Write("崖 ");
                            break;
                        case -1:
                            Console.Write("壁 ");
                            break;
                        case 0:
                            Console.Write
                                //(""+ x + y+ " ");
                                ("   ");
                            break;
                        default:
                            Console.Write("   ");
                            break;
                    }
                }
                Console.Write("壁 ");
                Console.WriteLine();
            }

            //下の辺を描画
            for (int x = -1; x <= this.Maze.Map.Width; x++)
            {
                Console.Write("壁 ");
            }
            Console.WriteLine();
        }
    }
}
