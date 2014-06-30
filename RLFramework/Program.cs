using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using RLFramework;
using RLFramework.RLAgents;
using RLFramework.RLEnvirontments;
using RLFramework.RLEnvirontments.Actions;
using RLFramework.RLEnvirontments.ExampleProblem;

using RLFramework.RLAgents.QLearningAgents;
using RLFramework.RLAgents.ActorCritics;

namespace RLFramework
{

    class Program
    {
        static void AbsoluteMazeProblem()
        {
            //Declare random
            MyRandom.RandomPool.Declare("action",(int)DateTime.Now.Ticks);

            //環境
            MazeEnvironment mazeEnv = new MazeEnvironment();
            mazeEnv.Map = new MazeMap(new int[,]{
                {  1,  0,  0,  0,  0 },
                {  0,  0,  0,  0,  0 },
                {  0,  0,  0,  0,  0 },
                {  0,  0,  0,  0,  3 },
                {  0,  0,  3,  0,  2 },
            }) ;

            //エージェントを宣言
            var mazeAgent 
                = new QLearningAgent<MazeEnvironment,PositionState,MoveAction>();
                //= new ActorCritic<MazeEnvironment,PositionState,MoveAction>();

                //エージェントに環境をセット
                mazeAgent.Environment = mazeEnv;

                //環境にエージェントをセット
                mazeEnv.AddAgent(mazeAgent);

                mazeAgent.Id = 1;
            
            //エージェントを宣言
            var mazeAgent2
                //= new QLearningAgent<MazeEnvironment,PositionState,FourDirectionAction>();
                = new ActorCritic<MazeEnvironment,PositionState,MoveAction>();

                //エージェントに環境をセット
                mazeAgent2.Environment = mazeEnv;

                //環境にエージェントをセット
                mazeEnv.AddAgent(mazeAgent2);

                mazeAgent2.Id = 2;

            //初期状態をセット
            mazeAgent2.CurrentState = mazeEnv.StartState;

            //初期状態をセット
            mazeAgent.CurrentState = mazeEnv.StartState;

            //View のセット
            MazeView mazeView = new MazeView();
            mazeView.Maze = mazeEnv;



            while(true)
            {

                mazeAgent.Act();
                mazeAgent2.Act();

                mazeView.show();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                mazeAgent.Show();
                Console.WriteLine();
                mazeAgent2.Show();

                string command = Console.ReadLine();  //を、キーをおすたびにやる。

                if (command.Equals("init"))
                {
                    mazeAgent.Init();
                    mazeAgent2.Init();
                }

                else if (command.Equals("q"))
                {
                    mazeAgent.Show ();
                    continue;
                }

                try //数字としてパースしてみて
                {   //パースできたらその回数ステップ進める
                    int num = int.Parse(command);

                    foreach (int j in Enumerable.Range(0, num))
                    {
                        mazeAgent.Act();
                        mazeAgent2.Act();
                    }
                }
                catch (Exception)
                {
                }
            }

        }

        static void LocalMazeProblem(){
            //Declare random
            MyRandom.RandomPool.Declare("action",(int)DateTime.Now.Ticks);

            //環境
            LocalViewMazeEnvironment mazeEnv = new LocalViewMazeEnvironment();
            mazeEnv.Map = new MazeMap(new int[,]{
                {  1,  0,  0,  0,  0 },
                {  0,  0,  0,  0,  0 },
                {  0,  0,  0,  0,  0 },
                {  0,  0,  0,  0,  3 },
                {  0,  0,  3,  0,  2 },
            }) ;

            //エージェントを宣言
            var mazeAgent1 
                //= new QLearningAgent<MazeEnvironment,PositionState,FourDirectionAction>();
                = new ActorCritic<LocalViewMazeEnvironment,LocalViewState,MoveAction>();

                //エージェントに環境をセット
                mazeAgent1.Environment = mazeEnv;

                //環境にエージェントをセット
                mazeEnv.AddAgent(mazeAgent1);

                mazeAgent1.Id = 1;

            
            //エージェントを宣言
            var mazeAgent2
                //= new QLearningAgent<MazeEnvironment,PositionState,FourDirectionAction>();
                = new QLearningAgent<LocalViewMazeEnvironment,LocalViewState,MoveAction>();

                //エージェントに環境をセット
                mazeAgent2.Environment = mazeEnv;

                //環境にエージェントをセット
                mazeEnv.AddAgent(mazeAgent2);

                mazeAgent2.Id = 2;

            //初期状態をセット
            mazeAgent2.CurrentState = mazeEnv.GetStartState(mazeAgent2);

            //初期状態をセット
            mazeAgent1.CurrentState = mazeEnv.GetStartState(mazeAgent1);

            //View のセット
            LocalViewMazeView mazeView = new LocalViewMazeView();
            mazeView.Maze = mazeEnv;



            while(true)
            {
                mazeAgent1.Act();
                mazeAgent2.Act();

                mazeView.show();

                Console.WriteLine();
                mazeAgent1.Show();
                Console.WriteLine();
                mazeAgent2.Show();
                Console.WriteLine();

                string command = Console.ReadLine();  //を、キーをおすたびにやる。

                if (command.Equals("init"))
                {
                    mazeAgent1.Init();
                    mazeAgent2.Init();
                }

                else if (command.Equals("q"))
                {
                    mazeAgent1.Show ();
                    continue;
                }

                try //数字としてパースしてみて
                {   //パースできたらその回数ステップ進める
                    int num = int.Parse(command);

                    foreach (int j in Enumerable.Range(0, num))
                    {
                        mazeAgent1.Act();
                        mazeAgent2.Act();
                    }
                }
                catch (Exception)
                {
                }
            }

        }

        static void Main(string[] args)
        {
            AbsoluteMazeProblem();
            
            //LocalMazeProblem();

        }
    }
}
