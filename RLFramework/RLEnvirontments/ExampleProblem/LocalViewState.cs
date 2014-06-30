using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLFramework.RLEnvirontments.States;

namespace RLFramework.RLEnvirontments.ExampleProblem
{
    /**/
    public class LocalViewState :IRLState
    {
        int[] localView = new int[9];

        public LocalViewState(int[] a)
        {
            if (a.Length == 9)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    this.localView[i] = a[i];
                    
                }
            }
            else
            {
                throw new Exception("視野の大きさが違います");
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as LocalViewState);
        }

        public bool Equals(IRLState obj)
        {
            return this.Equals(obj as LocalViewState);
        }


        // override object.Equals
        public bool Equals(LocalViewState obj)
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

            //長さが違ったら等しくない
            if (this.Length != obj.Length)
            {
                return false;
            }

            //ひとつでも違ったら等しくない
            for (int i = 0; i < this.Length; i++)
            {
                if (localView[i] != obj.localView[i])
                {
                    return false;
                }
            }

            //最後までパスしたら正しい
            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return localView.Sum();
        }

        public int Length
        {
            get
            {
                return localView.Length;
            }
        }

        public override string ToString()
        {
            string str = "< ";

            for (int i = 0; i < this.Length-1; i++)
            {
                str += localView[i] + " | ";
            }
            str += localView[this.Length - 1];

            return str + " >";
        }

    }
    //*/
}
