using System;
using System.Collections.Generic;
using System.Text;

namespace TPL.DataFlow.Implementation
{
    public interface IDownloaderMonitoringService
    {
        //Need to collect the data from blocks:
        // a. Block Level Update  --> Milestone data
        //  b. Hotel Level Update  --> Transactional Data
        //   c. Overall Summary     --> Execution Summary
        void UpdateMilestoneProgress(string milestoneName, List<string> data);

        void UpdateTransactionalProgress(List<string> data);

        void UpdateSummary(List<string> data);

        //It will also publish the updates
        // a. Periodic Updates     --> Transactional data will be periodically updated
        //  b. Milestone Updates    --> Started, Completed, Block Complete, immediate updates
        //   c. Overall Summary      --> Once the downloader is complete
        //    d. Failure Properties   --> Once the downloader is complete && Periodic



    }
}
