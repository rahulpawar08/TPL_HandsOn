using System.Collections.Generic;

namespace TPL.DataProviders
{
    public class TPLModel
    {
        public TPLModel()
        {
            BlockStatus = new Dictionary<string, BlockStatus>();
        }
        public int BatchNumber { get; set; }

        public Status Status { get; set; }

        public Dictionary<string, BlockStatus> BlockStatus { get; set; }
    }

    public enum Status
    {
        InQueue,
        Processing,
        ProcessingComplete,
        ProcessingFailed
    }

    public enum BlockStatus
    {
        InQueue,
        Skipped,
        Invalid_ReceivedWithFailureStatus,
        Processing,
        ProcessingComplete,
        ProcessingFailed
    }

    public static class TPLBlocks
    {
        public const string Fetcher = "FetcherBlock";
        public const string Delta = "DeltaCalculatorBlock";
        public const string Store = "StorageBlock";
        public const string Notifier = "NotifierBlock";
        public const string StatsPublisher = "StatsPublisherBlock";
    }
}