using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation
{
    class DownloaderMonitoringService : IDownloaderMonitoringService
    {
        const int batchSize = 10;
        BatchBlock<string> _transactionalDataBlock;
        ActionBlock<string[]> _publishStats;
        ILoggingService _fileLogger;

        public DownloaderMonitoringService()
        {
            _transactionalDataBlock = new BatchBlock<string>(batchSize);
            _publishStats = new ActionBlock<string[]>(data => Publish(data));
            _transactionalDataBlock.LinkTo(_publishStats);
            _fileLogger = new FileLoggingService();
        }

        private void Publish(string[] data)
        {
            List<string> logData = new List<string>(data);
            _fileLogger.LogData(logData);
        }

        public void UpdateMilestoneProgress(string milestoneName, List<string> data)
        {
            _fileLogger.LogData(milestoneName, data);
        }

        public void UpdateSummary(List<string> data)
        {
            _fileLogger.LogData(data);
        }

        public void UpdateTransactionalProgress(List<string> data)
        {
            foreach (var item in data)
                _transactionalDataBlock.Post(item);
        }
    }
}
