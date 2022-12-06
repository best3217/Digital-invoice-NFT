namespace Obius.Service.Dtos
{
    public class ContractResponse
    {
        public string blockHash { get; set; }
        public int blockNumber { get; set; }
        public object contractAddress { get; set; }
        public int cumulativeGasUsed { get; set; }
        public long effectiveGasPrice { get; set; }
        public string from { get; set; }
        public int gasUsed { get; set; }
        public string logsBloom { get; set; }
        public bool status { get; set; }
        public string to { get; set; }
        public string transactionHash { get; set; }
        public int transactionIndex { get; set; }
        public string type { get; set; }
        public Events events { get; set; }
    }
}




