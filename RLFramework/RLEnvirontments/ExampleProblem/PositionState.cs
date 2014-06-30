using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFramework.RLEnvirontments.ExampleProblem
{
    using States;

    public class PositionState :IRLState
    {
        int x, y;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y 
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public PositionState(PositionState original) 
        {
            this.x = original.x;
            this.y = original.y;
        }

        public PositionState(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj is PositionState)
            {
                return Equals(obj as PositionState);
            }
            else
            {
                throw new Exception("PositionStateじゃないと受け付けないよ");
            }
        }

        public bool Equals(IRLState obj)
        {
            if (obj is PositionState)
            {
                return Equals(obj as PositionState);
            }
            else
            {
                throw new Exception("PositionStateじゃないと受け付けないよ");
            }
        }

        public bool Equals(PositionState obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }


            return this.x == obj.x && this.y == obj.y;
            
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return x + y;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.X, this.Y);
        }

    }
}
