using System;

namespace Regulus.Samples.Chat1.Stride
{
    class ReleaseHelper
    {
        public readonly System.Collections.Generic.List<System.Action> Actions;
        public ReleaseHelper() : this(new System.Collections.Generic.List<Action>())
        {
            
        }
        public ReleaseHelper(System.Collections.Generic.IEnumerable<System.Action> actions)
        {
            Actions = new System.Collections.Generic.List<Action>(actions);
        }

        
        public void Release()
        {
            foreach (var action in Actions)
            {
                action();
            }
        }
    }
}
