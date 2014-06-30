using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RLFramework.RLAgents;
using RLFramework.RLEnvirontments.ExampleProblem;

namespace AgentTest
{
    [TestClass]
    public class IRLAgentTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            IRLAgent<MazeEnvironment,PositionState,FourDirectionAction,double> mazeAgent 
                = new RandomActionAgent<MazeEnvironment,PositionState,FourDirectionAction,double>();
            mazeAgent.Environment = new MazeEnvironment();

            mazeAgent.CurrentState = new PositionState(1,0);

            mazeAgent.Act();
            mazeAgent.Act();


            

        }
    }
}
