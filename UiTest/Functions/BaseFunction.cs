
using System.Threading;
using UiTest.Mapper;
using System;
using UiTest.Service.Factory;
using System.Threading.Tasks;

namespace UiTest.Functions
{
    public abstract class BaseFunction<T>
    {
        private readonly T _config;
        protected BaseFunction(T config)
        {
            _config = BaseFactory<T>.CreateInstance(typeof(T));
            if (config != null && !UpdateConfig(config))
            {
                throw new Exception($"{nameof(this.GetType)}: Update function Config failed. [{typeof(T)}]");
            }
            Cts = new CancellationTokenSource();
        }

        public CancellationTokenSource Cts {  get; set; }
        public bool IsCanceled => Cts?.IsCancellationRequested == true;
        public T Config => _config;
        public virtual void Cancel()
        {
            if (Cts?.IsCancellationRequested == false)
            {
                Cts?.Cancel();
            }
        }
        public void Resume()
        {
            if (Cts?.IsCancellationRequested == true)
            {
                Cts = new CancellationTokenSource();
            }
        }
        public abstract void Run();
        public virtual void Stop()
        {
            if (Cts?.IsCancellationRequested == false)
            {
                Cts?.Cancel();
            }
        }
        public bool UpdateConfig(T config)
        {
            if (_config == null) return false;
            if (MyObjectMapper.Copy(config, _config))
            {
                return true;
            }
            return false;
        }
    }
}
