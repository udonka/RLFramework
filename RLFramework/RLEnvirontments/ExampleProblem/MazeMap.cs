using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFramework.RLEnvirontments.ExampleProblem
{

    public class MazeMap
    {
        private int[,] map = new int[,]{
            /*シンプル */

            {  1,  0,  0,  0,  0,  0,  0 },
            {  0,  0,  0,  0,  0,  0,  0 },
            {  0,  0,  0,  0,  0,  0,  0 },
            {  0,  0,  0,  0,  0,  0,  0 },
            {  0,  0,  0,  0,  0,  0,  0 },
            {  0,  0,  0,  0,  0,  0,  0 },
            {  0,  0,  0,  0,  0,  3,  3 },
            {  0,  0,  0,  0,  0,  0,  2 },


            // */


        };



        public MazeMap()
        {
        }
        public MazeMap(int[, ]a)
        {

            map = new int[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    map[i, j] = a[i, j];
                }
            }

        }

        public int this[PositionState pos]    //x,y :: i,jの変換をわかりやすくしてくれるだけ。
        {
            get{
                return this.map[pos.Y, pos.X];
            }
        }

        public int this[int x, int y]    //x,y :: i,jの変換をわかりやすくしてくれるだけ。
        {
            get{
                return this.map[y, x];
            }
        }

        public bool CanGo(PositionState pos)
        {
            if ( !(
                (pos.X >= 0 && pos.X < Width) 
                &&(pos.Y >= 0 && pos.Y < Height)))
            {
                return false;
            }
            else
            {
                return !(this[pos.X, pos.Y] == -1);
            }
        }

        public int Width
        {
            get
            {
                return map.GetLength(1);
            }
        }

        public int Height
        {
            get
            {
                return map.GetLength(0);
            }
        }

        internal bool isStart(PositionState state)
        {
            return this[state] == 1;
        }
        
        internal bool isGoal(PositionState state)
        {
            return this[state] == 2;
        }
    }
}
