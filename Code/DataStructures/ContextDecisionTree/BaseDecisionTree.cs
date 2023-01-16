using System;

namespace UnityFoundation.Code
{
    // TODO: separar o contexto do output. Durante as decisões existem duas entidades que atualmente estão apenas no contexto. O contexto para cada decisão e se a decisão for final ela deveria voltar um output
    public abstract class BaseDecisionTree<TContext>
    {
        public IDecisionHandler<TContext> Root { get; set; }
        protected TContext Context { get; private set; }

        public void Evaluate()
        {
            Context = InitilizeContext();

            if(Root == null)
                throw new ArgumentNullException("Root decision was not set");

            if(Context == null)
                throw new ArgumentNullException("Context was not set");

            Root.Decide(Context);
        }

        public abstract TContext InitilizeContext();
    }
}