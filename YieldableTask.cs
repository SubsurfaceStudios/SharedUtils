using System.Threading.Tasks;
using UnityEngine;

namespace SubsurfaceStudios.Utilities.Async {

    public class YieldableTask<T> : CustomYieldInstruction {
        private readonly Task<T> await;
        public YieldableTask(Task<T> await) => this.await = await;

        public T result => await.Result;

        public override bool keepWaiting => !await.IsCompleted;
    }
    public class YieldableTask : CustomYieldInstruction {
        private readonly Task await;
        public YieldableTask(Task await) => this.await = await;
        public override bool keepWaiting => !await.IsCompleted;
    }
}
