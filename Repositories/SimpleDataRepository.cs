using System.Collections.Generic;
using demo_aspnet_for_web_apps.Models;

namespace demo_aspnet_for_web_apps.Repositories
{
    public class SimpleDataRepository
    {
        private List<SampleData> _data;
        private bool _cpuStop = false;

        public SimpleDataRepository()
        {
            _data = new List<SampleData>();
        }

        public void AddData(SampleData item)
        {
            _data.Add(item);
        }

        public int Count()
        {
            return _data.Count;
        }

        public bool GetCpuStop()
        {
            return _cpuStop;
        }

        public void SetCpuStop(bool stop)
        {
            _cpuStop = stop;
        }
    }
}