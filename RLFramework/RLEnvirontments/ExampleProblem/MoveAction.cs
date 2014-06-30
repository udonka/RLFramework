using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFramework.RLEnvirontments.ExampleProblem
{
    using RLFramework.RLEnvirontments.Actions;

    public class MoveAction :IRLAction
    {
        int dx; //1,0,-1
        int dy; //1,0,-1

        public int Dx
        {
            get
            {
                return dx;
            }

            set
            {
                dx = value;
            }

        }

        public int Dy
        {
            get
            {
                return dy;
            }

            set
            {
                dy = value;
            }
        }

        //interface に static を含めない制約によりあるインスタンス化用コンストラクタ
        public MoveAction()
        {
        }

        public MoveAction(int dx,int dy){
            this.dx = dx;
            this.dy = dy;
        }


        public virtual ISet<IRLAction> CandidateActions()//自分と同族の行動のリストを返す
        {
            return new HashSet<IRLAction>{
                new MoveAction(1,0),new FourDirectionAction(0,1),
                new MoveAction(-1,0),new FourDirectionAction(0,-1)};
        }


        // override object.Equals
        public bool Equals(IRLAction obj)
        {
            return this.Equals(obj as MoveAction);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MoveAction);
        }

        public bool Equals(MoveAction obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Dx == obj.Dx && this.Dy == obj.Dy;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Dx + Dy;
        }

        public override string ToString()
        {
            if (Dx == 1 && Dy == 0)
            {
                return "→";
            }
            else if(Dx == -1 && Dy == 0){
                return "←";
            }
            else if(Dx == 0 && Dy == 1){
                return "↓";
            }
            else if(Dx == 0 && Dy == -1){
                return "↑";
            }
            else if (Dx == 0 && Dy == 0)
            {
                return "x";
            }
            else
            {
                return "??";
            }

        }

    }
}
